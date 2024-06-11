<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RdlcReport.aspx.cs" Inherits="Create_Ticket.RdlcReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" height="600px"></rsweb:reportviewer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
