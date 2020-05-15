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
        private static ButtonManager bManager = new ButtonManager();
        public static ButtonManager BManager { get { return bManager; } }

        public RetailButtonSettingsViewModel()
        {
            bManager = new ButtonManager();
            bManager.UpdateButtons();
        }
    }
}
