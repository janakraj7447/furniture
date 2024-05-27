using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NS.FAM.Business;
using NS.FAM.Data.CustomEntities;

namespace NS.FAM.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserBusiness _iUserBusiness;

        public LoginController(IUserBusiness iUserBusiness)
        {
            _iUserBusiness = iUserBusiness;

        }
        public async Task<IActionResult> SignIn()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            try
            {
                var userDetails = await _iUserBusiness.GetUserDetailsByEmail(loginViewModel.Email);
                if (userDetails != null && userDetails.IsActive && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, userDetails.Password))
                {
                    TempData["WelcomeMessage"] = "Welcome, " + userDetails.FirstName + " " + userDetails.LastName;
                    var claims = new Claim[] { new Claim(ClaimTypes.Email, userDetails.Email), new Claim(ClaimTypes.Role, userDetails.RoleId.ToString()), new Claim(ClaimTypes.Name, userDetails.FirstName.ToString()), new Claim("UserId", userDetails.Id.ToString()) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    if (userDetails.RoleId == 1)
                    {
                        return RedirectToAction("Users", "User");
                    }
                    else
                    {
                        return RedirectToAction("UserProducts", "Product");
                    }
                }
                else if (userDetails != null && !userDetails.IsActive)
                {
                    ViewData["Errormsg"] = "In-Active User";
                    return View("Index");
                }
                else
                {
                    ViewData["Errormsg"] = "Incorrect Email or Password";
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("SignIn", "Login");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
