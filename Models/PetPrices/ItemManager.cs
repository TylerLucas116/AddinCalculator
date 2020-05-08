using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.PetPrices
{
    class ItemManager
    {
        private static Random random = new Random();
        private static string[] itemNames = new string[] { "Cereal", "Candy", "Drinks", "Organic", "Breakfast" };

        public static List<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < itemNames.Length; i++)
            {
                items.Add(new Item()
                {
                    Name = itemNames[i],
                    Size = random.Next(1, 100),
                    OnSale = random.Next(0, 2) == 0
                });
            }
            return items;
        }
    }
}
