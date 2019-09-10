<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegistrationForm.aspx.vb" Inherits="AppRegistration.RegistrationForm" %>

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
    <script type="text/javascript">
        //function to allow only numbers
        function numericsonly(ob) 
        {
            var invalidChars = /[^0-9]/gi
            if(invalidChars.test(ob.value)) 
            {
                ob.value = ob.value.replace(invalidChars,"");
            }
        } //function to allow only numbers ends here
        
        //On page load hide all tool tips
        $('.required').next('.tooltip_outer_feedback').hide();
        $('.required_feedback').next('.tooltip_outer').hide();
        //---
        
        //Onpage load
        $(document).ready(function()
        {
            //On key up in texbox's hide error messages for required fields
            $('.required').keyup(function()
            {
                $(this).next('.tooltip_outer').hide();
            });
            //On selected item change in dropdownlist hide error messages for required fields
            $('.dpreq').change(function()
            {
                $(this).next('.tooltip_outer').hide();
            });
            //On key up for mobile number avoid non-numeric characters
            $('.mobile').keyup(function()
            {
                $(this).next('.tooltip_outer').hide();
                numericsonly(this); // definition of this function is above
            });
            
            // On button click or submitting values
            $('.btn_validate').click(function(e) 
            {
                var empty_count=0; // variable to store error occured status
                $('.required').next('.tooltip_outer').hide();
                $('.required').each(function()
                {
                    if($(this).val().length === 0)
                    {
                        $(this).after("<div class='tooltip_outer'>
                        <div class='arrow-left'></div> 
                        <div class='tool_tip'>Can't be blank
                        </div></div>").show("slow");
                        empty_count=1;
                    }
                    else
                    {
                        $(this).next('.tooltip_outer').hide();
                        if($(this).hasClass('mobile'))
                        {
                            if($(this).val().length != 10)
                            {
                                $(this).after('<div class="tooltip_outer">
                                <div class="arrow-left"></div> 
                                <div class="tool_tip">Mobile should be 10 digits
                                </div></div>').show("slow");
                                empty_count=1; 
                            }
                            else
                            {
                                $(this).next('.tooltip_outer').hide();
                            }
                        }
                        if($(this).hasClass('email'))
                        {
                            $(this).next('.tooltip_outer').hide();
                            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]
                            {1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|
                            [0-9]{1,3})(\]?)$/;
                        if(filter.test($(this).val()) === false)
                        {
                            $(this).after('<div class="tooltip_outer">
                            <div class="arrow-left"></div> 
                            <div class="tool_tip">Invalid Email
                            </div></div>').show("slow");
                            empty_count=1;
                        }
                        else
                        {
                            $(this).next('.tooltip_outer').hide();
                        }
                    }
                }
                });
            $('.dpreq').next('.tooltip_outer').hide();
            $('.dpreq').each(function()
            {
                
                $(this).next('.tooltip_outer').hide();
                if($(this).attr("selectedIndex") === 0)
                {
                    $(this).after("<div class='tooltip_outer'>
                    <div class='arrow-left'></div> 
                    <div class='tool_tip'>Please select option
                    </div></div>").show("slow");
                    empty_count=1;
                }
                else
                {
                    $(this).next('.tooltip_outer').hide();
                }
            });
            if(empty_count===1)
            {
                e.preventDefault();
            }
            else
            {
                $('.tooltip_outer').hide();
                alert('Successful');
            }
        });
    </script>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#Register").click(function () {
                var EmailId = $("#email").val();
                if ($.trim(EmailId).length == 0) {
                    alert("Please Enter Email Address");
                    return false;
                }
                if (validateEmailAddress(EmailId)) {             
                    return true;
                }
                else {
                    alert('Invalid Email Address.Please enter valid email e.g abc@domain.com');
                    return false;
                }
            });
        });
        function validateEmailAddress(EmailId) {
            var expr = /^[a-zA-Z0-9._]+[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,4}$/;
            if (expr.test(EmailId)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>--%>


    <link href="Style.css" rel="stylesheet" />

    <style>
        @import url('https://fonts.googleapis.com/family=bitter|crete+round|pacifico');
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <%--<div class="container">
            <center>
            <table style="width: 600px; border: solid 1px silver; color: #565656; line-height: 25px;">
                <tr>
                    <td class="Header_style" colspan="2" align="center">
                        <h3>Registration</h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                    </td>
                    <td style="width: 400px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                        Mobile Number
                    </td>
                    <td style="width: 400px;">
                        <asp:TextBox ID="mobilephone" runat="server" class="required mobile"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                        First name
                    </td>
                    <td style="width: 400px;">
                        <asp:TextBox ID="firstname" runat="server" class="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                        Last name
                    </td>
                    <td style="width: 400px;">
                        <asp:TextBox ID="lastname" runat="server" class="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                        Date of Birth
                    </td>
                    <td style="width: 400px;">
                        <asp:DropDownList ID="Bulan" Placeholder="Month" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="Tanggal" Placeholder="Day" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="Tahun" Placeholder="Year" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                        Email Id
                    </td>
                    <td style="width: 400px;">
                        <asp:TextBox ID="TextBox2" runat="server" class="required email"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                    </td>
                    <td style="width: 400px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px;">
                    </td>
                    <td style="width: 400px;">
                        <asp:Button ID="Button1" class="button_style btn_validate" runat="server" Text="Button" />
                    </td>
                </tr>
            </table>
            </center>
        </div>--%>
        <section>
            <div class="container">
                <div class="inner2">
                    <h3><br />Registration</h3>

                    <asp:TextBox ID="mobilephone" Placeholder="Mobile Phone" runat="server" ></asp:TextBox>
                    <asp:TextBox ID="firstname" Placeholder="First Name" runat="server" ></asp:TextBox>
                    <asp:TextBox ID="lastname" Placeholder="Last Name" runat="server" ></asp:TextBox>
                    <%--<asp:Label ID="DOB" runat="server" text="Date of Birth" Font-Size="20px" Font-Names="Crete Round"></asp:Label>--%>
                    <p>Date of Birth</p>
                    <p><asp:DropDownList ID="Bulan" Placeholder="Month" runat="server" Font-Size="Large" Font-Names="Crete Round">
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
                    <asp:DropDownList ID="Tanggal" Placeholder="Day" runat="server" Font-Size="Large" Font-Names="Crete Round">
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
                    <asp:DropDownList ID="Tahun" Placeholder="Year" runat="server" Font-Size="Large" Font-Names="Crete Round">
                        <asp:ListItem Value="00">Year</asp:ListItem>
                    </asp:DropDownList></p>
                    <table class="table" style="width: 50%">
                        <tr>
                            <td style="width: 15%">
                                <asp:RadioButton ID="GenderMale" Placeholder="Gender" runat="server" Text="Male" Font-Size="Large" Font-Names="Crete Round" Width="100%"></asp:RadioButton>
                            </td>
                            <td style="width: 15%">
                                <asp:RadioButton ID="GenderFemale" Placeholder="Gender" runat="server" Text="Female" Font-Size="Large" Font-Names="Crete Round" Width="100%"></asp:RadioButton>
                            </td>
                        </tr>
                    </table>
                    <asp:TextBox ID="email" Placeholder="E-Mail" runat="server" ></asp:TextBox>

                    <asp:Button ID="Register" CssClass="btn" runat="server" text="Register" />
                </div>
            </div>
            <div class="inner1">
                <br />
                <br />
                <a class="fb"><asp:Button ID="Login" CssClass="btn" runat="server" text="Login" /></a>
                <br />
                <br />
            </div>
        </section>
    </form>
</body>
</html>
