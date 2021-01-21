using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AddInCalculator2._0.Models.AddInCalculator;
using Windows.Storage;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace AddInCalculator2._0.Handlers
{

    public class Database
    {
        private string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Calculator.db");

        public async void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("Calculator.db", CreationCollisionOption.OpenIfExists);
            using (SqliteConnection db = new SqliteConnection ($"Filename={ dbPath }"))
            {
                db.Open();

                string tableCommand = @"CREATE TABLE IF NOT EXISTS Retailers
                                       (RetailerID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name VARCHAR(50) NOT NULL,
                                        OnlineAbbrev VARCHAR(50) NOT NULL,
                                        FoodPercentage INT NOT NULL,
                                        NonfoodPercentage INT NOT NULL,
                                        NonfoodDfPercentage INT NOT NULL,
                                        FreezerPercentage INT NOT NULL,
                                        CoolerPercentage INT NOT NULL);";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        
        public void AddRetailer(Retailer NewRetailer)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={ dbPath }"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Retailers(Name, OnlineAbbrev, FoodPercentage, NonfoodPercentage, NonfoodDfPercentage, FreezerPercentage, CoolerPercentage) " +
                    "VALUES ($Name, $OnlineAbbrev, $FoodPercentage, $NonfoodPercentage, $NonfoodDfPercentage, $FreezerPercentage, $CoolerPercentage)";
                insertCommand.Parameters.AddWithValue("$Name", NewRetailer.Name);
                insertCommand.Parameters.AddWithValue("$OnlineAbbrev", NewRetailer.OnlineAbbrev);
                insertCommand.Parameters.AddWithValue("$FoodPercentage", NewRetailer.FoodPercentage);
                insertCommand.Parameters.AddWithValue("$NonfoodPercentage", NewRetailer.NonfoodPercentage);
                insertCommand.Parameters.AddWithValue("$NonfoodDfPercentage", NewRetailer.NonfoodDfPercentage);
                insertCommand.Parameters.AddWithValue("$FreezerPercentage", NewRetailer.FreezerPercentage);
                insertCommand.Parameters.AddWithValue("$CoolerPercentage", NewRetailer.CoolerPercentage);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public List<Retailer> LoadAllRetailers()
        {
            List<Retailer> RetailerList = new List<Retailer>();

            using (SqliteConnection db = new SqliteConnection($"Filename={ dbPath }"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM Retailers;", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Retailer NewRetailer = new Retailer();
                    NewRetailer.ID = Convert.ToInt32(query["RetailerID"]);
                    NewRetailer.Name = (String)query["Name"];
                    NewRetailer.OnlineAbbrev = (String)query["OnlineAbbrev"];
                    NewRetailer.FoodPercentage = Convert.ToDouble(query["FoodPercentage"]);
                    NewRetailer.NonfoodPercentage = Convert.ToDouble(query["NonfoodPercentage"]);
                    NewRetailer.NonfoodDfPercentage = Convert.ToDouble(query["NonfoodDfPercentage"]);
                    NewRetailer.FreezerPercentage = Convert.ToDouble(query["FreezerPercentage"]);
                    NewRetailer.CoolerPercentage = Convert.ToDouble(query["CoolerPercentage"]);

                    RetailerList.Add(NewRetailer);
                }
            }
            return RetailerList;
        }

        public void DeleteRetailer(Retailer Retailer)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={ dbPath }"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                deleteCommand.CommandText = String.Format(@"DELETE FROM RETAILERS WHERE RetailerID = '{0}';", Retailer.ID);
                deleteCommand.ExecuteReader();

                db.Close();
            }
        }

        public void UpdateRetailer(Retailer Retailer)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={ dbPath }"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = String.Format(@"UPDATE RETAILERS SET Name = '{0}', OnlineAbbrev = '{1}', FoodPercentage = '{2}', " +
                    "NonfoodPercentage = '{3}', NonfoodDfPercentage = '{4}', FreezerPercentage = '{5}', CoolerPercentage = '{6}' WHERE " + 
                    "RetailerID = '{7}';", Retailer.Name, Retailer.OnlineAbbrev, Retailer.FoodPercentage, Retailer.NonfoodPercentage, 
                    Retailer.NonfoodDfPercentage, Retailer.FreezerPercentage, Retailer.CoolerPercentage, Retailer.ID);
                updateCommand.ExecuteReader();

                db.Close();
            }
        }

        public Retailer LoadRetailer(Retailer OriginalRetailer)
        {
            Retailer NewRetailer = new Retailer();
            using (SqliteConnection db = new SqliteConnection($"Filename={ dbPath }"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = String.Format(@"SELECT * FROM RETAILERS WHERE RetailerID = '{0}';", OriginalRetailer.ID);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    NewRetailer.ID = Convert.ToInt32(query["RetailerID"]);
                    NewRetailer.Name = (String)query["Name"];
                    NewRetailer.OnlineAbbrev = (String)query["OnlineAbbrev"];
                    NewRetailer.FoodPercentage = Convert.ToDouble(query["FoodPercentage"]);
                    NewRetailer.NonfoodPercentage = Convert.ToDouble(query["NonfoodPercentage"]);
                    NewRetailer.NonfoodDfPercentage = Convert.ToDouble(query["NonfoodDfPercentage"]);
                    NewRetailer.FreezerPercentage = Convert.ToDouble(query["FreezerPercentage"]);
                    NewRetailer.CoolerPercentage = Convert.ToDouble(query["CoolerPercentage"]);
                }
                db.Close();
            }
            return NewRetailer;
        }
    }
}
