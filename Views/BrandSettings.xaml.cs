using AddInCalculator2._0.Models.PetPrices;
using AddInCalculator2._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public sealed partial class BrandSettings : Page
    {
        public BrandSettings()
        {
            this.InitializeComponent();

            BrandList.ItemsSource = PetViewModel.Manager.petBrands;
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
            EditFrame.Navigate(typeof(AddBrand));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            BrandList.ItemsSource = PetViewModel.Manager.petBrands;

            PetViewModel.Manager.DeletePetBrand();
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-K99E9GR\SQLEXPRESS;Initial Catalog=SommersMarket;Integrated Security=SSPI;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"Delete From Brands where BrandID =" + PetViewModel.Manager.DeleteBrandID + ";", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void BrandList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var item in e.AddedItems)
                {
                    PetBrands brand = (PetBrands)item;
                    PetViewModel.Manager.DeleteBrandID = Convert.ToInt32(brand.BrandID);
                    PetViewModel.Manager.DeleteBrandClass = Convert.ToInt32(brand.BrandClass);
                    PetViewModel.Manager.DeleteBrandName = Convert.ToString(brand.BrandName);
                }
            }
        }

        private void IDButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
