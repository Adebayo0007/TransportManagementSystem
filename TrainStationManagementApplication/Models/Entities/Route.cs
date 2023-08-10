namespace TrainStationManagementApplication.Models.Entities
{
    public class Route
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public string Distance { get; set; }

    }


}
