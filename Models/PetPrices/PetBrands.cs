using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInCalculator2._0.Models.PetPrices
{
    public class PetBrands : IEquatable<PetBrands> //IEquatable implemented for DeletePetBrand()
    {
        public string BrandName { get; set; }
        public int BrandClass { get; set; }
        public int BrandID { get; set; }

        public bool Equals(PetBrands other)
        {
            return (this.BrandID == other.BrandID);
        }
    }
}
