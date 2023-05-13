using hospital.Models;
using System.Collections.Generic;

namespace hospital.ViewModels
{
    public class RoomVM
    {
        public List<RoomMaster> RoomMasters { get; set; }
        public List<RoomBed> RoomBeds { get; set; }
        public List<RoomCategory> RoomCategories { get; set; }
        public List<AppDetail> AppDetails { get; set; }
        public List<OPD> OPDs { get; set; }
    }
}
