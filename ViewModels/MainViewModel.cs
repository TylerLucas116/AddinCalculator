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
       /* private static BoolToVisibilityConverter boolConverter;
        public static BoolToVisibilityConverter BoolConverter { get { return boolConverter; } }

        public MainViewModel()
        {
            boolConverter = new BoolToVisibilityConverter();
        }*/

        private bool ListsClicked = false;
        private bool AddinCalculatorClicked = false;

        public bool listsClicked
        {
            get
            {
                return ListsClicked;
            }
            set
            {
                ListsClicked = value;
                OnPropertyChanged("listsClicked");
            }
        }
        public bool addinCalculatorClicked
        {
            get
            {
                return AddinCalculatorClicked;
            }
            set
            {
                AddinCalculatorClicked = value;
                OnPropertyChanged("addinCalculatorClicked");
            }
        }

        /*public ICommand OkButtonClicked
        {
            get
            {
                return new DelegateCommand(FindResult);
            }
        }
        public void FindResult()
        {
            Calculator = new Calculator(Value1, Value2);
        }*/

        public void selectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigateTo(typeof(Settings));
                addinCalculatorClicked = false;
                listsClicked = false;
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "Home":
                        NavigateTo(typeof(Home));
                        addinCalculatorClicked = false;
                        listsClicked = false;
                        break;
                    case "Calculator":
                        NavigateTo(typeof(NewCalculator));
                        listsClicked = false;
                        addinCalculatorClicked = true;
                        break;
                    case "TestCalculator":
                        NavigateTo(typeof(TestCalculator));
                        listsClicked = false;
                        addinCalculatorClicked = false;
                        break;
                }
            }
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool))
            {
                return Visibility.Collapsed;
            }

            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
            }
            catch { }
            return Visibility.Collapsed;
        }

        /* ****************************Database demo from Udemy***************************************
        //create
        private async Task<SQLiteConnection> OpenOrRecreateConnection(bool ReCreate = false)
        {
            var filename = "buttons.sqlite";
            var folder = ApplicationData.Current.LocalFolder;

            if(ReCreate)
            {
                var file = await folder.TryGetItemAsync(filename);
                if(file != null)
                {
                    await file.DeleteAsync();
                }
            }
            var sqlpath = Path.Combine(folder.Path, filename);

            return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath);
        }

        //setup
        private async void buttonClick(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = await OpenOrRecreateConnection(true))
            {
                conn.CreateTable<ButtonInfo>();
                foreach (var info in ButtonInfo)
                    conn.InsertOrReplace(info);
            }
        }
        //open / read

        private async void buttonClick2(object sender, RoutedEventArgs e)
        {
            using (var conn = await OpenOrRecreateConnection())
            {
                var info = from p in conn.Table<ButtonInfo>() select p;
                var names = string.Join(", ", info.Select(t => t.Name));
                result.Text = names;
            }
        }
        */ 

    }
}
