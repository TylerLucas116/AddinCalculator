using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailerManager: INotifyPropertyChanged
    {
        public RetailerManager()
        {
            retailer = new Retailer();
            newRetailer = new Retailer();
            LoadRetailers();
        }

        private Retailer retailer;
        private Retailer newRetailer;
        private ObservableCollection<Retailer> retailers = new ObservableCollection<Retailer>();

        private string table = "Retailers";
        private string fieldname = "Retailer";
        private string objectPath = "AddInCalculator2._0.Models.AddInCalculator.Retailer";
        Type obType = (typeof(Retailer));

        private bool addCommandBarClicked = false;
        private bool editCommandBarClicked = false;

        public Retailer Retailer
        {
            get { return retailer; }
            set { retailer = value; }
        }
        public Retailer NewRetailer
        {
            get { return newRetailer; }
            set 
            { 
                newRetailer = value;
                OnPropertyChanged("NewRetailer");
            }
        }
        public ObservableCollection<Retailer> Retailers
        {
            get { return retailers; }
            set { retailers = value; }
        }

        public bool AddCommandBarClicked
        {
            get { return addCommandBarClicked; }
            set 
            { 
                addCommandBarClicked = value;
                OnPropertyChanged("AddCommandBarClicked");
            }
        }

        public bool EditCommandBarClicked
        {
            get { return editCommandBarClicked; }
            set
            {
                editCommandBarClicked = value;
                OnPropertyChanged("EditCommandBarClicked");
            }
        }
        public void AddRetailerClicked(object sender, RoutedEventArgs e)
        {
            if (AddCommandBarClicked == true)
                AddCommandBarClicked = false;
            else if (AddCommandBarClicked == false)
            {
                EditCommandBarClicked = false;
                AddCommandBarClicked = true;
            }
        }

        public void EditRetailerClicked(object sender, RoutedEventArgs e)
        {
            if (EditCommandBarClicked == true)
                EditCommandBarClicked = false;
            else if (EditCommandBarClicked == false)
            {
                AddCommandBarClicked = false;
                EditCommandBarClicked = true;
            }
        }

        public void AddRetailer()
        {
            Debug.WriteLine("New Retailer");
            Debug.WriteLine(NewRetailer.Name);
            Debug.WriteLine(NewRetailer.OnlineAbbrev);
            Debug.WriteLine(NewRetailer.NonfoodPercentage);
            Debug.WriteLine("Retailer");
            Debug.WriteLine(Retailer.Name);
            Debug.WriteLine(Retailer.OnlineAbbrev);
            Debug.WriteLine(Retailer.NonfoodPercentage);
            // add retailer to database
            Handlers.Database db = new Handlers.Database();
            db.WriteRecord<Retailer>(NewRetailer, table, db.BuildFieldObject("nvarchar", fieldname));

            UpdateRetailers();
        }
        public void DeleteRetailer()
        {
            // delete retailer from database
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(table, Retailer);

            UpdateRetailers();
        }
        public void EditRetailer()
        {
            // delete previous retailer
            DeleteRetailer();

            // add new retailer
            AddRetailer();

            //ClearNewRetailer();
        }

        public void LoadRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.ReturnAllRetailers<Retailer>(table, fieldname, objectPath);

            SortByName(retailerList);

            foreach (var retailer in retailerList)
            {
                Retailers.Add(retailer);
            }
        }
        public void UpdateRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.ReturnAllRetailers<Retailer>(table, fieldname, objectPath);

            SortByName(retailerList);

            Retailers.Clear();
            foreach (var retailer in retailerList)
            {
                Retailers.Add(retailer);
            }
        }

        public void RetailerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var item in e.AddedItems)
                {
                    Retailer = (Retailer)item;
                }
            }
        }

        public void SortByName(List<Retailer> retailList)
        {
            retailList.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        }

        private void ClearNewRetailer()
        {
            NewRetailer.Name = "";
            NewRetailer.OnlineAbbrev = "";
            NewRetailer.FoodPercentage = 0;
            NewRetailer.NonfoodPercentage = 0;
            NewRetailer.NonfoodDfPercentage = 0;
            NewRetailer.FreezerPercentage = 0;
            NewRetailer.CoolerPercentage = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
