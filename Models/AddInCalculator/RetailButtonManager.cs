using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddInCalculator2._0.Handlers;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButtonManager : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public RetailButtonManager()
        {
            UpdateRetailButtons();
        }
        private string name = "";
        private string onlineAbbrev = "";
        private double foodPercentage = 0;
        private double nonfoodPercentage = 0;
        private double nonfoodDfPercentage = 0;
        private double freezerPercentage = 0;
        private double coolerPercentage = 0;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set
            {
                onlineAbbrev = value;
                OnPropertyChanged("OnlineAbbrev");
            }
        }
        public double FoodPercentage
        {
            get { return foodPercentage; }
            set
            {
                foodPercentage = value;
                OnPropertyChanged("FoodPercentage");
            }
        }
        public double NonfoodPercentage
        {
            get { return nonfoodPercentage; }
            set
            {
                nonfoodPercentage = value;
                OnPropertyChanged("NonfoodPercentage");
            }
        }
        public double NonfoodDfPercentage
        {
            get { return nonfoodDfPercentage; }
            set
            {
                nonfoodDfPercentage = value;
                OnPropertyChanged("NonfoodDfPercentage");
            }
        }
        public double FreezerPercentage
        {
            get { return freezerPercentage; }
            set
            {
                freezerPercentage = value;
                OnPropertyChanged("FreezerPercentage");
            }
        }
        public double CoolerPercentage
        {
            get { return coolerPercentage; }
            set
            {
                coolerPercentage = value;
                OnPropertyChanged("CoolerPercentage");
            }
        }
        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();
        private string table = "Retailers";
        private string fieldname = "Retailer";
        private string objectPath = "AddInCalculator2._0.Models.AddInCalculator.Retailer";
        Type obType = (typeof(Retailer));

        public ObservableCollection<RetailButton> RetailButtons
        {
            get { return retailButtons; }
            set { retailButtons = value; }
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

            RetailButtons.Clear();

            foreach (Retailer item in retailerList)
            {
                RetailButton newButton = new RetailButton();
                newButton.Retailer = item;
                newButton.Visibility = true;
                RetailButtons.Add(newButton);
            }
        }
        public void AddRetailer(string name, string abbreviation, string foodPercentage, string nonfoodPercentage,
            string nonfoodDfPercentage, string freezerPercentage, string coolerPercentage)
        {
            // add Retailer object to database
            Retailer retailer = new Retailer()
            {
                Name = name,
                OnlineAbbrev = abbreviation,
                FoodPercentage = double.Parse(foodPercentage),
                NonfoodPercentage = double.Parse(nonfoodPercentage),
                NonfoodDfPercentage = double.Parse(nonfoodDfPercentage),
                FreezerPercentage = double.Parse(freezerPercentage),
                CoolerPercentage = double.Parse(coolerPercentage)
            };
            
            // write to database
            Handlers.Database db = new Handlers.Database();
            db.WriteRecord<Retailer>(retailer, table, db.BuildFieldObject("nvarchar", fieldname));

            UpdateRetailButtons();
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
            Retailer retailer = new Retailer();
            retailer.Name = Name;
            retailer.OnlineAbbrev = OnlineAbbrev;
            retailer.FoodPercentage = FoodPercentage;
            retailer.NonfoodPercentage = NonfoodPercentage;
            retailer.NonfoodDfPercentage = NonfoodDfPercentage;
            retailer.FreezerPercentage = FreezerPercentage;
            retailer.CoolerPercentage = CoolerPercentage;
            Handlers.Database db = new Handlers.Database();
            db.DeleteRetailer(table, retailer);

            UpdateRetailButtons();
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
