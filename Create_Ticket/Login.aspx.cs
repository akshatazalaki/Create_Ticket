using Create_Ticket.Assest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;
namespace Create_Ticket
{
    public partial class Login : System.Web.UI.Page
    {
        DBCalling Db = new DBCalling();
        //for creating a log errors
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
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
            try
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
                    Cache["username"] = UserName;
                    Cache["IsAuthenticated"] = true;
                    Cache["LastAccessed"] = DateTime.Now;
                    string uniqueId = Guid.NewGuid().ToString();
                    Cache["UniqueId"] = uniqueId;
                    Session["LoginTime"] = DateTime.Now;
                    // Creating a cookie for the session (optional)

                    HttpCookie authCookie = new HttpCookie("authCookie");
                    authCookie.Values["username"] = UserName;
                    authCookie.Expires = DateTime.Now.AddHours(12);
                    Response.Cookies.Add(authCookie);

                    StoreEnhancedBrowserFingerprintInSession();
                    Response.Redirect("~/Default.aspx",false);
                    return;


                }
                else
                {
                    lblMessage.Text = "Invalid UserName and Password";
                    TxtUserName.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error in btnLogin_Click");
                lblMessage.Text = "An error occurred during login.";
            }
        }

        private void StoreEnhancedBrowserFingerprintInSession()
        {
            string browserFingerprint = BrowserIdentifier.GenerateBrowserFingerprint(Request);
            //Session["BrowserFingerprint"] = EncryptionHelper.Encrypt(browserFingerprint);
            Cache["BrowserFingerprint"] = EncryptionHelper.Encrypt(browserFingerprint);
        }

        private void LogError(Exception ex, string message)
        {
            Logger.Error(ex, message);

            try
            {
                string logDirectory = Server.MapPath($"~/Logs/{DateTime.Now:yyyy-MM-dd}");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFilePath = Path.Combine(logDirectory, "ErrorLog.txt");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n{ex}\n";

                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception fileEx)
            {
                Logger.Error(fileEx, "Error while writing to log file");
            }
        }
    }
}