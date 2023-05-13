using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Number { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDeactive { get; set; }
        public List<AppDetail> AppDetails { get; set; }
        public List<OPD> OPDs { get; set; }
       
      

    }   
}   
