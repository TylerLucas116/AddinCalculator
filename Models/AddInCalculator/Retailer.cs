using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class Retailer
    {
        private string name;
        private string websiteURL;
        private string onlineAbbrev;
        private double onlinePrice;
        private double foodPercentage = 0;
        private double nonfoodPercentage = 0;
        private double nonfoodDFPercentage = 0;
        private double freezerPercentage = 0;
        private double coolerPercentage = 0;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string WebsiteURL
        {
            get { return websiteURL; }
            set { websiteURL = value; }
        }
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set { onlineAbbrev = value; }
        }
        public double OnlinePrice
        {
            get { return onlinePrice; }
            set { onlinePrice = value; }
        }
        private double FoodPercentage
        {
            get { return foodPercentage; }
            set { foodPercentage = value; }
        }
        private double NonfoodPercentage
        {
            get { return nonfoodPercentage; }
            set { nonfoodPercentage = value; }
        }
        private double NonfoodDFPercentage
        {
            get { return nonfoodDFPercentage; }
            set { nonfoodDFPercentage = value; }
        }
        private double FreezerPercentage
        {
            get { return freezerPercentage; }
            set { freezerPercentage = value; }
        }
        private double CoolerPercentage
        {
            get { return coolerPercentage; }
            set { coolerPercentage = value; }
        }
    }
}
