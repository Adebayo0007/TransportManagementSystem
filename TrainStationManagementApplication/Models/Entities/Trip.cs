namespace TrainStationManagementApplication.Models.Entities
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; }
        public string Destination { get; set; }
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public int SeatNumber { get; set; }
        public string Distance { get; set; } 
        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public string TrainId { get; set; }
        public string TrainNumber { get; set; }
        public Train Train { get; set; }
    }
}
