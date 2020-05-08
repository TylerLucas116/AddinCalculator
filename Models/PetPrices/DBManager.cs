using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.PetPrices
{
    public class DBManager
    {
        public string ConnectionString = @"Data Source=DESKTOP-K99E9GR\SQLEXPRESS;Initial Catalog=SommersMarket;Integrated Security=SSPI;";
        public string connectionString
        {
            get
            {
                return ConnectionString;
            }
        }
    }
}
