using System;

namespace naru.db
{
    public class NamedObject
    {
        public long ID { get; internal set; }
        public String m_sName { get; internal set; }
        
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public NamedObject(long nID, String sName)
        {
            ID = nID;
            m_sName = sName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
