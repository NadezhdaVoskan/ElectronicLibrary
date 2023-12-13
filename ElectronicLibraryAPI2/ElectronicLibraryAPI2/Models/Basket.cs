using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Basket
    {
        public int IdBasket { get; set; }
        public decimal Cost { get; set; }
        public int? RiderTicketId { get; set; }
        public int? BookId { get; set; }
        public int? PromocodeId { get; set; }
        public bool? Deleted_Basket { get; set; }
    }
}
