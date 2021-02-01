Imports System.Data.Odbc

Public Class fcarikalkulasipengiriman
    Dim pilih As String
    Dim kode As String
    Private Sub fcarikalkulasipengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        GridColumn2.FieldName = "tgl_kirim"

        GridColumn3.Caption = "Nama Supplier"
        GridColumn3.FieldName = "nama_supplier"

        GridColumn4.Caption = "Alamat Expedisi"
        GridColumn4.FieldName = "alamat_expedisi"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_kirim"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_kirim WHERE DATE(tb_kirim.tgl_kirim) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_kirim WHERE DATE(tb_kirim.tgl_kirim) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If
        Else
            sql = "SELECT * FROM tb_kirim WHERE tb_kirim.tgl_kirim"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcarikalkulasipengiriman = 1 Then
            fkalkulasipengiriman.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("id")
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