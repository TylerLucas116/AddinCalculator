using AddInCalculator2._0.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddButton : Page
    {
        public AddButton()
        {
            this.InitializeComponent();
            this.DataContext = ButtonSettingsViewModel.BManager;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonSettingsViewModel.BManager.AddButton(tbNewRetailer.Text, tbNewLabel.Text, tbNewAbbrev.Text, tbNewPercentage.Text, cbNewType.SelectedItem.ToString());
            tbNewRetailer.Text = String.Empty;
            tbNewLabel.Text = String.Empty;
            tbNewAbbrev.Text = String.Empty;
            tbNewPercentage.Text = String.Empty;
            cbNewType.SelectedIndex = -1;
        }

        private Models.AddInCalculator.Button buildButtonObject()
        {
            var myButton = new Models.AddInCalculator.Button();

            /*  public int ID { get; set; } //start at 1, 2, 3
                public string retailer { get; set; }
                public string label { get; set; } //Walmart, targer
                public string abbrev { get; set; } //wm, tg, etc (for pasting)
                public decimal percentage { get; set; } //75, 50, etc
                public string type { get; set; } //food,*/
            myButton.retailer = tbNewRetailer.Text;
            myButton.label = tbNewLabel.Text;
            myButton.abbrev = tbNewAbbrev.Text;
            myButton.percentage = Double.Parse(tbNewPercentage.Text);
            myButton.type = cbNewType.SelectedItem.ToString().ToLower();

            return myButton;
        }
    }
}
