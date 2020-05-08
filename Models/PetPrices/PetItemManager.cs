using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/****** Catchall written below for all exceptions at the moment *****/
namespace AddInCalculator2._0.Models.PetPrices
{
    public class PetItemManager : ObservableObject
    {
        public DBManager PetItemDB = new DBManager(); //PetItemDB holds connection string to connect to server
        public string BrandsQueryString = @"SELECT * FROM Brands ORDER BY BrandName;"; //Query string to get brands from server
        public string AddBrandQueryString = @"INSERT INTO Brands (BrandID, BrandName, BrandClass) VALUES(@BrandID, @BrandName, @BrandClass)";

        private string addBrandNameTextBox { get; set; }
        public string AddBrandNameTextBox
        {
            get
            {
                return addBrandNameTextBox;
            }
            set
            {
                addBrandNameTextBox = value;
                OnPropertyChanged("AddBrandNameTextBox");
            }
        }
        private string addBrandIDTextBox { get; set; }
        public string AddBrandIDTextBox
        {
            get
            {
                return addBrandIDTextBox;
            }
            set
            {
                addBrandIDTextBox = value;
                OnPropertyChanged("AddBrandIDTextBox");
            }
        }
        private string AddBrandClassTextBox { get; set; }
        public string addBrandClassTextBox
        {
            get
            {
                return AddBrandClassTextBox;
            }
            set
            {
                AddBrandClassTextBox = value;
                OnPropertyChanged("addBrandClassTextBox");
            }
        }

        private int deleteBrandID { get; set; }
        public int DeleteBrandID //Selecteditem in BrandSettings Listview
        {
            get => deleteBrandID;
            set
            {
                deleteBrandID = value;
                OnPropertyChanged("DeleteBrandID");
            }
        }
        private int deleteBrandClass { get; set; }
        public int DeleteBrandClass //Seleteditem in Brandsettings ListView
        {
            get => deleteBrandClass;
            set
            {
                deleteBrandClass = value;
                OnPropertyChanged("DeleteBrandClass");
            }
        }
        private string deleteBrandName { get; set; }
        public string DeleteBrandName  //Selecteditem in Brandsettings ListView
        {
            get => deleteBrandName;
            set
            {
                deleteBrandName = value;
                OnPropertyChanged("DeleteBrandName");
            }
        }

        public ObservableCollection<PetBrands> petBrands = new ObservableCollection<PetBrands>(); //PetBrands will hold all information for pet brands
        public ObservableCollection<PetBrands> PetBrands
        {
            get
            {
                return petBrands;
            }
        }


        public void UpdatePetBrands() //Method to retrieve all brands from server
        {
            using (SqlConnection connection = new SqlConnection(PetItemDB.ConnectionString))
            {
                SqlCommand command = new SqlCommand(BrandsQueryString, connection);
                //command.Connection = connection;
                //command.CommandText = BrandsQueryString.ToString(); 
                try
                {
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            PetBrands brand = new PetBrands()
                            {
                                BrandID = (int)dataReader["BrandID"],
                                BrandName = dataReader["BrandName"].ToString(),
                                BrandClass = (int)dataReader["BrandClass"],
                            };
                            petBrands.Add(brand);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AddPetBrand() //Method to add a pet brand
        {
            if (AddBrandNameTextBox != null)
            {
                using (SqlConnection connection = new SqlConnection(PetItemDB.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(AddBrandQueryString, connection);
                    //Create Parameters
                    command.Parameters.AddWithValue("@BrandID", Convert.ToInt32(AddBrandIDTextBox));
                    command.Parameters.AddWithValue("@BrandName", AddBrandNameTextBox);
                    command.Parameters.AddWithValue("@BrandClass", Convert.ToInt32(addBrandClassTextBox));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    PetBrands brand = new PetBrands
                    {
                        BrandName = AddBrandNameTextBox,
                        BrandClass = Convert.ToInt32(addBrandClassTextBox),
                        BrandID = Convert.ToInt32(AddBrandIDTextBox)
                    };
                    petBrands.Add(brand);
                }

                AddBrandNameTextBox = string.Empty;
                addBrandClassTextBox = string.Empty;
                AddBrandIDTextBox = string.Empty;
            }
            else return;
        }

        public void DeletePetBrand()
        {
            PetBrands brand = new PetBrands
            {
                BrandName = DeleteBrandName,
                BrandClass = DeleteBrandClass,
                BrandID = DeleteBrandID
            };
            petBrands.Remove(brand);
        }

        public async void UpdatePetBrandsAsync() //Method to retrieve all brands from server
        {
            using (SqlConnection connection = new SqlConnection(PetItemDB.ConnectionString))
            {
                SqlCommand command = new SqlCommand(BrandsQueryString, connection);
                //command.Connection = connection;
                //command.CommandText = BrandsQueryString.ToString(); 
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            PetBrands brand = new PetBrands()
                            {
                                BrandID = (int)dataReader["BrandID"],
                                BrandName = dataReader["BrandName"].ToString(),
                                BrandClass = (int)dataReader["BrandClass"],
                            };
                            petBrands.Add(brand);
                        }
                    };
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //Catch all
                catch(Exception exception)
                {
                    Debug.WriteLine("Exception caught.");
                    Debug.WriteLine(exception);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
