using System.Collections.Generic;

namespace hospital.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
        public bool IsDeactive { get; set; }
        public List<Product> Products { get; set; }

    }
}
