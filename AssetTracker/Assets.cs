using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker
{
    internal class Assets
    {
        public int Id { get; set; }
        public string Asset_type { get; set; }

        public List <Products> Products { get; set; }
    }
}
