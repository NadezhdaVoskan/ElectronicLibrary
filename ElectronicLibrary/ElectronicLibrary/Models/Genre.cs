using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class Genre
    {
        public int IdGenre { get; set; }
        public string NameGenre { get; set; }

        public bool? Deleted_Genre { get; set; }
    }
}
