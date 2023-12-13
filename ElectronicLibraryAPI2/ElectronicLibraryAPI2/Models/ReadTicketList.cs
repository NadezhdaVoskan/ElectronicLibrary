using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class ReadTicketList
    {
        public string УникальныйНомер { get; set; } = null!;
        public string? ДанныеОКлиенте { get; set; }
        public string? ВыдачаТовара { get; set; }
    }
}
