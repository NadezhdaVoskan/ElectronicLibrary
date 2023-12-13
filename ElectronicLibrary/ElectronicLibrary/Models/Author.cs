using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI.Models
{
    public partial class Author
    {
        public int IdAuthor { get; set; }
        public string FirstNameAuthor { get; set; }
        public string SecondNameAuthor { get; set; }
        public string MiddleNameAuthor { get; set; }

        public bool? Deleted_Author { get; set; }

        public string FullName => $"{FirstNameAuthor} {SecondNameAuthor} {MiddleNameAuthor}".Trim();

    }
}
