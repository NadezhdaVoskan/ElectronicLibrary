using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class PublisherView
    {
        public int? IdPublisherView { get; set; }
        public int? BookId { get; set; }
        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

    }
}
