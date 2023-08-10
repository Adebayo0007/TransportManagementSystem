using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _userService.GetAllUsers();
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> LoginUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserRequestModel model)
        {
           if (!ModelState.IsValid)
           {
                return View(model);
           }

            var user = await _userService.Login(model);
            if (!user.Status)
            {
                TempData["success"] = user.Message;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Data.Id),
                new Claim(ClaimTypes.Role, user.Data.Role),
                new Claim(ClaimTypes.Email, user.Data.Email),
                new Claim(ClaimTypes.Name, $"{user.Data.FirstName} {user.Data.LastName}"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (user.Data.Role.Equals("Admin"))
            {
                return RedirectToAction("Index", "Admins");
            }
            else if (user.Data.Role.Equals("Passenger"))
            {
                return RedirectToAction("AvailableTrains", "Trains");
            }

            return RedirectToAction("AddPassenger", "Passengers");
        }

        public IActionResult LogOut()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginUser");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user.Status == true)
            {
                return View(user);
            }
            return Content(user.Message);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var user = await _userService.GetUserById(id);

            if (user.Status == true)
            {
                return View(user);
            }
            return Content(user.Message);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Value cannot be null");
            }

            var user = await _userService.GetUserById(id);

            if (user.Status == true)
            {
                return View(user);
            }
            return Content(user.Message);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserRequestModel model)
        {
            var response = await _userService.UpdateUser(id, model);
            return RedirectToAction("Index");
        }
    }
}
