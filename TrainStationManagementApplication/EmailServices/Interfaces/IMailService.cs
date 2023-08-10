using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.EmailServices.Interfaces
{
    public interface IMailService
    {
        public Task<bool> SendEMailAsync(CreateMailRequestModel mailRequest);
    }
}
