using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Doctor> Doctors { get; set; }
      
        public bool IsDeactive { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
