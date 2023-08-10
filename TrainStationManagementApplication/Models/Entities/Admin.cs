using System.ComponentModel.DataAnnotations.Schema;

namespace TrainStationManagementApplication.Models.Entities
{
    public class Admin
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        public string StaffNumber { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
