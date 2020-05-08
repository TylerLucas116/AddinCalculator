using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class Button : IEquatable<Button>//For delete button
    {
        //[PrimaryKey]
        //public int ID { get; set; } //start at 1, 2, 3
        public string retailer { get; set; }
        public string label { get; set; } //Walmart, Target
        public string abbrev { get; set; } //wm, tg, etc (for pasting)
        public double percentage { get; set; } //75, 50, etc
        public string type { get; set; } //food, nonfood, etc
        public bool visibility { get; set; } //Visible or not visible

        public bool Equals(Button other)
        {
            return (this.retailer == other.retailer);
        }
    }
}
