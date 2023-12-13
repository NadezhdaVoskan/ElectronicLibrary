using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class Role
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }

        public bool? Deleted_Role { get; set; }

    }
}
