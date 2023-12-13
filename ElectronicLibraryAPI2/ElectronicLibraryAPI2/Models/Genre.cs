using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Genre
    {

        public int IdGenre { get; set; }
        public string NameGenre { get; set; } = null!;
        public bool? Deleted_Genre { get; set; }
    }
}
