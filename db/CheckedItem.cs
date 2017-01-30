using System;

namespace naru.db
{
    public class CheckedItem
    {
        public class CheckedListItem : NamedObject
        {
            private Boolean Checked { get; set; }
            
            public CheckedListItem(int nID, string sName, Boolean bChecked = true)
                : base(nID, sName)
            {
                Checked = bChecked;
            }
        }
    }
}
