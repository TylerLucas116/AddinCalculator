using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButtonManager : INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();
        private string table = "ButtonInfo";
        private string fieldname = "Button";
        private string objectPath = "AddInCalculator2._0.Models.AddInCalculator.Button";
        Type obType = (typeof(Models.AddInCalculator.Button));

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
                RetailButtons.Add(newButton);
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
