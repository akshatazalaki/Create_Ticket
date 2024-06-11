<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Create_Ticket.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        * {
            margin: 0;
            padding: 0;
        }

        .card-header {
            padding: 4px;
            background-color: rgba(0, 0, 0, 0.1); /* Light grey background color */
            border-bottom: 0px solid #ccc;
            font-weight: bold;
        }

        .card {
            width: 30%; /* Adjust the width as needed */
            margin: auto; /* Center the card horizontally */
            overflow: hidden;
            box-shadow: 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            padding: 5px;
            background-color: #fff;
            height: 100%;
        }

        .card-container-MDash {
            background-color: #F7F7FF; /* Full white background color */
            padding: 10px; /* Adjust padding to create a box around the text */
            margin: 20px;
            border: 1px solid #ddd; /* Add a border to create a card-like appearance */
            border-radius: 5px; /* Add border-radius for rounded corners */
            box-shadow: 0 3px 6px rgba(1, 1, 1, 0.5);
            height: 100%;
        }
        /* Custom styling for card header */

        .btn {
            font: 12px "Segoe UI";
            padding: 5px 13px;
            text-shadow: none;
        }

        .btn-primary {
            border-color: none;
            border: 0px;
        }


        /* Custom styling for dropdowns */
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

        .header-container {
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 20px;
        }

        #report {
            position: relative;
            left: 500px;
        }
    </style>
    <br />
    <br />
    <center>
        <div class="header-container">
            <div id="report">
                <h4>Report Page</h4>
            </div>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Style="margin-left: auto" Width="100px" />
        </div>


        <div class="card-container-MDash">
            <div>

                <h6>
                    <asp:Label ID="LabelReport" runat="server" Text="Select Report Type"></asp:Label></h6>
                <asp:DropDownList ID="ddlReport" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" Width="180px" CssClass="custom-dropdown"></asp:DropDownList>

            </div>
            <div style="display: flex">
                <div id="DateSelection" runat="server" visible="true">
                    <br />
                    <asp:Label ID="LabelFromDate" runat="server" Text="From Date"></asp:Label>
                    <asp:TextBox ID="TxtFromDate" runat="server" Width="180px" PlaceHolder="YYYY/MM/DD" Style="background-color: #f0f0f0; border: 1px solid #ccc;" autocomplete="off" onkeydown="return false;" />
                    <ajaxtoolkit:calendarextender id="FromDate" runat="server"
                        targetcontrolid="TxtFromDate"
                        format="yyyy/MM/dd"
                        popupbuttonid="EntryDateImage">
                    </ajaxtoolkit:calendarextender>

                    <asp:Label ID="LabelToDate" runat="server" Text="To Date" Visible="true"></asp:Label>
                    <asp:TextBox ID="TxtToDate" runat="server" Width="180px" PlaceHolder="YYYY/MM/DD" Style="background-color: #f0f0f0; border: 1px solid #ccc;" autocomplete="off" onkeydown="return false;" />
                    <ajaxtoolkit:calendarextender id="ToDate" runat="server"
                        targetcontrolid="TxtToDate"
                        format="yyyy/MM/dd"
                        popupbuttonid="EntryDateImage">
                    </ajaxtoolkit:calendarextender>
                </div>

                <div id="StatusSelection" runat="server" visible="false">
                    <br />
                    <asp:Label ID="LabelStatus" runat="server" Text="Status"></asp:Label>
                    <div id="divstat" runat="server">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Width="180px"></asp:DropDownList>
                    </div>
                </div>
                <div id="PrioritySelection" runat="server" visible="false">
                    <br />
                    <asp:Label ID="LabelPriority" runat="server" Text="Priority"></asp:Label>
                    <asp:DropDownList ID="ddlPriority" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged" Width="180px"></asp:DropDownList>
                </div>
            </div>
            <br />
            <asp:Button ID="btnFilter" runat="server" Text="View" Width="100px" OnClick="btnFilter_Click" CssClass="btn btn-primary" />
            <asp:ImageButton ID="ImageExcel" runat="server" ImageUrl="Assest/icons8-excel-50.png" OnClick="ImageExcel_Click" Visible="false"/>
            <asp:ImageButton ID="ImagePdf" runat="server" ImageUrl="Assest/icon-354355_640.png" Width="50px" Height="50px" OnClick="ImagePdf_Click" Visible="false" />
            <asp:Button ID="btnReport" runat="server" Text="Report Download" Width="100px" CssClass="btn btn-primary" OnClick="btnReport_Click" Visible="false" />

        </div>
    </center>
    <div class="gridview-container" style="display: flex; justify-content: center; margin-top: 8px">
        <asp:GridView ID="GridViewReport" runat="server" Visible="true" HeaderStyle-BackColor="DarkGray" BackColor="gainsboro" BorderColor="#92a8d1" Width="1200px" fontname="Verdana" AlternatingRowStyle-BackColor="#f2f2f2" AllowPaging="True" GridLines="Both"></asp:GridView>
    </div>
</asp:Content>
