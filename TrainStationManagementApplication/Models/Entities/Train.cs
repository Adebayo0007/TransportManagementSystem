using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using TrainStationManagementApplication.Models.Enums;

namespace TrainStationManagementApplication.Models.Entities
{
    public class Train :BaseEntity
    {
        public string Name { get; set; }
        public string TrainNumber { get; set; }
        public int Capacity { get; set; }
        public int AvailableSpace { get; set; }
        public bool IsAvailable { get; set; }
        public int SeatTrack { get; set; }
        public Route Route { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public DateTime DepartureTime { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
