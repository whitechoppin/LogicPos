Imports System.Data.Odbc

Module koneksi
    Public cnn As New OdbcConnection
    Public cnnx As New OdbcConnection

    Public cmmd As OdbcCommand

    Public dr As OdbcDataReader

    Public drpenjualan As OdbcDataReader
    Public drpembelian As OdbcDataReader

    Public drlunaspenjualan As OdbcDataReader
    Public drlunaspembelian As OdbcDataReader

    Public sql As String
    Public da As OdbcDataAdapter
    Public ds As DataSet
    '=========================

    'counter printing
    Dim counter As Integer
    '=========================

    Public Sub koneksii()
        Dim status As MsgBoxResult
        Try
            'untuk koneksi biasa : select atau delete data gak penting
            If cnn.State = ConnectionState.Closed Then
                cnn = New OdbcConnection("DSN=dsn_logicpos;MultipleActiveResultSets=True")
                cnn.Open()
            End If

            'untuk koneksi data berbasis transaksi : sekali save query > 1 query
            If cnnx.State = ConnectionState.Closed Then
                cnnx = New OdbcConnection("DSN=dsn_logicpos;MultipleActiveResultSets=True")
                cnnx.Open()
            End If
        Catch ex As Exception
            MsgBox("Koneksi ke Database bermasalah, Periksa koneksi Jaringan Anda.")
            status = MsgBox("Refresh koneksi database logicpos ?", MsgBoxStyle.YesNo, "Peringatan !")
            If status = MsgBoxResult.Yes Then
                Call koneksii()
            Else
                End
            End If
        End Try
    End Sub

    Public Sub diskoneksii()
        If cnn.State = ConnectionState.Open Then
            cnn.Close()
        End If

        'untuk koneksi data berbasis transaksi : sekali save query > 1 query
        If cnnx.State = ConnectionState.Open Then
            cnnx.Close()
        End If
    End Sub

    Public Sub historysave(keterangan As String, kode As String)
        Call koneksii()
        sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('" & keterangan & "','" & kode & "','" & fmenu.kodeuser.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
    End Sub

    Public Function cekcetakan(nomor As String) As Boolean
        Call koneksii()
        sql = "SELECT IFNULL(COUNT(*), 0) AS penghitung FROM tb_history_user WHERE keterangan_history LIKE '%mencetak%' AND created_by='" & fmenu.kodeuser.Text & "' AND kode_tabel = '" & nomor & "'"
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