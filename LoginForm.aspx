﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginForm.aspx.vb" Inherits="AppRegistration.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>

    <script type="text/javascript" src="js/jquery-1.12.4.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Register').click(function () {
                var summary = "";
                summary += isvalidMobilePhone();
                summary += isvalidFirstname();
                summary += isvalidLastname();                
                summary += isvalideMail();
                if (summary != "") {
                    alert(summary);
                    return false;
                }
                else {
                    return true;
                }
            })
        })
        function isvalidMobilePhone() {
            var temp = $("#mobilephone").val();
            if (temp == "") {
                return ("Please Enter Mobile Number" + "\n");
            }
            else {
                if (validateMobileNumber(temp)) {             
                    return "";
                }
                else {
                    return ("Invalid Mobile Number.Please enter valid Indonesian Mobile Number" + "\n");
                }
            }

        }
        function isvalidFirstname() {
            var temp = $("#firstname").val();
            if (temp == "") {
                return ("Please Enter First Name" + "\n");
            }
            else {
                return "";
            }
        }
        function isvalidLastname() {
            var temp = $("#lastname").val();
            if (temp == "") {
                return ("Please Enter Last Name" + "\n");
            }
            else {
                return "";
            }
        }
        function isvalideMail() {
            var temp = $("#email").val();
            if (temp == "") {
                return ("Please Enter E-Mail" + "\n");
            }
            else {
                if (validateEmailAddress(temp)) {             
                    return "";
                }
                else {
                    return ("Invalid Email Address.Please enter valid email e.g abc@domain.com" + "\n");
                }
            }

        }
        function validateMobileNumber(MobileNo) {
            var expr = /^((?:\+62|62)|0)[2-9]{1}[0-9]+$/;
            if (expr.test(MobileNo)) {
                return true;
            }
            else {
                return false;
            }
        }
        function validateEmailAddress(EmailId) {
            var expr = /^[a-zA-Z0-9._]+[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,4}$/;
            if (expr.test(EmailId)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <link href="Style.css" rel="stylesheet" />

    <style>
        @import url('https://fonts.googleapis.com/family=bitter|crete+round|pacifico');
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="container">
                <div class="inner3">
                    <h4><br />Registration</h4>

                    <asp:TextBox ID="mobilephone" Placeholder="Mobile Phone" runat="server" enabled="false"></asp:TextBox>
                    <asp:TextBox ID="firstname" Placeholder="First Name" runat="server" enabled="false"></asp:TextBox>
                    <asp:TextBox ID="lastname" Placeholder="Last Name" runat="server" enabled="false"></asp:TextBox>
                    <p>Date of Birth</p>
                    <p><asp:DropDownList ID="Bulan" Placeholder="Month" runat="server" Font-Size="Large" Font-Names="Crete Round" enabled="false" BackColor="DarkGray">
                        <asp:ListItem Value="00">Month</asp:ListItem>
                        <asp:ListItem Value="01">Jan</asp:ListItem>
                        <asp:ListItem Value="02">Feb</asp:ListItem>
                        <asp:ListItem Value="03">Mar</asp:ListItem>
                        <asp:ListItem Value="04">Apr</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">Jun</asp:ListItem>
                        <asp:ListItem Value="07">Jul</asp:ListItem>
                        <asp:ListItem Value="08">Aug</asp:ListItem>
                        <asp:ListItem Value="09">Sep</asp:ListItem>
                        <asp:ListItem Value="10">Oct</asp:ListItem>
                        <asp:ListItem Value="11">Nov</asp:ListItem>
                        <asp:ListItem Value="12">Dec</asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:DropDownList ID="Tanggal" Placeholder="Day" runat="server" Font-Size="Large" Font-Names="Crete Round" enabled="false" BackColor="DarkGray">
                        <asp:ListItem Value="00">Day</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:DropDownList ID="Tahun" Placeholder="Year" runat="server" Font-Size="Large" Font-Names="Crete Round" enabled="false" BackColor="DarkGray">
                        <asp:ListItem Value="00">Year</asp:ListItem>
                    </asp:DropDownList></p>
                    <table class="table" style="width: 50%">
                        <tr>
                            <td style="width: 15%">
                                <asp:RadioButton ID="GenderMale" Placeholder="Gender" runat="server" Text="Male" Font-Size="Large" Font-Names="Crete Round" Width="100%" enabled="false" BackColor="DarkGray"></asp:RadioButton>
                            </td>
                            <td style="width: 15%">
                                <asp:RadioButton ID="GenderFemale" Placeholder="Gender" runat="server" Text="Female" Font-Size="Large" Font-Names="Crete Round" Width="100%" enabled="false" BackColor="DarkGray"></asp:RadioButton>
                            </td>
                        </tr>
                    </table>
                    <asp:TextBox ID="email" Placeholder="E-Mail" runat="server" enabled="false"></asp:TextBox>

                    <asp:Button ID="Register" CssClass="btn1" runat="server" text="Register" enabled="false"/>
                </div>
            </div>
            <div class="inner1">
                <br />
                <br />
                <a class="tw"><asp:Button ID="Login" CssClass="btn" runat="server" text="Login" /></a>
                <br />
                <br />
            </div>
        </section>
    </form>
</body>
</html>
