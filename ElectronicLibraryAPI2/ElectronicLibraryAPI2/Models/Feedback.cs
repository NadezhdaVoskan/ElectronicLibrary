namespace ElectronicLibraryAPI2.Models
{
    public partial class Feedback
    {
        public int IdFeedback { get; set; }
        public string Message { get; set; }
        public string NameUserMessage { get; set; }
        public string EmailUserMessage { get; set; }
        public int? UserId { get; set; }
        public bool? Done { get; set; }
    }
}
