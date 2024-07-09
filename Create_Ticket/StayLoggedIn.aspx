<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StayLoggedIn.aspx.cs" Inherits="Create_Ticket.StayLoggedIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
            <h2>Your session is about to expire</h2>
            <asp:Button ID="btnStayLoggedIn" runat="server" Text="Stay Logged In" OnClick="btnStayLoggedIn_Click" />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
