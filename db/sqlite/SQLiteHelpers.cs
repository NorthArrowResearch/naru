using System;
using System.Data.SQLite;

namespace naru.db.sqlite
{
    class SQLiteHelpers
    {
        public static long GetScalarID(ref SQLiteCommand dbCom)
        {
            long nID = 0;
            object objID = dbCom.ExecuteScalar();
            if (objID != null && objID != DBNull.Value)
            {
                nID = (long)objID;
            }
            return nID;
        }

        public static double GetSafeValueDbl(ref SQLiteDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return 0;
            else
                return dbRead.GetDouble(dbRead.GetOrdinal(sColumnName));
        }

        public static Nullable<double> GetSafeValueNDbl(ref SQLiteDataReader dbRead, string sColumnName)
        {
            Nullable<double> fResult = new Nullable<double>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                fResult = dbRead.GetDouble(dbRead.GetOrdinal(sColumnName));
            return fResult;
        }

        public static long GetSafeValueInt(ref SQLiteDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return 0;
            else
                return dbRead.GetInt64(dbRead.GetOrdinal(sColumnName));
        }

        public static Nullable<long> GetSafeValueNInt(ref SQLiteDataReader dbRead, string sColumnName)
        {
            Nullable<long> nResult = new Nullable<long>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                nResult = dbRead.GetInt64(dbRead.GetOrdinal(sColumnName));
            return nResult;
        }

        public static bool GetSafeValueBool(ref SQLiteDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return false;
            else
                return dbRead.GetBoolean(dbRead.GetOrdinal(sColumnName));
        }

        public static DateTime GetSafeValueDT(ref SQLiteDataReader dbRead, string sColumnName)
        {
            if (dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                return DateTime.Now;
            else
                return (DateTime)dbRead[sColumnName];
        }

        public static Nullable<DateTime> GetSafeValueNDT(ref SQLiteDataReader dbRead, string sColumnName)
        {
            Nullable<DateTime> dtValue = new Nullable<DateTime>();
            if (!dbRead.IsDBNull(dbRead.GetOrdinal(sColumnName)))
                dtValue = dbRead.GetDateTime(dbRead.GetOrdinal(sColumnName));
            return dtValue;
        }

        public static string GetSafeValueStr(ref SQLiteDataReader dbRead, string sColumnName)
        {
            return GetSafeValueStr(ref dbRead, dbRead.GetOrdinal(sColumnName));
        }

        public static string GetSafeValueStr(ref SQLiteDataReader dbRead, int ColIndex)
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

        /// <summary>
        /// Safely add a string parameter to a SQLite command. Adds empty strings as NULL
        /// </summary>
        /// <param name="dbCom">Insert or update SQL command</param>
        /// <param name="txt">textbox containing string to be inserted</param>
        /// <param name="sParameterName">Name of parameter to create</param>
        /// <returns></returns>
        public static SQLiteParameter AddStringParameterN(ref SQLiteCommand dbCom, ref System.Windows.Forms.TextBox txt, string sParameterName)
        {
            return AddStringParameterN(ref dbCom, txt.Text, sParameterName);
        }

        public static SQLiteParameter AddStringParameterN(ref SQLiteCommand dbCom, string sStringValue, string sParameterName)
        {
            System.Diagnostics.Debug.Assert(dbCom.CommandText.ToLower().Contains("insert") || dbCom.CommandText.ToLower().Contains("update"), "SQL command must be an INSERT or UPDATE command");
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sParameterName), "The parameter name cannot be empty.");
            System.Diagnostics.Debug.Assert(!dbCom.Parameters.Contains(sParameterName), "The SQL command already contains a parameter with this name.");

            string sValue = sStringValue.Trim();
            SQLiteParameter p = dbCom.Parameters.Add(sParameterName, System.Data.DbType.String);
            if (string.IsNullOrEmpty(sValue))
                p.Value = DBNull.Value;
            else
            {
                p.Value = sValue;
                p.Size = sValue.Length;
            }

            return p;
        }

        public static SQLiteParameter AddDoubleParameterN(ref SQLiteCommand dbCom, double? fValue, string sParameterName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sParameterName), "The parameter name cannot be empty.");
            System.Diagnostics.Debug.Assert(!dbCom.Parameters.Contains(sParameterName), "The SQL command already contains a parameter with this name.");

            SQLiteParameter p = dbCom.Parameters.Add(sParameterName, System.Data.DbType.Double);
            if (fValue.HasValue)
                p.Value = fValue.Value;
            else
            {
                p.Value = DBNull.Value;
            }

            return p;
        }

        public static SQLiteParameter AddLongParameterN(ref SQLiteCommand dbCom, long? nValue, string sParameterName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sParameterName), "The parameter name cannot be empty.");
            System.Diagnostics.Debug.Assert(!dbCom.Parameters.Contains(sParameterName), "The SQL command already contains a parameter with this name.");

            SQLiteParameter p = dbCom.Parameters.Add(sParameterName, System.Data.DbType.Int64);
            if (nValue.HasValue)
                p.Value = nValue.Value;
            else
            {
                p.Value = DBNull.Value;
            }

            return p;
        }

    }

}
