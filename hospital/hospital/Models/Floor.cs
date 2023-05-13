using System.Collections.Generic;

namespace hospital.Models
{
    public class Floor
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public List<RoomMaster> RoomMasters { get; set; }
        public bool IsDeactive { get; set; }
    }
}
