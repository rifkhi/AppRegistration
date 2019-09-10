Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Web
Imports System.Web.Mail
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Web.UI.WebControls


Public Class clsMain

    Public Enum FormSaveResult
        Nochanges = 0
        SaveError = 1
        SaveSuccess = 2
    End Enum

    Public mDSN As String
    Public mConnDSN As SqlConnection
    Public mDataFiller As clsDataFiller
    Public SqlText As New SqlCommand

    Private sqlConn As New SqlConnection
    Private sConnect As String
    Private mConnect As String
    Private mBrowser As Object = Nothing
    Private mDBSERVER As String = ConfigurationManager.AppSettings("DBSERVER")
    Private mDBNAME As String = ConfigurationManager.AppSettings("DBNAME")
    Private mDBUSER As String = ConfigurationManager.AppSettings("DBUSER")
    Private mDBPASSWORD As String = ConfigurationManager.AppSettings("DBPASSWORD")
    Private mDSNTemp As String = "User ID={0}; Password={1}; Data Source=""{2}""; Initial Catalog={3}; Connect Timeout=0;"

    Public tbl_MstSetting As DataTable = clsDataset.CreateTblMstSetting()


#Region " Property "

    Public ReadOnly Property ConnDSN() As SqlConnection
        Get
            Return mConnDSN
        End Get
    End Property

    Public ReadOnly Property DSN() As String
        Get
            Return mDSN
        End Get
    End Property

    Public ReadOnly Property Browser() As Object
        Get
            Return mBrowser
        End Get
    End Property

    Public Property strConnect() As String
        Get
            Return mConnect
        End Get
        Set(ByVal value As String)
            mConnect = value
        End Set
    End Property

#End Region

