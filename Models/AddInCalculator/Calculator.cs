using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class Calculator : INotifyPropertyChanged
    {
        public Calculator()
        {
            nfButtonManager = new ButtonManager();
            NFButtonManager.InitializeCollections();
            NFButtonManager.UpdateNFButtons();
            retailButtonManager = new RetailButtonManager();
            
        }

        private ButtonManager nfButtonManager;
        private RetailButtonManager retailButtonManager;

        private double price;
        private bool operationClicked;
        private bool calculated;
        private bool hasDecimal;
        private string displayText;
        private string intermediateText;
        private string upc;
        private string textblockPrice;
        private string operation;

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
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public bool OperationClicked
        {
            get { return operationClicked; }
            set { operationClicked = value; }
        }

        public bool Calculated
        {
            get { return calculated; }
            set { calculated = value; }
        }
        public bool HasDecimal
        {
            get { return hasDecimal; }
            set { hasDecimal = value; }
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
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }


        public void NumberClicked(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "0" || (OperationClicked) || (Calculated))
                DisplayText = "";

            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            DisplayText += b.Content;
            OperationClicked = false;
            Calculated = false;
        }

        public void ClearEntry(object sender, RoutedEventArgs e)
        {
            Price = 0;
            DisplayText = "0";
            IntermediateText = "";
            HasDecimal = false;
        }
        public void Clear(object sender, RoutedEventArgs e)
        {
            Price = 0;
            DisplayText = "0";
            IntermediateText = "";
            HasDecimal = false;
        }

        public void BackSpace(object sender, RoutedEventArgs e)
        {
            string backspaceChar = "";
            if (DisplayText == "0" || DisplayText == "")
                return;
            else
            {
                backspaceChar = DisplayText[DisplayText.Length - 1].ToString();
                string backspaceString = DisplayText.Substring(0, (DisplayText.Length - 1));
                DisplayText = backspaceString;
            }

            if (backspaceChar == ".")
                HasDecimal = false;
        }

        public void Divide(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "" || DisplayText == ".")
                return;

            OperationClicked = true;
            Operation = "Divide";
            Price = Double.Parse(DisplayText);
            IntermediateText = Price + " /";
            HasDecimal = false;
        }

        public void Multiply(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "" || DisplayText == ".")
                return;

            OperationClicked = true;
            Operation = "Multiply";
            Price = Double.Parse(DisplayText);
            IntermediateText = Price + " *";
            HasDecimal = false;
        }

        public void Subtract(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "" || DisplayText == ".")
                return;

            OperationClicked = true;
            Operation = "Subtract";
            Price = Double.Parse(DisplayText);
            IntermediateText = Price + " -";
            HasDecimal = false;
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "" || DisplayText == ".")
                return;

            OperationClicked = true;
            Operation = "Add";
            Price = Double.Parse(DisplayText);
            IntermediateText = Price + " +";
            HasDecimal = false;
        }

        public void Calculate(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";

            if (DisplayText == "")
                return;

            OperationClicked = false;
            switch (Operation)
            {
                case "Add":
                    DisplayText = Math.Round((Price + Double.Parse(DisplayText)), 3).ToString();
                    break;
                case "Subtract":
                    DisplayText = Math.Round((Price - Double.Parse(DisplayText)), 3).ToString();
                    break;
                case "Multiply":
                    DisplayText = Math.Round((Price * Double.Parse(DisplayText)), 3).ToString();
                    break;
                case "Divide":
                    DisplayText = Math.Round((Price / Double.Parse(DisplayText)), 3).ToString();
                    break;
                default: //No other options
                    break;
            }
            Calculated = true;
            HasDecimal = false;
        }

        public void Decimal(object sender, RoutedEventArgs e)
        {
            if ((DisplayText == "0") || (OperationClicked) || (Calculated))
                DisplayText = "";
            if (HasDecimal == false)
            {
                Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
                DisplayText += b.Content;
                OperationClicked = false;
                Calculated = false;
                HasDecimal = true;
            }
            else
                return;
        }

        public void WebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == ".")
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.NonfoodPercentage / 100)).ToString();
        }

        public double RoundToNine(double value)
        {
            double roundedValue = Math.Round(value, 1);
            return roundedValue - 0.01;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
