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
    public class Calculator
    {

        private bool operationClicked = new bool();
        private bool calculated = new bool();
        private bool hasDecimal = new bool();
        private string displayText;
        private string intermediateText;
        private string upc;
        private string textblockPrice;
        private string operation;

        #region Calculator Functions
        public string NumberClicked(object sender, RoutedEventArgs e, string displayText, bool operationClicked, bool calculated) //0-9
        {
            if (displayText == "0" || (operationClicked) || (calculated))
                displayText = "";

            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;
            displayText += b.Content;
            operationClicked = false;
            calculated = false;
            return displayText;
        }

        public void Clear(object sender, RoutedEventArgs e, double price, string displayText, string intermediateText, bool hasDecimal) //Clear button
        {
            price = 0;
            displayText = "0";
            intermediateText = "";
            hasDecimal = false;
        }

        public void ClearEntry(object sender, RoutedEventArgs e, double price, string displayText, string intermediateText, bool hasDecimal) //Clear entry button
        {
            price = 0;
            displayText = "0";
            intermediateText = "";
            hasDecimal = false;
        }

        public void Backspace(object sender, RoutedEventArgs e, string displayText, bool hasDecimal)
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

        public void Decimal(object sender, RoutedEventArgs e, string displayText, bool operationClicked, bool calculated, bool hasDecimal)
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

        public void Divide(object sender, RoutedEventArgs e, string displayText, bool operationClicked, string operation, double price, string intermediateText, bool hasDecimal)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Divide";
            price = Double.Parse(displayText);
            intermediateText = price + " /";
            hasDecimal = false;
        }

        public void Multiply(object sender, RoutedEventArgs e, string displayText, bool operationClicked, string operation, double price, string intermediateText, bool hasDecimal)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Multiply";
            price = Double.Parse(displayText);
            intermediateText = price + " *";
            hasDecimal = false;
        }

        public void Subtract(object sender, RoutedEventArgs e, string displayText, bool operationClicked, string operation, double price, string intermediateText, bool hasDecimal)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Subtract";
            price = Double.Parse(displayText);
            intermediateText = price + " -";
            hasDecimal = false;
        }

        public void Add(object sender, RoutedEventArgs e, string displayText, bool operationClicked, string operation, double price, string intermediateText, bool hasDecimal)
        {
            if (displayText == "" || displayText == ".")
                return;

            operationClicked = true;
            operation = "Add";
            price = Double.Parse(displayText);
            intermediateText = price + " +";
            hasDecimal = false;
        }

        public void Equals(object sender, RoutedEventArgs e, string intermediateText, string displayText, bool operationClicked, string operation, double price, bool calculated, bool hasDecimal)
        {
            intermediateText = "";

            if (displayText == "")
                return;

            operationClicked = false;
            switch (operation)
            {
                case "Add":
                    displayText = Math.Round((price + Double.Parse(displayText)), 3).ToString();
                    break;
                case "Subtract":
                    displayText = Math.Round((price - Double.Parse(displayText)), 3).ToString();
                    break;
                case "Multiply":
                    displayText = Math.Round((price * Double.Parse(displayText)), 3).ToString();
                    break;
                case "Divide":
                    displayText = Math.Round((price / Double.Parse(displayText)), 3).ToString();
                    break;
                default: //No other options
                    break;
            }
            calculated = true;
            hasDecimal = false;
        }
        #endregion
    }


}
