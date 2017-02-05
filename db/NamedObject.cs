using System;

namespace naru.db
{
    public enum DBState
    {
        New,
        Edited,
        Unchanged
    }

    public class NamedObject
    {
        public long ID { get; internal set; }
        public String m_sName { get; internal set; }

        public DBState m_eState;
        
        public DBState State
        {
            get { return m_eState; }
            protected set
            {
                if (State != DBState.New)
                    State = DBState.Edited;
            }
        }

        public string Name
        {
            get { return m_sName; }
            set
            {
                m_sName = value;
                State = DBState.Edited;
            }
        }

        public NamedObject(long nID, String sName)
        {
            ID = nID;
            m_sName = sName;

            if (nID > 0)
                State = DBState.Unchanged;
            else
                State = DBState.New;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
