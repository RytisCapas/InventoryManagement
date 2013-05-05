using System.Web.Mvc;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.ServiceContracts;
using WarehouseInventoryManagement.Web.Helpers;
using WarehouseInventoryManagement.Web.Logic.Commands.Agreement.CreateAgreement;
using WarehouseInventoryManagement.Web.Logic.Commands.Agreement.GetAgreements;
using WarehouseInventoryManagement.Web.Logic.Models;
using WarehouseInventoryManagement.Web.Logic.Models.Agreement;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class HomeController : Controller
    {
        
        private readonly IUserService userService;
        
        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public virtual ActionResult Index()
        {
            var user = new User
                {
                    UserName = "Administratorius3",
                    Password = "admin5",
                };

            //var user1 = userService.CreateUser(user);

            return null;
        }

    }
}
