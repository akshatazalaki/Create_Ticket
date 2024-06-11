<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Create_Ticket.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        /* Center the entire login form */
        body {
            display: flex; /* Enable centering */
            justify-content: center;
            align-items: center;
            min-height: 100vh; /* Set minimum viewport height */
            font-family: Arial, sans-serif; /* Default font */
        }

        /* Style the login container */
        .login-container {
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent background */
            border-radius: 5px; /* Rounded corners */
            padding: 30px;
            color: white; /* Text color */
        }

        /* Style the table for better layout */
        table {
            width: 100%; /* Full-width table */
            border-collapse: collapse; /* Remove table borders */
        }

        /* Style table cells for improved spacing */
        td {
            padding: 10px;
        }

        /* Style labels */
        label {
            display: block; /* Display labels on their own lines */
            margin-bottom: 5px;
        }

        /* Style text input fields */
        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        /* Style the login button */
        .login-container button {
            background-color: #4CAF50; /* Green button color */
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            transition: background-color 0.2s ease-in-out; /* Smooth hover effect */
        }

            /* Add hover effect to the button */
            .login-container button:hover {
                background-color: #3e8e41; /* Darker green on hover */
            }

        .bg-img {
            /* The image used */
            /*background-image: url('../images/bkgrnd_2.jpg');*/
            /* background-image: url('Assest/LoginPage.png');*/
            background-image: url('Assest/login.png');
            /* min-height: 400px;*/
            /* Center and scale the image nicely */
            background-position: inherit;
            background-repeat: no-repeat;
            background-size: cover;
            position: relative;
        }
    </style>
</head>
<body class="bg-img">
    <div>
        <form id="form1" runat="server">
            <center>

                <div>
                    <h1>Login</h1>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblUserName" runat="server" Text="UserName"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPassword" runat="server" Text="Password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Text=""></asp:Label>
                </div>
            </center>
        </form>
    </div>
</body>
</html>
