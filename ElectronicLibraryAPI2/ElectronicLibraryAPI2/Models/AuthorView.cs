using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class AuthorView
    {
        public int? IdAuthorView { get; set; }
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }
        public bool? Deleted_Author_View { get; set; }

        public Book? Book { get; set; }
        public Author? Author { get; set; }
    }
}
