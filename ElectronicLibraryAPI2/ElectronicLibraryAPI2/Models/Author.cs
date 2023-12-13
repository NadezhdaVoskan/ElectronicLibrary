using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Author
    {

        public int IdAuthor { get; set; }
        public string FirstNameAuthor { get; set; } = null!;
        public string SecondNameAuthor { get; set; } = null!;
        public string? MiddleNameAuthor { get; set; }
        public bool? Deleted_Author { get; set; }


        public string? FullName => $"{FirstNameAuthor} {SecondNameAuthor} {MiddleNameAuthor}".Trim();

    }
}
