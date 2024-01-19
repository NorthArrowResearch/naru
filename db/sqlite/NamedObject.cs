using SandbarWorkbench;
using SandbarWorkbench.DBHelpers;
using System;
using System.ComponentModel;
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

            // Attach the SQL statement to the combo box to enabled reloading
            cbo.Tag = sSQL;

            return cbo.Items.Count;
        }

        public static int LoadComboColumnWithListItems(string conString, ref System.Windows.Forms.DataGridViewComboBoxColumn cbo, string sSQL, bool bAddSelectItem)
        {
            if (cbo == null)
                return 0;

            cbo.Items.Clear();

            BindingList<naru.db.NamedObject> lItems = new BindingList<naru.db.NamedObject>();
            using (System.Data.SQLite.SQLiteConnection dbCon = new System.Data.SQLite.SQLiteConnection(conString))
            {
                dbCon.Open();

                System.Data.SQLite.SQLiteCommand dbCom = new System.Data.SQLite.SQLiteCommand(sSQL, dbCon);
                System.Data.SQLite.SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    Int64 nID = (Int64)dbRead.GetValue(0);
                    lItems.Add(new naru.db.NamedObject((int)nID, dbRead.GetString(1)));
                }
            }

            if (bAddSelectItem)
                lItems.Add(new naru.db.NamedObject(0, "-- Select --"));

            cbo.DataSource = lItems;
            cbo.DisplayMember = "Name";
            cbo.ValueMember = "ID";

            return cbo.Items.Count;
        }

        public static int LoadComboWithListItems(string conString, ref System.Windows.Forms.ComboBox cbo, long nSelectID = 0)
        {
            if (cbo.Tag is string && !string.IsNullOrEmpty(cbo.Tag.ToString()))
                return LoadComboWithListItems(ref cbo, conString, cbo.Tag.ToString(), nSelectID);
            else
                throw new Exception("The combo box does not have an SQL query string attached as tag. Use overloaded method before this one.");
        }

        public static void SelectItem(ref System.Windows.Forms.ComboBox cbo, long nID)
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
        public static int LoadCheckListbox(ref System.Windows.Forms.CheckedListBox lst, string sDBCon, string sSQL, bool bCheckItems)
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
