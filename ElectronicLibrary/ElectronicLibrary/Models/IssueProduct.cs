using System;
using System.Collections.Generic;

namespace ElectronicLibrary.Models
{
    public partial class IssueProduct
    {
        public int IdIssueProduct { get; set; }
        public DateTime DateIssue { get; set; }
        public string Barcode { get; set; }
        public int? RiderTicketId { get; set; }
        public int BookId { get; set; }
        public decimal? CostIssueFix { get; set; }
        public int? StatusIssueId { get; set; }

        public bool? Deleted_Issue_Product { get; set; }

        public virtual Book Book { get; set; }
    }
}
