using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AddInCalculator2._0.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
            CreateDatabase();
        }

        #region Buttons
        private void BtnGetAllRecords_Click(object sender, RoutedEventArgs e)
        {
            lvRecords.Items.Clear();
            ReturnAllRecords();
        }

        private void BtnSearchRecords_Click(object sender, RoutedEventArgs e)
        {
            lvRecords.Items.Clear();
            if(tbSearch.Text == "")
            {
                tbResults.Text = "You must enter text to search.";
                return;
            }

            SearchRecords(cbSearch.SelectedItem.ToString(), tbSearch.Text);
            //SearchRecords("Type", tbSearch.Text);
        }

        private void BtnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            WriteRecord();
        }
        #endregion

        #region DatabaseTools
        #region Declarations
        String table = "ButtonInfo";
        String fieldname = "Button";
        String ObjectPath = "AddInCalculator2._0.Models.AddInCalculator.Button";
        Type obType = (typeof(Models.AddInCalculator.Button));
       /* private Models.AddInCalculator.Button BuildButtonObject()
        {
            var myButton = new Models.AddInCalculator.Button()
            {
                retailer = tbRetailer.Text,
                label = tbLabel.Text,
                abbrev = tbAbbreviation.Text,
                percentage = decimal.Parse(tbPercentage.Text),
                type = tbType.Text
            };
            return myButton;
        }*/

        #endregion
        #region Create Database
        private void CreateDatabase()
        {
            Handlers.Database db = new Handlers.Database();
            Handlers.DatabaseField myField = db.BuildFieldObject("nvarchar", fieldname);
            tbResults.Text = db.CreateDatabase(table, myField);
        }

        #endregion
        #region Write to Database
        private void WriteRecord()
        {
            Handlers.Database db = new Handlers.Database();
            var myButton = buildButtonObject();

            db.WriteRecord<Models.AddInCalculator.Button>(myButton, table, db.BuildFieldObject("nvarchar", fieldname));
        }
        #endregion
        #region Read from Database
        #region Return All Records
        private void ReturnAllRecords()
        {
            var myButtonList = new List<Models.AddInCalculator.Button>();
            Handlers.Database db = new Handlers.Database();

            myButtonList = db.ReturnAllRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath);
            foreach(Models.AddInCalculator.Button myButton in myButtonList)
            {
                lvRecords.Items.Add(myButton.retailer + "  " + myButton.label + "  " +
                    myButton.percentage.ToString() + "  " + myButton.type + "  " + myButton.abbrev);
            }
        }

        #endregion
        #region Search Records
        private void SearchRecords(String FieldToSearch, String SearchText)
        {
            //var myButtonList = new List<Models.AddInCalculator.Button>();

            Handlers.Database db = new Handlers.Database();

            var myButton = db.SearchRecords<Models.AddInCalculator.Button>(table, fieldname, ObjectPath, FieldToSearch, SearchText);

            lvRecords.Items.Add(myButton.retailer + "  " + myButton.label + "  " +
                    myButton.percentage.ToString() + "  " + myButton.type + "  " + myButton.abbrev);
        }

        #endregion
        #endregion
        #endregion

        //Add
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertObjectToStringJson();
        }

        //Retrieve
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetObjectStringFromDatabase();
        }

        //Build object graph for Button
        private Models.AddInCalculator.Button buildButtonObject()
        {
            var myButton = new Models.AddInCalculator.Button();

            /*  public int ID { get; set; } //start at 1, 2, 3
                public string retailer { get; set; }
                public string label { get; set; } //Walmart, targer
                public string abbrev { get; set; } //wm, tg, etc (for pasting)
                public decimal percentage { get; set; } //75, 50, etc
                public string type { get; set; } //food,*/
            myButton.retailer = tbRetailer.Text;
            myButton.label = tbLabel.Text;
            myButton.abbrev = tbAbbreviation.Text;
            myButton.percentage = Double.Parse(tbPercentage.Text);
            myButton.type = tbType.Text;

            return myButton;

        }

        //Convert object into string of json
        private void ConvertObjectToStringJson()
        {
            string content = string.Empty;

            var myButton = buildButtonObject();

            var js = new DataContractJsonSerializer(typeof(Models.AddInCalculator.Button)); //Serialize button type to js

            var ms = new MemoryStream();

            js.WriteObject(ms, myButton); //Pointer is at end of memory stream

            ms.Position = 0; //Move position to beginning of memory stream

            var reader = new StreamReader(ms); //Pass memorystream into streamreader

            content = reader.ReadToEnd();

            tbResults.Text = content;

            WriteStringToDatabase(content);
        }

        //Write string object to database
        private void WriteStringToDatabase(string myButtonString)
        {
            var db = new Handlers.Database();

            db.CreateDatabase();

            db.WriteRecord(myButtonString); //json string

            db.dbcon.Dispose(); //Helps if reading later

            tbResults.Text = tbResults.Text + "  Data Written Successfully";
        }
        //Get string object from database
        private void GetObjectStringFromDatabase() //lookup workaround
        {
            var db = new Handlers.Database();

            ConvertStringToObject(db.ReadRecords(1)); //If 0 is deleted, it wont exist
        }
        //Convert string object back to an object
        private void ConvertStringToObject(string ButtonObjectString)
        {
            var myButton = new Models.AddInCalculator.Button();

            var js = new DataContractJsonSerializer(typeof(Models.AddInCalculator.Button));

            //decypher the enconding of the serialization so we can send the bit array through stream reader
            byte[] byteArray = Encoding.UTF8.GetBytes(ButtonObjectString);

            var ms = new MemoryStream(byteArray);

            myButton = (Models.AddInCalculator.Button)js.ReadObject(ms);

            //tbLookupRetailer.Text = myButton.retailer.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ButtonSettings));
        }
    }
}
