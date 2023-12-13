using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class User
    {

        public int IdUser { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? SaltPassword { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CardId { get; set; }
        public int? RoleId { get; set; }
        public bool? Deleted_User { get; set; }

        public string? Email { get; set; }
    }
}
