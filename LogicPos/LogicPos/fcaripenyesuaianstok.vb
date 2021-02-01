Imports System.Data.Odbc

Public Class fcaripenyesuaianstok
    Dim pilih As String
    Dim kode As String

    Public tabelbarang As DataTable
    Private Sub fcaripenyesuaianstok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        'With GridView1
        '    'agar muncul footer untuk sum/avg/count
        '    .OptionsView.ShowFooter = True
        '    'buat sum harga
        '    .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
        'End With
        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "id"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tanggal_penyesuaian_stok"

        GridColumn3.Caption = "Gudang"
        GridColumn3.FieldName = "nama_gudang"

        GridColumn4.Caption = "User"
        GridColumn4.FieldName = "nama_user"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_penyesuaian_stok"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabelbarang = New DataTable

        With tabelbarang
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("status_stok")
        End With

        GridControl2.DataSource = tabelbarang

        GridColumn6.Caption = "Kode Stok"
        GridColumn6.FieldName = "kode_stok"
        GridColumn7.Caption = "Kode Barang"
        GridColumn7.FieldName = "kode_barang"
        GridColumn8.Caption = "Qty"
        GridColumn8.FieldName = "qty"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,#0"
        GridColumn9.Caption = "Status Stok"
        GridColumn9.FieldName = "status_stok"

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_penyesuaian_stok JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id JOIN tb_user ON tb_user.id = tb_penyesuaian_stok.user_id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_penyesuaian_stok JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id JOIN tb_user ON tb_user.id = tb_penyesuaian_stok.user_id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If
        Else
            sql = "SELECT * FROM tb_penyesuaian_stok JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id JOIN tb_user ON tb_user.id = tb_penyesuaian_stok.user_id"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaripenyesuaianstok = 1 Then
            fpenyesuaianstok.txtgopenyesuaianstok.Text = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("id")

        Call koneksi()
        sql = "SELECT * FROM tb_penyesuaian_stok_detail WHERE penyesuaian_stok_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelbarang.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("qty"), dr("status_stok"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call tabel_lunas()
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

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class