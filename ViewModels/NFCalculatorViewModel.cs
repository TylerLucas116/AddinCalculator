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
        #region Declarations

        HttpClient client = new HttpClient();
        private ButtonManager NFBManager { get; set; }
        public ButtonManager nfbManager { get { return NFBManager; } }

        #region Calculator Declarations
        private double price = new double();
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        private bool OperationClicked = new bool();
        public bool operationClicked
        {
            get
            {
                return OperationClicked;
            }
            set
            {
                OperationClicked = value;
            }
        }
        private bool Calculated = new bool();
        public bool calculated
        {
            get
            {
                return Calculated;
            }
            set
            {
                Calculated = value;
            }
        }

        private bool HasDecimal = new bool();
        public bool hasDecimal
        {
            get
            {
                return HasDecimal;
            }
            set
            {
                HasDecimal = value;
            }
        }
        public string operation = "";

        private string DisplayText { get; set; }
        public string displayText
        {
            get
            {
                return DisplayText;
            }
            set
            {
                DisplayText = value;
                OnPropertyChanged("displayText");
            }
        }

        private string IntermediateText { get; set; }
        public string intermediateText
        {
            get
            {
                return IntermediateText;
            }
            set
            {
                IntermediateText = value;
                OnPropertyChanged("intermediateText");
            }
        }
        private string UPC { get; set; }
        public string upc
        {
            get
            {
                return UPC;
            }
            set
            {
                UPC = value;
                OnPropertyChanged("upc");
            }
        }

        private string WalmartUrl { get; set; }
        public string walmartUrl
        {
            get
            {
                return WalmartUrl;
            }
            set
            {
                WalmartUrl = value;
            }
        }

        private bool WalmartInformation { get; set; }
        public bool walmartInformation
        {
            get
            {
                return WalmartInformation;
            }
            set
            {
                WalmartInformation = value;

            }
        }
        private bool TargetInformation { get; set; }
        public bool targetInformation
        {
            get
            {
                return TargetInformation;
            }
            set
            {
                TargetInformation = value;
            }
        }

        private string Url { get; set; }
        public string url
        {
            get
            {
                return Url;
            }
            set
            {
                Url = value;
                OnPropertyChanged("url");
            }
        }

        private double OnlinePrice { get; set; }
        public double onlinePrice
        {
            get
            {
                return OnlinePrice;
            }
            set
            {
                OnlinePrice = value;
                OnPropertyChanged("onlinePrice");
            }
        }

        private string TextblockPrice { get; set; }
        public string textblockPrice
        {
            get
            {
                return TextblockPrice;
            }
            set
            {
                TextblockPrice = value;
                OnPropertyChanged("textblockPrice");
            }
        }
        public DataPackage dataPackage = new DataPackage();

        private bool Found { get; set; }
        public bool found
        {
            get
            {
                return Found;
            }
            set
            {
                Found = value;
            }
        }

        private string OnlineAbbrev { get; set; }
        public string onlineAbbrev
        {
            get
            {
                return OnlineAbbrev;
            }
            set
            {
                OnlineAbbrev = value;
            }
        }

        // webview
        
        private string _webViewURISource;
        public string webViewURIsource
        {
            get
            {
                return _webViewURISource;
            }
            set
            {
                _webViewURISource = value;
                OnPropertyChanged("webViewURIsource");
            }
        }

        WebView webView = new WebView();

        ApiKey key = new ApiKey();

        #endregion

        #endregion

        public NFCalculatorViewModel()
        {
            NFBManager = new ButtonManager();
            nfbManager.InitializeCollections();
            nfbManager.UpdateNFButtons();
        }

        #region Calculation Methods

        public void NumberClicked(object sender, RoutedEventArgs e)
        {
            if (displayText == "0" || (operationClicked) || (calculated))
                displayText = "";

            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            displayText += b.Content;
            operationClicked = false;
            calculated = false;
        }

        public void ClearEntry(object sender, RoutedEventArgs e)
        {
            Price = 0;
            displayText = "0";
            intermediateText = "";
            hasDecimal = false;
        }
        public void Clear(object sender, RoutedEventArgs e)
        {
            Price = 0;
            displayText = "0";
            intermediateText = "";
            hasDecimal = false;
        }

        public void BackSpace(object sender, RoutedEventArgs e)
        {
            string backspaceChar = "";
            if (displayText == "0" || displayText == "")
                return;
            else
            {
                backspaceChar = displayText[displayText.Length - 1].ToString();
                string backspaceString = displayText.Substring(0, (displayText.Length - 1));
                displayText = backspaceString;
            }

            if (backspaceChar == ".")
                hasDecimal = false;
        }

        public void Divide(object sender, RoutedEventArgs e)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Divide";
            Price = Double.Parse(displayText);
            intermediateText = Price + " /";
            hasDecimal = false;
        }

        public void Multiply(object sender, RoutedEventArgs e)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Multiply";
            Price = Double.Parse(displayText);
            intermediateText = Price + " *";
            hasDecimal = false;
        }

        public void Subtract(object sender, RoutedEventArgs e)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Subtract";
            Price = Double.Parse(displayText);
            intermediateText = Price + " -";
            hasDecimal = false;
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Add";
            Price = Double.Parse(displayText);
            intermediateText = Price + " +";
            hasDecimal = false;
        }

        public void Calculate(object sender, RoutedEventArgs e)
        {
            intermediateText = "";

            if (displayText == "")
                return;

            operationClicked = false;
            switch (operation)
            {
                case "Add":
                    displayText = Math.Round((Price + Double.Parse(displayText)), 3).ToString();
                    break;
                case "Subtract":
                    displayText = Math.Round((Price - Double.Parse(displayText)), 3).ToString();
                    break;
                case "Multiply":
                    displayText = Math.Round((Price * Double.Parse(displayText)), 3).ToString();
                    break;
                case "Divide":
                    displayText = Math.Round((Price / Double.Parse(displayText)), 3).ToString();
                    break;
                default: //No other options
                    break;
            }
            calculated = true;
            hasDecimal = false;
        }

        public void Decimal(object sender, RoutedEventArgs e)
        {
            if ((displayText == "0") || (operationClicked) || (calculated))
                displayText = "";
            if (hasDecimal == false)
            {
                Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
                displayText += b.Content;
                operationClicked = false;
                calculated = false;
                hasDecimal = true;
            }
            else
                return;
        }

        public void WebsiteClick(object sender, RoutedEventArgs e)
        {
            intermediateText = "";
            if (displayText == "" || displayText == "0" || displayText == ".")
                return;

            operationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(displayText);
            displayText = RoundToNine(Price * (nfbManager.nfCollection[index].percentage / 100)).ToString();
        }

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
                if (found)
                {
                    operationClicked = false;
                    calculated = true;
                    hasDecimal = false;
                    displayText = onlinePrice.ToString();
                    textblockPrice = onlinePrice.ToString() + onlineAbbrev;
                }
                else
                {
                    var messageDialog = new MessageDialog("No price was found online.");
                    await messageDialog.ShowAsync();
                }

                // reset found
                found = false;
            }
        }
        public async Task searchWalmartNF()
        {
            try
            {
                walmartUrl = "http://api.walmartlabs.com/v1/items" + key.WalmartKey;
                
                url = (walmartUrl + upc);

                var walmartResponse = await client.GetAsync(new Uri(url));
                walmartResponse.EnsureSuccessStatusCode(); //added for success

                if (walmartResponse.IsSuccessStatusCode)
                {
                    var text = await walmartResponse.Content.ReadAsStringAsync();

                    JObject jsonText = JObject.Parse(text);

                    string jsonString = (string)jsonText["items"][0]["salePrice"];
                    Debug.WriteLine(jsonString);

                    if (jsonString != "") //If there was text in salePrice
                    {
                        found = true;
                        double priceHolder;
                        if(Double.TryParse(jsonString, out priceHolder))
                        {
                            onlinePrice = priceHolder;
                        }

                        bool walmartFound = false;
                        int i = 0;
                        // still need to traverse the collection to get correct percentage in case it changes in future
                        for (i = 0; (i < nfbManager.nfCollection.Count() && (walmartFound == false)); ++i) //Find walmart percentage, changed from i < 49
                        {
                            string nfcollection = nfbManager.nfCollection[i].retailer;
                            if (nfbManager.nfCollection[i].retailer == "Walmart")
                            {
                                walmartFound = true;
                                walmartInformation = true;
                            }
                        }

                        if (walmartInformation)
                        {
                            onlineAbbrev = (" @WM $" + onlinePrice.ToString());
                            onlinePrice *= (nfbManager.nfCollection[i - 1].percentage / 100); // added i - 1, wasn't accessing correct percentage before
                            onlinePrice = RoundToNine(onlinePrice); // added to round to 9
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("No Walmart information was found in the calculator");
                            await messageDialog.ShowAsync();
                            walmartInformation = false;
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
                url = (targetURL + upc);


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
                url = (cvsURL + upc);

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