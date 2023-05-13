using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace hospital.Models
{
    public class OPD
    {
        public int Id { get; set; }
        public string PatientCode { get; set; }
        public string PatientFullName { get; set; }
        public RoomBed RoomBed { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public int RoomBedId { get; set; }
        public string DateAdmit { get; set; }
        public string DateDischarge { get; set; }
        public bool IsDeactive { get; set; }
       
    }
}
