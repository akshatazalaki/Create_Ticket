using Create_Ticket.Assest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Helpers;


namespace Create_Ticket
{
    public partial class _Default : Page
    {
        DBCalling Db = new DBCalling();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string username = Session["username"] as string;
            string username = Cache["username"] as string;
            if (!IsPostBack)
            {
                
                string csrfToken = Guid.NewGuid().ToString();
                Session["CSRFToken"] = csrfToken;
                hiddenCSRFToken.Value = csrfToken;
                // CheckSessionTimeout();
                ValidateBrowserFingerprint();
                TaskNameDropDown();
                UserNameDropDown();
                PriorityDropDown();
                StatusDropDown();
                GetDetails();
                

            }
            if (Cache["username"] == null)
            {
                Response.Redirect("~/Login.aspx",false);
                return;
            }
            int id = Db.getUserRoleId("getUserCredentialId", username);
            HttpCookie usrnm = Request.Cookies["username"];
            //usrnm.Values.Equals("Admin")
            if (id!=1)
            {
                PanelTicket.Visible = false;
                panelUpdate.Visible = false;
                BtnBack.Visible = true;
                BtnCreate.Visible = false;
                BtnBack.Visible = false;

            }
        }
        private void CheckSessionTimeout()
        {
            if (Cache["IsAuthenticated"] != null && (bool)Cache["IsAuthenticated"])
            {
                DateTime lastAccessed = (DateTime)Cache["LastAccessed"];
                TimeSpan timeSinceLastAccess = DateTime.Now - lastAccessed;

                // Check if more than 1 minute has passed since last access
                if (timeSinceLastAccess.TotalMinutes > 1) // Adjust timeout as needed
                {
                    // Redirect to session timeout prompt page
                    Response.Redirect("~/StayLoggedIn.aspx");
                }
                else
                {
                    // Update last accessed time
                    Cache["LastAccessed"] = DateTime.Now;
                }
            }
            else
            {
                // Session not authenticated, redirect to login page
                Response.Redirect("~/Login.aspx",false);
            }
        }
        private void StatusDropDown()
        {
            try
            {
                // string username = Session["username"] as string;
                string username = Cache["username"] as string;

                DataTable dt = Db.GetDataStatus("sp_getStatus", username, true);
                ddlStatus.DataSource = dt;
                ddlStatus.DataTextField = "Status_name";
                ddlStatus.DataValueField = "id";
                ddlStatus.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select country--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }


        }

