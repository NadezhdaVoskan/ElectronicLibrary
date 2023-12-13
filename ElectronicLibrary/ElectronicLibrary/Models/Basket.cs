using ElectronicLibrary.Models;
using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI.Models
{
    public partial class Basket
    {
        public int IdBasket { get; set; }
        public decimal Cost { get; set; }
        public int RiderTicketId { get; set; }
        public int BookId { get; set; }
        public int PromocodeId { get; set; }

        public virtual Book Book { get; set; }

    }
}
