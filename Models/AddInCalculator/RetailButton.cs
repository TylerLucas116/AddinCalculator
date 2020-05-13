using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButton
    {
        RetailButton()
        {
            retailer = new Retailer();
        }

        private Retailer retailer;

        public Retailer Retailer
        {
            get { return retailer; }
        }
    }
}