        private void PriorityDropDown()
        {
            // throw new NotImplementedException();
            try
            {
                DataTable dt = Db.GetData("sp_getPriority", true);
                ddlPriority.DataSource = dt;
                ddlPriority.DataTextField = "Priority_name";
                ddlPriority.DataValueField = "id";
                ddlPriority.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select country--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UserNameDropDown()
        {
            // throw new NotImplementedException();
            try
            {
                DataTable dt = Db.GetData("sp_getUser", true);
                ddlName.DataSource = dt;
                ddlName.DataTextField = "Name";
                ddlName.DataValueField = "Id";
                ddlName.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select country--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TaskNameDropDown()
        {
            // throw new NotImplementedException();
            try
            {
                DataTable dt = Db.GetData("sp_getTasks", true);
                ddlTask.DataSource = dt;
                ddlTask.DataTextField = "Task_Name";
                ddlTask.DataValueField = "Task_id";
                ddlTask.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select country--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void FnBindGridviewColumns(DataTable dt)
        {
            try
            {
                ButtonField buttonField = new ButtonField();
                buttonField.ButtonType = ButtonType.Image;
                //buttonField.Text = "Edit";
                buttonField.ImageUrl = "~/Assest/icons8-edit-30.png";
                buttonField.ControlStyle.Height = 30;
                buttonField.ControlStyle.Width = 30;
                buttonField.CommandName = "upd";
                Grv.Columns.Add(buttonField);

                int columnsCount = dt.Columns.Count;
                for (int i = 0; i < columnsCount; i++)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = dt.Columns[i].ColumnName;
                    boundField.HeaderText = dt.Columns[i].ColumnName;
                    Grv.Columns.Add(boundField);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void GetDetails()
        {
            //throw new NotImplementedException();
            try
            {
                //string username = Session["username"] as string;
              string username = Cache["username"] as string;   
                int id = Db.getUserRoleId("getUserCredentialId", username);
              //  DataTable dt = new DataTable();
               DataTable dt = await GetTicketFromApiAsync(id,username);
                
               // dt = Db.GetDataDetails("sp_showDetails", id, username, true);
                ViewState["pageindex"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Grv.Columns.Count == 0)
                    {
                        FnBindGridviewColumns(dt);
                    }
                    Grv.DataSource = dt;
                    Grv.DataBind();
                    //ViewState["dt1"] = dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task<DataTable> GetTicketFromApiAsync(int id, string username)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    // string url = $"https://localhost:7155/api/createTicketApi/GetEmployeeTicketDetails/{id},{username}";
                    string url = $"https://localhost:7212/WeatherForecast/{id},{username}";
                    string response = await client.DownloadStringTaskAsync(new Uri(url));
                    return JsonConvert.DeserializeObject<DataTable>(response);
                }
            }
            catch (WebException e)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                // Handle any other exceptions
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        private async Task<bool> CreateTicketAsync(AddTicket ticket)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Construct the URL with parameters
                    var url = $"https://localhost:7155/api/createTicketApi/createTicketEmployee/" +
                              $"{ticket.TaskName}," +
                              $"{ticket.AssignedBy}," +
                              $"{ticket.AssignedTo}," +
                              $"{ticket.StatusDate:yyyy-MM-ddTHH:mm:ss}," +
                              $"{ticket.AssignedDate:yyyy-MM-ddTHH:mm:ss}," +
                              $"{ticket.StatusName}," +
                              $"{ticket.Priority}";

                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(url, null);
                    response.EnsureSuccessStatusCode(); // Throw on error code

                    return true; // Ticket created successfully
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP request error: {e.Message}");
                return false; // Failed to create ticket
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false; // Failed to create ticket
            }
        }

        private async Task<bool> UpdateTicketAsync(AddTicket ticket)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"https://localhost:7155/api/createTicketApi/updateTicketEmployee/" +
                              $"{ticket.Id},"+
                              $"{ticket.StatusName},"+
                              $"{ticket.StatusDate:yyyy-MM-ddTHH:mm:ss}";

                    // Send the PUT request
                    HttpResponseMessage response = await client.PutAsync(url, null);
                    response.EnsureSuccessStatusCode(); // Throw on error code

                    return true; // Ticket updated successfully
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP request error: {e.Message}");
                return false; // Failed to update ticket
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false; // Failed to update ticket
            }
        }

        protected async void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
               
                Assest.AddTicket _cparam = new Assest.AddTicket();
                _cparam.TaskName = Convert.ToInt32(ddlTask.Text); 
                _cparam.AssignedBy = 1;
                _cparam.AssignedTo = Convert.ToInt32(ddlName.Text);

                //_cparam.StatusDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));
                _cparam.StatusDate = DateTime.Now;
                _cparam.StatusName = Convert.ToInt32(ddlStatus.Text);
                _cparam.Priority = Convert.ToInt32(ddlPriority.Text);


                if (BtnCreate.Text == "Create")
                {
                    string sessionToken = Session["CSRFToken"] as string;
                    string formToken = hiddenCSRFToken.Value;

                    if (sessionToken == formToken)
                    {   
                        //_cparam.AssignedDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));
                        _cparam.AssignedDate = DateTime.Now;
                        //Db.AddEmployeeDetails(_cparam);
                        await CreateTicketAsync(_cparam);
                    }

                }
                //else
                //{

                //    panelUpdate.Visible = true;

                //    int TaskId = Convert.ToInt32(ViewState["TaskId"]);


                //    _cparam.Id = Convert.ToInt32(ViewState["TaskId"]);
                //    //_cparam.StatusName = Convert.ToInt32(ViewState["status"]);
                //    _cparam.StatusName = Convert.ToInt32(ddlStatus.SelectedValue);
                //    //_cparam.StatusDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));
                //    _cparam.StatusDate = DateTime.Now;
                //    await UpdateTicketAsync(_cparam);

                //    // Db.UpdatedDetails(_cparam);

                //    ScriptManager.RegisterStartupScript(this, GetType(), "StatusUpdatedPopup", "console.log('Status updated successfully'); alert('Status updated successfully');", true);
                //    BtnCreate.Text = "Create";
                //    // pnlupdateStatus.Visible = false;
                //    panelUpdate.Visible = false;
                //    //PanelBtn.Visible = false;


                //}



                else
                {
                    panelUpdate.Visible = true;

                    _cparam.Id = Convert.ToInt32(ViewState["TaskId"]);
                    _cparam.StatusName = Convert.ToInt32(ddlStatus.SelectedValue);
                    _cparam.StatusDate = DateTime.Now;
                    await UpdateTicketAsync(_cparam);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "StatusUpdatedPopup", "console.log('Status updated successfully'); alert('Status updated successfully');", true);
                    BtnCreate.Text = "Create";
                    panelUpdate.Visible = false;
                }



                PanelGrid.Visible = true;
                //PanelRegister.Visible = false;
                //PanelDeletePopUp.Visible = false;
                //GetFarmerDetails();
                //GetDetails();
                Server.TransferRequest(Request.Url.AbsolutePath, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;



            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx",false);
            return;
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report.aspx",false);
            return;
        }

        protected void Grv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Grv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "upd")
                {

                    ddlName.Enabled = false;
                    ddlPriority.Enabled = false;
                    ddlTask.Enabled = false;
                    PanelGrid.Visible = true;

                    BtnCreate.Text = "Update";
                    panelUpdate.Visible = true;
                    BtnCreate.Visible = true;
                    int rowIdex = int.Parse(e.CommandArgument.ToString());
                    ViewState["TaskId"] = Grv.Rows[rowIdex].Cells[1].Text.ToString();

                    int taskId = Convert.ToInt32(ViewState["TaskId"]);
                    TxtTicket.Text = taskId.ToString();

                    //   ViewState["status"] = Grv.Rows[rowIdex].Cells[7].Text.ToString();


                    string status = Grv.Rows[rowIdex].Cells[6].Text.ToString();
                    ddlStatus.SelectedValue = Db.getStatusId(status, "getStatusId").ToString();

                    PanelBack.Visible = true;

                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void ValidateBrowserFingerprint()
        {
            //string sessionBrowserFingerprint = Session["BrowserFingerprint"] as string;
            string sessionBrowserFingerprint = Cache["BrowserFingerprint"] as string;

            if (sessionBrowserFingerprint != null)
            {
                string currentBrowserFingerprint = BrowserIdentifier.GenerateBrowserFingerprint(Request);
                string decryptedSessionFingerprint = EncryptionHelper.Decrypt(sessionBrowserFingerprint);

                if (decryptedSessionFingerprint != currentBrowserFingerprint)
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
            Response.Redirect("~/Login.aspx", false);
            return;
           // HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}