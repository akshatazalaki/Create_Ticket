using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Create_Ticket
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["username"] as string;
            if (!IsPostBack)
            {
                if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                // Perform logout actions
                Logout();
                // Redirect to login page
                Response.Redirect("~/Login.aspx");
            }
        }

        private void Logout()
        {
            // throw new NotImplementedException();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["IsAuthenticated"] = false;
            //  Response.Redirect("Login.aspx");
        }
    }
}