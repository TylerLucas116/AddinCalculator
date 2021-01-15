using AddInCalculator2._0.Models.AddInCalculator;
using AddInCalculator2._0.ViewModels.ViewModelBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.ViewModels
{
    public class CalculatorViewModel : MainViewModelBase
    {
        public CalculatorViewModel()
        {
            calculator = new Calculator();
            scraper = new WebScraper();
            retailButtonManager = new RetailButtonManager();
        }

        private Calculator calculator;
        private WebScraper scraper;
        private RetailButtonManager retailButtonManager;

        public Calculator Calculator
        {
            get { return calculator; }
            set { }
        }
        public WebScraper Scraper
        {
            get { return scraper; }
            set { }
        }
        public RetailButtonManager RetailButtonManager
        {
            get { return retailButtonManager; }
            set { retailButtonManager = value; }
        }
    }
}
