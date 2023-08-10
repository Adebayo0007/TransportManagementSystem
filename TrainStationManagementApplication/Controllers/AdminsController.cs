using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Implementation;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }

        public async Task<IActionResult > Index()
        {
            var response = await _adminService.GetAllAdmins();
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(CreateAdminRequestModel model)
        {
            //model.Image = Request.Form.Files[0];
            if (ModelState.IsValid) 
            { 
                var response = await _adminService.CreateAdmin(model);
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
        public async Task<IActionResult> GetAdmin(string id)
        {
            var response = await _adminService.GetAdmin(id);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _adminService.GetAdmin(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }

            return View(response.Data);
        }

        [HttpPost,ActionName("DeleteAdmin")]
        public async Task<IActionResult> ConfirmDeleteAdmin(string id)
        {
            var response = await _adminService.DeleteAdmin(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAdmin(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _adminService.GetAdmin(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }

            return View(response.Data);
        }

        [HttpPost,ActionName("UpdateAdmin")]
        public async Task<IActionResult> ConfirmUpdateAdmin(string id, UpdateAdminRequestModel model)
        {

            if (!ModelState.IsValid)
            {
                return Content("check your input");
            }
            var response = await _adminService.UpdateAdmin(id, model);
            return RedirectToAction("index");
        }
    }
}