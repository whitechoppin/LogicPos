Imports System.Data.Odbc

Module modulekoneksi
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


    Public Sub koneksi()
        Dim status As MsgBoxResult
        'Dim DriverString As String
        Try
            'untuk koneksi biasa : select atau delete data gak penting
            'DriverString = "Driver={MySQL ODBC 8.0 ANSI Driver};Database=logicpos;server=localhost;User=root;Password=RumahLogika07092019;Option=67108864;"

            If cnn.State = ConnectionState.Closed Then
                cnn = New OdbcConnection("DSN=dsn_logicpos;MultipleActiveResultSets=True")
                'cnn = New OdbcConnection(DriverString)
                cnn.Open()
            End If

            'untuk koneksi data berbasis transaksi : sekali save query > 1 query
            If cnnx.State = ConnectionState.Closed Then
                cnnx = New OdbcConnection("DSN=dsn_logicpos;MultipleActiveResultSets=True")
                'cnnx = New OdbcConnection(DriverString)
                cnnx.Open()
            End If
        Catch ex As Exception
            MsgBox("Koneksi ke Database bermasalah, Periksa koneksi Jaringan Anda.")
            status = MsgBox("Refresh koneksi database logicpos ?", MsgBoxStyle.YesNo, "Peringatan !")
            If status = MsgBoxResult.Yes Then
                Call koneksi()
            Else
                End
            End If
        End Try
    End Sub

    Public Sub diskoneksi()
        If cnn.State = ConnectionState.Open Then
            cnn.Close()
        End If

        'untuk koneksi data berbasis transaksi : sekali save query > 1 query
        If cnnx.State = ConnectionState.Open Then
            cnnx.Close()
        End If
    End Sub

    Public Sub historysave(keterangan As String, kode As String, jenis As String)
        Call koneksi()
        sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, jenis_tabel, created_by, date_created) VALUES ('" & keterangan & "','" & kode & "','" & jenis & "','" & fmenu.kodeuser.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        cmmd.ExecuteNonQuery()
    End Sub

    Public Function cekcetakan(nomor As String, jenis As String) As Boolean
        Dim counter As Integer

        Call koneksi()
        sql = "SELECT IFNULL(COUNT(*), 0) AS penghitung FROM tb_history_user WHERE keterangan_history LIKE '%mencetak%' AND created_by='" & fmenu.kodeuser.Text & "' AND kode_tabel = '" & nomor & "' AND jenis_tabel='" & jenis & "'"
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