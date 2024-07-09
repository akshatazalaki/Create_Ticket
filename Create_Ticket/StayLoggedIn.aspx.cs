using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Create_Ticket
{
    public partial class StayLoggedIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check session authentication here if needed
            }
        }

        protected void btnStayLoggedIn_Click(object sender, EventArgs e)
        {
            // Extend session
            Cache["LastAccessed"] = DateTime.Now;

            // Redirect back to the original page
            Response.Redirect("~/Default.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Log out the user
            Session.Abandon();

            // Optionally, clear authentication cookies
            // HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            // authCookie.Expires = DateTime.Now.AddYears(-1);
            // Response.Cookies.Add(authCookie);

            // Redirect to login page
            Response.Redirect("~/Login.aspx");
        }
    }
}