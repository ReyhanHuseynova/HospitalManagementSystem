using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public bool Gender { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]

        public int Age { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]
       
        public string Number { get; set; }
        [Required]
        public string Birthday { get; set; }
        [Required]
        
        public DateTime RegDate { get; set; }
       

        public BloodGroup BloodGroup { get; set; }
        public int BloodGroupId { get; set; }


        public bool IsDeactive { get; set; }
        public List<AppDetail> AppDetails { get; set; }



    }
}
