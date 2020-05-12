using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    class Retailer
    {
        private string name;
        private string websiteURL;
        private string onlineAbbrev;
        private double onlinePrice;

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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set { onlineAbbrev = value; }
        }
    }
}
