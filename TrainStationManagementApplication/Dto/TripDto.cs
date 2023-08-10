using System.ComponentModel.DataAnnotations;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Models.Enums;

namespace TrainStationManagementApplication.Dto
{
    public class TripDto
    {
        public string Id { get; set; }
        public string TripName { get; set; }
        public string Destination { get; set; }
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public int SeatNumber { get; set; }
        public string Distance { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string RoleName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public int Capacity { get; set; }
        public int AvailableSpace { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class CreateTripRequestModel
    {
        [Required(ErrorMessage = "Please Provide TripName")]
        public string TripName { get; set; }
        [Required(ErrorMessage = "Please Provide Destination")]
        public string Destination { get; set; }
      
       
        [Required(ErrorMessage = "Please Provide Distance")]
        public string Distance { get; set; }
        public string TrainId { get; set; }
    }


    public class UpdateTripRequestModel
    {

        [Required(ErrorMessage = "Please Provide TripName")]
        public string TripName { get; set; }
        [Required(ErrorMessage = "Please Provide Destination")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Please Provide StartingStation")]
        public string StartingStation { get; set; }
        [Required(ErrorMessage = "Please Provide EndingStation")]
        public string EndingStation { get; set; }
        [Required(ErrorMessage = "Please Provide DepartureTime")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Please Provide SeatNumber")]
        public int SeatNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Distance")]
        public string Distance { get; set; }
    }
}
