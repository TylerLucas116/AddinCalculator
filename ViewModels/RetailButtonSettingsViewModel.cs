using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInCalculator2._0.Models.AddInCalculator;

namespace AddInCalculator2._0.ViewModels
{
    class RetailButtonSettingsViewModel
    {
        public RetailButtonSettingsViewModel()
        {
            ButtonManager = new RetailButtonManager();
            ButtonManager.UpdateRetailButtons();
        }

        private static RetailButtonManager buttonManager = new RetailButtonManager();

        public static RetailButtonManager ButtonManager
        {
            get { return buttonManager; }
            set { buttonManager = value; }
        }
    }
}
