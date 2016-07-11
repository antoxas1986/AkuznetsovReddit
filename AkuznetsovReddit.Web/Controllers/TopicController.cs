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
    /// Controller to perform topic tasks.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [MvcAuthorization]
    public class TopicController : Controller
    {
        private ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        /// <summary>
        /// Method to return create view.
        /// </summary>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Method to create new topic.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Create([System.Web.Http.FromBody]TopicVm model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", model);
            }
            TopicDto dto = Mapper.Map<TopicDto>(model);
            ValidationMessageList messages = new ValidationMessageList();
            _topicService.CreateTopic(dto, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Create", model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method to return delete topic view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Delete(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto topic = _topicService.GetTopicByTopicId(id, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Index");
            }
            TopicVm vm = Mapper.Map<TopicVm>(topic);
            return View(vm);
        }

        /// <summary>
        /// Method to delete topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="topic">The topic.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Delete(int id, [System.Web.Http.FromBody] TopicVm topic)
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto dto = Mapper.Map<TopicDto>(topic);
            _topicService.DeleteTopic(id, dto, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Delete", topic);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method to return edit topic view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Edit(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            TopicDto dto = _topicService.GetTopicByTopicId(id, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Edit", new { id = id });
            }

            ViewBag.User = Mapper.Map<UserVm>(SessionManager.User);
            TopicVm vm = Mapper.Map<TopicVm>(dto);
            return View(vm);
        }

        /// <summary>
        /// Method to edit topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Edit(int id, [System.Web.Http.FromBody]TopicVm post)
        {
            if (ModelState.IsValid)
            {
                ValidationMessageList messages = new ValidationMessageList();
                TopicDto dto = Mapper.Map<TopicDto>(post);

                _topicService.EditTopic(id, dto, messages);

                if (messages.HasError)
                {
                    var error = messages.GetFirstErrorMsg;
                    ModelState.AddModelError(string.Empty, error);
                    return View("Edit", post);
                }
                return RedirectToAction("Index");
            }

            return View("Edit", post);
        }

        /// <summary>
        /// Method to return index view of all topics.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.User = Mapper.Map<UserVm>(SessionManager.User);

            ICollection<TopicDto> dtos = _topicService.GetAllTopics();
            ICollection<TopicVm> vms = Mapper.Map<ICollection<TopicVm>>(dtos);

            return View(vms);
        }

        /// <summary>
        /// Method to restore deleted topic.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [MvcAuthorization(Roles = Roles.ADMIN)]
        public ActionResult Restore(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            _topicService.RestoreTopic(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
