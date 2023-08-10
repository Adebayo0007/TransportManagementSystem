using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;
using TrainStationManagementApplication.Service.Implementation;

namespace TrainStationManagementApplication.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _transactionService.GetAllTransactions();
            return View(response);
        }
        [HttpGet]
        public async Task AddTransaction(string trainId)
        {
            await _transactionService.CreateTransaction(trainId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsOfAPassenger(string passengerId)
        {
            if(ModelState.IsValid)
            { 
                var response = await _transactionService.GetAllTransactionsOfAPassenger(passengerId);
                return View(response);
            }
            ModelState.AddModelError("", "Fill the required details");
            return View(passengerId);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTransactionsOfTrainForAParticularDate(string trainNumber, DateTime date)
        {
            var response = await _transactionService.GetAllTransactionsOfTrainForAParticularDate(trainNumber, date);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction(string transactionId)
        {
            var response = await _transactionService.GetTransaction(transactionId);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTransaction(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                return Content("Value cannot be null");
            }

            var response = await _transactionService.GetTransaction(transactionId);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost,ActionName("DeleteTransaction")]
        public async Task<IActionResult> ConfirmDeleteTransaction(string transactionId)
        {
            var response = await _transactionService.DeleteTransaction(transactionId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTransaction(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                return Content("Value cannot be null");
            }

            var response = await _transactionService.GetTransaction(transactionId);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTransaction(string id, UpdateTransactionRequestModel model)
        {
            var response = await _transactionService.UpdateTransaction(id, model);
            return RedirectToAction("Index");
        }
    }
}
