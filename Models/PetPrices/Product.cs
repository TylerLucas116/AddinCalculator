using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.PetPrices
{
    public class Product : INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        public string ProductCode { get { return ProductID.ToString(); } }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitPriceString { get { return UnitPrice.ToString("######.00"); } }
        public int UnitsInStock { get; set; }
        public string UnitsInStockString { get { return UnitsInStock.ToString("#####0"); } }
        public int CategoryId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Product> GetProducts(string connectionString)
        {
            const string GetProductsQuery = "select ProductID, ProductName, QuantityPerUnit," +
               " UnitPrice, UnitsInStock, Products.CategoryID " +
               " from Products inner join Categories on Products.CategoryID = Categories.CategoryID " +
               " where Discontinued = 0";

            var products = new ObservableCollection<Product>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetProductsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new Product();
                                    product.ProductID = reader.GetInt32(0);
                                    product.ProductName = reader.GetString(1);
                                    product.QuantityPerUnit = reader.GetString(2);
                                    product.UnitPrice = reader.GetDecimal(3);
                                    product.UnitsInStock = reader.GetInt16(4);
                                    product.CategoryId = reader.GetInt32(5);
                                    products.Add(product);
                                }
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
