using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class ProductController : Controller
    {

        [Authorize]
        public virtual ActionResult Index()
        {
            return null;
        }

        [Authorize]
        public virtual ActionResult List()
        {
            return null;
        }

    }
}
