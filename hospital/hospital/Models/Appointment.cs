using System;
using System.Collections.Generic;

namespace hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
       
        public DateTime AppointmentDate { get; set; }
        public DateTime DateVisit { get; set; }
        public bool Status { get; set; }
        public bool IsDeactive { get; set; }
        public List<AppDetail> AppDetails { get; set; }





    }
}
