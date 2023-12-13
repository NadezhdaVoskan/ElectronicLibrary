using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class RiderTicket
    {

        public int IdRiderTicket { get; set; }
        public string NumberRiderTicket { get; set; }
        public DateTime DateTerm { get; set; }
        public int? UserId { get; set; }

        public bool Deleted_Rider_Ticket { get; set; }
        public virtual User User { get; set; }

    }
}
