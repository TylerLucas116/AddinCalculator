using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Lists : Page
    {
        public Lists()
        {
            this.InitializeComponent();
        }

        private async void RetrieveText_Click(object sender, RoutedEventArgs e)
        {
            var Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var File = await Folder.GetFileAsync("ProductInformation.txt");
            var Text = await FileIO.ReadTextAsync(File);
            //ResultTextBox.Text = Text;
        }

        private async void RetrieveImage_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            var stream = await file.OpenReadAsync();
            var image = new BitmapImage();
            image.SetSource(stream);
            //Pic.Source = image;
        }

    }
}
