using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    class Retailer
    {
        private double onlinePrice;
        private string websiteURL;

        public double OnlinePrice
        {
            get { return onlinePrice; }
            set
            {
                onlinePrice = value;
            }
        }
        public string WebsiteURL
        {
            get { return websiteURL; }
            set
            {
                websiteURL = value;
            }
        }
    }
}
