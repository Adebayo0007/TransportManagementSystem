using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Controllers
{
    public class TrainsController : Controller
    {
        private readonly ITrainService _trainService;

        public TrainsController(ITrainService trainService)
        {
            _trainService = trainService ?? throw new ArgumentNullException(nameof(trainService));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _trainService.GetAllTrain();
            return View(response.Data);
        }

        public async Task<IActionResult> AvailableTrains()
        {
            var response = await _trainService.GetAvailableTrains();
            return View(response.Data);
        }

         public async Task<IActionResult> UnAvailableTrains()
        {
            var response = await _trainService.GetUnAvailableTrains();
            return View(response.Data);
        }
          public async Task<IActionResult> UpdateTrainBackToAvailable(string trainId)
        {
            await _trainService.UpdateTrainBackToAvailable(trainId);
            return RedirectToAction("UnAvailableTrains");
        }

        [HttpGet]
        public async Task<IActionResult> AddTrain()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrain(CreateTrainRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _trainService.CreateTrain(model);
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
        public async Task<IActionResult> GetTrainById(string id)
        {
            var response = await _trainService.GetTrain(id);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetTrainsByName(string name)
        {
            var response = await _trainService.GetTrainsByName(name);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTrain(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _trainService.GetTrain(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost, ActionName("DeleteTrain")]
        public async Task<IActionResult> ConfirmDeleteTrain(string id)
        {
            var response = await _trainService.DeleteTrain(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTrain(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _trainService.GetTrain(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrain(string id, UpdateTrainRequestModel model)
        {
            var response = await _trainService.UpdateTrain(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTrainToIsAvailable(string trainNumber)
        {
            if (string.IsNullOrEmpty(trainNumber))
            {
                return Content("Value cannot be null");
            }

            var response = await _trainService.GetTrainByNumber(trainNumber);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost, ActionName("UpdateTrainToIsAvailable")]
        public async Task<IActionResult> ConfirmUpdateTrainToIsAvailable(string trainNumber)
        {
            var response = await _trainService.GetTrainByNumber(trainNumber);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTrainToNotAvailable(string trainNumber)
        {
            if (string.IsNullOrEmpty(trainNumber))
            {
                return Content("Value cannot be null");
            }

            var response = await _trainService.GetTrainByNumber(trainNumber);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost, ActionName("UpdateTrainToNotAvailable")]
        public async Task<IActionResult> ConfirmUpdateTrainToNotAvailable(string trainNumber)
        {
            var response = await _trainService.GetTrainByNumber(trainNumber);
            return RedirectToAction("Index");
        }
    }
}


