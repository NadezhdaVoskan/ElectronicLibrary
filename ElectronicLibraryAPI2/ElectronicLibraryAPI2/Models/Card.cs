using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Card
    {


        public int IdCard { get; set; }
        public string CardNumber { get; set; } = null!;
        public string CardHolder { get; set; } = null!;
        public string Validity { get; set; } = null!;
        public string CvcCode { get; set; } = null!;

        public bool? Deleted_Card { get; set; }
    }    
}
