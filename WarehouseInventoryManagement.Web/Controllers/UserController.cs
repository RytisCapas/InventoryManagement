using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity;
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

        [Authorize]
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
                Page = currentPage,
                Search = search
            };

            return View(usersViewModel);
        }

        public virtual ActionResult Delete(int id, string returnUrl)
        {
            return null;
        }

        public virtual ActionResult Edit(int id, string returnUrl)
        {
            return null;
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult CreateUser()
        {
            var model = new UserCreateViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult CreateUser(UserCreateViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var existing = userService.GetUserByUsername(model.UserName);

                    if (existing == null)
                    {
                        var user = ViewModelToEntityMapper.Mapper.Map(model, new User());

                        var roles = userService.GetAllRoles();
                        
                        if (roles != null && roles.Count > 0)
                        {
                            if (model.IsAdmin)
                            {
                                user.Roles.Add( roles.Find(x => x.Id == (int)UserRoleEnum.Admin ));
                            }
                            if (model.IsWorker)
                            {
                                user.Roles.Add(roles.Find(x => x.Id == (int)UserRoleEnum.Worker));
                            }
                        }
                        

                        user = userService.CreateUser(user);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Vartotojo vardas jau yra sistemoje, pasirinkite kitą.");
                    }
                }
                catch (Exception ex)
                {
                    
                }
                
            }

            return View(model);
        }

    }
}
