using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace hospital.Models
{
    public class RoomBed
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public RoomMaster RoomMaster { get; set; }
        public int RoomMasterId { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsMain { get; set;}
        public List<OPD> OPDs { get; set; }
    }
}
