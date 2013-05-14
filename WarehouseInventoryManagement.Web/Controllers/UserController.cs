using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using WarehouseInventoryManagement.DataEntities.Entities;
using WarehouseInventoryManagement.DataEntities.Enums;
using WarehouseInventoryManagement.Models.Dtos;
using WarehouseInventoryManagement.Models.Mappers.EntityToViewModel;
using WarehouseInventoryManagement.Models.Mappers.ViewModelToEntity;
using WarehouseInventoryManagement.Models.Models.Message;
using WarehouseInventoryManagement.Models.Models.User;
using WarehouseInventoryManagement.ServiceContracts;
using WarehouseInventoryManagement.Tools.Helpers;
using SortDirection = MvcContrib.Sorting.SortDirection;

namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class UserController : Controller
    {
        private readonly IUserService userService;

        private readonly ICryptoService cryptoService;

        public UserController(IUserService userService, ICryptoService cryptoService)
        {
            this.userService = userService;
            this.cryptoService = cryptoService;
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

        [HttpPost]
        [Authorize]
        public virtual ActionResult Delete(int id, string returnUrl)
        {
            var content = string.Empty;
            var success = true;
            var model = new MessageViewModel {IsError = true, Message = string.Empty};

            
            if (id > 0)
            {
                try
                {
                    var user = userService.GetUserById(id);

                    if (user != null)
                    {
                        userService.Delete(id);
                    }
                    else
                    {
                        model.Message = "Nepavyko ištrinti, vartotojas nerastas";
                        success = false;     
                    }

                }
                catch (Exception ex)
                {
                    model.Message = "Trinant įvyko klaida, bandykite dar kartą";
                    success = false;
                }

            }
            else
            {
                model.Message = "Nepavyko ištrinti, vartotojas nerastas";
                success = false;         
            }

            if (!success)
            {
                content = this.RenderPartialView(MVC.Shared.Views.Partial.Message, model);
            }

            return Json(new
                {
                    Success = success,
                    Content = content
                });
                  
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult Edit(int id, string returnUrl)
        {
            if (id != 0)
            {
                var user = userService.GetUserById(id);

                if (user == null)
                {
                    return RedirectToAction(MVC.User.List());
                }

                var viewModel = EntityToViewModelMapper.Mapper.Map(user, new UserViewModel());

                return View(viewModel);
            }
            return RedirectToAction(MVC.User.List());
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Edit(UserViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var currentUser = userService.GetUserById(model.Id);

                if (currentUser == null)
                {
                    return RedirectToAction(MVC.User.List());
                }

                currentUser = ViewModelToEntityMapper.Mapper.Map(model, currentUser);

                var roles = userService.GetAllRoles();

                currentUser.Roles = new List<Role>();
                
                if (roles != null && roles.Count > 0)
                {
                    if (model.IsAdmin)
                    {
                        currentUser.Roles.Add(roles.Find(x => x.Id == (int)UserRoleEnum.Admin));
                    }
                    if (model.IsWorker)
                    {
                        currentUser.Roles.Add(roles.Find(x => x.Id == (int)UserRoleEnum.Worker));
                    }
                }


                if (!string.IsNullOrEmpty(model.Password))
                {
                    if (model.Password != model.PasswordConfirmation)
                    {
                        ModelState.AddModelError("Password", " Nesutampa įvesti slaptažodžiai.");
                        return View(model);
                    }
                    else
                    {
                        currentUser.PasswordSalt = cryptoService.GetSalt();
                        currentUser.Password = cryptoService.GetHashedValue(model.Password, currentUser.PasswordSalt);
                    }
                }

                var user = userService.SaveUser(currentUser);


                if (user != null)
                {
                    ModelState.Clear();
                    var viewModel = EntityToViewModelMapper.Mapper.Map(user, new UserViewModel());
                    viewModel.Message = new MessageViewModel
                        {
                            IsError = false,
                            Message = "Vartotojo informacija atnaujinta"
                        };
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                model.Message = new MessageViewModel
                    {
                        IsError = true,
                        Message = "Išsaugant įvyko klaida, bandykite dar kartą"
                    };
                return View(model);
            }

            return RedirectToAction(MVC.User.List());

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
