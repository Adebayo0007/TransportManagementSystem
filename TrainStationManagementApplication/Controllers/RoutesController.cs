using Microsoft.AspNetCore.Mvc;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IRouteServicer _routeServicer;

        public RoutesController(IRouteServicer routeServicer)
        {
            _routeServicer = routeServicer ?? throw new ArgumentNullException(nameof(routeServicer));
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _routeServicer.GetAllRoutes();
            return View(routes);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoute()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute(string id, CreateRouteRequestModel model)
        {
            var response = await _routeServicer.CreateRoute(id, model);
            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetRoute(string id)
        {
            var response = await _routeServicer.GetRoute(id);
            if(response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _routeServicer.GetRoute(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost,ActionName("DeleteRoute")]
        public async Task<IActionResult> ConfirmDeleteRoute(string id)
        {
            var response = await _routeServicer.DeleteRoute(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRoute(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var response = await _routeServicer.GetRoute(id);

            if (response.Status == false)
            {
                return Content(response.Message);
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoute(string id, UpdateRouteRequestModel model)
        {
            var response = await _routeServicer.UpdateRoute(id, model);
            return RedirectToAction("Index");
        }
    }
}
