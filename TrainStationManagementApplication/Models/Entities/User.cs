namespace TrainStationManagementApplication.Models.Entities
{
    public class User : BaseUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Admin Admin { get; set; }
        public Passenger Passenger { get; set; }
        public Address Address { get; set; }
    }
}
