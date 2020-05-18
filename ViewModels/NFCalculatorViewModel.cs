using AddInCalculator2._0.Models.AddInCalculator;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AddInCalculator2._0.ViewModels
{
    public class NFCalculatorViewModel
    {
        public NFCalculatorViewModel()
        {
            calculator = new Calculator();
            scraper = new WebScraper();
            nfButtonManager = new ButtonManager();
            NFButtonManager.InitializeCollections();
            NFButtonManager.UpdateNFButtons();
            retailButtonManager = new RetailButtonManager();
        }

        private Calculator calculator;
        private WebScraper scraper;
        private ButtonManager nfButtonManager;
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
        public ButtonManager NFButtonManager
        {
            get { return nfButtonManager; }
            set { }
        }
        public RetailButtonManager RetailButtonManager
        {
            get { return retailButtonManager; }
            set { retailButtonManager = value; }
        }
    }
}