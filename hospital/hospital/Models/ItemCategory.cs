using System.Collections.Generic;

namespace hospital.Models
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
     
        public List<Product> Products { get; set; }
        
      
        public string Description { get; set; }
        public bool IsDeactive { get; set; }
       
    }
}
