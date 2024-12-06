using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker
{
    internal class Products
    {
        public int Id { get; set; }
        public string Office {  get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double LocalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime PurchaseData { get; set; }


        //navigation property
        public Assets Assets { get; set; }

        //the compiler will read the nav property and associate it with the Id column of the Assets table. Foreign key.
        public int AssetsId { get; set; }
    }
}
