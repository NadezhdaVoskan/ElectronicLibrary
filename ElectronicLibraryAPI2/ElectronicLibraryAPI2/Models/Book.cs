using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ElectronicLibraryAPI2.Models
{
    public partial class Book
    {

        public int? IdBook { get; set; }
        public string NameBook { get; set; } = null!;
        public DateTime PublicationDate { get; set; }
        public string BriefPlot { get; set; } = null!;
        public int? NumberPages { get; set; }
        public string? CoverPhoto { get; set; } = null!;
        public decimal? Price { get; set; }
        public bool? Deleted_Book { get; set; }

        public string? FormatFB2 { get; set; }
        public string? FormatTXT { get; set; }

        public virtual ICollection<GenreView>? GenreView { get; set; }

        public virtual ICollection<TypeLiteratureView>? TypeLiteratureView { get; set; }

        public virtual ICollection<PublisherView>? PublisherView { get; set; }

        public virtual ICollection<AuthorView>? AuthorView { get; set; }
    }
}
