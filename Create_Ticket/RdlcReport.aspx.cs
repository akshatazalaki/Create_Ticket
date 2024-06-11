using Create_Ticket.Assest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Create_Ticket

{
    public partial class RdlcReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDb2"].ConnectionString);
        DBCalling db = new DBCalling();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["report"] != null)
            {
                reportDownload();
            }
        }
        protected void reportDownload()

        {

            dt = (DataTable)Session["report"];

            //ReportViewer1.LocalReport.DataSources.Clear();

            LocalReport localReport = new LocalReport();
            

            //ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            //localReport.ReportPath = Server.MapPath("~/RDLC/Report1.rdlc");

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            localReport.ReportPath = Server.MapPath("~/Report1/RdlcReport.rdlc");

            localReport.DataSources.Add(rds);

            localReport.Refresh();

            //ReportParameter[] objparam = new ReportParameter[1];

            //objparam[0] = new ReportParameter("Type", "TestHeading", false);

            //localReport.SetParameters(objparam);

            string name = (String)Session["Reporttype"];

            if (name == "Based On Date")

            {

                string dt1 = (String)Session["datefrom"];

                string dt2 = (String)Session["dateto"];

                ReportParameter[] objparam = new ReportParameter[5];

                objparam[0] = new ReportParameter("Type", name, false);

                objparam[1] = new ReportParameter("Type", "From Date:", false);

                objparam[2] = new ReportParameter("Type", dt1, false);

                objparam[3] = new ReportParameter("Type", "To Date:", false);

                objparam[4] = new ReportParameter("Type", dt2, false);

                localReport.SetParameters(objparam);

            }

            else if (name == "Based On Priority")

            {

                String priority = (String)Session["Priority"];

                ReportParameter[] objparam = new ReportParameter[5];

                objparam[0] = new ReportParameter("Type", name, false);

                objparam[1] = new ReportParameter("Type", "Priority:", false);

                objparam[2] = new ReportParameter("Type", priority, false);

                objparam[3] = new ReportParameter("Type", "", false);

                objparam[4] = new ReportParameter("Type", "", false);

                localReport.SetParameters(objparam);

            }

            else

            {

                String status = (String)Session["Status"];

                ReportParameter[] objparam = new ReportParameter[5];

                objparam[0] = new ReportParameter("Type", name, false);

                objparam[1] = new ReportParameter("Type", "Status:", false);

                objparam[2] = new ReportParameter("Type", status, false);

                objparam[3] = new ReportParameter("Type", "", false);

                objparam[4] = new ReportParameter("Type", "", false);

                localReport.SetParameters(objparam);

            }

            String mimeType, encoding, fileNameExtension;

            String[] streams;

            Warning[] warnings;

            //---->> If we did any colours and given any background then this is considered as page.

            //localReport.EnableExternalImages = true;

            String deviceInfo = "<DeviceInfo><OutputFormat>pdf</OutputFormat><PageWidth>10in</PageWidth><PageHeight>13in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>0.5in</MarginLeft><MarginRight>0.5in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>";

            byte[] renderedBytes = localReport.Render(

                "pdf",

                deviceInfo,

                out mimeType,

                out encoding,

                out fileNameExtension,

                out streams,

                out warnings

            );

            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("Content-Disposition", "attachment; filename=TicketReport.pdf");

            Response.BinaryWrite(renderedBytes);

            Response.End();

        }

    }
}