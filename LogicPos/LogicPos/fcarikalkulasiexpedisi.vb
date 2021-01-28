Imports System.Data.Odbc

Public Class fcarikalkulasiexpedisi
    Dim pilih As String
    Dim kode As String
    Private Sub fcarikalkulasiexpedisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "id"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_pengiriman"

        GridColumn3.Caption = "Nama Expedisi"
        GridColumn3.FieldName = "nama_supplier"

        GridColumn4.Caption = "Alamat Expedisi"
        GridColumn4.FieldName = "alamat_expedisi"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_pengiriman"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_pengiriman WHERE DATE(tb_pengiriman.tgl_pengiriman) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_pengiriman WHERE tb_pengiriman.tgl_pengiriman BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If
        Else
            sql = "SELECT * FROM tb_pengiriman WHERE tb_pengiriman.tgl_pengiriman"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcarikalkulasiexpedisi = 1 Then
            fkalkulasiexpedisi.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("id")
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