using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class ButtonManager : INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Declarations

        private ObservableCollection<Button> NFCollection = new ObservableCollection<Button>();
        public ObservableCollection<Button> nfCollection
        {
            get
            {
                return NFCollection;
            }
            set
            {
                NFCollection = value;
            }
        }

        public ObservableCollection<Button> allbuttons = new ObservableCollection<Button>();

        public void updateButtonList(List<Button> nfButtonList, Button[] nfButtons)
        {
            int buttonCount = nfButtonList.Count() - 1; //For array indexing
            for (int i = 0; (i < buttonCount) && (i < 50); ++i)
            {
                nfCollection.Add(nfButtons[i]);
            }
        }

        public void InitializeCollections() //Call at beginning of program once
        {
            Button newButton = new Button
            {
                retailer = "",
                label = "",
                abbrev = "",
                percentage = 0,
                type = "",
                visibility = false
            }; //Blank button to initialize each collection

            for(int i = 0; i < 50; ++i)
            {
                nfCollection.Add(newButton);
            }
        }

        public void UpdateNFButtons()
        {
            var allButtons2 = new List<Button>();
            Handlers.Database db = new Handlers.Database();
            allButtons2 = db.ReturnAllRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath);

            SortByRetailer(allButtons2);

            int i = 0;
            foreach(Button item in allButtons2)
            {
                if(item.type == "Nonfood")
                {
                    nfCollection.RemoveAt(i);
                    nfCollection.Insert(i, item);
                    ++i;
                }
            }
        }

        //Set all UI Element button properties in NFCalculator to my user defined Button Class properties(Content/Visiblity so far)


        String table = "ButtonInfo";
        String fieldname = "Button";
        String ObjectPath = "AddInCalculator2._0.Models.AddInCalculator.Button";
        Type obType = (typeof(Models.AddInCalculator.Button));

        private string retailer { get; set; }
        public string Retailer
        {
            get
            {
                return retailer;
            }
            set
            {
                retailer = value;
                OnPropertyChanged("Retailer");
            }
        }

        private string label { get; set; }
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnPropertyChanged("Label");
            }
        }

        private string abbrev { get; set; }
        public string Abbrev
        {
            get
            {
                return abbrev;
            }
            set
            {
                abbrev = value;
                OnPropertyChanged("Abbrev");
            }
        }

        private double percentage { get; set; }
        public double Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value;
                OnPropertyChanged("Percentage");
            }
        }

        private string type { get; set; }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged(Type);
            }
        }

        #endregion

        #region Database
        #region Read From Database
 
        #region Search Records
        private void SearchRecords(String FieldToSearch, String SearchText)
        {
            //var myButtonList = new List<Models.AddInCalculator.Button>();

            Handlers.Database db = new Handlers.Database();
                                                                                                        //type          nonfood
            var myButton = db.SearchRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath, FieldToSearch, SearchText);
        }

        #endregion
        #region Update Buttons
        public void UpdateButtons()
        {
            var allButtons2 = new List<Button>();

            Handlers.Database db = new Handlers.Database();

            allButtons2 = db.ReturnAllRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath);

            allbuttons.Clear();

            foreach (var button in allButtons2) //Add each button to it's perspective observablecollection
            {
                allbuttons.Add(button);
            }
        }
        #endregion
        #endregion
        #region Write Button to Database
        public void AddButton(string newRetailer, string newLabel, string newAbbrev, string newPercentage, string newType)
        {
            //Add to database
            Models.AddInCalculator.Button newButton = new Models.AddInCalculator.Button();
            newButton.retailer = newRetailer;
            newButton.label = newLabel;
            newButton.abbrev = newAbbrev;
            newButton.percentage = Double.Parse(newPercentage);
            newButton.type = newType;
            newButton.visibility = true;
            Handlers.Database db = new Handlers.Database();
            db.WriteRecord<Models.AddInCalculator.Button>(newButton, table, db.BuildFieldObject("nvarchar", fieldname));

            UpdateButtons();
        }
        #endregion
        #region Delete Button from Database
        public void DeleteButton()
        {
            //delete button from database
            Button button = new Button();
            button.retailer = retailer;
            button.label = label;
            button.abbrev = abbrev;
            button.percentage = percentage;
            button.type = type;
            button.visibility = true;
            Handlers.Database db = new Handlers.Database();
            db.DeleteButton(table, button);

            UpdateButtons();
        }
        #endregion
        #endregion

        #region Observable Objects
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
        #endregion

        #region Internal Functions
        public void SortByRetailer(List<Button> buttonList)
        {
            buttonList.Sort((x, y) => x.retailer.CompareTo(y.retailer));
        }

        void SortByLabel(List<Button> buttonList)
        {
            buttonList.Sort((x, y) => x.label.CompareTo(y.label));
        }
        void SortByType(List<Button> buttonList)
        {
            buttonList.Sort((x, y) => x.type.CompareTo(y.type));
        }
        
        void SortByAbbrev(List<Button> buttonList)
        {
            buttonList.Sort((x, y) => x.abbrev.CompareTo(y.abbrev));
        }

        void SortByPercentage(List<Button> buttonList)
        {
            buttonList.Sort((x, y) => x.percentage.CompareTo(y.percentage));
        }

        #endregion
    }
}
