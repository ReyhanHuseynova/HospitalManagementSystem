using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace hospital.Models
{
    public class AppDetail
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor{ get; set; }
        public Patient Patient{ get; set; }
        public int PatientId{ get; set; }
        public int AppointmentId{ get; set; }
        public Appointment Appointment { get; set; }
       
    }
}
