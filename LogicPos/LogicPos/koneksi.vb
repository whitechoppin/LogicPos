Imports System.Data.Odbc

Module koneksi
    Public cnn As New OdbcConnection
    Public cmmd As OdbcCommand
    Public dr As OdbcDataReader
    Public sql As String
    Public da As OdbcDataAdapter
    Public ds As DataSet
    Public strConn As String = "DSN=dsn_logicpos"

    Public Sub koneksii()
        Dim keluar As MsgBoxResult
        'cnn = New OdbcConnection(strConn)
        'If cnn.State <> ConnectionState.Closed Then cnn.Close()
        'cnn.Open()
        Try
            cnn = New OdbcConnection("DSN=dsn_logicpos;MultipleActiveResultSets=True")
            If cnn.State = ConnectionState.Closed Then
                cnn.Open()
            End If
        Catch ex As Exception
            MsgBox("Koneksi ke Database bermasalah, Periksa koneksi Jaringan Anda.")
            keluar = MsgBox("Aplikasi akan di Close karena tidak terkoneksi database.", MsgBoxStyle.OkOnly, "Peringatan!!!")
            If keluar = MsgBoxResult.Ok Then
                End
            End If
        End Try
    End Sub

End Module
