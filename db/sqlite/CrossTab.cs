using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace naru.db.sqlite
{
    public class CrossTab
    {
        /// <summary>
        /// Build a cross tab table based on three SQL queries
        /// </summary>
        /// <param name="sDBCon">SQLite databaes connection string</param>
        /// <param name="sFirstColumnHeader">Column header text label for the first column of the cross tab</param>
        /// <param name="sqlCols">SQL query defining columns. First field must be the IDs of the items and the second field the names.</param>
        /// <param name="sqlRows">SQL query defining rows. First fields must be the IDs of the items and the second field the names.</param>
        /// <param name="sqlContents">SQL query defining the contents of the cross tab. First field must be the rows ID field. Second the columns field. Third the values of the cross tab as Doubles.</param>
        /// <returns></returns>s
        public static DataTable CreateCrossTab(string sDBCon, string sFirstColumnHeader, string sqlCols, string sqlRows, string sqlContents)
        {
            DataTable dt = new DataTable();

            // Add the first column of labels
            dt.Columns.Add(sFirstColumnHeader, Type.GetType("System.String"));

            // Add the remaining columns and build a dictionary of the IDs to the column index
            Dictionary<long, int> dColLookup = null;
            dt.Columns.AddRange(AppendDataColumns(sDBCon, sqlCols, out dColLookup));

            // Add all the rows to the DataTable. Initially set the row headers to the IDs of the items
            Dictionary<long, int> dRowLookup = null;
            AppendRows(sDBCon, sqlRows, ref dt, out dRowLookup);

            // Fill in the content of the cross tab using the two dictionaries to find appropraite rows and columns
            PopulateCrossTab(sDBCon, sqlContents, ref dt, dRowLookup, dColLookup);

            return dt;
        }

        protected static DataColumn[] AppendDataColumns(string sDBCon, string sqlCols, out Dictionary<long, int> dColumns)
        {
            List<DataColumn> cols = new List<DataColumn>();
            dColumns = new Dictionary<long, int>();

            using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand(sqlCols, dbCon);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    // Create the column using the name specified in the second field of the SQL
                    DataColumn newCol = new DataColumn(dbRead.GetString(1), Type.GetType("System.Double"));
                    System.Diagnostics.Debug.Assert(!cols.Contains<DataColumn>(newCol), "The column already exists");
                    cols.Add(newCol);

                    // Create a look up for this column using the ID field that is the first field of the SQL
                    System.Diagnostics.Debug.Assert(!dColumns.ContainsKey(dbRead.GetInt64(0)), "The table definition already contains a column for this metric");
                    dColumns[dbRead.GetInt64(0)] = cols.Count -1;
                }
            }

            return cols.ToArray<DataColumn>();
        }

        private static void AppendRows(string sDBCon, string sqlRows, ref DataTable dt, out Dictionary<long, int> dRows)
        {
            dRows = new Dictionary<long, int>();
            using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand(sqlRows, dbCon);
                System.Diagnostics.Debug.Print("Crosstab row SQL: {0}", sqlRows);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    long nRowID = dbRead.GetInt64(0);
                    DataRow row = dt.NewRow();
                    row.SetField<string>(0, naru.db.sqlite.SQLiteHelpers.GetSafeValueStr(ref dbRead, 1));
                    dRows[nRowID] = dt.Rows.Count;
                    dt.Rows.Add(row);
                }
            }
        }

        private static void PopulateCrossTab(string sDBCon, string sqlContents, ref DataTable dt, Dictionary<long, int> dRows, Dictionary<long, int> dCols)
        {
            using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand(sqlContents, dbCon);
                System.Diagnostics.Debug.Print("Crosstab content SQL: {0}", sqlContents);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    if (!dbRead.IsDBNull(2))
                    {
                        long nRowID = dbRead.GetInt64(0);
                        if (dRows.ContainsKey(nRowID))
                        {
                            int rowIndex = dRows[nRowID];
                            System.Diagnostics.Debug.Assert(rowIndex < dt.Rows.Count);

                            long nColID = dbRead.GetInt64(1);
                            if (dCols.ContainsKey(nColID))
                            { 
                                int colIndex = dCols[nColID] + 1; // Don't forget that there's one fixed column before the data columns
                                System.Diagnostics.Debug.Assert(colIndex < dt.Columns.Count);
                                dt.Rows[rowIndex].SetField<double>(colIndex, dbRead.GetDouble(2));
                            }
                        }
                    }
                }
            }
        }
    }
}
