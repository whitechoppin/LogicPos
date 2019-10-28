Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class fcaribarang
    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double
    Private Sub fcaribarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.MdiParent = fmenu
        'Call koneksii()
        Call awal()
        Call tabel()
    End Sub
    Sub awal()
        cmbcari.SelectedIndex = 1
        txtcari.Focus()
        txtcari.Text = isicari
        'txtcari.Clear()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang"
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.FieldName = "nama_barang"
        GridColumn3.Caption = "Jenis"
        GridColumn3.FieldName = "jenis_barang"
        'GridColumn4.Width = "60"
        GridColumn4.Caption = "Satuan"
        GridColumn4.FieldName = "satuan_barang"
        'GridColumn6.Width = "40"
        'GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn6.DisplayFormat.FormatString = "##,##0"
        'GridColumn7.Width = "40"
        'GridColumn7.Caption = "Harga"
        'GridColumn7.FieldName = "harga"
        'GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn7.DisplayFormat.FormatString = "##,##0"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        If tutup > 0 Then
            '    Using cnn As New OdbcConnection(strConn)
            '        'sql = "Select tb_barang.kode, tb_barang.nama , tb_barang.satuan, tb_barang.stok from tb_barang "
            '        sql = "SELECT tb_barang.kode, tb_barang.nama, tb_barang.satuan, tb_barang.kategori, tb_barang.stok, tb_price.harga as harga from tb_barang join tb_price on tb_barang.kode = tb_price.idbrg where tb_price.idcustomer='" & Strings.Right(fpenjualan.cmbcustomer.Text, 8) & "'"

            '        da = New OdbcDataAdapter(sql, cnn)
            '        cnn.Open()
            '        ds = New DataSet
            '        da.Fill(ds)
            '        GridControl1.DataSource = Nothing
            '        GridControl1.DataSource = ds.Tables(0)
            '        Call grid()
            '        cnn.Close()
            '    End Using
            'Else
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang from tb_barang "
                da = New OdbcDataAdapter(sql, cnn)
                cnn.Open()
                ds = New DataSet
                da.Fill(ds)
                GridControl1.DataSource = Nothing
                GridControl1.DataSource = ds.Tables(0)
                Call grid()
                cnn.Close()
            End Using
        End If

    End Sub
    Sub cari()
        'Call koneksii()
        If cmbcari.SelectedIndex = 0 Then
            pilih = "tb_barang.kode_barang"
        Else
            If cmbcari.SelectedIndex = 1 Then
                pilih = "tb_barang.nama_barang"
            End If
        End If
        If tutup > 0 Then
            '    Using cnn As New OdbcConnection(strConn)
            '        'sql = "Select tb_barang.kode_barang, tb_barang.nama_barang , tb_barang.satuan_barang, tb_barang.jenis_barang from tb_barang  where " & pilih & " LIKE '%" & txtcari.Text & "%'  "
            '        sql = "SELECT tb_barang.kode, tb_barang.nama, tb_barang.satuan, tb_barang.kategori,tb_barang.stok, tb_price.harga as harga from tb_barang join tb_price on tb_barang.kode = tb_price.idbrg where " & pilih & " LIKE '%" & txtcari.Text & "%' and tb_price.idcustomer='" & Strings.Right(fpenjualan.cmbcustomer.Text, 8) & "'"
            '        da = New OdbcDataAdapter(sql, cnn)
            '        cnn.Open()
            '        ds = New DataSet
            '        da.Fill(ds)
            '        GridControl1.DataSource = Nothing
            '        GridControl1.DataSource = ds.Tables(0)
            '        Call grid()
            '        PictureBox1.Image = Nothing
            '        cnn.Close()
            '    End Using
            '    Call ambil_gbr()
            'Else
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang from tb_barang  where " & pilih & " LIKE '%" & txtcari.Text & "%'  "
                'sql = "SELECT tb_barang.kode, tb_barang.nama, tb_barang.satuan, tb_barang.kategori,tb_barang.modal, tb_price.harga from tb_barang join tb_price on tb_barang.kode = tb_price.idbrg where " & pilih & " LIKE '%" & txtcari.Text & "%' and tb_price.idcustomer='" & Strings.Right(fpenjualan.cmbcustomer.Text, 8) & "'"
                da = New OdbcDataAdapter(sql, cnn)
                cnn.Open()
                ds = New DataSet
                da.Fill(ds)
                GridControl1.DataSource = Nothing
                GridControl1.DataSource = ds.Tables(0)
                Call grid()
                PictureBox1.Image = Nothing
                cnn.Close()
            End Using
            Call ambil_gbr()
        End If
    End Sub
    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub
    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + kode + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                modalbarang = dr("modal_barang")
                LabelHarga.Text = Format(modalbarang, "##,##0")
                foto = dr("gambar_barang")
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
                cnn.Close()
            End If
        End Using
    End Sub

    Private Sub Gridview1_KeyUp(sender As Object, e As KeyEventArgs)
        'Dim a As Integer = GridView1.FocusedRowHandle
        'If a = 0 Then
        'txtcari.Focus()
        'Else
        Call ambil_gbr()
        'End If
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs)
        Call ambil_gbr()
    End Sub
    Private Sub btnmasuk_Click(sender As Object, e As EventArgs) Handles btnmasuk.Click
        'Me.Close()
        kodes = Me.GridView1.GetFocusedRowCellValue("kode_barang")

        If tutup = 1 Then
            fpricelist.txtkode.Text = kodes
            Me.Hide()
            '    fpenjualan.txtkodeitem.Text = kodes
            '    Me.Hide()
            '    fpenjualan.txtbanyak.Focus()


            'Else
            '    If tutup = 2 Then
            '        fpembelian.txtkodeitem.Text = kode
            '        Me.Hide()
            '        fpembelian.txtbanyak.Focus()
            '    Else
            '        If tutup = 3 Then
            '            flaporanpembelian.txtkode.Text = kode
            '            Me.Hide()
            '        Else
            '            If tutup = 4 Then
            '                flaporanpenjualan.txtkode.Text = kode
            '                Me.Hide()
            '            Else
            '                If tutup = 5 Then
            '                    fpricelist.txtkode.Text = kode
            '                    Me.Hide()
            '                End If
            '            End If
            '        End If
            '    End If
        End If
    End Sub
    Private Sub txtcari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcari.KeyDown
        If e.KeyCode = Keys.Down Then
            GridView1.Focus()
            Call ambil_gbr()
        End If

    End Sub
    Sub ExportToExcel()
        'https://www.devexpress.com/Support/Center/Question/Details/Q430217
        'https://documentation.devexpress.com/#WindowsForms/CustomDocument1874
        'storing current layout
        GridView1.SaveLayoutToXml("C:\data\tempLayout.xml")
        For Each column As GridColumn In GridView1.Columns
            'make all export columns visible
            column.Visible = True
        Next
        Dim a As String = InputBox("Nama File", "Input Nama file ")
        GridView1.ExportToXls("C:\data\" + a + ".xls")
        'restoring the layout, the layout file needs to be deleted manually
        GridView1.RestoreLayoutFromXml("C:\data\tempLayout.xml")
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutup = 1 Then
            fpricelist.txtkode.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
            Me.Hide()
        ElseIf tutup = 2 Then
            fpembelian.txtkodebarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
            Me.Hide()
        End If
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyUp
        Call ambil_gbr()
    End Sub
End Class