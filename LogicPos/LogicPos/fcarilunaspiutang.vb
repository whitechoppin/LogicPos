Imports System.Data.Odbc

Public Class fcarilunaspiutang
    Dim pilih As String
    Dim kode As String

    Private Sub fcarilunaspiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_lunas"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tanggal_transaksi"
        GridColumn3.Caption = "Nama Pelanggan"
        GridColumn3.FieldName = "nama_pelanggan"
        GridColumn4.Caption = "Bayar"
        GridColumn4.FieldName = "bayar_lunas"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"
        GridColumn5.Caption = "No Bukti"
        GridColumn5.FieldName = "no_bukti"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT tb_pelunasan_piutang.kode_lunas, tb_pelunasan_piutang.bayar_lunas, tb_pelunasan_piutang.tanggal_transaksi, tb_pelanggan.nama_pelanggan, tb_pelunasan_piutang.no_bukti FROM tb_pelunasan_piutang JOIN tb_pelanggan WHERE tb_pelunasan_piutang.kode_pelanggan = tb_pelanggan.kode_pelanggan AND DATE(tb_pelunasan_piutang.tanggal_transaksi) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb_pelunasan_piutang.kode_lunas, tb_pelunasan_piutang.bayar_lunas, tb_pelunasan_piutang.tanggal_transaksi, tb_pelanggan.nama_pelanggan, tb_pelunasan_piutang.no_bukti FROM tb_pelunasan_piutang JOIN tb_pelanggan WHERE tb_pelunasan_piutang.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_pelunasan_piutang.tanggal_transaksi BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If
        Else
            sql = "SELECT tb_pelunasan_piutang.kode_lunas, tb_pelunasan_piutang.bayar_lunas, tb_pelunasan_piutang.tanggal_transaksi, tb_pelanggan.nama_pelanggan, tb_pelunasan_piutang.no_bukti FROM tb_pelunasan_piutang JOIN tb_pelanggan WHERE tb_pelunasan_piutang.kode_pelanggan = tb_pelanggan.kode_pelanggan"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaripelunasanpiutang = 1 Then
            flunaspiutang.txtgolunas.Text = Me.GridView1.GetFocusedRowCellValue("kode_lunas")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub

    Private Sub cbperiode_CheckedChanged(sender As Object, e As EventArgs) Handles cbperiode.CheckedChanged
        If cbperiode.Checked = True Then
            dtawal.Enabled = True
            dtakhir.Enabled = True
        Else
            dtawal.Enabled = False
            dtakhir.Enabled = False
        End If
    End Sub
End Class