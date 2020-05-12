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
    public class NFCalculatorViewModel :INotifyPropertyChanged
    {
        public NFCalculatorViewModel()
        {
            nfButtonManager = new ButtonManager();
            NFButtonManager.InitializeCollections();
            NFButtonManager.UpdateNFButtons();
            calculator = new Calculator();
            scraper = new WebScraper();
        }

        #region Declarations

        HttpClient client = new HttpClient();
        ApiKey key = new ApiKey();
        public DataPackage dataPackage = new DataPackage();
        WebView webView = new WebView();
        private ButtonManager nfButtonManager;
        private Calculator calculator;
        private WebScraper scraper;

        private double onlinePrice;
        private double test;
        private string displayText;
        private string intermediateText;
        private string upc;
        private string textblockPrice;

        private string url;
        private string walmartUrl = "http://api.walmartlabs.com/v1/items";
        private bool found;

        private bool walmartInformation;
        private bool targetInformation;
        private string onlineAbbrev;

        public ButtonManager NFButtonManager
        {
            get { return nfButtonManager; }
            set { }
        }
        public Calculator Calculator
        {
            get { return calculator; }
            set { }
        }
        public WebScraper Scraper
        {
            get { return scraper; }
            set { scraper = value; }
        }

        public double OnlinePrice
        {
            get { return onlinePrice; }
            set
            {
                onlinePrice = value;
                OnPropertyChanged("OnlinePrice");
            }
        }

        public string DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                OnPropertyChanged("DisplayText");
            }
        }
        public string IntermediateText
        {
            get { return intermediateText; }
            set
            {
                intermediateText = value;
                OnPropertyChanged("IntermediateText");
            }
        }
        public string UPC
        {
            get { return upc; }
            set
            {
                upc = value;
                OnPropertyChanged("UPC");
            }
        }
        public string TextblockPrice
        {
            get { return textblockPrice; }
            set
            {
                textblockPrice = value;
                OnPropertyChanged("TextblockPrice");
            }
        }
        public string URL
        {
            get { return url; }
            set { url = value; }
        }
        public string WalmartUrl
        {
            get { return walmartUrl; }
            set { walmartUrl = "http://api.walmartlabs.com/v1/items" + key.WalmartKey; }
        }
        public bool Found
        {
            get { return found; }
            set { found = value; }
        }

        public bool WalmartInformation
        {
            get { return walmartInformation; }
            set { walmartInformation = value; }
        }
        public bool TargetInformation
        {
            get { return targetInformation; }
            set { targetInformation = value; }
        }
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set { onlineAbbrev = value; }
        }

        #endregion

        #region Calculation Methods

        public double RoundToNine(double value)
        {
            double roundedValue = Math.Round(value, 1);
            return roundedValue - 0.01;
        }

        public async void UPCSearch(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                
                // search Walmart
                try {
                    await searchWalmartNF();
                    //if (!found || !walmartInformation)
                } catch (HttpRequestException httpException) {
                    Debug.WriteLine("HTTP exception caught.");
                    Debug.WriteLine(httpException);
                } catch (Exception exception) {
                    Debug.WriteLine("Exception caught.");
                    Debug.WriteLine(exception);
                }

                /*
                // search Target
                try {
                    await searchTargetNF();
                } catch (HttpRequestException httpException) {
                    Debug.WriteLine("HTTP exception caught.");
                    Debug.WriteLine(httpException);
                } catch (Exception exception) {
                    Debug.WriteLine("Exception caught.");
                    Debug.WriteLine(exception);
                }*/

                
                // search CVS
                /*try
                {
                    await searchCVSNF();
                }
                catch (HttpRequestException httpException)
                {
                    Debug.WriteLine("HTTP exception caught.");
                    Debug.WriteLine(httpException);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Exception caught.");
                    Debug.WriteLine(exception);
                }*/

                // if a price was found
                if (Found)
                {
                    DisplayText = OnlinePrice.ToString();
                    TextblockPrice = OnlinePrice.ToString() + onlineAbbrev;
                }
                else
                {
                    var messageDialog = new MessageDialog("No price was found online.");
                    await messageDialog.ShowAsync();
                }

                // reset found
                Found = false;
            }
        }
        public async Task searchWalmartNF()
        {
            try
            { 
                url = (WalmartUrl + key.WalmartKey + UPC);
                var walmartResponse = await client.GetAsync(new Uri(url));
                walmartResponse.EnsureSuccessStatusCode();

                if (walmartResponse.IsSuccessStatusCode)
                {
                    var text = await walmartResponse.Content.ReadAsStringAsync();

                    JObject jsonText = JObject.Parse(text);

                    string jsonString = (string)jsonText["items"][0]["salePrice"];
                    Debug.WriteLine(jsonString);

                    if (jsonString != "") //If there was text in salePrice
                    {
                        Found = true;
                        double priceHolder;
                        if(Double.TryParse(jsonString, out priceHolder))
                        {
                            OnlinePrice = priceHolder;
                        }

                        bool walmartFound = false;
                        int i = 0;
                        // still need to traverse the collection to get correct percentage in case it changes in future
                        for (i = 0; (i < NFButtonManager.nfCollection.Count() && (walmartFound == false)); ++i)
                        {
                            string nfcollection = NFButtonManager.nfCollection[i].retailer;
                            if (NFButtonManager.nfCollection[i].retailer == "Walmart")
                            {
                                walmartFound = true;
                                WalmartInformation = true;
                            }
                        }

                        if (WalmartInformation)
                        {
                            OnlineAbbrev = (" @WM $" + OnlinePrice.ToString());
                            OnlinePrice *= (NFButtonManager.nfCollection[i - 1].percentage / 100);
                            OnlinePrice = RoundToNine(OnlinePrice);
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("No Walmart information was found in the calculator");
                            await messageDialog.ShowAsync();
                            WalmartInformation = false;
                        }
                    }
                    else
                    {
                        return;
                    }              
                }
            }
            catch (HttpRequestException httpException)
            {
                Debug.WriteLine("HTTP exception caught.");
                Debug.WriteLine(httpException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught.");
                Debug.WriteLine(exception);
            }
        }

        // search target using agility pack
        public async Task searchTargetNF()
        {
            try
            {
                string targetURL = "https://www.target.com/s?searchTerm=";
                url = (targetURL + UPC);


                webView.Navigate(new Uri(url));
                //webView.DOMContentLoaded += DOMContentLoaded;
                //webView.NavigationCompleted += NavigationCompleted;
                
                // delay to ensure navigation completes
                await Task.Delay(6500);
                string html = await webView.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                var text = html;
                var doc = new HtmlDocument();
                doc.LoadHtml(text);

                var price = doc.DocumentNode.SelectSingleNode("//*[@id=\"mainContainer\"]/div[3]/div[2]/div/div[1]/div[3]/div/ul/li/div/div[2]/div/div/div/div[2]/span");
                //var price = doc.DocumentNode.SelectNodes("//div[@class='styles__StyledPricePromoWrapper-e5kry1-12 gzebgK']");
                Debug.WriteLine("Target price found:");
                Debug.WriteLine(price.InnerText);
            }
            catch (HttpRequestException httpException)
            {
                Debug.WriteLine("HTTP exception caught.");
                Debug.WriteLine(httpException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught.");
                Debug.WriteLine(exception);
            }
        }

        public async Task searchCVSNF()
        {
            try
            {
                string cvsURL = "https://www.cvs.com/search?searchTerm=";
                url = (cvsURL + UPC);

                webView.Navigate(new Uri(url));
                await Task.Delay(10000);
                string html = await webView.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var price = doc.DocumentNode.SelectSingleNode("//*[@id=\"root\"]/div/div/div/div[2]/div/div/div/div/div[7]/div[2]/div[3]/div[2]/div/div/div/div/div/div[2]/div/div/div/div/div/a/div[2]/div[1]/div[3]/div");
                Debug.WriteLine("CVS price found");
                Debug.WriteLine(price.InnerText);
            }
            catch (HttpRequestException httpException)
            {
                Debug.WriteLine("HTTP exception caught.");
                Debug.WriteLine(httpException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught.");
                Debug.WriteLine(exception);
            }
        }
        #endregion

        #region Previous DB Code


        #region Declarations
        String table = "ButtonInfo";
        String fieldname = "Button";
        String ObjectPath = "AddInCalculator2._0.Models.AddInCalculator.Button";
        Type obType = (typeof(Models.AddInCalculator.Button));
        //Button button1 = new Button();
        #endregion
        #region Create Database
        private void CreateDatabase()
        {
            Handlers.Database db = new Handlers.Database();
            Handlers.DatabaseField myField = db.BuildFieldObject("nvarchar", fieldname);
        }

        #endregion
        #region Read From Database
        private void ReturnAllRecords()
        {
            var myButtonList = new List<Models.AddInCalculator.Button>();
            Handlers.Database db = new Handlers.Database();

            myButtonList = db.ReturnAllRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath);
            /*foreach (Models.AddInCalculator.Button myButton in myButtonList)
            {
                lvRecords.Items.Add(myButton.retailer + "  " + myButton.label + "  " +
                    myButton.percentage.ToString() + "  " + myButton.type + "  " + myButton.abbrev);
            }*/
        }
        #endregion
        #region Write to Database
        private void WriteRecord()
        {
            Handlers.Database db = new Handlers.Database();
            var myButton = buildButtonObject();

            db.WriteRecord<Models.AddInCalculator.Button>(myButton, table, db.BuildFieldObject("nvarchar", fieldname));
        }
        #endregion
         private Models.AddInCalculator.Button buildButtonObject()
        {
            var myButton = new Models.AddInCalculator.Button();

            /*  public int ID { get; set; } //start at 1, 2, 3
                public string retailer { get; set; }
                public string label { get; set; } //Walmart, targer
                public string abbrev { get; set; } //wm, tg, etc (for pasting)
                public decimal percentage { get; set; } //75, 50, etc
                public string type { get; set; } //food,*/
            //myButton.retailer = tbRetailer.Text;
            //myButton.label = tbLabel.Text;
            //myButton.abbrev = tbAbbreviation.Text;
            //myButton.percentage = Decimal.Parse(tbPercentage.Text);
            //myButton.type = tbType.Text;

            return myButton;

        }
        #endregion

        #region Observable Objects
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }

        // event handling for dom content loaded
        /*
        public event TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> DOMContentLoaded
        {

        }*/
        public async void DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            try
            {
                string html = await sender.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                var text = html;
                var doc = new HtmlDocument();
                doc.LoadHtml(text);

                var price = doc.DocumentNode.SelectSingleNode("//*[@id=\"mainContainer\"]/div[3]/div[2]/div/div[1]/div[3]/div/ul/li/div/div[2]/div/div/div/div[2]/span");
                //var price = doc.DocumentNode.SelectNodes("//div[@class='styles__StyledPricePromoWrapper-e5kry1-12 gzebgK']");
                Debug.WriteLine("Target price found:");
                Debug.WriteLine(price.InnerText);
            }
            catch (HttpRequestException httpException)
            {
                Debug.WriteLine("HTTP exception caught.");
                Debug.WriteLine(httpException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught.");
                Debug.WriteLine(exception);
            }
        }

        public async void NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                string html = await sender.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                var text = html;
                var doc = new HtmlDocument();
                doc.LoadHtml(text);

                var price = doc.DocumentNode.SelectSingleNode("//*[@id=\"mainContainer\"]/div[3]/div[2]/div/div[1]/div[3]/div/ul/li/div/div[2]/div/div/div/div[2]/span");
                //var price = doc.DocumentNode.SelectNodes("//div[@class='styles__StyledPricePromoWrapper-e5kry1-12 gzebgK']");
                Debug.WriteLine("Target price found:");
                Debug.WriteLine(price.InnerText);
            }
            catch (HttpRequestException httpException)
            {
                Debug.WriteLine("HTTP exception caught.");
                Debug.WriteLine(httpException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught.");
                Debug.WriteLine(exception);
            }
        }

        private delegate void DomLoadedEventHandler(object source, WebViewDOMContentLoadedEventArgs args);
        #endregion

        }
}