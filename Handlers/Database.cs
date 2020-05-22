﻿using System;
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
        public SQLiteConnection dbcon = new SQLiteConnection((App.Current as App).DatabaseFileName + ".db");

        public String CreateDatabase(String tableName, DatabaseField Field)
        {
            String sSql = String.Format(@"CREATE TABLE IF NOT EXISTS {0}
                                       (ID Integer Primary Key AutoIncrement NOT NULL,
                                         {1} {2} NOT NULL);", tableName, Field.FieldName, Field.FieldType);
            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);
            cnStatement.Step();

            return sSql;
        }

        public void WriteRecord<objectType>(objectType myObject, String tableName, DatabaseField Field)
        {
            String myObjectString = ConvertObjectToString<objectType>(myObject);

            String sSql = String.Format(@"INSERT INTO [{0}]([{1}]) VALUES('{2}');", tableName, Field.FieldName, myObjectString);

            dbcon.Prepare(sSql).Step();
        }

        public List<objectType> ReturnAllRecords<objectType>(String tableName, String FieldName, String objectPath)
        {
            String sSql = String.Format(@"SELECT * FROM {0};", tableName);

            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            var objectList = new List<objectType>();
            while (cnStatement.Step() == SQLiteResult.ROW)
            {
                var objectString = cnStatement[FieldName].ToString(); //[FieldName] == Button
                objectList.Add(ConvertStringToObject<objectType>(objectString, objectPath));
            }
            return objectList;
        }
        public List<objectType> ReturnAllRetailers<objectType>(String tableName, String FieldName, String objectPath)
        {
            String sSql = String.Format(@"SELECT * FROM {0};", tableName);

            ISQLiteStatement cnStatement = dbcon.Prepare(sSql);

            var objectList = new List<objectType>();
            while (cnStatement.Step() == SQLiteResult.ROW)
            {
                var objectString = cnStatement[FieldName].ToString(); //[FieldName] == Button
                objectList.Add(ConvertStringToObject<objectType>(objectString, objectPath));
            }
            return objectList;
        }

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

        public void DeleteRetailer(String tableName, Retailer retailer)
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
