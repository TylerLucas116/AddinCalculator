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
        private int id = 0;
        private string name = "";
        private string onlineAbbrev = "";
        private int foodPercentage = 0;
        private int nonfoodPercentage = 0;
        private int nonfoodDfPercentage = 0;
        private int freezerPercentage = 0;
        private int coolerPercentage = 0;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
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
        public int FoodPercentage
        {
            get { return foodPercentage; }
            set
            { 
                foodPercentage = value;
                OnPropertyChanged("FoodPercentage");
            }
        }
        public int NonfoodPercentage
        {
            get { return nonfoodPercentage; }
            set 
            { 
                nonfoodPercentage = value;
                OnPropertyChanged("NonfoodPercentage");
            }
        }
        public int NonfoodDfPercentage
        {
            get { return nonfoodDfPercentage; }
            set
            { 
                nonfoodDfPercentage = value;
                OnPropertyChanged("NonfoodDfPercentage");
            }
        }
        public int FreezerPercentage
        {
            get { return freezerPercentage; }
            set
            { 
                freezerPercentage = value;
                OnPropertyChanged("FreezerPercentage");
            }
        }
        public int CoolerPercentage
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
