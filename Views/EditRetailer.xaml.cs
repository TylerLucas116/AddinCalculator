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
using AddInCalculator2._0.Models.AddInCalculator;
using AddInCalculator2._0.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditRetailer : Page
    {
        public EditRetailer()
        {
            this.InitializeComponent();
            this.DataContext = RetailButtonSettingsViewModel.ButtonManager;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // delete retailer first
            RetailButtonSettingsViewModel.ButtonManager.DeleteRetailer();

            // add new retailer based on UI fields
            RetailButtonSettingsViewModel.ButtonManager.AddRetailer(BuildRetailer());
            tbRetailer.Text = String.Empty;
            tbAbbreviation.Text = String.Empty;
            tbFoodPercentage.Text = String.Empty;
            tbNonfoodPercentage.Text = String.Empty;
            tbNonfoodDfPercentage.Text = String.Empty;
            tbFreezerPercentage.Text = String.Empty;
            tbCoolerPercentage.Text = String.Empty;
        }

        private Retailer BuildRetailer()
        {
            var newRetailer = new Retailer()
            {
                Name = tbRetailer.Text,
                OnlineAbbrev = tbAbbreviation.Text,
                FoodPercentage = double.Parse(tbFoodPercentage.Text),
                NonfoodPercentage = double.Parse(tbNonfoodPercentage.Text),
                NonfoodDfPercentage = double.Parse(tbNonfoodDfPercentage.Text),
                FreezerPercentage = double.Parse(tbFreezerPercentage.Text),
                CoolerPercentage = double.Parse(tbCoolerPercentage.Text)
            };

            return newRetailer;
        }
    }
}
