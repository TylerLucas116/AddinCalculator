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
using AddInCalculator2._0.Models.AddInCalculator;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RetailButtonSettings : Page
    {
        public RetailButtonSettings()
        {
            this.InitializeComponent();
            RetailButtonSettingsViewModel btnViewModel = new RetailButtonSettingsViewModel();
            Retailers.ItemsSource = RetailButtonSettingsViewModel.ButtonManager.RetailButtons;
            EditFrame.Navigate(typeof(BlankPage));
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditFrame.CurrentSourcePageType == typeof(AddRetailer) || (EditFrame.CurrentSourcePageType == typeof(EditRetailer)))
            {
                if (EditFrame.CanGoBack)
                {
                    EditFrame.GoBack();
                }
            }
            else
            {
                EditFrame.Navigate(typeof(AddRetailer));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RetailButtonSettingsViewModel.ButtonManager.DeleteRetailer();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditFrame.CurrentSourcePageType == typeof(AddRetailer) || (EditFrame.CurrentSourcePageType == typeof(EditRetailer)))
            {
                if (EditFrame.CanGoBack)
                {
                    EditFrame.GoBack();
                }
            }
            else
            {
                EditFrame.Navigate(typeof(EditRetailer));
            }
        }

        private void Retailers_OnSelectionChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var item in e.AddedItems)
                {
                    RetailButton selectedRetailer = (RetailButton)item;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.Name = selectedRetailer.Retailer.Name;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.OnlineAbbrev = selectedRetailer.Retailer.OnlineAbbrev;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.FoodPercentage = selectedRetailer.Retailer.FoodPercentage;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.NonfoodPercentage = selectedRetailer.Retailer.NonfoodPercentage;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.NonfoodDfPercentage = selectedRetailer.Retailer.NonfoodDfPercentage;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.FreezerPercentage = selectedRetailer.Retailer.FreezerPercentage;
                    RetailButtonSettingsViewModel.ButtonManager.Retailer.CoolerPercentage = selectedRetailer.Retailer.CoolerPercentage;

                    if (EditFrame.CurrentSourcePageType == typeof(EditButton))
                    {
                        // Views.EditButton.tbEditRetailer.Text = ButtonSettingsViewModel.BManager.Retailer;
                    }
                }
            }
        }
    }
}
