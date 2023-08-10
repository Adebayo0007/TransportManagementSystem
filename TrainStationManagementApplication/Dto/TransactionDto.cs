using System.ComponentModel.DataAnnotations;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Models.Enums;

namespace TrainStationManagementApplication.Dto
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
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
        public string ReferenceNumber { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public string DepartureTime { get; set; }
        public int SeatNumber { get; set; }
        public double Distance { get; set; }
        public string TrainNumber { get; set; }
    }

    

    public class UpdateTransactionRequestModel
    {
        [Required(ErrorMessage = "Please Provide Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please Provide FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Provide LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Provide PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please Provide ReferenceNumber")]
        public string ReferenceNumber { get; set; }
        [Required(ErrorMessage = "Please Provide DateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
