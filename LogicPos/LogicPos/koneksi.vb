Imports System.Data.Odbc

Module koneksi
    'koneksi
    Public cnn As New OdbcConnection
    Public cmmd As OdbcCommand
    Public dr As OdbcDataReader
    Public drpenjualan As OdbcDataReader
    Public drpembelian As OdbcDataReader
    Public drlunaspenjualan As OdbcDataReader
    Public drlunaspembelian As OdbcDataReader

    Public sql As String
    Public da As OdbcDataAdapter
    Public ds As DataSet
    Public strConn As String = "DSN=dsn_logicpos"
    '=========================

    'counter printing

    Dim counter As Integer

    '=========================

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

    Public Sub historysave(keterangan As String, kode As String)
        Call koneksii()
        sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('" & keterangan & "','" & kode & "','" & fmenu.statususer.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
    End Sub

    Public Function cekcetakan(nomor As String) As Boolean
        Call koneksii()
        sql = "SELECT IFNULL(COUNT(*), 0) AS penghitung FROM tb_history_user WHERE keterangan_history LIKE '%mencetak%' AND created_by='" & fmenu.statususer.Text & "' AND kode_tabel = '" & nomor & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            counter = dr("penghitung")

            If counter < flogin.maxprinting Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function
End Module