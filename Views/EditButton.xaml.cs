using AddInCalculator2._0.Models.AddInCalculator;
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
    public sealed partial class EditButton : Page
    {
        public EditButton()
        {
            this.InitializeComponent();
            DataContext = ButtonSettingsViewModel.BManager;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Delete button first
            ButtonSettingsViewModel.BManager.DeleteButton();

            //Add new button based on user-inputted info
            ButtonSettingsViewModel.BManager.AddButton(tbEditRetailer.Text, tbEditLabel.Text, tbEditAbbrev.Text, tbEditPercentage.Text, cbEditType.SelectedItem.ToString());
            tbEditRetailer.Text = String.Empty;
            tbEditLabel.Text = String.Empty;
            tbEditAbbrev.Text = String.Empty;
            tbEditPercentage.Text = String.Empty;
            cbEditType.SelectedIndex = -1;
        }
    }
}
