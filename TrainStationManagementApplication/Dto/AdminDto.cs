using System.ComponentModel.DataAnnotations;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Models.Enums;

namespace TrainStationManagementApplication.Dto
{
    public class AdminDto
    {
        public string Id { get; set; } 
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string RoleName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class CreateAdminRequestModel
    {
        [Required(ErrorMessage = "Please Provide Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Provide Your Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Provide Your FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Provide Your LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Provide Your Image")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Please Provide Your PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Provide Your Gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please Provide Your Street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Please Provide Your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Provide Your State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Provide Your Country")]
        public string Country { get; set; }
    }

    public class UpdateAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string RoleName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
