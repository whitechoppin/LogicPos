Imports System.Data.Odbc

Public Class fcaritransferbarang
    Dim pilih As String
    Dim kode As String

    Public tabelbarang As DataTable
    Private Sub fcaritransferbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        GridColumn1.Caption = "id transfer"
        GridColumn1.FieldName = "id"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tanggal_transfer_barang"
        GridColumn3.Caption = "Dari Gudang"
        GridColumn3.FieldName = "dari_gudang"
        GridColumn4.Caption = "Ke Gudang"
        GridColumn4.FieldName = "ke_gudang"
        GridColumn5.Caption = "User"
        GridColumn5.FieldName = "kode_user"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabelbarang = New DataTable

        With tabelbarang
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("qty", GetType(Double))
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

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT tb.id, dari.nama_gudang AS dari_gudang, ke.nama_gudang AS ke_gudang, tb.tanggal_transfer_barang, usr.kode_user FROM tb_transfer_barang AS tb JOIN tb_gudang AS dari ON dari.id = tb.dari_gudang_id JOIN tb_gudang AS ke ON ke.id = tb.ke_gudang_id JOIN tb_user AS usr ON usr.id = tb.user_id WHERE DATE(tanggal_transfer_barang) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb.id, dari.nama_gudang AS dari_gudang, ke.nama_gudang AS ke_gudang, tb.tanggal_transfer_barang, usr.kode_user FROM tb_transfer_barang AS tb JOIN tb_gudang AS dari ON dari.id = tb.dari_gudang_id JOIN tb_gudang AS ke ON ke.id = tb.ke_gudang_id JOIN tb_user AS usr ON usr.id = tb.user_id WHERE DATE(tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If
        Else
            sql = "SELECT tb.id, dari.nama_gudang AS dari_gudang, ke.nama_gudang AS ke_gudang, tb.tanggal_transfer_barang, usr.kode_user FROM tb_transfer_barang AS tb JOIN tb_gudang AS dari ON dari.id = tb.dari_gudang_id JOIN tb_gudang AS ke ON ke.id = tb.ke_gudang_id JOIN tb_user AS usr ON usr.id = tb.user_id"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaritransferbarang = 1 Then
            ftransferbarang.txtgotransferbarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_transfer_barang")
        End If
        Me.Close()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("id")

        Call koneksi()
        sql = "SELECT * FROM tb_transfer_barang_detail WHERE transfer_barang_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelbarang.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("qty"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call tabel_lunas()
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