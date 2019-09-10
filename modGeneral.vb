Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Microsoft.VisualBasic
Imports System.Security.Cryptography


Module modGeneral

    Public mssge As String = ""
    Private Const KEY As String = "CIMB NIAGA - NON BRANCH CHANNEL" '"<random value goes here>"

    <System.Runtime.CompilerServices.Extension()> _
    Public Function EncryptAndHash(ByVal value As String) As String
        Dim des As New MACTripleDES()
        Dim md5 As New MD5CryptoServiceProvider()
        des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY))
        Dim encrypted As String = Convert.ToBase64String(des.ComputeHash(Encoding.UTF8.GetBytes(value))) + "-"c + Convert.ToBase64String(Encoding.UTF8.GetBytes(value))

        Return HttpUtility.UrlEncode(encrypted)
    End Function

    ''' <summary>
    ''' Returns null if string has been modified since encryption
    ''' </summary>
    ''' <param name="encoded"></param>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension()> _
    Public Function DecryptWithHash(ByVal encoded As String) As String
        Dim des As New MACTripleDES()
        Dim md5 As New MD5CryptoServiceProvider()
        des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY))

        Dim decoded As String = HttpUtility.UrlDecode(encoded)
        ' in the act of url encoding and decoding, plus (valid base64 value) gets replaced with space (invalid base64 value). this reverses that.
        decoded = decoded.Replace(" ", "+")
        Dim value As String = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split("-"c)(1)))
        Dim savedHash As String = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split("-"c)(0)))
        Dim calculatedHash As String = Encoding.UTF8.GetString(des.ComputeHash(Encoding.UTF8.GetBytes(value)))

        If savedHash <> calculatedHash Then
            Return Nothing
        End If

        Return value
    End Function

    Public Sub MsgBoxOut(ByVal oPage As Object, ByVal sPesan As String)
        If Not CType(oPage, System.Web.UI.Page).ClientScript.IsStartupScriptRegistered("pesan") Then
            CType(oPage, System.Web.UI.Page).ClientScript.RegisterStartupScript(CType(oPage, System.Web.UI.Page).GetType(), "pesan", "<script type=""text/javascript"" language=""javascript"">alert('" & sPesan & "');</script>")
        End If
    End Sub

    Public Sub GotoPrevPage(ByVal oPage As Object)
        Dim xScript As String = "<script language=JavaScript>" & _
                   "history.go(-1)" & " </script>"
        If Not CType(oPage, System.Web.UI.Page).ClientScript.IsStartupScriptRegistered("gotoprev") Then
            CType(oPage, System.Web.UI.Page).ClientScript.RegisterStartupScript(CType(oPage, System.Web.UI.Page).GetType(), "gotoprev", xScript)
        End If
    End Sub

    Public Sub GotoPage(ByVal oPage As Object, ByVal strUrl As String)
        Dim xScript As String = "<script language=JavaScript>" & _
                   "window.open('" & strUrl & "', '_self') </script>"
        If Not CType(oPage, System.Web.UI.Page).ClientScript.IsStartupScriptRegistered("gotopage") Then
            CType(oPage, System.Web.UI.Page).ClientScript.RegisterStartupScript(CType(oPage, System.Web.UI.Page).GetType(), "gotopage", xScript)
        End If
    End Sub

    Public Sub ClosePage(ByVal oPage As Object)
        Dim xScript As String = "<script language=JavaScript>" & _
                   "window.close(); </script>"
        If Not CType(oPage, System.Web.UI.Page).ClientScript.IsStartupScriptRegistered("closePage") Then
            CType(oPage, System.Web.UI.Page).ClientScript.RegisterStartupScript(CType(oPage, System.Web.UI.Page).GetType(), "closePage", xScript)
        End If
    End Sub

    Public Function cekNull(ByVal sF As Object) As String
        If IsDBNull(sF) Then
            cekNull = ""
        Else
            cekNull = sF
        End If
    End Function

    Public Function cekNullDate(ByVal sF As Object) As DateTime
        If IsDBNull(sF) Then
            cekNullDate = CDate("12/31/9999")
        Else
            cekNullDate = sF
        End If
    End Function

    Public Function GenerateNumber(ByVal str As String) As String
        Select Case Len(str)
            Case "4"

            Case "3"

            Case "2"

            Case "1"

            Case Else

        End Select
        Return str
    End Function

End Module
