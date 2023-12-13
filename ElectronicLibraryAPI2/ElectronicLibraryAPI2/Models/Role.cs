using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Role
    {

        public int IdRole { get; set; }
        public string NameRole { get; set; } = null!;
        public bool? Deleted_Role { get; set; }
    }
}
