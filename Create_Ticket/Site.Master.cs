using Create_Ticket.Assest;
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
            //it will not go back
           
           
            if (!IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                CheckSessionExpiration();
                // UpdateLastActivity();
                //if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
                //{
                //    Response.Redirect("~/Login.aspx");
                //}
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ValidateUniqueId();
                    ValidateBrowserFingerprint();
                    return;
                }

                if (Cache["IsAuthenticated"] == null || !(bool)Cache["IsAuthenticated"])
                {
                    // Redirect to login page
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }
                //if (hfPopupVisible.Value == "true")
                //{
                //    popupDelete.Show();
                //}
            }
          //  hfPopupVisible.Value = GetLastActivity().ToString("o");

        }
        private void CheckSessionExpiration()
        {
            if (Session["LoginTime"] == null)
            {
                // Session login time not set (possibly not logged in)
                Response.Redirect("~/Login.aspx",false); // Redirect to login page
                return;
            }

            DateTime loginTime = (DateTime)Session["LoginTime"];
            int sessionTimeoutMinutes = 1; // Assuming session timeout is 1 minute

            if ((DateTime.Now - loginTime).TotalMinutes >= sessionTimeoutMinutes)
            {
                // Session has expired
                Session.Abandon(); // Clear session
                Session.Clear();
                // Register client-side script to show popup and redirect
                string script = "<script>alert('Your session has expired. Please login again.'); window.location = '/Login.aspx';</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionExpired", script);

                // Additional cleanup if needed
            }
            else
            {
                // Update session login time to current time
                Session["LoginTime"] = DateTime.Now;
            }
        }
        private void UpdateLastActivity()
        {
            Session["LastAccessed"] = DateTime.Now;
        }
        private DateTime GetLastActivity()
        {
            return Session["LastAccessed"] != null ? (DateTime)Session["LastAccessed"] : DateTime.MinValue;
        }

        private void ValidateUniqueId()
        {
            //string uniqueIdFromQuery = Request.QueryString["uid"];
            string uniqueIdFromSession = Cache["UniqueId"] as string;
            if (string.IsNullOrEmpty(uniqueIdFromSession))
            {
                // Unique IDs do not match or are missing, invalidate the session and redirect
                InvalidateSessionAndRedirect();
            }
        }


        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            //if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            //{
            //    // Perform logout actions
            //    Logout();
            //    // Redirect to login page
            //    Response.Redirect("~/Login.aspx");
            //}

            Logout();
        }

        private void Logout()
        {
            HttpCookie authCookie = Request.Cookies["AuthToken"];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }

            // Clear Cache
            Cache["IsAuthenticated"] = false; // Set IsAuthenticated to false
            Cache.Remove("Username"); // Remove any cached user-specific data if needed

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();


            // Clear client-side caching
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Redirect to login page
            Response.Redirect("~/Login.aspx", false);
        }

        private void ValidateBrowserFingerprint()
        {
            //string sessionBrowserFingerprint = Session["BrowserFingerprint"] as string;
            string sessionBrowserFingerprint = Cache["BrowserFingerprint"] as string;

            if (sessionBrowserFingerprint != null)
            {
                string currentBrowserFingerprint = BrowserIdentifier.GenerateBrowserFingerprint(Request);

                if (sessionBrowserFingerprint != currentBrowserFingerprint)
                {
                    // Browser fingerprints do not match, invalidate the session and redirect
                    InvalidateSessionAndRedirect();
                }
            }
            else
            {
                // Browser fingerprint is missing in session, invalidate session and redirect
                InvalidateSessionAndRedirect();
            }
        }



        private void InvalidateSessionAndRedirect()
        {
            //Session.Abandon();
            Cache["IsAuthenticated"] = false; // Set IsAuthenticated to false
            Cache.Remove("Username"); // Remove any cached user-specific data if needed

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

           // Clear client-side caching
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Redirect("~/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        //Logout
        protected void btnStayLoggedIn_Click(object sender, EventArgs e)
        {
          // hfPopupVisible.Value = "true";
                    Logout();
        }
        //stay Loged in
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Cache["LastAccessed"] = DateTime.Now;
            Response.Redirect(Request.Url.ToString(), false);
        }

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    // Check if the user is authenticated and the session is valid
        //    if (HttpContext.Current.Cache["IsAuthenticated"] == null || HttpContext.Current.Cache["LastAccessed"] == null)
        //    {
        //       // hfPopupVisible.Value = "true";
        //        //popupDelete.Show();
        //    }
        //    else
        //    {
        //        var loginTime = HttpContext.Current.Cache["LastAccessed"] as DateTime?;
        //        if (loginTime != null && DateTime.Now.Subtract(loginTime.Value).TotalMinutes > 1)
        //        {

        //           // hfPopupVisible.Value = "true";
        //           // popupDelete.Show();
        //        }
        //        else
        //        {
        //           // hfPopupVisible.Value = "true";
        //           // popupDelete.Show();
        //        }

        //    }            
        //}
    }
}
