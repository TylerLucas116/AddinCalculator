using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddInCalculator2._0.Commands;
using AddInCalculator2._0.Converters;
using AddInCalculator2._0.Navigation;
using AddInCalculator2._0.ViewModels.ViewModelBases;
using AddInCalculator2._0.Views;
using SQLite;
using SQLite.Net;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AddInCalculator2._0.ViewModels
{
    public class MainViewModel : MainViewModelBase 
    {
        private bool addinCalculatorClicked = false;

        public bool AddinCalculatorClicked
        {
            get
            {
                return addinCalculatorClicked;
            }
            set
            {
                addinCalculatorClicked = value;
                OnPropertyChanged("AddinCalculatorClicked");
            }
        }

        public void selectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigateTo(typeof(Settings));
                AddinCalculatorClicked = false;
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "Home":
                        NavigateTo(typeof(Home));
                        AddinCalculatorClicked = false;
                        break;
                    case "Calculator":
                        NavigateTo(typeof(NFCalculator));
                        AddinCalculatorClicked = true;
                        break;
                }
            }
        }

        public void calculatorCategoryClicked(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Controls.Button b = (Windows.UI.Xaml.Controls.Button)sender;

            if (b.Name == "Food")
                NavigateTo(typeof(FoodCalculator));
            else if (b.Name == "Nonfood")
                NavigateTo(typeof(NFCalculator));
            else if (b.Name == "NonfoodDF")
                NavigateTo(typeof(NonfoodDFCalculator));
            else if (b.Name == "Cooler")
                NavigateTo(typeof(CoolerCalculator));
            else if (b.Name == "Freezer")
                NavigateTo(typeof(FreezerCalculator));
        }
    }
}
