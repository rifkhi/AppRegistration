Imports System
Imports System.Data
Imports System.Data.SqlClient


Public Class RegistrationForm
    Inherits System.Web.UI.Page

    Private Main As New clsMain

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then

            Dim thn As Integer = DateTime.Now.Year
            For i As Integer = thn - 50 To thn
                Dim li As ListItem = New ListItem(i)
                Me.Tahun.Items.Add(li)
            Next

            Me.Login.Text = "Footer"

        End If

    End Sub

    Protected Sub Register_Click(sender As Object, e As EventArgs) Handles Register.Click
        Dim gender As String = ""

        If Me.GenderMale.Checked = True Then
            gender = "Male"
        Else
            If Me.GenderFemale.Checked = True Then
                gender = "Female"
            Else
                gender = ""
            End If
        End If

        Try
            If Main.Insert_Update_Data("INSERT INTO registration.dbo.Users(mobilephone,FirstName,LastName,dob,Gender,Email) VALUES('" & Me.mobilephone.Text.Trim() & "','" & Me.firstname.Text.Trim() & "', '" & Me.lastname.Text.Trim() & "','" & Me.Tahun.SelectedValue & "-" & Me.Bulan.SelectedValue & "-" & Me.Tanggal.SelectedValue & "','" & gender & "','" & email.Text.Trim & "')") Then
                Response.Redirect("LoginForm.aspx")
            Else
                MsgBoxOut(Me, "can't save the data!")
            End If
        Catch ex As Exception
            MsgBoxOut(Me, "there's an error you have to find out!")
        End Try
        

    End Sub


End Class