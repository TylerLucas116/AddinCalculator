using AddInCalculator2._0.Models.PetPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.ViewModels
{
    class PetViewModel
    {
        private static PetItem currentItem;
        public static PetItem CurrentItem { get { return currentItem; } }
        private static PetItemManager manager;
        public static PetItemManager Manager { get { return manager; } }
        private static DBManager dbmanager;
        public static DBManager DBManager { get => dbmanager; }

        public PetViewModel()
        {
            currentItem = new PetItem();
            manager = new PetItemManager();
            dbmanager = new DBManager();
            manager.UpdatePetBrandsAsync();
        }
    }
}
