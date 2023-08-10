using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _addressService.GetAllAddress();
            return View(response);
        }


    }
}
