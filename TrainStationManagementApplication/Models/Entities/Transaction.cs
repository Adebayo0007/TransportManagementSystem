using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainStationManagementApplication.Models.Entities
{
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        //public string Id { get; set; } = Guid.NewGuid().ToString().Replace('-', '/').Substring(0, 10);
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime DateCreated { get; set; } 
        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public string TrainId { get; set; }
        public string TrainNumber { get; set; }
        public Train Train { get; set; }
    }
}
