using System.ComponentModel.DataAnnotations;

namespace TrainStationManagementApplication.Dto
{
    public class RouteDto
    {
        public string Id { get; set; }
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public string Distance { get; set; }
    }

    public class CreateRouteRequestModel
    {
        [Required(ErrorMessage = "Please Provide  StartingStation")]
        public string StartingStation { get; set; }
        [Required(ErrorMessage = "Please Provide  EndingStation")]
        public string EndingStation { get; set; }
        [Required(ErrorMessage = "Please Provide  Distance")]
        public string Distance { get; set; }
    }

    public class UpdateRouteRequestModel
    {
        [Required(ErrorMessage = "Please Provide  StartingStation")]
        public string StartingStation { get; set; }
        [Required(ErrorMessage = "Please Provide  EndingStation")]
        public string EndingStation { get; set; }
        [Required(ErrorMessage = "Please Provide  Distance")]
        public string Distance { get; set; }
    }
}
