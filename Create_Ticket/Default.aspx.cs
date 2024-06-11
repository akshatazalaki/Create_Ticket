using Create_Ticket.Assest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Create_Ticket
{
    public partial class _Default : Page
    {
        DBCalling Db = new DBCalling();
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["username"] as string;
            if (!IsPostBack)
            {
                TaskNameDropDown();
                UserNameDropDown();
                PriorityDropDown();
                StatusDropDown();
                GetDetails();

            }
            if (Session["username"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            int id = Db.getUserRoleId("getUserCredentialId", username);
            if (id != 1)
            {
                PanelTicket.Visible = false;
                panelUpdate.Visible = false;
                BtnBack.Visible = true;
                BtnCreate.Visible = false;
                BtnBack.Visible = false;

            }
        }

        private void StatusDropDown()
        {
            try
            {
                string username = Session["username"] as string;
                DataTable dt = Db.GetDataStatus("sp_getStatus", username, true);
                ddlStatus.DataSource = dt;
                ddlStatus.DataTextField = "Status_name";
                ddlStatus.DataValueField = "id";
                ddlStatus.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select country--", "0"));
            }
            catch (Exception ex)
            {

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
            { }
        }

        private void GetDetails()
        {
            //throw new NotImplementedException();
            try
            {
                string username = Session["username"] as string;
                DataTable dt = new DataTable();
                int id = Db.getUserRoleId("getUserCredentialId", username);
                dt = Db.GetDataDetails("sp_showDetails", id, username, true);
                ViewState["pageindex"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Grv.Columns.Count == 0)
                    {
                        FnBindGridviewColumns(dt);
                    }
                    Grv.DataSource = dt;
                    Grv.DataBind();
                    // ViewState["dt1"] = dt;
                }
            }
            catch (Exception ex)
            {

            }
        }

       
     

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                Assest.Parameters _cparam = new Assest.Parameters();
                _cparam.TaskName = ddlTask.Text;
                _cparam.AssignedBy = 1;
                _cparam.AssignedTo = Convert.ToInt32(ddlName.Text);

                _cparam.StatusDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));
                _cparam.StatusName = Convert.ToInt32(ddlStatus.Text);
                _cparam.Priority = Convert.ToInt32(ddlPriority.Text);


                if (BtnCreate.Text == "Create")
                {
                    _cparam.AssignedDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));
                    Db.AddEmployeeDetails(_cparam);
                }
                else
                {

                    panelUpdate.Visible = true;

                    int TaskId = Convert.ToInt32(ViewState["TaskId"]);


                    _cparam.Id = Convert.ToInt32(ViewState["TaskId"]);
                    //_cparam.StatusName = Convert.ToInt32(ViewState["status"]);
                    _cparam.StatusName = Convert.ToInt32(ddlStatus.SelectedValue);
                    _cparam.StatusDate = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy/MM/dd"));

                    Db.UpdatedDetails(_cparam);

                    ScriptManager.RegisterStartupScript(this, GetType(), "StatusUpdatedPopup", "console.log('Status updated successfully'); alert('Status updated successfully');", true);
                    BtnCreate.Text = "Create";
                    // pnlupdateStatus.Visible = false;
                    panelUpdate.Visible = false;
                    //PanelBtn.Visible = false;


                }
                //PanelGrid.Visible = true;
                //PanelRegister.Visible = false;
                //PanelDeletePopUp.Visible = false;
                //GetFarmerDetails();
                GetDetails();


            }
            catch (Exception ex) { }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report.aspx");
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
            catch (Exception ex) { }
        }
    }
}