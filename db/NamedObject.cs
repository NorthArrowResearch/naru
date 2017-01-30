using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace naru.db
{
    public class NamedObject
    {
        private long m_nID;
        private String m_sName;

        public NamedObject(long nID, String sName)
        {
            m_nID = nID;
            m_sName = sName;
        }

        public override string ToString()
        {
            return m_sName;
        }

        public long ID
        {
            get
            {
                return m_nID;
            }
        }

     
    }
}
    }
}
