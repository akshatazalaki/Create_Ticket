﻿using Create_Ticket.Assest;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI
using System.Windows.Forms.DataVisualization.Charting;

namespace Create_Ticket
{
    public partial class DashBoard : System.Web.UI.Page
    {
        DBCalling Db = new DBCalling();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Db.Card("SP_ValuesAccName", true);
            barchartLit1.Text = BarChart(dt);

            DataTable dt1 = Db.Card("SP_ValuesAccStatus", true);
            piechartLit1.Text = PieChart(dt1);

            DataTable dt2 = Db.Card("SP_ValuesAccPriority", true);
            linechartLit1.Text = LineChart(dt2);
            if (!IsPostBack)
            {
                cards();

                charts();

            }


        }

        private void charts()
        {
            //throw new NotImplementedException();
            DataTable dt = Db.Card("SP_ValuesAccName", true);
            barchartLit1.Text = BarChart(dt);

            DataTable dt1 = Db.Card("SP_ValuesAccStatus", true);
            piechartLit1.Text = PieChart(dt1);

            DataTable dt2 = Db.Card("SP_ValuesAccPriority", true);
            linechartLit1.Text = LineChart(dt2);
        }

        public string PieChart(DataTable dt)
        {
            string Value1 = "";
            string Value2 = "";
            string Chart = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    Value1 += ",";
                    Value2 += ",";
                }

