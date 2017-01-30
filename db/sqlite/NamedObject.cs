using System;
using System.Data.SQLite;

namespace naru.db.sqlite
{
    public class NamedObject
    {
        public static int LoadComboWithListItems(ref System.Windows.Forms.ComboBox cbo, string sDBCon, string sSQL, long nSelectID = 0)
        {
            cbo.Items.Clear();

            using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
            {
                dbCon.Open();

                SQLiteCommand dbCom = new SQLiteCommand(sSQL, dbCon);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    Int64 nID = (Int64)dbRead.GetValue(0);
                    int nIn = cbo.Items.Add(new naru.db.NamedObject(nID, dbRead.GetString(1)));
                    if (nID == nSelectID)
                        cbo.SelectedIndex = nIn;
                }
            }

            return cbo.Items.Count;
        }

        public static void SelectItem(ref System.Windows.Forms.ComboBox cbo, int nID)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i] is naru.db.NamedObject)
                {
                    if (((naru.db.NamedObject)cbo.Items[i]).ID == nID)
                    {
                        cbo.SelectedIndex = i;
                        return;
                    }
                }
            }
        }
    }

    public class CheckedListItem
    {
        public static int LoadComboWithListItems(ref System.Windows.Forms.CheckedListBox lst, string sDBCon, string sSQL, bool bCheckItems)
        {
            lst.Items.Clear();

            using (SQLiteConnection dbCon = new SQLiteConnection(sDBCon))
            {
                dbCon.Open();

                SQLiteCommand dbCom = new SQLiteCommand(sSQL, dbCon);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    long nID = 0;
                    if (dbRead.GetFieldType(0) == Type.GetType("System.Int16"))
                        nID = (int)dbRead.GetInt16(0);
                    else if (dbRead.GetFieldType(0) == Type.GetType("System.Int32"))
                        nID = dbRead.GetInt32(0);
                    else if (dbRead.GetFieldType(0) == Type.GetType("System.Int64"))
                        nID = (int)dbRead.GetInt64(0);
                    else
                        throw new Exception("Unhandled field type in column 0");

                    int nIn = lst.Items.Add(new naru.db.NamedObject(nID, dbRead.GetString(1)));
                    lst.SetItemChecked(nIn, bCheckItems);
                }
            }

            return lst.Items.Count;
        }
    }
}
