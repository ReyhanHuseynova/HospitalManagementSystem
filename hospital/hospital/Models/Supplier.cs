using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Phone { get; set; }
        public bool IsDeactive { get; set; }
        public List<Inventory> Inventories { get; set; }


    }
}
