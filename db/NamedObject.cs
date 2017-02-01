using System;

namespace naru.db
{
    public class NamedObject
    {
        public long ID { get; internal set; }
        public String Name { get; internal set; }

        public NamedObject(long nID, String sName)
        {
            ID = nID;
            Name = sName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
