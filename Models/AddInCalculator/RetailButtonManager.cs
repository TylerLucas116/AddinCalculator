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
    /// <summary>
    /// Provides the necessary methods and fields to support the RetailButton class in the UI calculator
    /// </summary>
    public class RetailButtonManager
    {
        public RetailButtonManager()
        {
            InitializeRetailButtons();
        }

        private ObservableCollection<RetailButton> retailButtons = new ObservableCollection<RetailButton>();

        /// <summary>
        /// The RetailButtons property represents an observable collection for the front end UI calculator buttons
        /// </summary>
        /// <value>The RetailButtons property gets/sets the value of the private field retailButtons</value>
        public ObservableCollection<RetailButton> RetailButtons
        {
            get { return retailButtons; }
            set { retailButtons = value; }
        }

        /// <summary>
        /// Initializes the RetailButtons list which is bound to the 35 UI retail buttons
        /// in the calculators
        /// </summary>
        /// <seealso cref="RetailButtons"/>
        /// <seealso cref="UpdateRetailButtons()"/>
        public void InitializeRetailButtons() 
        {
            RetailButton newButton = new RetailButton();

            for (int i = 0; i < 35; ++i)
            {
                RetailButtons.Add(newButton);
            }

            UpdateRetailButtons();
        }

        /// <summary>
        /// Updates RetailButtons list with retailers from the database
        /// </summary>
        /// <seealso cref="RetailButtons"/>
        /// <seealso cref="SortByName(ObservableCollection{Retailer}))"/>
        public void UpdateRetailButtons()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.LoadAllRetailers();

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

        /// <summary>
        /// Sorts <paramref name="retailList"/> in ascending order by name
        /// </summary>
        /// <param name="retailList">A list of retailers</param>
        /// <returns> Sorted <paramref name="retailList"/> in ascending order by name</returns>
        public ObservableCollection<Retailer> SortByName(ObservableCollection<Retailer> retailList)
        {
            ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(retailList.OrderBy(Retailer => Retailer.Name));
            retailList.Clear();
            foreach (Retailer i in tmp)
                retailList.Add(i);
            return retailList;
        }
    }
}
