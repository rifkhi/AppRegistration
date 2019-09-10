Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls


Public Class clsDataFiller
    Public mDSN As String


#Region " Constructor "

    Public Sub New(ByVal dsn As String)
        Me.mDSN = dsn
    End Sub

#End Region


    Public Function DataFill_SP(ByRef page As System.Web.UI.Page, ByRef datatable As DataTable, ByVal procedure As String, ByVal ParamNamecriteria As String, ByVal criteria As String, Optional ByVal ParamNamecriteria1 As String = "", Optional ByVal criteria1 As String = "", Optional ByVal ParamNamecriteria2 As String = "", Optional ByVal criteria2 As String = "") As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter

        dbCmd = New SqlCommand(procedure, dbConn)
        If ParamNamecriteria1.Trim <> "" And criteria1.Trim <> "" Then
            dbCmd.Parameters.Add(ParamNamecriteria1, SqlDbType.VarChar)
            dbCmd.Parameters(ParamNamecriteria1).Value = criteria1
        End If
        If ParamNamecriteria2.Trim <> "" And criteria2.Trim <> "" Then
            dbCmd.Parameters.Add(ParamNamecriteria2, SqlDbType.VarChar)
            dbCmd.Parameters(ParamNamecriteria2).Value = criteria2
        End If
        dbCmd.Parameters.Add(ParamNamecriteria, SqlDbType.VarChar)
        dbCmd.Parameters(ParamNamecriteria).Value = criteria

        dbCmd.CommandType = CommandType.StoredProcedure
        dbDA = New SqlDataAdapter(dbCmd)

        Try
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            'Throw ex
            MsgBoxOut(page, ex.Message.ToString)
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function

    Public Function DataFill_SP_Conn(ByRef dbConn As SqlConnection, ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String) As Boolean
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.Parameters.Add("@Criteria", SqlDbType.VarChar)
        dbCmd.Parameters("@Criteria").Value = criteria

        dbCmd.CommandType = CommandType.StoredProcedure
        dbDA = New SqlDataAdapter(dbCmd)
        Try
            dbDA.Fill(datatable)
        Catch ex As Exception
            Throw ex
        End Try

        Return True

    End Function

    Public Function DataFillLimit_SP(ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, ByVal limit As Integer) As Boolean
        Dim dbConn As OleDb.OleDbConnection = New OleDb.OleDbConnection(Me.mDSN)
        Dim dbCmd As OleDb.OleDbCommand
        Dim dbDA As OleDb.OleDbDataAdapter

        dbCmd = New OleDb.OleDbCommand(procedure, dbConn)
        dbCmd.Parameters.Add("@Criteria", Data.OleDb.OleDbType.VarChar)
        dbCmd.Parameters("@Criteria").Value = criteria

        dbCmd.Parameters.Add("@Limit", Data.OleDb.OleDbType.Integer)
        dbCmd.Parameters("@Limit").Value = limit

        dbCmd.CommandType = CommandType.StoredProcedure
        dbDA = New OleDb.OleDbDataAdapter(dbCmd)

        Try
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function


    Public Function DataFillForCombo(ByRef page As System.Web.UI.Page, ByVal valuemember As String, ByVal displaymember As String, ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal channel_id As String = "") As Boolean
        Dim row As System.Data.DataRow

        datatable.Clear()
        row = datatable.NewRow
        row.Item(valuemember) = "0"
        row.Item(displaymember) = " -- PILIH -- "
        datatable.Rows.InsertAt(row, 0)

        If procedure <> "" Then
            Me.DataFill(page, datatable, procedure, criteria, channel_id)
        End If

        Return True
    End Function

    Public Function DataFillComboNoDataColumn(ByRef page As System.Web.UI.Page, ByVal valuemember As String, ByVal displaymember As String, ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal withOption As Boolean = True, Optional ByVal channel_id As String = "") As Boolean
        Dim row As System.Data.DataRow
        Dim dc As New System.Data.DataColumn

        If withOption = True Then
            datatable.Clear()
            datatable.Columns.Clear()
            datatable.Columns.Add(New DataColumn(displaymember))
            datatable.Columns.Add(New DataColumn(valuemember))
            row = datatable.NewRow
            row.Item(displaymember) = " -- PILIH -- "
            row.Item(valuemember) = "0"
            datatable.Rows.InsertAt(row, 0)
        End If
        If procedure <> "" Then
            Me.DataFill(page, datatable, procedure, criteria, channel_id)
        End If

        Return True
    End Function


    Public Function ComboFill(ByRef page As System.Web.UI.Page, ByRef combobox As DropDownList, ByVal valuemember As String, ByVal displaymember As String, ByRef datatable As DataTable, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "", Optional ByVal WithOption As Boolean = False, Optional ByVal WithOptionSpec As Boolean = False) As Boolean
        Dim row As System.Data.DataRow

        datatable.Clear()

        If procedure <> "" Then
            Me.DataFill(page, datatable, procedure, criteria, orderby)
        End If

        If WithOption = True Then
            row = datatable.NewRow
            row.Item(displaymember) = " -- PILIH -- "
            row.Item(valuemember) = "0"
            datatable.Rows.InsertAt(row, 0)
        End If
        If WithOptionSpec = True Then
            row = datatable.NewRow
            row.Item(displaymember) = " -- PILIH -- "
            row.Item(valuemember) = " -- PILIH -- "
            datatable.Rows.InsertAt(row, 0)
        End If

        combobox.DataSource = datatable
        combobox.DataTextField = displaymember
        combobox.DataValueField = valuemember
        combobox.DataBind()

        Return True
    End Function

    Public Function ComboLink(ByRef combobox As DropDownList, ByVal valuemember As String, ByVal displaymember As String, ByRef datatable As DataTable, ByVal withOption As Boolean) As Boolean
        Dim row As System.Data.DataRow

        If withOption Then
            row = datatable.NewRow
            row.Item(valuemember) = "0"
            row.Item(displaymember) = " -- PILIH -- "
            datatable.Rows.InsertAt(row, 0)
        End If

        combobox.DataSource = datatable
        combobox.DataValueField = valuemember
        combobox.DataTextField = displaymember
        combobox.DataBind()

        Return True
    End Function


    Public Function DataFillField(ByRef datatable As DataTable, ByVal procedure As String, ByVal field As String, ByVal criteria As String, Optional ByVal channel_id As String = "") As Boolean
        Dim dbConn As OleDb.OleDbConnection = New OleDb.OleDbConnection(Me.mDSN)
        Dim dbCmd As OleDb.OleDbCommand
        Dim dbDA As OleDb.OleDbDataAdapter

        dbCmd = New OleDb.OleDbCommand(procedure, dbConn)
        If channel_id <> "" Then
            dbCmd.Parameters.Add("@channel_id", Data.OleDb.OleDbType.VarChar)
            dbCmd.Parameters("@channel_id").Value = channel_id
        End If
        dbCmd.Parameters.Add("@field", Data.OleDb.OleDbType.VarChar)
        dbCmd.Parameters("@field").Value = field
        dbCmd.Parameters.Add("@Criteria", Data.OleDb.OleDbType.VarChar)
        dbCmd.Parameters("@Criteria").Value = criteria

        dbCmd.CommandType = CommandType.StoredProcedure
        dbCmd.CommandTimeout = 0
        dbDA = New OleDb.OleDbDataAdapter(dbCmd)

        Try
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function

    Public Function DataFillConfig(ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter

        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)

        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0
        dbDA = New SqlDataAdapter(dbCmd)

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function

    Public Function DataFill(ByRef page As System.Web.UI.Page, ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter

        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)

        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0
        dbDA = New SqlDataAdapter(dbCmd)

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            'Throw ex
            MsgBoxOut(page, ex.Message.ToString)
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function

    Public Function DataFillint(ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal orderby As String = "") As Integer
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter
        Dim rslt As Integer = 0

        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)

        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0
        dbDA = New SqlDataAdapter(dbCmd)

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            datatable = New DataTable()
            rslt = dbDA.Fill(datatable)
            dbDA.Dispose()
        Catch ex As Exception
            'Throw ex
            'MsgBoxOut(Page, ex.Message.ToString)
            rslt = 0
        Finally
            dbConn.Close()
        End Try

        Return rslt

    End Function

    Public Function DataFillWithOption(ByRef page As System.Web.UI.Page, ByRef datatable As DataTable, ByVal procedure As String, ByVal criteria As String, Optional ByVal withOption As Boolean = False) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbDA As SqlDataAdapter

        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If withOption = True Then
            procedure = procedure & " OPTION (EXPAND VIEWS)"
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        'If criteria <> "" Then
        '    dbCmd.Parameters.Add("@Criteria", SqlDbType.VarChar)
        '    dbCmd.Parameters("@Criteria").Value = criteria
        'End If

        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0
        dbDA = New SqlDataAdapter(dbCmd)

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbDA.Fill(datatable)
        Catch ex As Exception
            'Throw ex
            MsgBoxOut(page, ex.Message.ToString)
        Finally
            dbConn.Close()
        End Try

        Return True

    End Function

    Public Function Reader(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Object
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dbReader As SqlDataReader = Nothing

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbReader = dbCmd.ExecuteReader
            'Return dbReader
        Catch ex As SqlException
            MsgBoxOut(page, ex.Message.ToString)
            'Throw New ApplicationException(ex.Message)
        Finally
            dbConn.Close()
        End Try

        Return dbReader
    End Function

    Public Function GetDataScalar(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim rslt As String = ""

        If criteria <> "" Then
            procedure = procedure & " where " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()

            rslt = dbCmd.ExecuteScalar()
        Catch ex As SqlException
            'MsgBoxOut(page, ex.Message.ToString)
            'Throw New ApplicationException(ex.Message)
            rslt = ""
        Finally
            dbConn.Close()
        End Try

        Return rslt
    End Function

    Public Function GetDataScalar(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim rslt As String = ""

        If criteria <> "" Then
            procedure = procedure & " where " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()

            rslt = dbCmd.ExecuteScalar().ToString()
        Catch ex As SqlException
            'MsgBoxOut(page, ex.Message.ToString)
            Throw New ApplicationException(ex.Message)
            rslt = ""
        Finally
            dbConn.Close()
        End Try

        Return rslt
    End Function

    Public Function GetDataScalarInteger(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Integer
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim rslt As Integer = 0

        If criteria <> "" Then
            procedure = procedure & " where " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            rslt = CInt(dbCmd.ExecuteScalar().ToString())
        Catch ex As SqlException
            rslt = 0
        Finally
            dbConn.Close()
        End Try

        Return rslt
    End Function

    Public Function GetString(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dtReader As SqlDataReader
        Dim hsl As String

        hsl = ""
        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dtReader = dbCmd.ExecuteReader
            If dtReader.HasRows Then
                dtReader.Read()
                If IsDBNull(dtReader.GetValue(0)) Then
                    hsl = ""
                    dtReader.Close()
                Else
                    hsl = dtReader.GetValue(0)
                    dtReader.Close()
                End If
            End If
        Catch ex As Exception
            MsgBoxOut(page, ex.Message.ToString)
            'Throw New ApplicationException(ex.Message)
        Finally
            dbConn.Close()
        End Try

        GetString = hsl

    End Function

    Public Function GetString(ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As String
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dtReader As SqlDataReader
        Dim hsl As String

        hsl = ""
        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dtReader = dbCmd.ExecuteReader
            If dtReader.HasRows Then
                dtReader.Read()
                If IsDBNull(dtReader.GetValue(0)) Then
                    hsl = ""
                    dtReader.Close()
                Else
                    hsl = dtReader.GetValue(0)
                    dtReader.Close()
                End If
            End If
        Catch ex As Exception
            'MsgBoxOut(page, ex.Message.ToString)
            Throw New ApplicationException(ex.Message)
        Finally
            dbConn.Close()
        End Try

        GetString = hsl

    End Function

    Public Function GetInt(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Integer
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dtReader As SqlDataReader
        Dim hsl As Integer

        hsl = 0
        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dtReader = dbCmd.ExecuteReader
            If dtReader.HasRows Then
                dtReader.Read()
                If IsDBNull(dtReader.GetValue(0)) Then
                    hsl = 0
                    dtReader.Close()
                Else
                    hsl = dtReader.GetValue(0)
                    dtReader.Close()
                End If
            End If
        Catch ex As Exception
            MsgBoxOut(page, ex.Message.ToString)
            'Throw New ApplicationException(ex.Message)
        Finally
            dbConn.Close()
        End Try

        GetInt = hsl

    End Function

    Public Function GetDec(ByRef page As System.Web.UI.Page, ByVal procedure As String, Optional ByVal criteria As String = "", Optional ByVal orderby As String = "") As Decimal
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand
        Dim dtReader As SqlDataReader
        Dim hsl As Decimal

        hsl = 0.0
        If criteria <> "" Then
            procedure = procedure & " WHERE " & criteria
        End If

        If orderby <> "" Then
            procedure = procedure & " ORDER BY " & orderby
        End If

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandType = CommandType.Text
        dbCmd.CommandTimeout = 0

        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dtReader = dbCmd.ExecuteReader
            If dtReader.HasRows Then
                dtReader.Read()
                If IsDBNull(dtReader.GetValue(0)) Then
                    hsl = 0.0
                    dtReader.Close()
                Else
                    hsl = dtReader.GetValue(0)
                    dtReader.Close()
                End If
            End If
        Catch ex As Exception
            MsgBoxOut(page, ex.Message.ToString)
            'Throw New ApplicationException(ex.Message)
        Finally
            dbConn.Close()
        End Try

        GetDec = hsl

    End Function

    Public Function Insert_Update_Data(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandTimeout = 0
        dbCmd.CommandType = Data.CommandType.Text
        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbCmd.ExecuteNonQuery()
            Insert_Update_Data = True
        Catch ex As Exception
            Insert_Update_Data = False
            MsgBoxOut(page, ex.Message.ToString)
            'Throw ex
        Finally
            dbConn.Close()
        End Try
    End Function

    Public Function Insert_Update_Data(ByVal procedure As String) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandTimeout = 0
        dbCmd.CommandType = Data.CommandType.Text
        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbCmd.ExecuteNonQuery()
            Insert_Update_Data = True
        Catch ex As Exception
            Insert_Update_Data = False
            MsgBoxOut("Insert Update Data Error", ex.Message.ToString)
        Finally
            dbConn.Close()
        End Try
    End Function

    Public Function Delete_Data(ByRef page As System.Web.UI.Page, ByVal procedure As String) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandTimeout = 0
        dbCmd.CommandType = Data.CommandType.Text
        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbCmd.ExecuteNonQuery()
            Delete_Data = True
        Catch ex As Exception
            Delete_Data = False
            MsgBoxOut(page, ex.Message.ToString)
            'Throw ex
        End Try
    End Function

    Public Function ExecQuery(ByVal procedure As String) As Boolean
        Dim dbConn As SqlConnection = New SqlConnection(Me.mDSN)
        Dim dbCmd As SqlCommand

        dbCmd = New SqlCommand(procedure, dbConn)
        dbCmd.CommandTimeout = 0
        dbCmd.CommandType = Data.CommandType.Text
        Try
            If dbConn.State = ConnectionState.Open Then
                dbConn.Close()
            End If
            dbConn.Open()
            dbCmd.ExecuteNonQuery()
            ExecQuery = True
        Catch ex As Exception
            ExecQuery = False
            MsgBoxOut("Execute Query Error", ex.Message.ToString)
        Finally
            dbConn.Close()
        End Try
    End Function

End Class
