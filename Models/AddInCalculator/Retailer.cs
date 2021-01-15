using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class Retailer
    {
        private string name = "";
        private string onlineAbbrev = "";
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
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set { onlineAbbrev = value; }
        }
        public double FoodPercentage
        {
            get { return foodPercentage; }
            set { foodPercentage = value; }
        }
        public double NonfoodPercentage
        {
            get { return nonfoodPercentage; }
            set { nonfoodPercentage = value; }
        }
        public double NonfoodDFPercentage
        {
            get { return nonfoodDFPercentage; }
            set { nonfoodDFPercentage = value; }
        }
        public double FreezerPercentage
        {
            get { return freezerPercentage; }
            set { freezerPercentage = value; }
        }
        public double CoolerPercentage
        {
            get { return coolerPercentage; }
            set { coolerPercentage = value; }
        }
    }
}
