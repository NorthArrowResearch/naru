using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace naru.db.sqlite
{
    public class CrossTabMultiColumn : CrossTab
    {
        public static DataTable CreateCrossTab(string sDBCon, List<Tuple<string, string>> KeyColumns, string sqlCols, string sqlRows, string sqlContents)
        {
            DataTable dt = new DataTable();

            //Dictionary<string, int> keyColumnIndices = new Dictionary<string, int>();

            foreach (Tuple<string, string> aCol in KeyColumns)
            {
                // Add the key column of labels
                dt.Columns.Add(aCol.Item1, Type.GetType("System.String"));
                dt.Columns[dt.Columns.Count - 1].Caption = aCol.Item2;
                //keyColumnIndices[aCol.Item1] = dt.Columns.Count - 1;
            }

            // Add the remaining columns and build a dictionary of the IDs to the column index
            Dictionary<long, int> dColLookup = null;
            dt.Columns.AddRange(AppendDataColumns(sDBCon, sqlCols, out dColLookup));

            // Fill in the content of the cross tab using the two dictionaries to find appropraite rows and columns
            PopulateCrossTab(sDBCon, sqlContents, ref dt, KeyColumns, dColLookup);

            return dt;

        }

        //private static void AppendRows(string sDBCon, string sqlRows, ref DataTable dt, List<Tuple<string, string>> Keycolumns, out Dictionary<long, object> dRows)
        //{
        //    dRows = new Dictionary<long, object>();
        //    using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
        //    {
        //        dbCon.Open();
        //        SQLiteCommand dbCom = new SQLiteCommand(sqlRows, dbCon);
        //        System.Diagnostics.Debug.Print("Crosstab row SQL: {0}", sqlRows);
        //        SQLiteDataReader dbRead = dbCom.ExecuteReader();
        //        while (dbRead.Read())
        //        {
        //            for (int i = 0; i < Keycolumns.Count; i++)
        //            {
        //                long nIDValue = dbRead.GetInt64(dbRead.GetOrdinal(Keycolumns[i].Item1));
        //                if (i < Keycolumns.Count - 1)
        //                    dRows
        //            }


        //            long nRowID = dbRead.GetInt64(0);
        //            DataRow row = dt.NewRow();
        //            row.SetField<string>(0, naru.db.sqlite.SQLiteHelpers.GetSafeValueStr(ref dbRead, 1));
        //            dRows[nRowID] = dt.Rows.Count;
        //            dt.Rows.Add(row);
        //        }
        //    }
        //}

        private static void PopulateCrossTab(string sDBCon, string sqlContents, ref DataTable dt, List<Tuple<string, string>> Keycolumns, Dictionary<long, int> dCols)
        {
            Dictionary<long, object> dRows = new Dictionary<long, object>();

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
                        int rowIndex = GetRowIndex(dRows, Keycolumns, ref dbRead, 0, dt.Rows.Count);

                        if (rowIndex == dt.Rows.Count)
                        {
                            DataRow row = dt.NewRow();
                            dt.Rows.Add(row);
                        }

                        long nColID = dbRead.GetInt64(dbRead.GetOrdinal("MetricID"));
                        if (dCols.ContainsKey(nColID))
                        {
                            int colIndex = dCols[nColID] + Keycolumns.Count; // Don't forget that there's fixed columns before the data columns
                            System.Diagnostics.Debug.Assert(colIndex < dt.Columns.Count);
                            dt.Rows[rowIndex].SetField<double>(colIndex, dbRead.GetDouble(dbRead.GetOrdinal("MetricValue")));
                        }

                        foreach (Tuple<string,string> col in Keycolumns)
                            dt.Rows[rowIndex].SetField<string>(dt.Columns.IndexOf(col.Item1), dbRead[col.Item1].ToString());
                    }
                }
            }
        }

        private static int GetRowIndex(Dictionary<long, object> dRows, List<Tuple<string, string>> Keycolumns, ref SQLiteDataReader dbRead, int keyIndex, int nExistingRowCount)
        {
            int nRowIndex = 0;

            long nKeyValue = dbRead.GetInt64(dbRead.GetOrdinal(Keycolumns[keyIndex].Item1));
            if (dRows.ContainsKey(nKeyValue))
            {
                if (keyIndex < Keycolumns.Count - 1)
                {
                    nRowIndex = GetRowIndex((Dictionary<long, object>)dRows[nKeyValue], Keycolumns, ref dbRead, keyIndex + 1, nExistingRowCount);
                }
                else
                {
                    nRowIndex = (int)dRows[nKeyValue];
                }
            }
            else
            {
                dRows[nKeyValue] = new Dictionary<long, object>();

                if (keyIndex < Keycolumns.Count - 1)
                {
                    nRowIndex = GetRowIndex((Dictionary<long, object>)dRows[nKeyValue], Keycolumns, ref dbRead, keyIndex + 1, nExistingRowCount);
                }
                else
                {
                    dRows[nKeyValue] = nExistingRowCount;
                    nRowIndex = nExistingRowCount;
                }
            }

            return nRowIndex;
        }
    }
}
