using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class Retailer : INotifyPropertyChanged
    {
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
