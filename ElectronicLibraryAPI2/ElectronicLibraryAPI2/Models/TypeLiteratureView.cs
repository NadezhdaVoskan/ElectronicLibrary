using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class TypeLiteratureView
    {
        public int? IdTypeLiteratureView { get; set; }
        public int? BookId { get; set; }
        public int? TypeLiteratureId { get; set; }

        public TypeLiterature? TypeLiterature { get; set; }
        public bool? Deleted_Type_Literature_View { get; set; }
    }
}
