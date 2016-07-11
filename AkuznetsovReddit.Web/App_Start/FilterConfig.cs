using AkuznetsovReddit.Web.Filters;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler());
        }
    }
}
