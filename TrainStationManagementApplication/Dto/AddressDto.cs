using System.ComponentModel.DataAnnotations;

namespace TrainStationManagementApplication.Dto
{
    public class AddressDto
    {
        public string Id { get; set; } 
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class CreateAddressRequestModel
    {
        [Required(ErrorMessage = "Please Provide Your Street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Please Provide Your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Provide Your State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Provide Your Country")]
        public string Country { get; set; }
    }

    public class UpdateAddressRequestModel
    {
        [Required(ErrorMessage = "Please Provide Your Street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Please Provide Your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Provide Your State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Provide Your Country")]
        public string Country { get; set; }
    }
}
