using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class TypeLiterature
    {
        public int IdTypeLiterature { get; set; }
        public string NameTypeLiterature { get; set; }

        public bool? Deleted_Type_Literature { get; set; }
    }
}
