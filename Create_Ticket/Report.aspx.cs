﻿using Create_Ticket.Assest;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Create_Ticket
{
    public partial class Report : System.Web.UI.Page
    {
        DBCalling Db = new DBCalling();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set default dates
                TxtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
                // DataTable filter = new DataTable();

                // Retrieve data for filter 
                DataTable filter = Db.Card("PRoCName", true);
                ViewState["pageindex"] = filter;

                // Initialize an empty list to store selected records
                ViewState["SelectedRecords"] = new List<string>();

                ReportDropDown();
                PriorityDropDown();
                StatusDropDown();
                //ddlReport_SelectedIndexChanged(null, null);
            }
            else
            {
                // Re-bind GridView and maintain selected records
                BindGridViewData();
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SelectedRecords"] = new List<string>();
                ImageExcel.Visible = true;
                ImagePdf.Visible = true;
                btnReport.Visible = true;
                btnGetSelectedIDs.Visible = true;
                BindGridViewData();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void btnGetSelectedIDs_Click(object sender, EventArgs e)
        {
            UpdateSelectedRecords();
            List<string> selectedTaskIds = (List<string>)ViewState["SelectedRecords"];

            if (selectedTaskIds.Count > 0)
            {
                string selectedIdsText = string.Join(",", selectedTaskIds);
                SelectedIdsLabel.Text = "Selected Task IDs: " + selectedIdsText;
            }
            else
            {
                SelectedIdsLabel.Text = "No Task IDs selected.";
            }
        }
        protected void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = ViewState["grid"] as DataTable;
                string taskId = DataBinder.Eval(e.Row.DataItem, dt.Columns[0].ColumnName).ToString();
                CheckBox cbItem = (CheckBox)e.Row.FindControl("cbItem");

                if (cbItem != null)
                {
                    cbItem.Attributes["data-taskid"] = taskId;

                    List<string> selectedTaskIds = (List<string>)ViewState["SelectedRecords"];
                    if (selectedTaskIds.Contains(taskId))
                    {
                        cbItem.Checked = true;
                    }
                }
            }
        }
        protected void GridViewReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateSelectedRecords();
            GridViewReport.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void BindGridViewData()
        {
            DataTable dt;
            dt = new DataTable();
            DateTime FD;
            DateTime TD;
            if (int.Parse(ddlReport.SelectedValue) == 1)
            {
                FD = Convert.ToDateTime(TxtFromDate.Text);
                TD = Convert.ToDateTime(TxtToDate.Text);
            }
            else
            {
                FD = DateTime.Today.Date;
                TD = DateTime.Today.Date;
            }
            Session["Reporttype"] = ddlReport.SelectedItem.Text;
            Session["datefrom"] = TxtFromDate.Text;
            Session["dateto"] = TxtToDate.Text;
            Session["Priority"] = ddlPriority.SelectedItem.Text;
            Session["Status"] = ddlStatus.SelectedItem.Text;

            int ReportId = Convert.ToInt32(ddlReport.SelectedValue);
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);
            int PrioritId = Convert.ToInt32(ddlPriority.SelectedValue);
            dt = Db.DateCalling(FD, TD, ReportId, statusId, PrioritId);
            ViewState["grid"] = dt;

            //DataTable dt = ViewState["grid"] as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                GridViewReport.Columns.Clear();

                // Add the checkbox column first
                TemplateField checkboxField = new TemplateField();
                checkboxField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "Select All");
                checkboxField.ItemTemplate = new GridViewTemplate(ListItemType.Item, "Select");
                GridViewReport.Columns.Add(checkboxField);

                // Dynamically add the rest of the columns
                int columnsCount = dt.Columns.Count;
                for (int i = 0; i < columnsCount; i++)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = dt.Columns[i].ColumnName;
                    boundField.HeaderText = dt.Columns[i].ColumnName;
                    GridViewReport.Columns.Add(boundField);
                }

                GridViewReport.DataSource = dt;
                GridViewReport.DataBind();

                ViewState["dt1"] = dt;
                ViewState["grid"] = dt;

                GridViewReport.Visible = true;
            }
            Session["report"] = dt;
        }
        private void UpdateSelectedRecords()
        {
            List<string> selectedTaskIds = (List<string>)ViewState["SelectedRecords"];

            foreach (GridViewRow row in GridViewReport.Rows)
            {
                CheckBox cbItem = (CheckBox)row.FindControl("cbItem");
                string taskId = cbItem.Attributes["data-taskid"];

                if (cbItem.Checked)
                {
                    if (!selectedTaskIds.Contains(taskId))
                    {
                        selectedTaskIds.Add(taskId);
                    }
                }
                else
                {
                    if (selectedTaskIds.Contains(taskId))
                    {
                        selectedTaskIds.Remove(taskId);
                    }
                }
            }

            ViewState["SelectedRecords"] = selectedTaskIds;
        }



    


        #region Methods
        private void ReportDropDown()
        {
            // throw new NotImplementedException();
            try
            {
                DataTable dt = Db.GetData("sp_report", true);
                ddlReport.DataSource = dt;
                ddlReport.DataTextField = "S_Name";
                ddlReport.DataValueField = "S_Id";
                ddlReport.DataBind();
                // ddlReport.Items.Insert(0, new ListItem("--select Type--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void StatusDropDown()
        {
            // throw new NotImplementedException();
            try
            {
                DataTable dt = Db.GetData("SP_getStatus1", true);
                ddlStatus.DataSource = dt;
                ddlStatus.DataTextField = "Status_name";
                ddlStatus.DataValueField = "id";
                ddlStatus.DataBind();
                // ddlStatus.Items.Insert(0, new ListItem("--select status--", "0"));
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
                // ddlPriority.Items.Insert(0, new ListItem("--select Priority--", "0"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void BindGrid()
        {
            btnGetSelectedIDs.Visible = false;
            //throw new NotImplementedException();
            DataTable obj = (DataTable)ViewState["pageindex"];
            int ReportTypeId = int.Parse(ddlReport.SelectedValue);
            string showFilter = "";

            for (int i = 0; i < obj.Rows.Count; i++)
            {
                if (int.Parse(obj.Rows[i]["ID"].ToString()) == ReportTypeId)
                {
                    showFilter = obj.Rows[i]["StatusFilter"].ToString();
                    break;
                }
            }

            if (showFilter[0].ToString() == "1")
                DateSelection.Visible = true;
            else
                DateSelection.Visible = false;

            if (showFilter[1].ToString() == "1")
                DateSelection.Visible = true;
            else
                DateSelection.Visible = false;

            if (showFilter[2].ToString() == "1")
                StatusSelection.Visible = true;
            else
                StatusSelection.Visible = false;

            if (showFilter[3].ToString() == "1")
                PrioritySelection.Visible = true;
            else
                PrioritySelection.Visible = false;

            // Clear previous grid data
            // GridViewReport.Columns.Clear();
            GridViewReport.DataSource = null;
            GridViewReport.DataBind();
            SelectedIdsLabel.Text = string.Empty;
        }
        private void RecreateGridView()
        {
            if (ViewState["grid"] != null)
            {
                DataTable dt = ViewState["grid"] as DataTable;

                GridViewReport.Columns.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        BoundField boundField = new BoundField
                        {
                            DataField = column.ColumnName,
                            HeaderText = column.ColumnName
                        };
                        GridViewReport.Columns.Add(boundField);
                    }

                    TemplateField checkboxField = new TemplateField
                    {
                        HeaderTemplate = new GridViewTemplate(ListItemType.Header, "Select All"),
                        ItemTemplate = new GridViewTemplate(ListItemType.Item, "Select")
                    };
                    GridViewReport.Columns.Insert(0, checkboxField);
                    GridViewReport.DataSource = dt;
                    GridViewReport.DataBind();
                }
            }
        }
        #endregion

        #region Events
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ImageExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable gridData = ViewState["grid"] as DataTable;
            StringBuilder csvContent = new StringBuilder();

            // Add column headers
            foreach (TableCell headerCell in GridViewReport.HeaderRow.Cells)
            {
                csvContent.Append(headerCell.Text + ",");
            }
            csvContent.AppendLine();

            // Add data rows
            foreach (GridViewRow row in GridViewReport.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    csvContent.Append(cell.Text + ",");
                }
                csvContent.AppendLine();
            }

            // Write content to a file
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewData.csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.Output.Write(csvContent.ToString());
            Response.Flush();
            Response.End();
        }
        protected void ImagePdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable gridData = ViewState["grid"] as DataTable;
            ExportDataTableToPdf(gridData, "SampleData.pdf");
        }
        private void ExportDataTableToPdf(DataTable dt, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add a title to the document
                // document.Add(new Paragraph("Sample DataTable to PDF"));
                // document.Add(new Paragraph(" ")); // Add a blank line

                PdfPTable table = new PdfPTable(dt.Columns.Count);
                // Add the headers
                foreach (DataColumn column in dt.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
                // Add the rows
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        table.AddCell(item.ToString());
                    }
                }
                document.Add(table);
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("RdlcReport");
        }


        #endregion

       
    }
    // Template class for checkbox column
    public class GridViewTemplate : ITemplate
    {
        private ListItemType _type;
        private string _columnName;

        public GridViewTemplate(ListItemType type, string colname)
        {
            _type = type;
            _columnName = colname;
        }
        public void InstantiateIn(Control container)
        {
            if (_type == ListItemType.Header)
            {
                CheckBox cbHeader = new CheckBox();
                cbHeader.ID = "cbHeader";
                cbHeader.Attributes["onclick"] = "SelectAllCheckboxes(this)";
                cbHeader.Text = "Select All";
                cbHeader.AutoPostBack = true;
                container.Controls.Add(cbHeader);
            }
            else if (_type == ListItemType.Item)
            {   
                CheckBox cbItem = new CheckBox();
                cbItem.ID = "cbItem";
                cbItem.AutoPostBack = true;
                container.Controls.Add(cbItem);
            }

        }
    }
}



