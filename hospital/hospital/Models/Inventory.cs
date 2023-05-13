using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace hospital.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
       
        public ItemStore ItemStore { get; set; }
        public int ItemStoreId { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        public bool IsDeactive { get; set; }
      
       

    }
}
