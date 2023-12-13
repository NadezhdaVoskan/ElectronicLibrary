using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Publisher
    {

        public int IdPublisher { get; set; }
        public string NamePublisher { get; set; } = null!;

        public bool? Deleted_Publisher { get; set; }
    }
}
