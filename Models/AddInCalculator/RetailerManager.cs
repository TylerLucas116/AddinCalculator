using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailerManager
    {
        public RetailerManager()
        {
            retailer = new Retailer();

        }
        private Retailer retailer;
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
        public ObservableCollection<Retailer> Retailers
        {
            get { return retailers; }
            set { retailers = value; }
        }

        public void AddRetailer(Retailer retailer)
        {
            // add Retailer object to database
            Handlers.Database db = new Handlers.Database();
            db.WriteRecord<Retailer>(retailer, table, db.BuildFieldObject("nvarchar", fieldname));

            UpdateRetailers();
        }
        public void DeleteRetailer()
        {
            //delete button from database
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(table, Retailer);

            UpdateRetailers();
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
                    Retailer = (Retailer)item;
                }
            }
        }

        public void SortByName(List<Retailer> retailList)
        {
            retailList.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        }
    }
}
