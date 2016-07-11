using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Filters;
using AkuznetsovReddit.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Controllers
{
    /// <summary>
    /// Controller for admin tasks
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [MvcAuthorization]
    public class AdminController : Controller
    {
        private IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Method to return list of all users in the Database
        /// </summary>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Index()
        {
            ICollection<UserDto> dtos = _userService.GetAllUsers();
            ICollection<UserVm> vms = Mapper.Map<ICollection<UserVm>>(dtos);
            ViewBag.User = Mapper.Map<UserDto>(SessionManager.User);
            return View(vms);
        }
    }
}
