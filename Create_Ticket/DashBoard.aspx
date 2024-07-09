<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Create_Ticket.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>--%>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <style>
                .card-container-MDash {
                    background-color: #F7F7FF;
                    padding: 10px; /* Adjust padding to create a box around the text */
                    margin: 20px;
                    border: 1px solid #ddd; /* Add a border to create a card-like appearance */
                    border-radius: 5px; /* Add border-radius for rounded corners */
                    box-shadow: 0 3px 6px rgba(1, 1, 1, 0.5);
                    height: 100%;
                }

                    .card-container-MDash:hover {
                        background-color: #EFEFEF; /* Slightly darker background color on hover */
                        box-shadow: 0 5px 10px rgba(0, 0, 0, 0.5); /* Enhanced shadow effect */
                        transform: translateY(-5px); /* Slight lift effect */
                    }

                .card-content {
                    display: flex;
                    flex-direction: column;
                    flex-grow: 1;
                    text-align: center;
                    overflow: hidden;
                    width: 100%;
                }

                .ticket-details {
                    text-align: center; /* Centers the text within the container */
                    padding: 10px;
                    border: 1px solid #dee2e6; /* Optional: adds a border around the container */
                    border-radius: 8px; /* Optional: adds rounded corners to the container */
                    background-color: #ffffff; /* Optional: sets a background color for the container */
                    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Optional: adds a subtle shadow */
                    width: 200px;
                    border: 2px solid #6c757d; /* Adds a darker border around the container */
                    border-radius: 12px; /* Adds rounded corners to the container */
                    background-color: #FFE4E1; /* Sets a white background color for the container */
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                }

                    .ticket-details h6 {
                        font-size: 20px; /* Sets the font size */
                        color: #333; /* Sets the text color */
                    }

                .btn {
                    font: 12px "#8e0dfd69";
                    padding: 5px 13px;
                    text-shadow: none;
                }

                .btn-primary {
                    border: none;
                    background: blueviolet;
                    color: white;
                }
            </style>
            <center>
                <div class="ticket-details">
                    <h6>Ticket Details</h6>
                </div>
            </center>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.3.2/html2canvas.min.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
            <script>
                async function downloadChartAsPNG(chartId, filename) {
                    const canvas = document.getElementById(chartId);
                    const link = document.createElement('a');
                    const dataURL = await html2canvas(canvas).then(canvas => canvas.toDataURL('image/png'));
                    link.href = dataURL;
                    link.download = filename;
                    link.click();
                    window.location.reload();
                }

                async function downloadChartAsPDF(chartId, filename) {
                    const { jsPDF } = window.jspdf;
                    const canvas = document.getElementById(chartId);
                    const dataURL = await html2canvas(canvas).then(canvas => canvas.toDataURL('image/png'));
                    const pdf = new jsPDF();
                    pdf.addImage(dataURL, 'PNG', 10, 10);
                    pdf.save(filename);
                    window.location.reload();
                }
            </script>

            <div style="display: flex" class="card-container-MDash">
                <asp:Repeater runat="server" ID="repeater">
                    <ItemTemplate>
                        <div class="card-content card-container-MDash" style="background-color: #F7F7FF;">
                            <h5><%# Eval("Title") %></h5>
                            <p><%# Eval("Description") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div style="display: flex; margin-top: 30px; align-items: center; justify-content: center; background-color: antiquewhite">
                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="barchartLit1" runat="server"></asp:Literal>
                    <button onclick="downloadChartAsPNG('barchart', 'bar-chart.png')" class="btn btn-primary">PNG</button>
                    <button onclick="downloadChartAsPDF('barchart', 'bar-chart.pdf')" class="btn btn-primary">PDF</button>

                    <%-- <asp:Button ID="btnDownloadChartPNG" runat="server" Text="PNG" OnClick="btnDownloadChartPNG_Click" CssClass="btn btn-primary" />
           <asp:Button ID="btnDownloadChartPDF" runat="server" Text="PDF" OnClick="btnDownloadChartPDF_Click" CssClass="btn btn-primary" />--%>
                    <%-- <asp:Button ID="btnDownloadChartPDF" runat="server" Text="Download Bar Chart as PDF" OnClick="btnDownloadChartPDF_Click" CssClass="btn btn-primary" />--%>
                </div>
                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="piechartLit1" runat="server"></asp:Literal>
                    <button onclick="downloadChartAsPNG('piechart', 'pie-chart.png')" class="btn btn-primary">PNG</button>
                    <button onclick="downloadChartAsPDF('piechart', 'pie-chart.pdf')" class="btn btn-primary">PDF</button>
                    <%--  <asp:Button ID="btnDownloadChartPNG1" runat="server" Text="PNG" OnClick="btnDownloadChartPNG1_Click" CssClass="btn btn-primary" />
           <asp:Button ID="Button2" runat="server" Text="PDF" OnClick="btnDownloadChartPDF_Click" CssClass="btn btn-primary" />--%>
                </div>
                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="linechartLit1" runat="server"></asp:Literal>
                    <button onclick="downloadChartAsPNG('linechart', 'line-chart.png')" class="btn btn-primary">PNG</button>
                    <button onclick="downloadChartAsPDF('linechart', 'line-chart.pdf')" class="btn btn-primary">PDF</button>
                    <%--   <asp:Button ID="btnDownloadChartPNG2" runat="server" Text="PNG" OnClick="btnDownloadChartPNG2_Click" CssClass="btn btn-primary" />
           <asp:Button ID="Button4" runat="server" Text="PDF" OnClick="btnDownloadChartPDF_Click" CssClass="btn btn-primary" />--%>
                </div>


            </div>



            <div style="display: flex" class="card-container-MDash">
                <asp:Repeater runat="server" ID="repeater1">
                    <ItemTemplate>
                        <div class="card-content card-container-MDash" style="height: 150px; background-color: #F7F7FF;">
                            <h5><%# Eval("Title") %></h5>
                            <p><%# Eval("Description") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div style="display: flex; margin-top: 30px; align-items: center; justify-content: center; background-color: antiquewhite">
                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="ltrnew0" runat="server"></asp:Literal>
                </div>



                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="ltrnew1" runat="server"></asp:Literal>
                </div>
            </div>
            <div style="display: flex; margin-top: 30px; align-items: center; justify-content: center; background-color: antiquewhite">
                <div class="card-container-MDash" style="background-color: #FFE4E1;">
                    <asp:Literal ID="ltrnew2" runat="server"></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