//public class CheckBoxTemplate : ITemplate
//{
//    public void InstantiateIn(Control container)
//    {
//        CheckBox checkBox = new CheckBox();
//        checkBox.ID = "rowCheckBox";
//        container.Controls.Add(checkBox);
//    }
//}

//public class HeaderCheckBoxTemplate : ITemplate
//{
//    public void InstantiateIn(Control container)
//    {
//        CheckBox headerCheckBox = new CheckBox();
//        headerCheckBox.ID = "headerCheckBox";
//        headerCheckBox.AutoPostBack = false;
//        headerCheckBox.CheckedChanged += new EventHandler(HeaderCheckBox_CheckedChanged);
//        container.Controls.Add(headerCheckBox);

//        Label selectAllLabel = new Label();
//        selectAllLabel.Text = "Select All";
//        container.Controls.Add(selectAllLabel);
//    }

//    private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
//    {
//        CheckBox headerCheckBox = (CheckBox)sender;
//        // GridView gridView = (GridView)headerCheckBox.NamingContainer.NamingContainer;
//        GridView gridView = (GridView)headerCheckBox.NamingContainer.Parent.Parent;
//        foreach (GridViewRow row in gridView.Rows)
//        {
//            if (row.RowType == DataControlRowType.DataRow)
//            {
//                CheckBox rowCheckBox = (CheckBox)row.FindControl("rowCheckBox");
//                if (rowCheckBox != null)
//                {
//                    rowCheckBox.Checked = headerCheckBox.Checked;
//                }
//            }
//        }
//    }
//}
//protected void rowCheckBox_CheckedChanged1(object sender, EventArgs e)
//{
//    CheckBox checkBox = (CheckBox)sender;
//    GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
//}
//protected void headerCheckBox_CheckedChanged(object sender, EventArgs e)
//{
//    CheckBox checkhead = (CheckBox)GridViewReport.HeaderRow.FindControl("headerCheckBox");
//    foreach (GridViewRow row in GridViewReport.Rows)
//    {
//        CheckBox checkrow = (CheckBox)row.FindControl("rowCheckBox");
//        if (checkhead.Checked == true)
//        {
//            checkrow.Checked = true;
//        }
//        else
//        {
//            checkrow.Checked = false;
//        }

