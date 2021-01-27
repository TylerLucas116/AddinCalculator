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
        private BindingList<Retailer> retailerList = new BindingList<Retailer>();
        private bool addCommandBarClicked = false;
        private bool editCommandBarClicked = false;

        public Retailer Retailer
        {
            get { return retailer; }
            set 
            { 
                retailer = value;
                OnPropertyChanged("Retailer");
            }
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

        public BindingList<Retailer> RetailerList
        {
            get { return retailerList; }
            set { retailerList = value; }
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
            // add retailer to database
            Handlers.Database db = new Handlers.Database();
            //db.WriteRecord<Retailer>(NewRetailer, table, db.BuildFieldObject("nvarchar", fieldname));
            db.AddRetailer(NewRetailer);

            UpdateRetailers();
            ClearNewRetailer();
        }
        public void DeleteRetailer()
        {
            // delete retailer from database
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(Retailer);

            UpdateRetailers();
        }
        public void EditRetailer()
        {
            Handlers.Database db = new Handlers.Database();
            db.UpdateRetailer(NewRetailer);

            UpdateRetailers();
        }

        public void LoadRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.LoadAllRetailers();

            SortByName(retailerList);

            foreach (var retailer in retailerList)
            {
                Retailers.Add(retailer);
            }
        }
        public void UpdateRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.LoadAllRetailers();

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
            Handlers.Database db = new Handlers.Database();
            NewRetailer = db.LoadRetailer(Retailer);
        }

        public void SortByName(List<Retailer> retailList)
        {
            retailList.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        }

        public void SortByRetailer()
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.Name));
            Retailers.Clear();
            foreach (Retailer i in tmp)
                Retailers.Add(i);
        }

        public void SortByAbbreviation()
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.OnlineAbbrev));
            Retailers.Clear();
            foreach (Retailer i in tmp)
                Retailers.Add(i);
        }

        public void SortByFood()
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.FoodPercentage));
            Retailers.Clear();
            foreach (Retailer i in tmp)
                Retailers.Add(i);
        }

        public void SortByNonfood()
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.NonfoodPercentage));
            Retailers.Clear();
            foreach (Retailer i in tmp)
                Retailers.Add(i);
        }

        public void SortByNonfoodDf()
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.NonfoodDfPercentage));
            Retailers.Clear();
            foreach (Retailer i in tmp)
                Retailers.Add(i);
        }

        public void SortByFreezer()
        {

        }

        public void SortByCooler()
        {

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
