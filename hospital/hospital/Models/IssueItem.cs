using System;
using System.Collections.Generic;

namespace hospital.Models
{
    public class IssueItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsDeactive { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
       

    }
}
