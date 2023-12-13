namespace ElectronicLibraryAPI2.Models
{
    public partial class ReturnBook
    {

        public int? IdReturnBook { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public string? ReasonReturn { get; set; }
        public string? NameUserForReturn { get; set; }
        public string? EmailUserForReturn { get; set; }

        public bool DeletedReturn { get; set; }

        public bool? ReturnAgree { get; set; }

        public string? ReasonNoAgree { get; set; }

        public Book? Book { get; set; }
    }
}
