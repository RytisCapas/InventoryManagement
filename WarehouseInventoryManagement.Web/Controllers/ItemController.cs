using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity;
using WarehouseInventoryManagement.Models.Models;
using WarehouseInventoryManagement.Models.Models.Message;
using WarehouseInventoryManagement.ServiceContracts;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [Authorize]
        public virtual ActionResult List(GridSortOptions gridSortOptions, int? page = null, int? itemsPerPage = null, string search = null)
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

            return View(entitieslistViewModel);
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult CreateItem(bool created = false)
        {
            var model = new ItemCreateViewModel();

            model.Message = new MessageViewModel();

            if (created)
            {
                model.Message.IsError = false;
                model.Message.Message = "Naujas įrašas sukurtas sėkmingai";
            }
            

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult CreateItem(ItemCreateViewModel model)
        {
            var message = new MessageViewModel
                        {
                            IsError = false,
                            Message = string.Empty
                        };

            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var item = ViewModelToEntityMapper.Mapper.Map(model, new Item());

                    var savedItem = itemService.Create(item);

                    return RedirectToAction(MVC.Item.CreateItem(true));

                }
                catch (Exception ex)
                {
                    message.IsError = true;
                    message.Message = "Sukuriant naują įrašą įvyko klaida, bandykite dar kartą";
                }

            }

            model = model ?? new ItemCreateViewModel(); 
            model.Message = message;

            return View(model);
        }
        public virtual ActionResult Index()
        {
            var role = new State
                {
                    StateName = "Pašalintas iš sandėlio",
                };
            var savedRole = itemService.Create(role);
             

            return null;
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Delete(Guid id, string returnUrl)
        {
            return null;
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult Edit(Guid id, string returnUrl)
        {
            return null;
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Edit(ItemViewModel model)
        {
            return null;
        }
    }
}
