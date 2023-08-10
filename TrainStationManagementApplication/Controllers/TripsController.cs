using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;
using System.Security.Claims;

namespace TrainStationManagementApplication.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService _tripService;
        private readonly ITrainService _trainService;

        public TripsController(ITripService tripService, ITrainService trainService)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _tripService.GetAllTrips();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTripsOfToday()
        {
            var response = await _tripService.GetAllTripsOfToday();
            return View(response.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTripsOfATrain(string trainNumber)
        {
            var response = await _tripService.GetAllTripsOfATrain(trainNumber);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTripsOfTrainForParticularDay(DateTime date, string trainNumber)
        {
            var response = await _tripService.GetAllTripsOfTrainForParticularDay(date, trainNumber);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> AddTrip(string trainId)
        {
            var train = await _trainService.GetTrain(trainId);
            return View(train);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrip(CreateTripRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _tripService.CreateTrip(model);
                var myId = TempData.Peek("specialId");
                return RedirectToAction("Reciept", new { id = myId });
            }
            ModelState.AddModelError("", "Fill the required details");
            return View(model);
        }


        public async Task<IActionResult> Reciept(string id)
        {
            var train = await _trainService.GetTrainAfterBeingUnAvailable(id);
            if (train.Status == false)
            {
                return Content(train.Message);
            }
            return View(train);

            //for sending pdf is as follows

            //var name = User.FindFirst(ClaimTypes.Name).Value;
            //var amount = train.Data.Amount;
            //var date = DateTime.Now;
            //byte[] pdfBytes = await _tripService.GenerateReceipt(name, amount, date);
            //return File(pdfBytes, "application/pdf", "Receipt.pdf");

        }


        [HttpGet]
        public async Task<IActionResult> GetTrip(string id)
        {
            var response = await _tripService.GetTrip(id);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrip(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _tripService.GetTrip(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost, ActionName("DeleteTrip")]
        public async Task<IActionResult> ConfirmDeleteTrain(string id)
        {
            var response = await _tripService.DeleteTrip(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTrip(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _tripService.GetTrip(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrip(string id, UpdateTripRequestModel model)
        {
            var response = await _tripService.UpdateTrip(id, model);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Transaction(string amount)
        {
            return View();
        }


    }
}
