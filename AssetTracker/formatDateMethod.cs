using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker
{
    public class formatDate
    {
        public DateTime start = new DateTime();
        public DateTime end = new DateTime();

        public int RedDate(DateTime start, DateTime end)
        {
            start = DateTime.Now;
            int diff = (start.Month + start.Year * 12) - (end.Month + end.Year * 12);

            return diff;
        }



    }
}