using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    /// <summary>
    /// Provides the functionality for the Retailer UI buttons in the calculator, such as Target, Walmart, etc.
    /// </summary>
    public class RetailButton
    {
        public RetailButton()
        {
            retailer = new Retailer();
        }

        private Retailer retailer;
        private bool visibility = false;

        /// <summary>
        /// Sets the visibility for the RetailButtons in the UI calculator, such as Walmart, Target, etc. This should always be true 
        /// if there is data for a retailer
        /// </summary>
        /// <value>gets/sets the private field visibility</value>
        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        /// <summary>
        /// The Retailer property represents the retailer information for each UI RetailerButton
        /// </summary>
        /// <value>gets/sets the private field retailer</value>
        public Retailer Retailer
        {
            get { return retailer; }
            set { retailer = value; }
        }
    }
}
