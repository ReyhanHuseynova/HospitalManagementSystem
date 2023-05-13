using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hospital.Models
{
    public class RoomCategory
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<RoomMaster> RoomMasters { get; set; }
        public bool IsDeactive { get; set; }
    }
}
