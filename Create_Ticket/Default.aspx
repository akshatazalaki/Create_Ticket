<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Create_Ticket._Default" Async="true"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .button-margin {
            margin: 10px;
            display: inline-flex; /* Adjust the margin values as needed */
            justify-content: space-around;
        }

        .custom-dropdown {
            width: 180px;
            padding: 5px;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-family: "Segoe UI", Arial, sans-serif;
            font-size: 12px;
            color: #333;
        }
    </style>
    <main>      
        <asp:HiddenField ID="hiddenCSRFToken" runat="server" />
        <asp:Panel ID="PanelTicket" runat="server">
            <div style="display: flex;">
                <div>
                    <asp:Label ID="LabelUser" runat="server" Text="User"></asp:Label>
                    <asp:DropDownList ID="ddlName" runat="server" Width="180px" CssClass="custom-dropdown"></asp:DropDownList>
                </div>
                &nbsp &nbsp &nbsp
                <div>
                    <asp:Label ID="LabelTask" runat="server" Text="Task"></asp:Label>
                    <asp:DropDownList ID="ddlTask" runat="server" Width="180px" CssClass="custom-dropdown"></asp:DropDownList>
                </div>
                &nbsp &nbsp &nbsp
                <div>
                    <asp:Label ID="LabelPriority" runat="server" Text="Priority"></asp:Label>
                    <asp:DropDownList ID="ddlPriority" runat="server" Width="180px" CssClass="custom-dropdown"></asp:DropDownList>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="panelUpdate" runat="server" Visible="false">
            <div style="display: flex">
                <div>
                    <label for="TicketId">Ticket ID</label>
                    <asp:TextBox ID="TxtTicket" runat="server" Width="50" Height="25" Enabled="false"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="LabelStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="180px" CssClass="custom-dropdown"></asp:DropDownList>
                </div>
            </div>
        </asp:Panel>

          <!-- Hidden field to store anti-forgery token -->
      <asp:HiddenField ID="hfAntiForgeryToken" runat="server" />

        <div class="button-margin"> 
            <asp:Panel ID="PanelCreate" runat="server" Visible="true">
                <asp:Button ID="BtnCreate" runat="server" Text="Create" OnClick="BtnCreate_Click" Margin-Right="10px" />
            </asp:Panel>
            &nbsp &nbsp
            <asp:Panel ID="PanelBack" runat="server">
                <asp:Button ID="BtnBack" runat="server" Text="Back" OnClick="BtnBack_Click" />
            </asp:Panel>
            &nbsp &nbsp
            <asp:Panel ID="PanelReport" runat="server">
                <asp:Button ID="BtnReport" runat="server" Text="Report" OnClick="BtnReport_Click" />
            </asp:Panel>
        </div>

        <asp:Panel ID="PanelGrid" runat="server" Visible="true">
            <div class="gridview-container" style="display: flex; justify-content: center; margin-top: 8px">
                <asp:GridView ID="Grv" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="Grv_PageIndexChanging" OnRowCommand="Grv_RowCommand" HeaderStyle-BackColor="DarkGray" BackColor="gainsboro" BorderColor="#92a8d1" Width="1200px" fontname="Verdana"  AlternatingRowStyle-BackColor="#f2f2f2"  GridLines="Both" ><%--AllowPaging="True"--%>
                </asp:GridView>
                <asp:LinkButton ID="linkButton" runat="server" />
            </div>
        </asp:Panel>
    </main>
    

</asp:Content>
