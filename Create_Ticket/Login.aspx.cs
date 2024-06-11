using Create_Ticket.Assest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Create_Ticket
{
    public partial class Login : System.Web.UI.Page
    {
        DBCalling Db = new DBCalling();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            CredentialValidation();
        }

        public void CredentialValidation()
        {
            string UserName = TxtUserName.Text;
            string Password = TxtPassword.Text;

            if (UserName == null && Password == null)
            {
                lblMessage.Text = "Please Enter UserName and Password";
            }
            else if (UserName == "")
            {
                lblMessage.Text = "Please Enter UserName";
                TxtUserName.Text = string.Empty;

            }
            else if (Password == "")
            {
                lblMessage.Text = "Please enter password";
                TxtPassword.Text = string.Empty;

            }
            else if (Db.LoginCredentials(UserName, Password).Equals("Login successful"))
            {
                Session["username"] = UserName;
                Session["IsAuthenticated"] = true;
                Response.Redirect("Default.aspx");

            }
            else
            {
                lblMessage.Text = "Invalid UserName and Password";
                TxtUserName.Text = string.Empty;

            }
        }
    }
}