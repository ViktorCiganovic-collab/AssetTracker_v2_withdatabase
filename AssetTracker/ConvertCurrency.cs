using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker
{

    public static class DoConversion
    {
        public static double PerformConversion(string firstCurrency, string secondCurrency, double currencyValue)
        {
            var client = new WebClient();
            var conversionRateResponse = client.DownloadString($"http://currencies.apps.grandtrunk.net/getlatest/{firstCurrency}/{secondCurrency}");

            return currencyValue * Convert.ToDouble(conversionRateResponse, CultureInfo.InvariantCulture);
        }

    }



}