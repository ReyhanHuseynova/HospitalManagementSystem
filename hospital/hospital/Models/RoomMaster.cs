using System.Collections.Generic;

namespace hospital.Models
{
    public class RoomMaster
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public double Rate { get; set; }
        public int TotalBed { get; set; }
        public List<RoomBed> RoomBeds { get; set; }
       
        public RoomCategory RoomCategory { get; set; }
        public int RoomCategoryId { get; set; }
        public Floor Floor { get; set; }
        public int FloorId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