#Region " Database Services "

    Public Function ExecQuery(ByVal procedure As String) As Boolean
        Return mDataFiller.ExecQuery(procedure)
    End Function

    Public Function Insert_Update_Data(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Boolean
        Return mDataFiller.Insert_Update_Data(page, procedure)
    End Function

    Public Function Insert_Update_Data(ByVal procedure As String) As Boolean
        Return mDataFiller.Insert_Update_Data(procedure)
    End Function

    Public Function Delete_Data(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Boolean
        Return mDataFiller.Delete_Data(page, procedure)
    End Function


    Public Function GetString(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Return mDataFiller.GetString(page, procedure, criteria, orderby)
    End Function

    Public Function GetString(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Return mDataFiller.GetString(procedure, criteria, orderby)
    End Function

    Public Function GetInt(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Integer
        Return mDataFiller.GetInt(page, procedure, criteria, orderby)
    End Function

    Public Function GetDec(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Decimal
        Return mDataFiller.GetDec(page, procedure, criteria, orderby)
    End Function

    Public Function GetDataScalar(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Return mDataFiller.GetDataScalar(procedure, criteria, orderby)
    End Function

    Public Function GetDataScalar(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Return mDataFiller.GetDataScalar(page, procedure, criteria, orderby)
    End Function

    Public Function GetDataScalarInteger(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Integer
        Return mDataFiller.GetDataScalar(procedure, criteria, orderby)
    End Function

    Public Function Reader(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Object
        Return Me.mDataFiller.Reader(page, procedure)
    End Function

    Public Function DataFillConfig(ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Boolean
        dt.Clear()
        Return mDataFiller.DataFillConfig(dt, procedure, criteria, orderby)
    End Function

    Public Function DataFill(ByRef page As System.Web.UI.Page, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Boolean
        dt.Clear()
        Return mDataFiller.DataFill(page, dt, procedure, criteria, orderby)
    End Function

    Public Function DataFillint(ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Boolean
        dt.Clear()
        Return mDataFiller.DataFillint(dt, procedure, criteria, orderby)
    End Function

    Public Function DataFillWithOption(ByRef page As System.Web.UI.Page, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal withOption As Boolean = False) As Boolean
        dt.Clear()
        Return mDataFiller.DataFillWithOption(page, dt, procedure, criteria, withOption)
    End Function


    Public Function DataFillForCombo(ByRef page As System.Web.UI.Page, ByVal valuemember As String, ByVal displaymember As String, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal channel_id As String = "") As Boolean
        Return mDataFiller.DataFillForCombo(page, valuemember, displaymember, dt, procedure, criteria, channel_id)
    End Function

    Public Function DataFillComboNoDataColumn(ByRef page As System.Web.UI.Page, ByVal valuemember As String, ByVal displaymember As String, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal withOption As Boolean = True, Optional ByVal channel_id As String = "") As Boolean
        Return mDataFiller.DataFillComboNoDataColumn(page, valuemember, displaymember, dt, procedure, criteria, withOption, channel_id)
    End Function


    Public Function ComboFill(ByRef page As System.Web.UI.Page, ByRef combobox As DropDownList, ByVal valuemember As String, ByVal displaymember As String, ByRef dt As DataTable, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "", Optional ByVal WithOption As Boolean = False, Optional ByVal WithOptionSpec As Boolean = False) As Boolean
        Return mDataFiller.ComboFill(page, combobox, valuemember, displaymember, dt, procedure, criteria, orderby, WithOption, WithOptionSpec)
    End Function

    Public Function ComboLink(ByRef combobox As DropDownList, ByVal valuemember As String, ByVal displaymember As String, ByRef datatable As DataTable, ByVal withOption As Boolean) As Boolean
        Return mDataFiller.ComboLink(combobox, valuemember, displaymember, datatable, withOption)
    End Function

    Public Function ComboFillFromDataTable(ByRef combobox As DropDownList, ByVal valuemember As String, ByVal displaymember As String, ByRef dt As DataTable) As Boolean
        combobox.DataSource = dt
        combobox.DataValueField = valuemember
        combobox.DataTextField = displaymember
        combobox.DataBind()

        Return True
    End Function


    Public Function DataFill_SP(ByRef page As System.Web.UI.Page, ByRef dt As DataTable, ByVal procedure As String, ByVal ParamNamecriteria As String, ByVal criteria As String, Optional ByVal ParamNamecriteria1 As String = "", Optional ByVal criteria1 As String = "", Optional ByVal ParamNamecriteria2 As String = "", Optional ByVal criteria2 As String = "") As Boolean
        dt.Clear()
        Return mDataFiller.DataFill_SP(page, dt, procedure, ParamNamecriteria, criteria, ParamNamecriteria1, criteria1, ParamNamecriteria2, criteria2)
    End Function

    Public Function DataFill_SP_Conn(ByRef dbConn As SqlConnection, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String) As Boolean
        dt.Clear()
        Return mDataFiller.DataFill_SP_Conn(dbConn, dt, procedure, criteria)
    End Function

    Public Function DataFillLimit_SP(ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String, ByVal limit As Integer, Optional ByVal channel_id As String = "") As Boolean
        dt.Clear()
        Return mDataFiller.DataFillLimit_SP(dt, procedure, criteria, limit)
    End Function


    Public Function DataFillFromCache(ByRef page As System.Web.UI.Page, ByRef dt As DataTable, ByVal procedure As String, ByVal criteria As String) As Boolean
        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            If Me.Browser IsNot Nothing Then
                If Me.Browser.IsDataTableCached(procedure, criteria) Then
                    dt.Clear()
                    dt = Me.Browser.GetCachedDataTable(procedure, criteria)
                Else
                    dt.Clear()
                    Return Me.mDataFiller.DataFill(page, dt, procedure, criteria)
                End If
            Else
                dt.Clear()
                Return Me.mDataFiller.DataFill(page, dt, procedure, criteria)
            End If

            Return True
        Catch ex As Exception
            MsgBoxOut(page, ex.Message)
            Return False
        End Try
    End Function

    Public Function DataFillFieldFromCache(ByRef dt As DataTable, ByVal procedure As String, ByVal field As String, ByVal criteria As String) As Boolean
        Try
            If dt Is Nothing Then
                dt = New DataTable
            End If

            If Me.Browser IsNot Nothing Then
                If Me.Browser.IsDataTableCached(procedure, field, criteria) Then
                    dt.Clear()
                    dt = Me.Browser.GetCachedDataTable(procedure, field, criteria)
                Else
                    dt.Clear()
                    Return Me.mDataFiller.DataFillField(dt, procedure, field, criteria)
                End If
            Else
                dt.Clear()
                Return Me.mDataFiller.DataFillField(dt, procedure, field, criteria)
            End If

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function RefreshCachedTable(ByVal procedure As String, ByVal criteria As String) As Boolean
        'dim refreshed as 
        If Me.Browser IsNot Nothing Then
            Me.Browser.RefreshCachedDataTable(procedure, criteria)
        End If

        Return True
    End Function

    Public Function RefreshCachedTableField(ByVal procedure As String, ByVal field As String, ByVal criteria As String) As Boolean
        'dim refreshed as 
        If Me.Browser IsNot Nothing Then
            Me.Browser.RefreshCachedDataTable(procedure, field, criteria)
        End If

        Return True
    End Function

#End Region


    Public Sub New()
        MyBase.New()
        GetConnect(strConnect)
    End Sub

    Public Function GetConnect(Optional ByVal strConnect As String = "") As Boolean
        If strConnect = "" Then
            Me.mConnect = String.Format(mDSNTemp, mDBUSER, mDBPASSWORD, mDBSERVER, mDBNAME) 'ConfigurationManager.AppSettings("CIMB").ToString
        Else
            Me.mConnect = strConnect
        End If

        Try
            If Me.sqlConn.ConnectionString = "" Then
                Me.sqlConn.ConnectionString = Me.mConnect
            End If

            mConnDSN = New SqlConnection(mConnect)
            System.Data.SqlClient.SqlConnection.ClearPool(New System.Data.SqlClient.SqlConnection(Me.mConnect))

            RefreshConn()

            If Me.DSN = "" Then
                Me.mDSN = Me.mConnect
                Me.mDataFiller = New clsDataFiller(Me.mDSN)

                DataFillConfig(tbl_MstSetting, "SELECT * FROM " & ConfigurationManager.AppSettings("MSTSettings").ToString, "")
            Else
                Me.mDSN = Me.mConnect
                Me.mDataFiller = New clsDataFiller(Me.mDSN)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub RefreshConn()
        If sqlConn.State = Data.ConnectionState.Open Then
            sqlConn.Close()
        End If
        If CheckNetworkCondition() = True Then
            sqlConn.Open()
        End If
    End Sub

    Public Function CheckNetworkCondition() As Boolean
        Try
            ''AndAlso ServerAccessible()
            If My.Computer.Network.IsAvailable AndAlso PingOK() Then
                Return True
            Else 'CLEARALLPOOLS IS THE SOLUTION
                'System.Data.SqlClient.SqlConnection.ClearAllPools()
                System.Data.SqlClient.SqlConnection.ClearPool(New System.Data.SqlClient.SqlConnection(Me.sConnect))
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function PingOK() As Boolean
        Try
            Dim ping As Ping = New Ping()
            Dim pingreply As PingReply = ping.Send(ConfigurationManager.AppSettings("ServerName").ToString)

            If pingreply.Status = IPStatus.Success Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

End Class
