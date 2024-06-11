using LocalStore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Medlem(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //Kolla inloggning. Om användarnamn och lösenord stämmer kan användaren se "/admin".
        [HttpPost]
        public async Task<IActionResult> Index(AdminModel adminModel, string returnUrl = "")
        {
            bool validUser = CheckUser(adminModel);

            if (validUser == true)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, adminModel.Username));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                
                if (returnUrl != "")
                    return Redirect(returnUrl);
                else 
                    return RedirectToAction("Index", "Home");
            }

            //Om användarnamn och lösenord är fel visas "felmeddelande" på "Medlem"-metoden.
            else 
            {
                ViewBag.ErrorMessage = "Fel användarnamn eller lösenord";
                ViewData["ReturnUrl"] = returnUrl;
                return View("Medlem");

            }

        }

        //Hårdkodad inloggningsuppgifter
        private bool CheckUser(AdminModel adminModel)
        {
            if (adminModel.Username.ToUpper() == "" && adminModel.Password == "")
            {

                return true;
            }

            else
                return false;
        }

        //Logga ut och kom tillbaka på startsidan
        public async Task<IActionResult> SignOutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Utbud()
        {
            return View();
        }


    }
}
    



        


    

