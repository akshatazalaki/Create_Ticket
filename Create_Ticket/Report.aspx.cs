using Create_Ticket.Assest;
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

                DataTable filter = new DataTable();
                filter = Db.Card("PRoCName", true);
                ViewState["pageindex"] = filter;

                ReportDropDown();
                PriorityDropDown();
                StatusDropDown();
                //ddlReport_SelectedIndexChanged(null, null);

            }
        }

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

            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
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

            GridViewReport.DataSource = null;
            GridViewReport.DataBind();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ImageExcel.Visible = true;
                ImagePdf.Visible = true;
                btnReport.Visible = true;
                DataTable dt = new DataTable();
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
                GridViewReport.DataSource = dt;
                GridViewReport.DataBind();
                GridViewReport.Visible = true;
                ViewState["grid"] = dt;
                Session["report"] = dt;

            }
            catch (Exception ex)
            {

            }
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
    }
}