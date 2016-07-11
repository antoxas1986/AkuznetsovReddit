using AkuznetsovReddit.Core.Models;
using System.Web;
using System.Web.SessionState;

namespace AkuznetsovReddit.Core.Utilities
{
    /// <summary>
    /// Session manager class
    /// </summary>
    public class SessionManager
    {
        public static UserDto User
        {
            get { return ((Session["User"] is UserDto) ? (UserDto)Session["User"] : null); }
            set { Session["User"] = value; }
        }

        private static HttpSessionState Session
        {
            get
            {
                return (HttpContext.Current == null) ? null : HttpContext.Current.Session;
            }
        }

        public static void Abandon()
        {
            Session.Abandon();
        }
    }
}
