Imports System
Imports System.Data
Imports System.Data.SqlClient


Public Class LoginForm
    Inherits System.Web.UI.Page

    Private Main As New clsMain

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then

            Dim thn As Integer = DateTime.Now.Year
            For i As Integer = thn - 50 To thn
                Dim li As ListItem = New ListItem(i)
                Me.Tahun.Items.Add(li)
            Next

            Me.Login.Text = "Login"

        End If

    End Sub

    Protected Sub Register_Click(sender As Object, e As EventArgs) Handles Register.Click

    End Sub


End Class