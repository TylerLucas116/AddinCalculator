using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    /// <summary>
    /// Contains all the necessary information a grocery retailer would have.
    /// </summary>
    public class Retailer : INotifyPropertyChanged
    {
        private int id = 0;
        private string name = "";
        private string onlineAbbrev = "";
        private Double foodPercentage = 0;
        private Double nonfoodPercentage = 0;
        private Double nonfoodDfPercentage = 0;
        private Double freezerPercentage = 0;
        private Double coolerPercentage = 0;

        /// <summary>
        /// The ID property represents a unique Retailer's id number
        /// </summary>
        /// <value>The ID property gets/sets the value of the private field id</value>
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// The Name property represents a Retailer's name
        /// </summary>
        /// <value>The Name property gets/sets the value of the private field name</value>
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// The OnlineAbbrev property represents a Retailer's online abbreviation, such as WM for Walmart
        /// </summary>
        /// <value>The OnlineAbbrev property gets/sets the value of the private field onlineAbbrev</value>
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set
            { 
                onlineAbbrev = value;
                OnPropertyChanged("OnlineAbbrev");
            }
        }

        /// <summary>
        /// The FoodPercentage property represents a Retailer's online food % to multiply a price by, such as .56 * 1.99
        /// </summary>
        /// <value>The FoodPercentage property gets/sets the value of the private field foodPercentage</value>
        public Double FoodPercentage
        {
            get { return foodPercentage; }
            set
            { 
                foodPercentage = value;
                OnPropertyChanged("FoodPercentage");
            }
        }

        /// <summary>
        /// The NonfoodPercentage property represents a Retailer's online nonfood % to multiply a price by, such as .56 * 1.99
        /// </summary>
        /// <value>The NonfoodPercentage property gets/sets the value of the private field nonfoodPercentage</value>
        public Double NonfoodPercentage
        {
            get { return nonfoodPercentage; }
            set 
            { 
                nonfoodPercentage = value;
                OnPropertyChanged("NonfoodPercentage");
            }
        }

        /// <summary>
        /// The NonfoodDfPercentage property represents a Retailer's online nonfood df % to multiply a price by, such as .56 * 1.99
        /// </summary>
        /// <value>The NonfoodDfPercentage property gets/sets the value of the private field nonfoodDfPercentage</value>
        public Double NonfoodDfPercentage
        {
            get { return nonfoodDfPercentage; }
            set
            { 
                nonfoodDfPercentage = value;
                OnPropertyChanged("NonfoodDfPercentage");
            }
        }

        /// <summary>
        /// The FreezerPercentage property represents a Retailer's online freezer % to multiply a price by, such as .56 * 1.99
        /// </summary>
        /// <value>The FreezerPercentage property gets/sets the value of the private field freezerPercentage</value>
        public Double FreezerPercentage
        {
            get { return freezerPercentage; }
            set
            { 
                freezerPercentage = value;
                OnPropertyChanged("FreezerPercentage");
            }
        }

        /// <summary>
        /// The CoolerPercentage property represents a Retailer's online cooler % to multiply a price by, such as .56 * 1.99
        /// </summary>
        /// <value>The CoolerPercentage property gets/sets the value of the private field coolerPercentage</value>
        public Double CoolerPercentage
        {
            get { return coolerPercentage; }
            set 
            { 
                coolerPercentage = value;
                OnPropertyChanged("CoolerPercentage");
            }
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
