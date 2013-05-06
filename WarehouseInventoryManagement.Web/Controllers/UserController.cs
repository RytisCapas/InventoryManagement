using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Models.User;
using WarehouseInventoryManagement.ServiceContracts;
using SortDirection = MvcContrib.Sorting.SortDirection;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class UserController : Controller
    {
        private readonly IUserService userService;
        
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public virtual ActionResult Index()
        {
            return null;
        }

        public virtual ActionResult List(GridSortOptions gridSortOptions, int? page = null, int? itemsPerPage = null, string search = null)
        {
            if (gridSortOptions.Column == null)
            {
                gridSortOptions.Column = "Id";
            }

            var entitiesperPage = !itemsPerPage.HasValue ? PagedEntityListFilterDto.DefaultItemsPerPage : itemsPerPage.Value;
            var currentPage = !page.HasValue? 1 : page.Value;

            var userFilter = new PagedEntityListFilterDto
            {
                AscendingOrder = gridSortOptions.Direction == SortDirection.Ascending,
                Column = gridSortOptions.Column,
                StartPage = currentPage,
                ItemsPerPage = entitiesperPage,
                SearchText = search
            };

            var users = userService.GetPage(userFilter);

            var userEntitiesViewModel = EntityToViewModelMapper.Mapper.Map(users.Entities, new List<UserViewModel>());

            var pagination = new CustomPagination<UserViewModel>(userEntitiesViewModel, users.Page, entitiesperPage, users.Count);

            var usersViewModel = new UserListViewModel
            {
                Users = pagination,
                GridSortOptions = gridSortOptions,
                Page = currentPage
            };



            return View();
        }

    }
}
