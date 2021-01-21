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
        public SQLiteConnection dbcon = new SQLiteConnection((App.Current as App).DatabaseFileName + ".db");

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

        public List<Retailer> GetAllRetailers()
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
                    NewRetailer.Name = (String)query["Name"];
                    NewRetailer.OnlineAbbrev = (String)query["OnlineAbbrev"];
                    NewRetailer.FoodPercentage = Convert.ToInt32(query["FoodPercentage"]);
                    NewRetailer.NonfoodPercentage = Convert.ToInt32(query["NonfoodPercentage"]);
                    NewRetailer.NonfoodDfPercentage = Convert.ToInt32(query["NonfoodDfPercentage"]);
                    NewRetailer.FreezerPercentage = Convert.ToInt32(query["FreezerPercentage"]);
                    NewRetailer.CoolerPercentage = Convert.ToInt32(query["CoolerPercentage"]);

                    RetailerList.Add(NewRetailer);
                }

            }
            return RetailerList;
        }

        public void DeleteRetailer(Retailer retailer)
        {
            String sSql = String.Format(@"DELETE FROM {0} WHERE Retailer = ('{1}');", tableName, ConvertObjectToString<Retailer>(retailer));
            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);
            cnStatement.Step();
        }
        private string ConvertObjectToString<objectType>(objectType myObject)
        {
            string content = String.Empty;

            var js = new DataContractJsonSerializer(typeof(objectType));

            var ms = new MemoryStream();

            js.WriteObject(ms, myObject);
            ms.Position = 0;

            var reader = new StreamReader(ms);

            content = reader.ReadToEnd();
            return content;
        }
        private objectType ConvertStringToObject<objectType>(string objectString, string myObjectPath)
        {
            objectType myObject = (objectType)Activator.CreateInstance(Type.GetType(myObjectPath));
            var js = new DataContractJsonSerializer(typeof(objectType));
            byte[] byteArray = Encoding.UTF8.GetBytes(objectString);
            var ms = new MemoryStream(byteArray);

            myObject = (objectType)js.ReadObject(ms);

            return myObject;
        }
        
        //public SQLiteConnection dbcon = new SQLiteConnection("NFButtons.db");
        //Create Database file
    }

    #region Database Field Object Class
    public class DatabaseField
    {
        public String FieldType { get; set; }
        public String FieldName { get; set; }

    }
    #endregion  
}
