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
    /// <summary>
    /// Provides the necessary methods and fields to support the Retailer class's  the UI calculator
    /// </summary>
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
        private bool addCommandBarClicked = false;
        private bool editCommandBarClicked = false;
        private bool sortedByName = false;
        private bool sortedByAbbrev = false;
        private bool sortedByFood = false;
        private bool sortedByNonfood = false;
        private bool sortedByNonfoodDf = false;
        private bool sortedByFreezer= false;
        private bool sortedByCooler = false;

        /// <summary>
        /// The Retailer property represents any grocery retailer, such as Walmart
        /// </summary>
        /// <value>The Retailer property gets/sets the value of the private field retailer</value>
        public Retailer Retailer
        {
            get { return retailer; }
            set 
            { 
                retailer = value;
                OnPropertyChanged("Retailer");
            }
        }

        /// <summary>
        /// The NewRetailer property represents any new grocery retailer to be added, such as Walmart
        /// </summary>
        /// <value>The NewRetailer property gets/sets the value of the private field newRetailer</value>
        public Retailer NewRetailer
        {
            get { return newRetailer; }
            set 
            { 
                newRetailer = value;
                OnPropertyChanged("NewRetailer");
            }
        }

        /// <summary>
        /// The Retailers property represents all the retailers available for the users to interact with
        /// </summary>
        /// <value>The Retailers property gets/sets the value of the private field retailers</value>
        public ObservableCollection<Retailer> Retailers
        {
            get { return retailers; }
            set { retailers = value; }
        }

        /// <summary>
        /// The AddCommandBarClicked property is true if the user clicks the '+' in the retailer settings
        /// </summary>
        /// <value>The AddCommandBarClicked property gets/sets the value of the private field addCommandBarClicked</value>
        public bool AddCommandBarClicked
        {
            get { return addCommandBarClicked; }
            set 
            { 
                addCommandBarClicked = value;
                OnPropertyChanged("AddCommandBarClicked");
            }
        }

        /// <summary>
        /// The EditCommandBarClicked property is true if the user clicks the pencil (edit) icon in the retailer settings
        /// </summary>
        /// <value>The EditCommandBarClicked property gets/sets the value of the private field editCommandBarClicked</value>
        public bool EditCommandBarClicked
        {
            get { return editCommandBarClicked; }
            set
            {
                editCommandBarClicked = value;
                OnPropertyChanged("EditCommandBarClicked");
            }
        }

        /// <summary>
        /// Handles the event that a user clicks on the '+' icon in the retailer settings.
        /// </summary>
        /// <param name="sender">The '+' button in the retailer settings</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the event that a user clicks on the pencil (edit) icon in the retailer settings.
        /// </summary>
        /// <param name="sender">The pencil/edit button in the retailer settings</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Adds a new retailer to the database and updates list of retailers
        /// </summary>
        public void AddRetailer()
        {
            // add retailer to database
            Handlers.Database db = new Handlers.Database();
            //db.WriteRecord<Retailer>(NewRetailer, table, db.BuildFieldObject("nvarchar", fieldname));
            db.AddRetailer(NewRetailer);

            UpdateRetailers();
            ClearNewRetailer();
        }

        /// <summary>
        /// Deletes a retailer from the database and updates the list of retailers
        /// </summary>
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
            Retailers = db.LoadAllRetailers();

            SortRetailersByName();
            sortedByName = false;
        }
        public void UpdateRetailers()
        {
            Handlers.Database db = new Handlers.Database();
            var retailerList = db.LoadAllRetailers();

            Retailers.Clear();
            foreach (var retailer in retailerList)
            {
                Retailers.Add(retailer);
            }

            sortedByName = false;
            SortRetailersByName();
            sortedByName = false;
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

        public void SortRetailersByName()
        {
            if (sortedByName == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.Name));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByName = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.Name));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByName = false;
            }        
        }

        public void SortRetailersByAbbreviation()
        {
            if (sortedByAbbrev == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.OnlineAbbrev));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByAbbrev = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.OnlineAbbrev));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByAbbrev = false;
            } 
        }

        public void SortRetailersByFood()
        {
            if (sortedByFood == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.FoodPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByFood = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.FoodPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByFood = false;
            }  
        }

        public void SortRetailersByNonfood()
        {
            if (sortedByNonfood == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.NonfoodPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByNonfood = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.NonfoodPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByNonfood = false;
            }
        }

        public void SortRetailersByNonfoodDf()
        {
            if (sortedByNonfoodDf == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.NonfoodDfPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByNonfoodDf = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.NonfoodDfPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByNonfoodDf = false;
            }
        }

        public void SortRetailersByFreezer()
        {
            if (sortedByFreezer == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.FreezerPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByFreezer = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.FreezerPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByFreezer = false;
            }
        }

        public void SortRetailersByCooler()
        {
            if (sortedByCooler == false)
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderBy(Retailer => Retailer.CoolerPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByCooler = true;
            }
            else
            {
                ObservableCollection<Retailer> tmp = new ObservableCollection<Retailer>(Retailers.OrderByDescending(Retailer => Retailer.CoolerPercentage));
                Retailers.Clear();
                foreach (Retailer i in tmp)
                    Retailers.Add(i);
                sortedByCooler = false;
            }
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
