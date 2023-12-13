using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class GenreView
    {
        public int? IdGenreView { get; set; }
        public int? BookId { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
