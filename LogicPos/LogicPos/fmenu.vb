Imports System.Data.Odbc
Public Class fmenu
    Private Sub exitStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitStripMenuItem.Click
        Me.Close()
        Me.Dispose()
        Application.Exit()
    End Sub
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statustgl.Text = Now.ToLongDateString
        Timer.Start()
        'frmlogin.show()
        'frmlogin.mdiparent=me
        'Call koneksii()
        'statususer.Text = flogin.
        Call tabel_pembelian()
        'Label2.Location = New System.Drawing.Point(Me.Size.Width - Label2.Size.Width - 20, 35)
        'GridControl1.Location = New System.Drawing.Point(Me.Size.Width - GridControl1.Size.Width - 20, 70)
        GridControl1.Visible = False
    End Sub
    Sub grid_pembelian()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_pembelian"

        GridColumn2.Caption = "Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "Tanggal Pembelian"
        GridColumn3.FieldName = "tgl_pembelian"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total Pembelian"
        GridColumn5.FieldName = "total_pembelian"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "Satuan"
        GridColumn6.FieldName = "satuan_barang"
        GridColumn6.Visible = False

        'GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Harga Jual"
        GridColumn7.FieldName = "harga_jual"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"
        GridColumn7.Visible = False

        GridColumn8.Caption = "Subtotal"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,##0"
        GridColumn8.Visible = False

        GridColumn9.Caption = "Laba"
        GridColumn9.FieldName = "keuntungan"
        GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn9.DisplayFormat.FormatString = "##,##0"
        GridColumn9.Visible = False

        'GridColumn10.Caption = "Idbrg"
        'GridColumn10.FieldName = "idbarang"
        GridColumn10.Visible = False
        GridColumn11.Caption = "Kasir"
        GridColumn11.FieldName = "kode_user"
        GridColumn11.Visible = False

        GridColumn12.Caption = "Metode Bayar"
        GridColumn12.FieldName = "metode_pembayaran"
        GridColumn12.Visible = False

        '        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        '       GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridControl1.Visible = True

    End Sub
    Sub tabel_pembelian()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_pembelian JOIN tb_supplier ON tb_supplier.kode_supplier = tb_pembelian.kode_supplier WHERE tb_pembelian.lunas_pembelian = 0 AND tb_pembelian.tgl_jatuhtempo_pembelian = now() "
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid_pembelian()
            cnn.Close()
        End Using
    End Sub
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        statusjam.Text = TimeOfDay
    End Sub
    Private Sub DataBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBarangToolStripMenuItem.Click
        fbarang.Show()
    End Sub
    Private Sub DataCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataCustomerToolStripMenuItem.Click
        fcustomer.Show()
    End Sub
    Private Sub DataSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataSupplierToolStripMenuItem.Click
        fsupplier.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        fpembelian.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem.Click
        fpenjualan.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPembelianToolStripMenuItem.Click
        flaporanpembelian.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPenjualanToolStripMenuItem.Click
        flaporanpenjualan.Show()
    End Sub
    Private Sub InfoStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LStokBarangToolStripMenuItem.Click
        flaporstokbarang.Show()
    End Sub
    Private Sub ManageUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataUserToolStripMenuItem.Click
        fuser.Show()
    End Sub
    Private Sub fmenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
    Private Sub PrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrinterToolStripMenuItem.Click
        fprinter.Show()
    End Sub
    Private Sub PricelistGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PricelistGroupToolStripMenuItem.Click
        fpricelist.Show()
    End Sub
    Private Sub MenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuUtama.ItemClicked

    End Sub
    Private Sub DataGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataGudangToolStripMenuItem.Click
        fgudang.Show()
    End Sub

    Private Sub DataKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataKasToolStripMenuItem.Click
        fkas.Show()
    End Sub

    Private Sub BarangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangKeluarToolStripMenuItem.Click
        fbarangkeluar.Show()
    End Sub

    Private Sub BarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangMasukToolStripMenuItem.Click
        fbarangmasuk.Show()
    End Sub

    Private Sub ReturPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPenjualanToolStripMenuItem.Click
        freturjual.Show()
    End Sub

    Private Sub ReturPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPembelianToolStripMenuItem.Click
        freturbeli.Show()
    End Sub

    Private Sub TransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferBarangToolStripMenuItem.Click
        ftransferbarang.Show()
    End Sub

    Private Sub PelunasanPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanPiutangToolStripMenuItem.Click
        flunaspiutang.Show()
    End Sub

    Private Sub PelunasanUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanUtangToolStripMenuItem.Click
        flunasutang.Show()
    End Sub

    Private Sub KasMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasMasukToolStripMenuItem.Click
        fkasmasuk.Show()
    End Sub

    Private Sub KasKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasKeluarToolStripMenuItem.Click
        fkaskeluar.Show()
    End Sub

    Private Sub TransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferKasToolStripMenuItem.Click
        ftransferkas.Show()
    End Sub

    Private Sub LUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LUtangToolStripMenuItem.Click
        flaporanutang.Show()
    End Sub

    Private Sub fmenu_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        'Label2.Location = New System.Drawing.Point(Me.Size.Width - Label2.Size.Width - 20, 35)
        'GridControl1.Location = New System.Drawing.Point(Me.Size.Width - GridControl1.Size.Width - 20, 70)
    End Sub

    Private Sub LPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPiutangToolStripMenuItem.Click
        flaporanpiutang.Show()
    End Sub

    Private Sub LaporanTransaksiKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTransaksiKasToolStripMenuItem.Click
        flaportransaksikas.Show()
    End Sub

    Private Sub LAkunMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunMasukToolStripMenuItem.Click
        flaporankasmasuk.Show()
    End Sub

    Private Sub LAkunKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunKeluarToolStripMenuItem.Click
        flaporankaskeluar.Show()
    End Sub

    Private Sub LTransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferKasToolStripMenuItem.Click
        flaporankas.Show()
    End Sub

    Private Sub LTransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferBarangToolStripMenuItem.Click

    End Sub
End Class
