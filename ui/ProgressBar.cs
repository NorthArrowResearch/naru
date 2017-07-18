using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naru.ui
{
    public class ProgressBar
    {
        public static int ProgressPercent(int nVisitCounter, int TotalNumberVisits)
        {
            if (TotalNumberVisits == 0)
                return 0;
            else
            {
                if (nVisitCounter == 0)
                    return 0;
                else
                    if (nVisitCounter == TotalNumberVisits)
                    return 100;
                else
                    return (int)(100.0 * (double)nVisitCounter / (double)TotalNumberVisits);
            }
        }
    }
}
