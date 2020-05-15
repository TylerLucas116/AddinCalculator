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
    public sealed partial class RetailButtonSettings : Page
    {
        public RetailButtonSettings()
        {
            this.InitializeComponent();
            ButtonSettingsViewModel btnViewModel = new ButtonSettingsViewModel();
            Retailers.ItemsSource = ButtonSettingsViewModel.BManager.allbuttons;
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
            if (EditFrame.CurrentSourcePageType == typeof(AddButton) || (EditFrame.CurrentSourcePageType == typeof(EditButton)))
            {
                if (EditFrame.CanGoBack)
                {
                    EditFrame.GoBack();
                }
            }
            else
            {
                EditFrame.Navigate(typeof(AddButton));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonSettingsViewModel.BManager.DeleteButton();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditFrame.CurrentSourcePageType == typeof(AddButton) || (EditFrame.CurrentSourcePageType == typeof(EditButton)))
            {
                if (EditFrame.CanGoBack)
                {
                    EditFrame.GoBack();
                }
            }
            else
            {
                EditFrame.Navigate(typeof(EditButton));
            }
        }

        private void Retailers_OnSelectionChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var item in e.AddedItems)
                {
                    Models.AddInCalculator.Button button = (Models.AddInCalculator.Button)item;
                    ButtonSettingsViewModel.BManager.Retailer = button.retailer;
                    ButtonSettingsViewModel.BManager.Abbrev = button.abbrev;
                    ButtonSettingsViewModel.BManager.Percentage = button.percentage;
                    ButtonSettingsViewModel.BManager.Type = button.type;

                    if (EditFrame.CurrentSourcePageType == typeof(EditButton))
                    {
                        // Views.EditButton.tbEditRetailer.Text = ButtonSettingsViewModel.BManager.Retailer;
                    }
                }
            }
        }
    }
}
