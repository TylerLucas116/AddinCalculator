using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AddInCalculator2._0.Models.AddInCalculator;
using SQLitePCL;

namespace AddInCalculator2._0.Handlers
{

    public class Database
    {
        #region Delcarations

        public SQLiteConnection dbcon = new SQLiteConnection((App.Current as App).DatabaseFileName + ".db");
        #endregion

        #region External Methods
        #region Create Database
        public String CreateDatabase(String tableName, DatabaseField Field)
        {
            String sSql = String.Format(@"CREATE TABLE IF NOT EXISTS {0}
                                       (ID Integer Primary Key AutoIncrement NOT NULL,
                                         {1} {2} NOT NULL);", tableName, Field.FieldName, Field.FieldType);
            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);
            cnStatement.Step();

            return sSql;
        }


        #endregion
        #region Write to Database
        public void WriteRecord<objectType>(objectType myObject, String tableName, DatabaseField Field)
        {
            String myObjectString = ConvertObjectToString<objectType>(myObject);

            String sSql = String.Format(@"INSERT INTO [{0}]([{1}]) VALUES('{2}');", tableName, Field.FieldName, myObjectString);

            dbcon.Prepare(sSql).Step();
        }
        #endregion
        #region Read from Database
        #region Return All Records
        public List<objectType> ReturnAllRecords<objectType>(String tableName, String FieldName, String objectPath)
        {
            var objectList = new List<objectType>();

            String objectString = "";

            String sSql = String.Format(@"SELECT * FROM {0};", tableName);

            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            while(cnStatement.Step() == SQLiteResult.ROW)
            {
                objectString = cnStatement[FieldName].ToString(); //[FieldName] == Button
                objectList.Add(ConvertStringToObject<objectType>(objectString, objectPath));
            }
            return objectList;
        }
        #endregion
        #region Search Records
        public objectType SearchRecords<objectType>(String tableName, String FieldName, String objectPath, String FieldToSearch, String SearchString)
        {
            String objectString = string.Empty;

            String sSql = String.Format(@"SELECT * FROM {0};", tableName);
            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            while(cnStatement.Step() == SQLiteResult.ROW)
            {
                objectString = cnStatement[FieldName].ToString();

                objectType myObject = (objectType)Activator.CreateInstance(Type.GetType(objectPath));

                myObject = ConvertStringToObject<objectType>(objectString, objectPath);

                PropertyInfo propInfo = typeof(objectType).GetProperty(FieldToSearch);

                if(propInfo.GetValue(myObject).ToString() == SearchString)
                {
                    return myObject;
                }
            }
            return (objectType)Activator.CreateInstance(Type.GetType(objectPath));
        }
        #endregion
        #region BuildFieldObject
        /// <summary>
        /// Builds a field for the database
        /// </summary>
        /// <param name="tp">Type of Value in the field ex. Integer, nvarchar, etc.</param>
        /// <param name="name">Name of Field</param>
        /// <returns></returns>
        public Handlers.DatabaseField BuildFieldObject(String tp, String name)
        {
            var myField = new Handlers.DatabaseField()
            {
                FieldName = name,
                FieldType = tp
            };

            return myField;
        }
        #endregion
        #endregion
        #region Delete From Database
        public void DeleteButton(String tableName,  Models.AddInCalculator.Button button)
        {
            String sSql = String.Format(@"DELETE FROM {0} WHERE Button = ('{1}');", tableName, ConvertObjectToString<Models.AddInCalculator.Button>(button));
            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);
            cnStatement.Step();
        }
        #endregion
        #endregion

        #region Internal Methods
        #region Convert Object To String
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
        #endregion
        #region Convert String To Object
        private objectType ConvertStringToObject<objectType>(string objectString, string myObjectPath)
        {
            objectType myObject = (objectType)Activator.CreateInstance(Type.GetType(myObjectPath));
            var js = new DataContractJsonSerializer(typeof(objectType));
            byte[] byteArray = Encoding.UTF8.GetBytes(objectString);
            var ms = new MemoryStream(byteArray);

            myObject = (objectType)js.ReadObject(ms);

            return myObject;
        }
   


        #endregion

        #endregion
        //public SQLiteConnection dbcon = new SQLiteConnection("NFButtons.db");
        //Create Database file
        public void CreateDatabase()
        {
            string sSql = @"CREATE TABLE IF NOT EXISTS NFButtonInfo
                            (ID Integer Primary Key AutoIncrement NOT NULL,
                            Button nvarchar NOT NULL);";

            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            cnStatement.Step(); // Runs the command
        }

        //Write Record
        public void WriteRecord(string Button)
        {
            //Validation done when entering info in xaml fields
            string sSql = @"INSERT INTO [NFButtonInfo] ([Button]) VALUES ('" + Button + "');";
            /* For multiple objects, could do WriteRecord(string Car, string Track, string Something)
             * string sSql = @"INSERT INTO [NFButtonInfo] ([Button],[Track],[Something]) VALUES ('" + Button + "','" + Track + "','" + Something "');";
             */

            dbcon.Prepare(sSql).Step(); //Shorthand w/ step
        }

        //Read Record
        public string ReadRecords(int recordID)
        {
            string sSql = @"SELECT * FROM NFButtonInfo;";

            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            while(cnStatement.Step() == SQLiteResult.ROW) //While there's a row there
            {
                if(cnStatement["ID"].ToString() == recordID.ToString())
                {
                    return cnStatement["Button"].ToString();
                }
            }

            return "Button Not Found";
        }
    }

    #region Database Field Object Class
    public class DatabaseField
    {
        public String FieldType { get; set; }
        public String FieldName { get; set; }

    }
    #endregion  
}
