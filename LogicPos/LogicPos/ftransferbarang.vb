Imports System.Data.Odbc

Public Class ftransferbarang
    Public tabel As DataTable
    'variabel dalam penjualan
    Public jenis, satuan, kodebarangkeluar, kodetransaksi As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglbarangkeluar As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    'variabel edit penjualan
    Dim countingbarang As Integer
    Private Sub ftransferbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        'kodebarangkeluar = currentnumber()
        'Call inisialisasi(kodebarangkeluar)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
        End With
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_transfer_barang,3) FROM tb_transfer_barang WHERE DATE_FORMAT(MID(`kode_transfer_barang`, 3 , 6), ' %y ')+ MONTH(MID(`kode_transfer_barang`,3 , 6)) + DAY(MID(`kode_transfer_barang`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_transfer_barang,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "TB" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "TB" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "TB" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "TB" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function

    Function currentnumber()
        Call koneksii()
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang ORDER BY kode_transfer_barang DESC LIMIT 1;"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Return dr.Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang WHERE date_created < (SELECT date_created FROM tb_transfer_barang WHERE kode_transfer_barang = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
            Else
                'Call inisialisasi(previousnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang WHERE date_created > (SELECT date_created FROM tb_transfer_barang WHERE kode_transfer_barang = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
            Else
                'Call inisialisasi(nextingnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub

End Class