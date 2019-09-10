Imports System
Imports System.Data


Public Class clsDataset

    Public Shared Function CreateTblMstSetting() As DataTable
        Dim tbl As DataTable = New DataTable

        tbl.Columns.Clear()
        tbl.Columns.Add(New DataColumn("setting_id", GetType(System.String)))
        tbl.Columns.Add(New DataColumn("setting_value", GetType(System.String)))

        '-------------------------------
        'Default Value: 
        tbl.Columns("setting_id").DefaultValue = ""
        tbl.Columns("setting_value").DefaultValue = ""

        Return tbl
    End Function

End Class
