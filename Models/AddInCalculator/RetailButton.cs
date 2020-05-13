﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButton : IEquatable<RetailButton>//For delete button
    {
        RetailButton()
        {
            retailer = new Retailer();
        }

        private Retailer retailer;
        private bool visibility;

        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public Retailer Retailer
        {
            get { return retailer; }
            set { retailer = value; }
        }

        public bool Equals(RetailButton other)
        {
            return (this.retailer.Name == other.retailer.Name);
        }
    }
}
