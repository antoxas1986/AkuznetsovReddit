using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Models;
using AutoMapper;
using System;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Controllers
{
    /// <summary>
    /// Controller to perform login tasks
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class LoginController : Controller
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Method to return login page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Login Page";
            return View();
        }

        /// <summary>
        /// Logins the user into session.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([System.Web.Http.FromBody]LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Login");
            }
            LoginDto dto = Mapper.Map<LoginDto>(model);

            ValidationMessageList messages = new ValidationMessageList();

            _loginService.LoginUser(dto, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Index");
            }

            return RedirectToAction("Index", "Topic");
        }

        /// <summary>
        /// Test method to pass input credentials.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login2()
        {
            ValidationMessageList messages = new ValidationMessageList();
            LoginDto dto = new LoginDto()
            {
                UserName = "anton",
                Password = "test"
            };
            _loginService.LoginUser(dto, messages);
            return RedirectToAction("Index", "Topic");
        }

        /// <summary>
        /// Logoffs user from session.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff()
        {
            _loginService.Logoff();
            return RedirectToAction("Index", "Login");
        }
    }
}
