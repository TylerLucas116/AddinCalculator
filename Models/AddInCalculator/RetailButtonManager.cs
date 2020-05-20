using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using AddInCalculator2._0.Handlers;
using AddInCalculator2._0.ViewModels;
using AddInCalculator2._0.Views;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButtonManager : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public RetailButtonManager()
        {
            InitializeRetailButtons();
            UpdateRetailButtons();
            UpdateRetailers();
            retailer = new Retailer();
        }

        private Retailer retailer;

        public Retailer Retailer
        {
            get { return retailer; }
            set { retailer = value; }
        }

        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();
        private ObservableCollection<Retailer> retailers = new ObservableCollection<Retailer>();
        private string table = "Retailers";
        private string fieldname = "Retailer";
        private string objectPath = "AddInCalculator2._0.Models.AddInCalculator.Retailer";
        Type obType = (typeof(Retailer));

        public ObservableCollection<RetailButton> RetailButtons
        {
            get { return retailButtons; }
            set { retailButtons = value; }
        }
        public ObservableCollection<Retailer> Retailers
        {
            get { return retailers; }
            set { retailers = value; }
        }

        public void InitializeRetailButtons() //Call at beginning of program once
        {
            RetailButton newButton = new RetailButton();//Blank button to initialize each collection

            for (int i = 0; i < 50; ++i) // 50 button limit for UI
            {
                RetailButtons.Add(newButton);
            }
        }

        public void UpdateRetailButtons()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.ReturnAllRetailers<Retailer>(table, fieldname, objectPath);

            SortByName(retailerList);

            int i = 0;
            foreach (Retailer item in retailerList)
            {
                RetailButton newButton = new RetailButton();
                newButton.Retailer = item;
                newButton.Visibility = true;
                RetailButtons.RemoveAt(i);
                RetailButtons.Insert(i, newButton);
                ++i;
            }
        }

        public void AddRetailer(Retailer retailer)
        {
            // add Retailer object to database
            Handlers.Database db = new Handlers.Database();
            db.WriteRecord<Retailer>(retailer, table, db.BuildFieldObject("nvarchar", fieldname));

            UpdateRetailButtons();
        }
        public void DeleteRetailer()
        {
            //delete button from database
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(table, Retailer);

            UpdateRetailButtons();
        }

        public void UpdateRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.ReturnAllRetailers<Retailer>(table, fieldname, objectPath);

            SortByName(retailerList);

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
                    Retailer = (Retailer) item;
                }
            }
        }

        public void SortByName(List<Retailer> retailList)
        {
            retailList.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }
    }
}
