using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace naru.db
{
    public enum DBState
    {
        New,
        Edited,
        Unchanged
    }

    public class EditableNamedObject : NamedObject
    {
        public DBState m_eState;

        public DBState State
        {
            get { return m_eState; }
            protected set
            {
                if (m_eState != DBState.New)
                    m_eState = DBState.Edited;
            }
        }

        public EditableNamedObject(long nID, String sName)
            : base(nID, sName)
        {
            if (nID > 0)
                State = DBState.Unchanged;
            else
                State = DBState.New;
        }

        public new string Name
        {
            get { return m_sName; }
            set
            {
                if (string.Compare(m_sName, value) != 0)
                {
                    base.Name = value;
                    if (m_eState != DBState.New)
                        m_eState = DBState.Edited;
                }
            }
        }

        protected static void AddParameter(ref SQLiteCommand dbCom, string sParameterName, System.Data.DbType dbType, object objValue)
        {
            SQLiteParameter p = null;
            if (dbCom.Parameters.Contains(sParameterName))
                p = dbCom.Parameters[sParameterName];
            else
            {
                p = dbCom.Parameters.Add(sParameterName, dbType);
            }

            if (objValue == null)
                p.Value = DBNull.Value;
            else
                p.Value = objValue;
        }
    }
}
