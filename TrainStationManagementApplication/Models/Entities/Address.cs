namespace TrainStationManagementApplication.Models.Entities
{
    public class Address
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
