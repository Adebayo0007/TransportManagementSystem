using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Controllers
{
    public class PassengersController : Controller
    {
        private readonly IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var responses = await _passengerService.GetAllPassengers();
            return View(responses.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddPassenger()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassenger(CreatePassengerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _passengerService.CreatePassenger(model);
                if (response.Status == false)
                {
                    return Content(response.Message);
                }
                return RedirectToAction("Index");
            }
                ModelState.AddModelError("", "Fill the required details");
                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPassenger(string id)
        {
            var response = await _passengerService.GetPassenger(id);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePassenger(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _passengerService.GetPassenger(id);
            if (response.Status == false)
            {
                return Content(response.Message);
            }   
            return View(response);

        }
        [Authorize]
        [HttpPost,ActionName("DeletePassenger")]
        public async Task<IActionResult> ConfirmDeletePassenger(string id)
        {
            var response = await _passengerService.DeletePassenger(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassenger(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _passengerService.GetPassenger(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassenger(string id, UpdatePassengerRequestModel model)
        {
            var response = await _passengerService.UpdatePassenger(id, model);
            if(User.FindFirst(ClaimTypes.Role).Value == "Admin")
            {
              return RedirectToAction("Index");
            }
            var passengerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return RedirectToAction("GetPassenger", new {id = passengerId});
        }
    }
}
