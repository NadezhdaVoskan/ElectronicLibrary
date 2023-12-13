using ElectronicLibrary.Models;
using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI.Models
{
    public partial class AuthorView
    {
        public int? IdAuthorView { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
