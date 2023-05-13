using System.Collections.Generic;

namespace hospital.Models
{
    public class BloodGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public List<Patient> Patients { get; set; }
        public bool IsDeactive { get; set; }
    }
}
