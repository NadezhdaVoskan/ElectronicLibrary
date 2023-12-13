using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class TypeLiterature
    {


        public int IdTypeLiterature { get; set; }
        public string NameTypeLiterature { get; set; } = null!;
        public bool? Deleted_Type_Literature { get; set; }

    }
}
