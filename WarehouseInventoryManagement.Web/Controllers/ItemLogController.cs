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
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public class ItemLogController : Controller
    {
        private readonly IItemService itemService;

        private readonly IItemLogService itemLogService;

        public ItemLogController(IItemService itemService, IItemLogService itemLogService)
        {
            this.itemService = itemService;
            this.itemLogService = itemLogService;
        }

        public ActionResult List(GridSortOptions gridSortOptions, int? page = null, int? itemsPerPage = null, string search = null)
        {
            if (gridSortOptions.Column == null)
            {
                gridSortOptions.Column = "Id";
            }

            var entitiesperPage = !itemsPerPage.HasValue ? PagedEntityListFilterDto.DefaultItemsPerPage : itemsPerPage.Value;
            var currentPage = !page.HasValue ? 1 : page.Value;

            var filter = new PagedEntityListFilterDto
            {
                AscendingOrder = gridSortOptions.Direction == SortDirection.Ascending,
                Column = gridSortOptions.Column,
                StartPage = currentPage,
                ItemsPerPage = entitiesperPage,
                SearchText = search
            };

            var entities = itemService.GetPage(filter);

            var entitiesViewModel = EntityToViewModelMapper.Mapper.Map(entities.Entities, new List<ItemViewModel>());

            var pagination = new CustomPagination<ItemViewModel>(entitiesViewModel, entities.Page, entitiesperPage, entities.Count);

            var entitieslistViewModel = new ItemListViewModel
            {
                Items = pagination,
                GridSortOptions = gridSortOptions,
                Page = currentPage,
                Search = search
            };

            return null;
        }

    }
}