                Value1 += "'" + dt.Rows[i][0].ToString() + "'";
                Value2 += "'" + dt.Rows[i][1].ToString() + "'";

            }
            // Chart = "<canvas id =\"barchart" + count + "\" style=\"width:100%;max-width:600px\"></canvas>\r\n\r\n<script>\r\nvar xValues = [" + Value1 + "];\r\nvar yValues = [" + Value2 + "];\r\nvar barColors = [\"red\", \"green\",\"blue\",\"orange\",\"brown\"];\r\n\r\nnew Chart(\"barchart" + count + "\", {\r\n  type: \"bar\",\r\n  data: {\r\n    labels: xValues,\r\n    datasets: [{\r\n      backgroundColor: barColors,\r\n      data: yValues\r\n    }]\r\n  },\r\n  options: {\r\n    legend: {display: false},\r\n    title: {\r\n      display: true,\r\n      text: \"World Wine Production 2018\"\r\n    }\r\n  }\r\n});\r\n</script>";

            Chart = "<canvas id=\"piechart" + "\" style=\"width:100%; hight:277px; max-width:600px;height:185px;width:300px\"></canvas>\r\n\r\n<script>\r\nvar xValues = [" + Value1 + "];\r\nvar yValues = [" + Value2 + "];\r\nvar barColors = [\r\n  \"#b91d47\",\r\n  \"#00aba9\",\r\n  \"#2b5797\",\r\n  \"#e8c3b9\",\r\n  \"#1e7145\"\r\n];\r\n\r\nnew Chart(\"piechart" + "\", {\r\n  type: \"pie\",\r\n  data: {\r\n    labels: xValues,\r\n    datasets: [{\r\n      backgroundColor: barColors,\r\n      data: yValues\r\n    }]\r\n  },\r\n  options: {\r\n    title: {\r\n      display: true,\r\n      text: \"Status Wise Ticket Details\"\r\n    }\r\n  }\r\n});\r\n</script>";
            return Chart;
        }

        public string BarChart(DataTable dt)
        {
            string Value1 = "";
            string Value2 = "";
            string Chart = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (i > 0)
                {
                    Value1 += ",";
                    Value2 += ",";
                }

                Value1 += "'" + dt.Rows[i][0].ToString() + "'";
                Value2 += "'" + dt.Rows[i][1].ToString() + "'";

            }
            Chart = "<canvas id =\"barchart" + "\" style=\"width:100%;max-width:600px;height:185px;width:300px\"></canvas>\r\n\r\n<script>\r\nvar xValues = [" + Value1 + "];\r\nvar yValues = [" + Value2 + "];\r\nvar barColors = [\"red\", \"green\",\"blue\",\"orange\",\"brown\"];\r\n\r\nnew Chart(\"barchart" + "\", {\r\n  type: \"bar\",\r\n  data: {\r\n    labels: xValues,\r\n    datasets: [{\r\n      backgroundColor: barColors,\r\n      data: yValues\r\n    }]\r\n  },\r\n  options: {\r\n    legend: {display: false},\r\n    title: {\r\n      display: true,\r\n      text: \"Number Of Tickets For Each User\"\r\n    }\r\n  }\r\n});\r\n</script>";


            return Chart;
        }

        public string LineChart(DataTable dt)
        {
            string Value1 = "";
            string Value2 = "";
            string Chart = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (i > 0)
                {
                    Value1 += ",";
                    Value2 += ",";
                }

                Value1 += "'" + dt.Rows[i][0].ToString() + "'";
                Value2 += "'" + dt.Rows[i][1].ToString() + "'";

            }
            Chart = "<canvas id =\"linechart" + "\" style=\"width:100%;max-width:600px;height:185px;width:300px\"></canvas>\r\n\r\n<script>\r\nvar xValues = [" + Value1 + "];\r\nvar yValues = [" + Value2 + "];\r\nvar barColors = [\"red\", \"green\",\"blue\",\"orange\",\"brown\"];\r\n\r\nnew Chart(\"linechart" + "\", {\r\n  type: \"line\",\r\n  data: {\r\n    labels: xValues,\r\n    datasets: [{\r\n      backgroundColor: barColors,\r\n      data: yValues\r\n    }]\r\n  },\r\n  options: {\r\n    legend: {display: false},\r\n    title: {\r\n      display: true,\r\n      text: \"Priority Wise Ticket Details\"\r\n    }\r\n  }\r\n});\r\n</script>";


            return Chart;
        }


        private void cards()
        {
            //throw new NotImplementedException();
            DataTable dt = Db.Card("SP_TotalTicket", true);
            string Total = dt.Rows[0][0].ToString();

            DataTable dt1 = Db.Card("SP_ValuesAccName", true);

            string Name1 = dt1.Rows[0][0].ToString();
            string Value1 = dt1.Rows[0][1].ToString();

            string Name2 = dt1.Rows[1][0].ToString();
            string Value2 = dt1.Rows[1][1].ToString();

            string Name3 = dt1.Rows[2][0].ToString();
            string Value3 = dt1.Rows[2][1].ToString();

            DataTable dt2 = Db.Card("SP_ValuesAccStatus", true);
            string Status1 = dt2.Rows[0][0].ToString();
            string Value4 = dt2.Rows[0][1].ToString();

            string Status2 = dt2.Rows[1][0].ToString();
            string Value5 = dt2.Rows[1][1].ToString();

            string Status3 = dt2.Rows[2][0].ToString();
            string Value6 = dt2.Rows[2][1].ToString();

            string Status4 = dt2.Rows[3][0].ToString();
            string Value7 = dt2.Rows[3][1].ToString();


            DataTable dt3 = Db.Card("SP_ValuesAccPriority", true);

            string Prio1 = dt3.Rows[0][0].ToString();
            string Value8 = dt3.Rows[0][1].ToString();

            string Prio2 = dt3.Rows[1][0].ToString();
            string Value9 = dt3.Rows[1][1].ToString();

            string Prio3 = dt3.Rows[2][0].ToString();
            string Value10 = dt3.Rows[2][1].ToString();

            //List<Assest.Parameter> card = new List<Assest.Parameter>
            //{
            //    //new Assest.Parameter { Title = "Total Tickets", Description =Total},
            //    //new Assest.Parameter { Title = "Number Of Tickets", Description =$"{Name1} : {Value1} <br/> {Name2} : {Value2} <br/> {Name3} : {Value3}"},
            //    //new Assest.Parameter { Title = "Accoring to status", Description = $"{Status1} : {Value4} <br/> {Status2} : {Value5} <br/> {Status3} : {Value6} <br/> {Status4} : {Value7}" },
            //    //new Assest.Parameter { Title = "Accoring to Priority", Description = $"{Prio1} : {Value8} <br/> {Prio2} : {Value9} <br/> {Prio3} : {Value10}" },
            //};

            List<Assest.Parameters> card = new List<Assest.Parameters>
            {
                new Assest.Parameters { Title = "Total Tickets", Description =Total},
                // new Assest.Parameter { Title = "Number Of Tickets", Description =$"{Name1} : {Value1}" },
              new Assest.Parameters { Title =$" {Status1}", Description =$" {Value4}" },
              new Assest.Parameters { Title =$" {Status2}", Description =$" {Value5}" },
              new Assest.Parameters { Title =$" {Status3}", Description =$" {Value6}" },
               new Assest.Parameters { Title =$" {Status4}", Description =$" {Value7}" },
            };
            //new Assest.Parameter { Title = "Pending", Description =$"{Status1} : {Value4}" },
            //new Assest.Parameter { Title = "Pending", Description =$"{Status1} : {Value4}" },
            //new Assest.Parameter { Title = "Pending", Description =$"{Status1} : {Value4}" },

            //new Assest.Parameter { Title = "Number Of Tickets", Description =$"{Name1} : {Value1}" }, List<Assest.Parameter> card = new List<Assest.Parameter>
            // new Assest.Parameter { Title = "Number Of Tickets", Description =$"{Name1} : {Value1}
            List<Assest.Parameters> card1 = new List<Assest.Parameters> {
                new Assest.Parameters { Title = "Number Of Tickets", Description =$"{Name1} : {Value1} <br/> {Name2} : {Value2} <br/> {Name3} : {Value3}"},
                new Assest.Parameters { Title = "Accoring to status", Description = $"{Status1} : {Value4} <br/> {Status2} : {Value5} <br/> {Status3} : {Value6} <br/> {Status4} : {Value7}" },
                new Assest.Parameters { Title = "Accoring to Priority", Description = $"{Prio1} : {Value8} <br/> {Prio2} : {Value9} <br/> {Prio3} : {Value10}" },
            };

            repeater.DataSource = card;
            repeater.DataBind();

            repeater1.DataSource = card1;
            repeater1.DataBind();

        }

        //protected void btnDownloadChartPNG_Click(object sender, EventArgs e)
        //{
        //    // DownloadChartAsImage("barchart", "bar-chart.png");
        //    btnpngdownload("barchart");
        //}

        //private void btnpngdownload(string chartId)
        //{
        //    // throw new NotImplementedException();

        //    using (var stream = new MemoryStream())
        //    {
        //        // Find the chart control by its ID
        //        var chart = (Chart)FindControl(chartId);
        //        if (chart != null)
        //        {
        //            // Save the chart as an image in memory stream
        //            chart.SaveImage(stream, ChartImageFormat.Png);

        //            // Write the memory stream to the response
        //            Response.Clear();
        //            Response.ContentType = "image/png";
        //            Response.AddHeader("Content-Disposition", $"attachment; filename=chart.png");
        //            Response.BinaryWrite(stream.ToArray());
        //            Response.End();
        //        }
        //    }
            //using (var bitmap = new Bitmap(600, 400))
            //{
            //   // using (var graphics = Graphics.FromImage(bitmap))

            //    {
            //        // Draw your chart here (placeholder code)
            //        graphics.Clear(Color.White);
            //        graphics.DrawString("Your Chart", new Font("Arial", 20), Brushes.Black, new PointF(100, 100));

            //        // Save the bitmap to a memory stream
            //        using (var stream = new MemoryStream())
            //        {
            //            bitmap.Save(stream, ImageFormat.Png);
            //            byte[] bytes = stream.ToArray();

            //            Response.Clear();
            //            Response.ContentType = "image/png";
            //            Response.AddHeader("Content-Disposition", "attachment; filename=chart.png");
            //            Response.BinaryWrite(bytes);
            //            Response.End();
            //        }
            //    }
            //}
        }

        //  protected void btnDownloadChartPDF_Click(object sender, EventArgs e)
        // {
        // DownloadChartAsPDF("barchart", "bar-chart.pdf");
        //using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
        // {
        //     using (var stream = new MemoryStream())
        //     {
        //         PdfWriter.GetInstance(document, stream);
        //         document.Open();

        //         // Create a temporary bitmap to draw the chart
        //         using (var bitmap = new Bitmap(600, 400))
        //         {
        //             using (var graphics = Graphics.FromImage(bitmap))
        //             {
        //                 // Draw your chart here (placeholder code)
        //                 graphics.Clear(Color.White);
        //                 graphics.DrawString("Your Chart", new Font("Arial", 20), Brushes.Black, new PointF(100, 100));

        //                 // Save the bitmap to a memory stream
        //                 using (var chartStream = new MemoryStream())
        //                 {
        //                     bitmap.Save(chartStream, ImageFormat.Png);
        //                     iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartStream.ToArray());
        //                     document.Add(chartImage);
        //                 }
        //             }
        //         }

        //         document.Close();
        //         byte[] bytes = stream.ToArray();

        //         Response.Clear();
        //         Response.ContentType = "application/pdf";
        //         Response.AddHeader("Content-Disposition", "attachment; filename=chart.pdf");
        //         Response.BinaryWrite(bytes);
        //         Response.End();
        //        }
        //    }
        //}

        // }


        //public string GenerateBarChart(DataTable dt)
        //{
        //    Chart chart = new Chart();
        //    chart.Width = 600;
        //    chart.Height = 400;
        //    chart.RenderType = RenderType.ImageTag;
        //    chart.Palette = ChartColorPalette.BrightPastel;
        //    chart.Titles.Add("Number Of Tickets For Each User");

        //    ChartArea chartArea = new ChartArea();
        //    chart.ChartAreas.Add(chartArea);

        //    Series series = new Series();
        //    series.Name = "Tickets";
        //    series.ChartType = SeriesChartType.Bar;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        series.Points.AddXY(row[0].ToString(), Convert.ToDouble(row[1]));
        //    }

        //    chart.Series.Add(series);

        //    string chartImagePath = Server.MapPath("~/Charts/BarChart.png");
        //    chart.SaveImage(chartImagePath, ChartImageFormat.Png);
        //    return $"<img src='/Charts/BarChart.png' alt='Bar Chart' />";
        //}

        //public string GeneratePieChart(DataTable dt)
        //{
        //    Chart chart = new Chart();
        //    chart.Width = 600;
        //    chart.Height = 400;
        //    chart.RenderType = RenderType.ImageTag;
        //    chart.Palette = ChartColorPalette.BrightPastel;
        //    chart.Titles.Add("Status Wise Ticket Details");

        //    ChartArea chartArea = new ChartArea();
        //    chart.ChartAreas.Add(chartArea);

        //    Series series = new Series();
        //    series.Name = "Status";
        //    series.ChartType = SeriesChartType.Pie;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        series.Points.AddXY(row[0].ToString(), Convert.ToDouble(row[1]));
        //    }

        //    chart.Series.Add(series);

        //    string chartImagePath = Server.MapPath("~/Charts/PieChart.png");
        //    chart.SaveImage(chartImagePath, ChartImageFormat.Png);
        //    return $"<img src='/Charts/PieChart.png' alt='Pie Chart' />";
        //}

        //public string GenerateLineChart(DataTable dt)
        //{
        //    Chart chart = new Chart();
        //    chart.Width = 600;
        //    chart.Height = 400;
        //    chart.RenderType = RenderType.ImageTag;
        //    chart.Palette = ChartColorPalette.BrightPastel;
        //    chart.Titles.Add("Priority Wise Ticket Details");

        //    ChartArea chartArea = new ChartArea();
        //    chart.ChartAreas.Add(chartArea);

        //    Series series = new Series();
        //    series.Name = "Priority";
        //    series.ChartType = SeriesChartType.Line;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        series.Points.AddXY(row[0].ToString(), Convert.ToDouble(row[1]));
        //    }

        //    chart.Series.Add(series);

        //    string chartImagePath = Server.MapPath("~/Charts/LineChart.png");
        //    chart.SaveImage(chartImagePath, ChartImageFormat.Png);
        //    return $"<img src='/Charts/LineChart.png' alt='Line Chart' />";
        //}

        //protected void btnDownloadChartPDF_Click(object sender, EventArgs e)
        //{
        //    string chartImagePath = Server.MapPath("~/Charts/BarChart.png");
        //    GeneratePDF("Bar Chart", chartImagePath);
        //}

        //protected void btnDownloadPieChartPDF_Click(object sender, EventArgs e)
        //{
        //    string chartImagePath = Server.MapPath("~/Charts/PieChart.png");
        //    GeneratePDF("Pie Chart", chartImagePath);
        //}

        //protected void btnDownloadLineChartPDF_Click(object sender, EventArgs e)
        //{
        //    string chartImagePath = Server.MapPath("~/Charts/LineChart.png");
        //    GeneratePDF("Line Chart", chartImagePath);
        //}

        //private void GeneratePDF(string chartTitle, string chartImagePath)
        //{
        //    Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();

        //        pdfDoc.Add(new Paragraph(chartTitle));
        //        iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartImagePath);
        //        chartImage.ScaleToFit(500f, 400f);
        //        pdfDoc.Add(chartImage);

        //        pdfDoc.Close();
        //        byte[] bytes = stream.ToArray();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", $"attachment;filename={chartTitle}.pdf");
        //        Response.BinaryWrite(bytes);
        //        Response.End();
        //    }
        //}


    
}

