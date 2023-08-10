using TrainStationManagementApplication.Models.Enums;

namespace TrainStationManagementApplication.Models.Entities
{
    public abstract class BaseUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
