using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class GenreView
    {
        public int? IdGenreView { get; set; }
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
        public bool? Deleted_Genre_View { get; set; }
    }
}
