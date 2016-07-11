using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Models;
using AutoMapper;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Controllers
{
    /// <summary>
    /// Controller to register new Account
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class RegisterController : Controller
    {
        private IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        /// <summary>
        /// Method to return index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Register";
            return View();
        }

        /// <summary>
        /// Method to register new account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([System.Web.Http.FromBody]RegisterVm model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            RegisterDto dto = Mapper.Map<RegisterDto>(model);

            ValidationMessageList messages = new ValidationMessageList();

            _registerService.RegisterUser(dto, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Index");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
