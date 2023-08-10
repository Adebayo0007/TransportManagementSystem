namespace TrainStationManagementApplication.Models.Entities
{
    public class Passenger 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
