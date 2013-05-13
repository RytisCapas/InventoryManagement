using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity;
using WarehouseInventoryManagement.Models.Models;
using WarehouseInventoryManagement.Models.Models.Message;
using WarehouseInventoryManagement.Models.Models.User;
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

            //var userFilter = new PagedEntityListFilterDto
            //{
            //    AscendingOrder = gridSortOptions.Direction == SortDirection.Ascending,
            //    Column = gridSortOptions.Column,
            //    StartPage = currentPage,
            //    ItemsPerPage = entitiesperPage,
            //    SearchText = search
            //};

            //var users = userService.GetPage(userFilter);

            //var userEntitiesViewModel = EntityToViewModelMapper.Mapper.Map(users.Entities, new List<UserViewModel>());

            //var pagination = new CustomPagination<UserViewModel>(userEntitiesViewModel, users.Page, entitiesperPage, users.Count);

            //var usersViewModel = new UserListViewModel
            //{
            //    Users = pagination,
            //    GridSortOptions = gridSortOptions,
            //    Page = currentPage,
            //    Search = search
            //};

            //return View(usersViewModel);

            return View();
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult CreateItem(MessageViewModel messageModel)
        {
            var model = new ItemCreateViewModel();
            
            model.Message = messageModel == null ? new MessageViewModel() : messageModel;

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

                    message.Message = savedItem.Name + " išsaugotas sėkmingai";

                    return RedirectToAction(MVC.Item.CreateItem(message));

                }
                catch (Exception ex)
                {
                    message.IsError = true;
                    message.Message = "Sukuriant naują įrašą įvyko klaida, bandykite dar kartą";
                }

            }

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

    }
}
