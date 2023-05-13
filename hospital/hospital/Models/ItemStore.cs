using System.Collections.Generic;

namespace hospital.Models
{
    public class ItemStore
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public bool IsDeactive { get; set; }
        public List<Inventory> Inventories { get; set; }

    }
}
