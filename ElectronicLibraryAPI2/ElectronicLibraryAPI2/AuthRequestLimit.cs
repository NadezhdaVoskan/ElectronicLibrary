namespace ElectronicLibraryAPI2
{
    public class AuthRequestLimit : Attribute
    {
        public int TimeWindow { get; set; }
        public int MaxRequests { get; set; }
    }
}
