using System;
using System.Collections.Generic;

namespace ElectronicLibraryAPI2.Models
{
    public partial class PublisherView
    {
        public int? IdPublisherView { get; set; }
        public int? BookId { get; set; }
        public int? PublisherId { get; set; }

        public Publisher? Publisher { get; set; }
        public bool? Deleted_Publisher_View { get; set; }
    }
}
