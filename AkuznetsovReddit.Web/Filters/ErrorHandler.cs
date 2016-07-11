using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Filters
{
    public class ErrorHandler : IResultFilter, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            HttpException httpException = filterContext.Exception as HttpException;
            ExecuteCustomViewResult(filterContext.Controller.ControllerContext, "~/Views/Error/ServerError.cshtml");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            HttpStatusCodeResult httpStatusCodeResult = filterContext.Result as HttpStatusCodeResult;

            if (httpStatusCodeResult == null)
            {
                return;
            }

            if (httpStatusCodeResult.StatusCode == (int)HttpStatusCode.NotFound)
            {
                ExecuteCustomViewResult(filterContext.Controller.ControllerContext, "~/Views/Error/NotFound.cshtml");
            }
            else if (httpStatusCodeResult.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                ExecuteCustomViewResult(filterContext.Controller.ControllerContext, "~/Views/Error/ServerError.cshtml");
            }
            else if (httpStatusCodeResult.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                ExecuteCustomViewResult(filterContext.Controller.ControllerContext, "~/Views/Error/Forbidden.cshtml");
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        private void ExecuteCustomViewResult(ControllerContext controllerContext, string viewName)
        {
            var viewResult = new ViewResult
            {
                ViewName = viewName,
                ViewData = controllerContext.Controller.ViewData,
                TempData = controllerContext.Controller.TempData
            };
            viewResult.ExecuteResult(controllerContext);
            controllerContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
