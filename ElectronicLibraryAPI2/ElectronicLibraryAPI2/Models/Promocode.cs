using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Promocode
    {

        public int IdPromocode { get; set; }
        public string NamePromocode { get; set; } = null!;
        public int Discount { get; set; }
        public bool? Deleted_Promocode { get; set; }
    }
}
