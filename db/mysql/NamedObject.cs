using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace naru.db.mysql
{
    public class NamedObject
    {
        public static int LoadComboWithListItems(ref System.Windows.Forms.ComboBox cbo, string sDBCon, string sSQL, long nSelectID = 0)
        {
            cbo.Items.Clear();

            using (MySqlConnection dbCon = new MySqlConnection(sDBCon))
            {
                dbCon.Open();

                MySqlCommand dbCom = new MySqlCommand(sSQL, dbCon);
                MySqlDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                {
                    long nID = dbRead.GetInt64(0);
                    int nIn = cbo.Items.Add(new naru.db.NamedObject(nID, dbRead.GetString(1)));
                    if (nID == nSelectID)
                        cbo.SelectedIndex = nIn;
                }
            }

            return cbo.Items.Count;
        }
    }
}
