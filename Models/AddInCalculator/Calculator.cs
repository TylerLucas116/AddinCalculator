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
            retailButtonManager = new RetailButtonManager();
        }

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

        /// <summary>
        /// The RetailButtonManager property represents a RetailButtonManager class object  
        /// </summary>
        /// <value>The RetailButtonManager property gets/sets the value of the private field retailButtonManager</value>
        public RetailButtonManager RetailButtonManager
        {
            get { return retailButtonManager; }
            set { retailButtonManager = value; }
        }

        /// <summary>
        /// The Price property represents the price of a retail item
        /// </summary>
        /// <value>The Price property gets/sets the value of the private field price</value>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// The OperationClicked property represents a calculator operation such as +,-,/ that was clicked
        /// </summary>
        /// <value>The OperationClicked property gets/sets the value of the private field operationClicked</value>
        public bool OperationClicked
        {
            get { return operationClicked; }
            set { operationClicked = value; }
        }

        /// <summary>
        /// The Calculated property checks to see if a price has been calculated using the UI calculator
        /// </summary>
        /// <value>The Calculated property gets/sets the value of the private field calculated</value>
        public bool Calculated
        {
            get { return calculated; }
            set { calculated = value; }
        }

        /// <summary>
        /// The HasDecimal property checks to see if a decimal is present in the UI calculator
        /// </summary>
        /// <value>The HasDecimal property gets/sets the value of the private field hasDecimal</value>
        public bool HasDecimal
        {
            get { return hasDecimal; }
            set { hasDecimal = value; }
        }

        /// <summary>
        /// The DisplayText property represents the main text in the UI calculator
        /// </summary>
        /// <value>The DisplayText property gets/sets the value of the private field displayText</value>
        public string DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                OnPropertyChanged("DisplayText");
            }
        }

        /// <summary>
        /// The IntermediateText property represents the intermediate text in the UI calculator above the DisplayText
        /// </summary>
        /// <value>The IntermediateText property gets/sets the value of the private field intermediateText</value>
        public string IntermediateText
        {
            get { return intermediateText; }
            set
            {
                intermediateText = value;
                OnPropertyChanged("IntermediateText");
            }
        }

        /// <summary>
        /// The Operation property represents the actual operation clicked from the UI calculator such as +,-,/
        /// </summary>
        /// <value>The Operation property gets/sets the value of the private field operation</value>
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        /// <summary>
        /// NumberClicked is called whenever a number is clicked on the UI calculator.
        /// Updates <see cref="DisplayText"/> to include the number that was clicked, and sets <see cref="OperationClicked"/> and 
        /// <see cref="Calculated"/> to false.
        /// </summary>
        /// <param name="sender">The button that was pressed, such as 1, 2 , 3 etc.</param>
        /// <param name="e"></param>
        public void NumberClicked(object sender, RoutedEventArgs e)
        {
            if (DisplayText == "0" || (OperationClicked) || (Calculated))
                DisplayText = "";

            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            DisplayText += b.Content;
            OperationClicked = false;
            Calculated = false;
        }

        /// <summary>
        /// Clears all text in the UI calculator.
        /// </summary>
        /// <param name="sender">The CE or C button in the UI calculator</param>
        /// <param name="e"></param>
        public void ClearEntry(object sender, RoutedEventArgs e)
        {
            Price = 0;
            DisplayText = "0";
            IntermediateText = "";
            HasDecimal = false;
        }

        /// <summary>
        /// Clears all text in the UI calculator.
        /// </summary>
        /// <param name="sender">The CE or C button in the UI calculator</param>
        /// <param name="e"></param>
        public void Clear(object sender, RoutedEventArgs e)
        {
            Price = 0;
            DisplayText = "0";
            IntermediateText = "";
            HasDecimal = false;
        }

        /// <summary>
        /// Logically represents the user deleting a character in the UI calculator
        /// </summary>
        /// <param name="sender">The backspace button in the calculator</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Logically represents the user pressing the divide '/' button in the UI calculator
        /// </summary>
        /// <param name="sender">The '/' button in the UI calculator</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Logically represents the user pressing the divide '*' button in the UI calculator
        /// </summary>
        /// <param name="sender">The '*' button in the UI calculator</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Logically represents the user pressing the divide '-' button in the UI calculator
        /// </summary>
        /// <param name="sender">The '-' button in the UI calculator</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Logically represents the user pressing the divide '+' button in the UI calculator
        /// </summary>
        /// <param name="sender">The '+' button in the UI calculator</param>
        /// <param name="e"></param>
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

        public void NFWebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == "." || DisplayText == null)
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.NonfoodPercentage / 100)).ToString();
        }

        public void FoodWebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == "." || DisplayText == null)
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.FoodPercentage / 100)).ToString();
        }

        public void NFDFWebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == "." || DisplayText == null)
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.NonfoodDfPercentage / 100)).ToString();
        }

        public void FreezerWebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == "." || DisplayText == null)
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.FreezerPercentage / 100)).ToString();
        }

        public void CoolerWebsiteClick(object sender, RoutedEventArgs e)
        {
            IntermediateText = "";
            if (DisplayText == "" || DisplayText == "0" || DisplayText == "." || DisplayText == null)
                return;

            OperationClicked = true;
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            int index = Int32.Parse(b.Name.Substring(6)) - 1; //All buttons named - Button1, Button2 - Corresponding to index
            Price = Double.Parse(DisplayText);
            DisplayText = RoundToNine(Price * (RetailButtonManager.RetailButtons[index].Retailer.CoolerPercentage / 100)).ToString();
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
