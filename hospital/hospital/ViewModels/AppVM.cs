using hospital.Models;
using System.Collections.Generic;

namespace hospital.ViewModels
{
    public class AppVM
    {
        public List<Appointment> Appointments { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
