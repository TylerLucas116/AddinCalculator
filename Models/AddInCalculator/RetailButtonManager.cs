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
    public class RetailButtonManager
    {
        public RetailButtonManager()
        {
            InitializeRetailButtons();
            UpdateRetailButtons();
        }

        private string table = "Retailers";
        private string fieldname = "Retailer";
        private string objectPath = "AddInCalculator2._0.Models.AddInCalculator.Retailer";
        Type obType = (typeof(Retailer));

        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();

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
            var retailerList = db.GetAllRetailers();

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

        public void SortByName(List<Retailer> retailList)
        {
            retailList.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
        }
    }
}
