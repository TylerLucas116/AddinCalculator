﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.AddInCalculator
{
    public class RetailButton
    {
        private string name;
        private string websiteUrl;
        private string onlineAbbrev;
        private double onlinePrice;
        private double onlinePercentage;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string WebsiteURL
        {
            get { return websiteUrl; }
            set { websiteUrl = value; }
        }
        public string OnlineAbbrev
        {
            get { return onlineAbbrev; }
            set { onlineAbbrev = value; }
        }
        public double OnlinePrice
        {
            get { return onlinePrice; }
            set { onlinePrice = value; }
        }
        private double OnlinePercentage
        {
            get { return onlinePercentage; }
            set { onlinePercentage = value; }
        }
    }
}
