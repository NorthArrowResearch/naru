using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                base.Name = value;
                if (m_eState != DBState.New)
                    m_eState = DBState.Edited;
            }
        }
    }
}
