using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Models;
using WarehouseInventoryManagement.Models.Models.Item;
using WarehouseInventoryManagement.ServiceContracts;
using WarehouseInventoryManagement.Tools.Helpers;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class ItemLogController : Controller
    {
        private readonly IItemService itemService;

        private readonly IItemLogService itemLogService;

        public ItemLogController(IItemService itemService, IItemLogService itemLogService)
        {
            this.itemService = itemService;
            this.itemLogService = itemLogService;
        }

        [Authorize]
        public virtual ActionResult List(DateFilter model)
        {
            model = model ?? new DateFilter();
            var entities = itemLogService.GetItemLogs(model);

            var entitiesViewModel = EntityToViewModelMapper.Mapper.Map(entities, new List<ItemLogViewModel>());

            var viewModel = new ItemLogListViewModel
                {
                    DateFilter = model,
                    Logs = entitiesViewModel
                };

            return View(viewModel);
        }

        [Authorize]
        public virtual ActionResult ExportLogsToCsv(DateFilter model)
        {
            model = model ?? new DateFilter();
            var entities = itemLogService.GetItemLogs(model);

            var entitiesViewModel = EntityToViewModelMapper.Mapper.Map(entities, new List<ItemLogViewModel>());

            var date = DateTime.Now.ToShortDateString().Replace(@"/", @"-");

            return new CsvResult<ItemLogViewModel>(entitiesViewModel)
            {
                FileDownloadName = string.Format("Ataskaita_{0}.csv",  date)
            };
        }

    }
}
