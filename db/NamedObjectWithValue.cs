using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace naru.db
{
    class NamedObjectWithValue : NamedObject
    {
        private decimal m_fValue;

        public NamedObjectWithValue(long nID, string sName, decimal fValue)
            : base(nID, sName)
        {
            m_fValue = fValue;
        }

        public decimal Value
        {
            get { return m_fValue; }
            set
            {
                if (value < 0)
                    throw new Exception("The value cannot be less than zero.");
                else
                    m_fValue = value;
            }
        }
    }
}