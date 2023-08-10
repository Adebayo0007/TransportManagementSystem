using System.ComponentModel.DataAnnotations;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Models.Enums;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Dto
{
    public class TrainDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TrainNumber { get; set; }
        public int Capacity { get; set; }
        public decimal Amount { get; set; }
        public int AvailableSpace { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public string Distance { get; set; }

    }

    public class CreateTrainRequestModel
    {
        [Required(ErrorMessage = "Please Provide Train Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Provide TrainNumber")]
        public string TrainNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Capacity")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please Provide AvailableSpace")]
        public int AvailableSpace { get; set; }
        [Required(ErrorMessage = "Please Provide Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please Provide IsAvailable")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Please Provide DepartureTime")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Please Provide StartingStation")]
        public string StartingStation { get; set; }
        [Required(ErrorMessage = "Please Provide EndingStation")]
        public string EndingStation { get; set; }
        [Required(ErrorMessage = "Please Provide Distance")]
        public string Distance { get; set; }
    }

    public class UpdateTrainRequestModel
    {
        [Required(ErrorMessage = "Please Provide Train Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Provide TrainNumber")]
        public string TrainNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Capacity")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please Provide AvailableSpace")]
        public int AvailableSpace { get; set; }
        [Required(ErrorMessage = "Please Provide Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please Provide IsAvailable")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Please Provide DepartureTime")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Please Provide StartingStation")]
        public string StartingStation { get; set; }
        [Required(ErrorMessage = "Please Provide EndingStation")]
        public string EndingStation { get; set; }
        [Required(ErrorMessage = "Please Provide Distance")]
        public string Distance { get; set; }
    }
}
