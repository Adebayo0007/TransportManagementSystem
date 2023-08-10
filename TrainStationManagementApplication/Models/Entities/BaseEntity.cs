namespace TrainStationManagementApplication.Models.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0,10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}
