using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace naru.db.mysql
{
    class MySQLHelpers
    {
        public enum ConnectionTestResults
        {
            OK,
            ConnectionFail,
            MissingServer,
            MissingDatabase,
            MissingUserName,
            MissingPassword
        }

        private const string RootConnectionStringMaster = "server={0};uid={1};pwd={2};database={3};Port={4}";

        public static string BuildConnectionString(string sServer, string sDatabase, string sUserName, string sPassword, ushort nPort)
        {
            return string.Format(RootConnectionStringMaster, sServer, sUserName, sPassword, sDatabase, nPort.ToString());
        }

        public static ConnectionTestResults TestConnection(string sServer, string sDatabase, string sUserName, string sPassword, ushort nPort, string sqlTest)
        {
            ConnectionTestResults eResult = ConnectionTestResults.ConnectionFail;

            if (string.IsNullOrEmpty(sServer))
                return ConnectionTestResults.MissingServer;

            if (string.IsNullOrEmpty(sDatabase))
                return ConnectionTestResults.MissingDatabase;

            if (string.IsNullOrEmpty(sUserName))
                return ConnectionTestResults.MissingUserName;

            if (string.IsNullOrEmpty(sPassword))
                return ConnectionTestResults.MissingPassword;

            try
            {
                string sCon = naru.db.mysql.MySQLHelpers.BuildConnectionString(sServer, sDatabase, sUserName, sPassword, nPort);
                using (MySql.Data.MySqlClient.MySqlConnection dbCon = new MySql.Data.MySqlClient.MySqlConnection(sCon))
                {
                    dbCon.Open();

                    // Query that should be successful but return no rows
                    MySql.Data.MySqlClient.MySqlCommand dbCom = new MySql.Data.MySqlClient.MySqlCommand(sqlTest, dbCon);
                    dbCom.ExecuteReader();
                    eResult = ConnectionTestResults.OK;
                }
            }
            catch (Exception ex)
            {
                eResult = ConnectionTestResults.ConnectionFail;
                ex.Data["MySQL Server"] = sServer;
                ex.Data["MySQL Port"] = nPort.ToString();
                ex.Data["MySQL Database"] = sDatabase;
                ex.Data["MySQL UserName"] = sUserName;
                ex.Data["MySQL Password"] = sPassword;
                ex.Data["Test SQL"] = sqlTest;
                throw;
            }

            return eResult;
        }

        public static void AddParameter(ref MySqlCommand dbCom, ref TextBox ctrl, string sParameterName)
        {
            if (string.IsNullOrEmpty(ctrl.Text))
            {
                MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.String);
                p.Value = DBNull.Value;
            }
            else
            {
                dbCom.Parameters.AddWithValue(sParameterName, ctrl.Text);
            }
        }

        public static void AddParameter(ref MySqlCommand dbCom, ref NumericUpDown ctrl, string sParameterName, int nExponent = 0)
        {
            if (nExponent == 0)
                dbCom.Parameters.AddWithValue(sParameterName, ctrl.Value);
            else
                dbCom.Parameters.AddWithValue(sParameterName, ctrl.Value * (decimal)Math.Pow(10, nExponent));
        }

        public static void AddParameter(ref MySqlCommand dbCom, ref ComboBox ctrl, string sParameterName)
        {
            if (ctrl.SelectedIndex < 0)
            {
                MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.Int64);
                p.Value = DBNull.Value;
            }
            else
            {
                dbCom.Parameters.AddWithValue(sParameterName, ((naru.db.NamedObject)ctrl.SelectedItem).ID);
            }
        }

        public static void AddParameter(ref MySqlCommand dbCom, ref CheckBox ctrl, string sParameterName)
        {
            MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.Bit);
            p.Value = ctrl.Checked;
        }

        public static void AddNParameter(ref MySqlCommand dbCom, ref CheckBox ctrl, ref DateTimePicker dt, string sParameterName)
        {
            MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.DateTime);
            if (ctrl.Checked)
                p.Value = dt.Value;
            else
                p.Value = DBNull.Value;
        }

        public static void AddNParameter(ref MySqlCommand dbCom, ref CheckBox ctrl, ref NumericUpDown val, string sParameterName)
        {
            MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.Double);
            if (ctrl.Checked)
                p.Value = val.Value;
            else
                p.Value = DBNull.Value;
        }

        /// <summary>
        /// Safely add a string parameter to a SQLite command. Adds empty strings as NULL
        /// </summary>
        /// <param name="dbCom">Insert or update SQL command</param>
        /// <param name="txt">textbox containing string to be inserted</param>
        /// <param name="sParameterName">Name of parameter to create</param>
        /// <returns></returns>
        public static MySqlParameter AddStringParameterN(ref MySqlCommand dbCom, ref System.Windows.Forms.TextBox txt, string sParameterName)
        {
            return AddStringParameterN(ref dbCom, txt.Text, sParameterName);
        }

        public static MySqlParameter AddStringParameterN(ref MySqlCommand dbCom, string sStringValue, string sParameterName)
        {
            System.Diagnostics.Debug.Assert(dbCom.CommandText.ToLower().Contains("insert") || dbCom.CommandText.ToLower().Contains("update"), "SQL command must be an INSERT or UPDATE command");
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sParameterName), "The parameter name cannot be empty.");
            System.Diagnostics.Debug.Assert(!dbCom.Parameters.Contains(sParameterName), "The SQL command already contains a parameter with this name.");

            string sValue = sStringValue.Trim();
            MySqlParameter p = dbCom.Parameters.Add(sParameterName, MySqlDbType.String);
            if (string.IsNullOrEmpty(sValue))
                p.Value = DBNull.Value;
            else
            {
                p.Value = sValue;
                p.Size = sValue.Length;
            }

            return p;
        }

        public static long GetScalarID(ref MySqlCommand dbCom)
        {
            long nID = 0;
            object objID = dbCom.ExecuteScalar();
            if (objID != null && objID != DBNull.Value)
            {
                nID = (long)objID;
            }
            return nID;
        }

        public static long GetScalarID(MySqlConnection dbCon, string sSQL)
        {
            MySqlCommand dbCom = new MySqlCommand(sSQL, dbCon);
            return GetScalarID(ref dbCom);
        }

        public static long GetScalarID(string sDBCon, string sSQL)
        {
            long nResult = 0;
            using (MySqlConnection dbCon = new MySqlConnection(sDBCon))
            {
                dbCon.Open();
                nResult = GetScalarID(dbCon, sSQL);
            }
            return nResult;
        }

        public static double GetSafeValueDbl(ref MySqlDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return 0;
            else
                return dbRead.GetDouble(dbRead.GetOrdinal(sColumnName));
        }

        public static Nullable<double> GetSafeValueNDbl(ref MySqlDataReader dbRead, string sColumnName)
        {
            Nullable<double> fResult = new Nullable<double>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                fResult = dbRead.GetDouble(dbRead.GetOrdinal(sColumnName));
            return fResult;
        }

        public static long GetSafeValueInt(ref MySqlDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return 0;
            else
                return dbRead.GetInt64(dbRead.GetOrdinal(sColumnName));
        }

        public static Nullable<long> GetSafeValueNInt(ref MySqlDataReader dbRead, string sColumnName)
        {
            Nullable<long> nResult = new Nullable<long>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                nResult = dbRead.GetInt64(dbRead.GetOrdinal(sColumnName));
            return nResult;
        }

        public static bool GetSafeValueBool(ref MySqlDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return false;
            else
                return dbRead.GetBoolean(dbRead.GetOrdinal(sColumnName));
        }

        public static DateTime GetSafeValueDT(ref MySqlDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return DateTime.Now;
            else
                return (DateTime)dbRead[sColumnName];
        }

        public static Nullable<DateTime> GetSafeValueNDT(ref MySqlDataReader dbRead, string sColumnName)
        {
            Nullable<DateTime> dtValue = new Nullable<DateTime>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                dtValue = dbRead.GetDateTime(dbRead.GetOrdinal(sColumnName));
            return dtValue;
        }

        public static string GetSafeValueStr(ref MySqlDataReader dbRead, string sColumnName)
        {
            return GetSafeValueStr(ref dbRead, dbRead.GetOrdinal(sColumnName));
        }

        public static string GetSafeValueStr(ref MySqlDataReader dbRead, int ColIndex)
        {
            string sValue = string.Empty;
            if (!dbRead.IsDBNull(ColIndex))
            {
                switch (dbRead[ColIndex].GetType().Name.ToLower())
                {
                    case "string":
                        sValue = dbRead.GetString(ColIndex);
                        break;

                    case "int64":
                        sValue = dbRead.GetInt64(ColIndex).ToString();
                        break;

                    default:
                        throw new Exception("Unhandled data type.");
                }
            }
            return sValue;
        }

        public static void FillTextBox(ref MySqlDataReader dbRead, string sColumnName, ref TextBox txt)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                txt.Text = string.Empty;
            else
                txt.Text = dbRead.GetString(sColumnName);
        }

        public static void FillNumericUpDown(ref MySqlDataReader dbRead, string sColumnName, ref NumericUpDown val, int nExponent = 0)
        {
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
            {
                switch (dbRead.GetDataTypeName(dbRead.GetOrdinal(sColumnName)).ToLower())
                {
                    case "Int64":
                    case "int":
                        decimal fRawIntValue = (decimal)dbRead.GetInt64(sColumnName);
                        if (nExponent != 0)
                            val.Value = fRawIntValue * (decimal)Math.Pow(10, nExponent);
                        else
                            val.Value = fRawIntValue;
                        break;

                    case "double":
                        decimal fRawValue = (decimal)dbRead.GetDouble(sColumnName);
                        if (nExponent != 0)
                            val.Value = fRawValue * (decimal)Math.Pow(10, nExponent);
                        else
                            val.Value = fRawValue;
                        break;

                    default:
                        throw new Exception(string.Format("Unhandled data type filling numeric up down for type {0}", dbRead.GetDataTypeName(dbRead.GetOrdinal(sColumnName)).ToLower()));
                }
            }
        }
    }
}
