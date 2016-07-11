using AkuznetsovReddit.Core.Constants;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Core.Utilities;
using AkuznetsovReddit.Services.Interfaces;
using AkuznetsovReddit.Web.Filters;
using AkuznetsovReddit.Web.Models;
using AutoMapper;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Controllers
{
    /// <summary>
    /// Controller to perform tasks with posts (articles)
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [MvcAuthorization]
    public class PostsController : Controller
    {
        private IPostsService _postsService;
        private ITopicService _topicService;
        private IUserService _userService;

        public PostsController(IPostsService postsService, ITopicService topicService, IUserService userService)
        {
            _postsService = postsService;
            _topicService = topicService;
            _userService = userService;
        }

        /// <summary>
        /// Method to return create new post view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            ViewBag.TopicId = id;
            return View();
        }

        /// <summary>
        /// Method to create new post.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([System.Web.Http.FromBody]PostFullVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }
            PostFullDto dto = Mapper.Map<PostFullDto>(model);

            ValidationMessageList messages = new ValidationMessageList();

            _postsService.CreatePost(dto, messages);

            if (messages.HasError)
            {
                string error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            return RedirectToAction("Index", new { id = dto.TopicId });
        }

        /// <summary>
        /// Method to delete post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto postDto = _postsService.GetFullPostByPostId(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ViewBag.Error = error;
                return HttpNotFound();
            }

            _postsService.DeletePost(postDto, messages);
            return RedirectToAction("Index", new { id = postDto.TopicId });
        }

        /// <summary>
        /// Method to return post details view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = _postsService.GetFullPostByPostId(id, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ViewBag.Error = error;
                return HttpNotFound();
            }

            ViewBag.User = Mapper.Map<UserVm>(SessionManager.User);
            PostFullVm vm = Mapper.Map<PostFullVm>(post);
            ViewBag.Author = _userService.GetUserByUserId(vm.UserId);
            return View(vm);
        }

        /// <summary>
        /// Method to return edit post view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int topicId, int postId)
        {
            ValidationMessageList messages = new ValidationMessageList();
            PostFullDto post = _postsService.GetFullPostByPostId(postId, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ViewBag.Error = error;
                return HttpNotFound();
            }

            UserDto user = SessionManager.User;

            if (user.Role.RoleName == Roles.ADMIN || user.UserId == post.UserId)
            {
                ViewBag.User = Mapper.Map<UserVm>(user);
                PostFullVm vm = Mapper.Map<PostFullVm>(post);
                return View(vm);
            }

            return RedirectToAction("Forbidden", "Error");
        }

        /// <summary>
        /// Method to perform edit on post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([System.Web.Http.FromBody]PostFullVm post)
        {
            if (ModelState.IsValid)
            {
                ValidationMessageList messages = new ValidationMessageList();
                PostFullDto dto = Mapper.Map<PostFullDto>(post);

                _postsService.EditPost(dto, messages);

                if (messages.HasError)
                {
                    var error = messages.GetFirstErrorMsg;
                    ModelState.AddModelError(string.Empty, error);
                    return View("Edit", post);
                }
                return RedirectToAction("Details", new { id = post.PostId });
            }

            return View(post);
        }

        /// <summary>
        /// Method to return index post view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            UserVm user = Mapper.Map<UserVm>(SessionManager.User);
            ViewBag.User = user;
            ValidationMessageList messages = new ValidationMessageList();

            if (user.Role.RoleName.Equals(Roles.ADMIN))
            {
                AdminTopicWithPostsDto dto = _topicService.GetTopicWithPostsByTopicId<AdminTopicWithPostsDto>(id, messages);
                TopicWithPostsVm vms = Mapper.Map<TopicWithPostsVm>(dto);
                return View(vms);
            }

            TopicWithPostsDto topic = _topicService.GetTopicWithPostsByTopicId<TopicWithPostsDto>(id, messages);
            TopicWithPostsVm vm = Mapper.Map<TopicWithPostsVm>(topic);
            return View(vm);
        }

        /// <summary>
        /// Method to restore deleted post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Restore(int topicId, int postId)
        {
            ValidationMessageList messages = new ValidationMessageList();
            _postsService.RestorePost(postId, messages);

            if (messages.HasError)
            {
                var error = messages.GetFirstErrorMsg;
                ModelState.AddModelError(string.Empty, error);
                return View("Index", new { id = topicId });
            }

            return RedirectToAction("Index", new { id = topicId });
        }
    }
}
