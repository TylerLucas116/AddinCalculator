using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.PetPrices
{
    public class PetItem : ObservableObject
    {
        private string type;
        public string Type
        {
            get
            {
                if (string.IsNullOrEmpty(type))
                {
                    return "Unknown";
                }
                else
                {
                    return type;
                }
            }
            set { type = value; }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        private double size;
        public double Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        private string units;
        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
