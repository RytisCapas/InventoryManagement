using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseInventoryManagement.Common.Membership;
using WarehouseInventoryManagement.ServiceContracts;
using WarehouseInventoryManagement.Web.Logic.Models;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IUserService userService;
        
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public virtual ActionResult Login(string returnUrl, string message)
        {
            var model = new LogOnModel();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Login(LogOnModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, @"Neteisingai įvestas slaptažodis arba prisijungimo vardas");
                }
                else
                {
                    var user = userService.ValidateUser(model.UserName, model.Password);
                    if (user != null)
                    {
                        WarehouseManagementAuthentication.AuthenticateUser(user.UserName, user.Id, user.FirstName, false);

                        if (Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl.Length > 1 && model.ReturnUrl.StartsWith("/")
                           && !model.ReturnUrl.StartsWith("//") && !model.ReturnUrl.StartsWith("/\\"))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction(MVC.Product.Index());
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, @"Vartotojas nerastas arba slaptažodis - neteisingas");
                    }
                }

            }
            catch (Exception exc)
            {
                ModelState.AddModelError(string.Empty, "Įvyko klaida, bandykite dar kartą.");
            }
            return View(MVC.Account.Views.Login);
        }

    }
}

