Imports System.Data.Odbc

Public Class fpreviewutang
    Dim kode As String
    Public tabellunas As DataTable

    Private Sub fpreviewutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call tabel_pembelian()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
        End With

        Call comboboxsupplier()
    End Sub

    Sub comboboxsupplier()
        Call koneksii()
        cmmd = New OdbcCommand("SELECT * FROM tb_supplier", cnn)
        cmbsupplier.Items.Clear()
        cmbsupplier.AutoCompleteCustomSource.Clear()
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsupplier.AutoCompleteCustomSource.Add(dr("kode_supplier"))
                cmbsupplier.Items.Add(dr("kode_supplier"))
            End While
        End If
    End Sub

    Sub grid_pembelian()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_pembelian"

        GridColumn2.Caption = "Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "Tanggal pembelian"
        GridColumn3.FieldName = "tgl_pembelian"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total pembelian"
        GridColumn5.FieldName = "total_pembelian"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("kode_lunas")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn6.Caption = "Kode"
        GridColumn6.FieldName = "kode_lunas"
        GridColumn6.Caption = "Tanggal"
        GridColumn6.FieldName = "tgl_pelunasan"
        GridColumn7.Caption = "Terima"
        GridColumn7.FieldName = "terima_utang"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel_pembelian()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_pembelian.keterangan_pembelian, tb_pembelian.no_nota_pembelian, tb_supplier.nama_supplier FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND tb_pembelian.kode_supplier='" & cmbsupplier.Text & "' AND tb_pembelian.lunas_pembelian =0 AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_pembelian.keterangan_pembelian, tb_pembelian.no_nota_pembelian, tb_supplier.nama_supplier FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND tb_pembelian.kode_supplier='" & cmbsupplier.Text & "' AND tb_pembelian.lunas_pembelian =0 AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        Call grid_pembelian()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE kode_pembelian ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("kode_lunas"), dr("last_updated"), dr("terima_utang"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_pembelian()
    End Sub

    Private Sub fpreviewutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub
End Class