//    }
//}
//protected void GridViewReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
//{
//    GridViewReport.PageIndex = e.NewPageIndex;
//    RecreateGridView();
//}

//if (dt != null && dt.Rows.Count > 0)
//{
//    GridViewReport.Columns.Clear();
//    foreach (DataColumn column in dt.Columns)
//    {
//        BoundField boundField = new BoundField
//        {
//            DataField = column.ColumnName,
//            HeaderText = column.ColumnName
//        };
//        GridViewReport.Columns.Add(boundField);
//    }

//    TemplateField checkboxField = new TemplateField
//    {
//        HeaderTemplate = new GridViewTemplate(ListItemType.Header, "Select All"),
//        ItemTemplate = new GridViewTemplate(ListItemType.Item, "Select")
//    };
//    GridViewReport.Columns.Insert(0, checkboxField); // Add the checkbox column as the first column

//    // Bind DataTable to GridView
//    GridViewReport.DataSource = dt;
//    GridViewReport.DataBind();

//    ViewState["dt1"] = dt;
//    GridViewReport.Visible = true;
//    ViewState["grid"] = dt;
//}

//switch (_type)
//{
//    case ListItemType.Header:
//        CheckBox cbHeader = new CheckBox();
//        cbHeader.ID = "cbHeader";
//        cbHeader.Attributes["onclick"] = "SelectAllCheckboxes(this)";
//        container.Controls.Add(cbHeader);
//        Literal selectAllLiteral = new Literal();
//        selectAllLiteral.Text = " Select All";
//        container.Controls.Add(selectAllLiteral);
//        break;

//    case ListItemType.Item:
//        CheckBox cbItem = new CheckBox();
//        cbItem.ID = "cbItem";
//        container.Controls.Add(cbItem);
//        break;
//}