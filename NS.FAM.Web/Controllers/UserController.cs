using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NS.FAM.Business;
using NS.FAM.Data.CustomEntities;
using NS.FAM.Web.Models;

namespace NS.FAM.Web.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
    public readonly IUserBusiness _iUserBusiness;

    public UserController(ILogger<UserController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IUserBusiness iUserBusiness)
    {
        _logger = logger;
        _environment = environment;
        _iUserBusiness = iUserBusiness;

    }

    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        try
        {
            var countryList = await _iUserBusiness.GetCountryList();
            ViewBag.Country = new SelectList(countryList, "CountryId", "CountryName");
            return View();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserViewModel userViewModel, IFormFile ProfilePic)
    {
        try
        {
            string wwwPath = this._environment.WebRootPath;
            string contentPath = this._environment.ContentRootPath;
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<string> uploadedFiles = new List<string>();
            string fileName = Path.GetFileName(ProfilePic.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                ProfilePic.CopyTo(stream);
                uploadedFiles.Add(fileName);
                ViewBag.Message += string.Format("<b>{0}</b> Profile pic uploaded.<br />", fileName);
            }
            userViewModel.Password = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);
            userViewModel.ProfilePic = fileName;
            userViewModel.Gender = userViewModel.Gender;

            userViewModel.CountryId = userViewModel.CountryId;
            userViewModel.StateId = userViewModel.StateId;
            userViewModel.CityId = userViewModel.CityId;

            await _iUserBusiness.AddUser(userViewModel);
            return RedirectToAction(actionName: "SignUp", controllerName: "User");
        }
        catch (Exception ex)
        {
            // _iloggingBusiness.LogError("AddUser", User.Identity.Name, ex);
            // return View("Error", new ErrorViewModel { ErrorMessage = ex.Message });
            throw new Exception("", ex);
        }
    }

      public JsonResult GetState(string id)
        {
            try
            {
                int countryId = Convert.ToInt32(id);
                var states = _iUserBusiness.GetStateItems(countryId);
                return Json(states);
            }

            catch (Exception ex)
            {
                // _iloggingBusiness.LogError("GetState", User.Identity.Name, ex);
                throw new Exception(ex.Message);

            }
        }

          public JsonResult GetCity(string id)
        {
            try
            {
                int stateId = Convert.ToInt32(id);
                var cities = _iUserBusiness.GetCityItems(stateId);
                return Json(cities);
            }
            catch (Exception ex)
            {
                // _iloggingBusiness.LogError("GetCity", User.Identity.Name, ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task<IActionResult> Users(string searchData)
        {
            try
            {
                var UserDetail = await _iUserBusiness.GetAllUsers(searchData);

                return View(UserDetail);
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

          public async Task<IActionResult> ActivateDeactivateRecord(int Id)
        {
            try
            {
                await _iUserBusiness.ActivateDeactivateRecord(Id);
                return RedirectToAction(actionName: "Users", controllerName: "User");
            }
            catch (Exception ex)
            {
                // _iloggingBusiness.LogError("ActivateDeactivateRecord", User.Identity.Name, ex);
                // return View("Error", new ErrorViewModel { ErrorMessage = ex.Message });
                  throw new Exception(ex.Message);

            }
        }

          [HttpGet]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                long userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                var userProfile = await _iUserBusiness.GetProfileById(userId);
                return View(userProfile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
