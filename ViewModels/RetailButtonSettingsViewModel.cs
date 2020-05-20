using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInCalculator2._0.Models.AddInCalculator;

namespace AddInCalculator2._0.ViewModels
{
    public class RetailButtonSettingsViewModel
    {
        public RetailButtonSettingsViewModel()
        {
            retailManager = new RetailerManager();
        }

        private RetailerManager retailManager;

        public RetailerManager RetailManager
        {
            get { return retailManager; }
            set { retailManager = value; }
        }
    }
}
