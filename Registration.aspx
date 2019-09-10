<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registration.aspx.vb" Inherits="AppRegistration.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>

    <link href="Style.css" rel="stylesheet" />

    <style>
        @import url('https://fonts.googleapis.com/family=bitter|crete+round|pacifico');
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="container">
                <div class="inner1">
                    <span> Sign In with Social Network</span><br />
                    <br />
                    <a href="#" class="fb">Log In with Facebook</a>
                    <br />
                    <a href="#" class="tw">Log In with Twitter</a>
                    <br />
                    <a href="#" class="gl">Log In with Google+</a>
                    <br />
                </div>
                <div class="inner2">
                    <h3>Registration</h3>

                    <asp:TextBox ID="mobilephone" Placeholder="Mobile Phone" runat="server" ></asp:TextBox>
                    <asp:TextBox ID="firstname" Placeholder="First Name" runat="server" ></asp:TextBox>
                    <asp:TextBox ID="lastname" Placeholder="Last Name" runat="server" ></asp:TextBox>
                    <asp:DropDownList ID="Bulan" Placeholder="Month" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="Tanggal" Placeholder="Day" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="Tahun" Placeholder="Year" runat="server"></asp:DropDownList>
                    <asp:RadioButton ID="GenderMale" Placeholder="Gender" runat="server" Text="Male"></asp:RadioButton>
                    <asp:RadioButton ID="GenderFemale" Placeholder="Gender" runat="server" Text="Female"></asp:RadioButton>
                    <asp:TextBox ID="email" Placeholder="E-Mail" runat="server" ></asp:TextBox>

                    <asp:Button ID="Register" CssClass="btn" runat="server" text="Register"/>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
