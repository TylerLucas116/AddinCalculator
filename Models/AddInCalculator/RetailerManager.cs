using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailerManager
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

        public Retailer Retailer
        {
            get { return retailer; }
            set { retailer = value; }
        }
        public Retailer NewRetailer
        {
            get { return newRetailer; }
            set { newRetailer = value; }
        }
        public ObservableCollection<Retailer> Retailers
        {
            get { return retailers; }
            set { retailers = value; }
        }

        public void AddRetailer(object sender, RoutedEventArgs e)
        {
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
        public void EditRetailer(object sender, RoutedEventArgs e)
        {
            // delete previous retailer
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(table, Retailer);
            UpdateRetailers();

            // add new retailer
            db.WriteRecord<Retailer>(NewRetailer, table, db.BuildFieldObject("nvarchar", fieldname));

            ClearNewRetailer();
            UpdateRetailers();
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
            NewRetailer.FoodPercentage = 0;
            NewRetailer.NonfoodPercentage = 0;
            NewRetailer.NonfoodDfPercentage = 0;
            NewRetailer.FreezerPercentage = 0;
            NewRetailer.CoolerPercentage = 0;
        }
    }
}
