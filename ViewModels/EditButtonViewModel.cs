﻿using AddInCalculator2._0.Models.AddInCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.ViewModels
{
    class EditButtonViewModel
    {
        private static ButtonManager bManager = new ButtonManager();
        public static ButtonManager BManager { get { return bManager; } }

        public EditButtonViewModel()
        {
            bManager = new ButtonManager();
        }
    }
}
