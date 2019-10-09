Imports System.Data.Odbc

Public Class fpricelist
    Public isi As String
    Public isi2 As String
    Dim harga As Double = 0
    Dim orderan As Double = 0
    Private Sub fpricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        fcaribarang.Visible = False
    End Sub
    Sub awal()
        txtkodecus.Clear()
        txtnamacus.Clear()
        txtkode.Clear()
        txtnama.Clear()
        txtnama.Enabled = False
        txtharga.Text = 0
        txtkodecus.Text = "00000000"
        txtnamacus.Enabled = False

        txtkode.Focus()

        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
    End Sub
    Sub awal1()
        txtkode.Clear()
        txtnama.Clear()
        txtnama.Enabled = False
        txtharga.Text = 0
        txtnamacus.Enabled = False

        txtkode.Focus()

        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Width = "35"
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.FieldName = "nama_barang"
        GridColumn3.Caption = "Jenis"
        GridColumn3.FieldName = "jenis_barang"
        GridColumn3.Width = "60"
        GridColumn4.Caption = "Satuan"
        GridColumn4.FieldName = "satuan_barang"
        GridColumn5.Caption = "Harga Jual"
        GridColumn5.FieldName = "harga_jual"
        GridColumn5.Width = "60"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "Rp ##,##0"
        GridControl1.Visible = True
    End Sub
    Sub caricust()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "Select tb_barang.kode_barang, tb_barang.nama_barang , tb_barang.jenis_barang ,tb_barang.satuan_barang , tb_price_group.harga_jual, tb_pelanggan.kode_pelanggan, tb_pelanggan.nama_pelanggan as cust from tb_price_group join tb_barang on tb_barang.kode_barang=tb_price_group.kode_barang join tb_pelanggan on tb_pelanggan.kode_pelanggan=tb_price_group.kode_pelanggan where tb_pelanggan.kode_pelanggan = '" & txtkodecus.Text & "' "
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
        Call koneksii()
        sql = "Select tb_pelanggan.nama_pelanggan from tb_pelanggan where tb_pelanggan.kode_pelanggan = '" & txtkodecus.Text & "' "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamacus.Text = dr("nama_pelanggan")
        Else
            txtnamacus.Text = ""
        End If
    End Sub
    Private Sub txtkodecus_TextChanged(sender As Object, e As EventArgs) Handles txtkodecus.TextChanged
        Call caricust()
    End Sub
    Sub search()
        tutup = 1
        Dim panjang As Integer = txtkode.Text.Length
        fcaribarang.Show()
        fcaribarang.txtcari.Focus()
        fcaribarang.txtcari.DeselectAll()
        fcaribarang.txtcari.SelectionStart = panjang
        'Me.txtkode.Clear()
    End Sub
    Private Sub txtkode_TextChanged(sender As Object, e As EventArgs) Handles txtkode.TextChanged
        isi = txtkode.Text
        isicari = isi
        'If Strings.Left(txtkode.Text, 1) Like "[A-Z, a-z]" Then
        'Call search()
        'Else
        Call cari()
        'End If
        'Call cek_item()
    End Sub
    Sub cari()
        Call koneksii()
        Dim kataCari As String = txtkode.Text
        'sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & kataCari & "'"
        'cmmd = New OdbcCommand(sql, cnn)
        'dr = cmmd.ExecuteReader
        'If dr.HasRows Then
        '    txtnama.Text = dr("nama_barang")
        'Else
        '    txtnama.Text = ""
        'End If
        sql = "select * from tb_price_group join tb_barang on tb_barang.kode_barang = tb_price_group.kode_barang  where tb_price_group.kode_barang='" & txtkode.Text & "' and tb_price_group.kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            btnedit.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            txtnama.Text = dr("nama_barang")
            harga = dr("harga_jual")
            txtharga.Text = Format(harga, "##,##0")
            'txtharga.SelectionStart = Len(txtharga.Text)
        Else
            sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtnama.Text = dr("nama_barang")
            Else
                txtnama.Text = ""
            End If

            btnedit.Enabled = False
                btnhapus.Enabled = False
                btntambah.Enabled = True
                txtharga.Text = 0
            End If
    End Sub
    Private Sub txtharga_TextChanged(sender As Object, e As EventArgs) Handles txtharga.TextChanged
        If txtharga.Text = "" Then
            txtharga.Text = 0
        Else
            harga = txtharga.Text
            txtharga.Text = Format(harga, "##,##0")
            txtharga.SelectionStart = Len(txtharga.Text)
        End If
    End Sub
    Sub save_new_item()
        Call koneksii()
        sql = "select * from tb_price_group where kode_barang='" & txtkode.Text & "' and kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.Read Then
            MsgBox("Item Sudah ada")
            Exit Sub
        Else
            sql = "insert into tb_price_group (kode_barang, kode_pelanggan, harga_jual) values ('" & txtkode.Text & "', '" & txtkodecus.Text & "', '" & harga & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MsgBox("Data Tersimpan")
            Call clearbrg()
        End If
        Call caricust()
    End Sub
    Sub clearbrg()
        txtnama.Clear()
        txtharga.Clear()
        txtkode.Clear()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If txtkodecus.Text.Length = 0 Then
            MsgBox("Customer Belum Di pilih")
            Exit Sub
        Else
            If txtkode.Text.Length = 0 And txtharga.Text.Length = 0 Then
                MsgBox("Item Belum Di isi")
                Exit Sub
            Else
                Call save_new_item()
            End If
        End If
    End Sub
    Private Sub txtharga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtharga.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        tutup = 1
        fcaribarang.ShowDialog()
    End Sub
    Private Sub btncaricus_Click(sender As Object, e As EventArgs) Handles btncaricus.Click
        tutupcus = 1
        fcaricust.ShowDialog()
    End Sub
    Sub cek_item()
        Call koneksii()
        sql = "select * from tb_price_group where kode_barang='" & txtkode.Text & "' and kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            btnedit.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            'harga = dr("harga")
            'txtharga.Text = Format(harga, "##,##0")
            'txtharga.SelectionStart = Len(txtharga.Text)
        Else
            btnedit.Enabled = False
            btnhapus.Enabled = False
            btntambah.Enabled = True
            txtharga.Text = 0
        End If
    End Sub
    Sub hapus()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Using cnn As New OdbcConnection(strConn)
                sql = "DELETE FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "' and kode_pelanggan='" & txtkodecus.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                cnn.Close()
                MessageBox.Show(txtnama.Text + " Berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Refresh()
                Call awal()
            End Using
        End If

    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        txtkode.Text = GridView1.GetFocusedRowCellValue("kode_barang")
        Call cari()
        'txtnama.Text = GridView1.GetFocusedRowCellValue("nama_barang")
        'harga = GridView1.GetFocusedRowCellValue("harga")
        'txtharga.Text = Format(harga, "##,##0")
        'txtharga.SelectionStart = Len(txtharga.Text)
        'Call cek_item()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        sql = "Select * FROM tb_price_group WHERE  kode_barang='" & txtkode.Text & "' and kode_barang='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            Exit Sub
        Else
            Call hapus()
        End If
    End Sub
    Sub edit()
        Call koneksii()
        sql = "select * from tb_price_group where kode_barang='" & txtkode.Text & "' and kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Call koneksii()
        sql = "Update tb_price_group Set harga_jual='" & harga & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() where kode_barang='" & txtkode.Text & "' and kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        MsgBox("Data terupdate", MsgBoxStyle.Information, "Success")
        Call awal1()
        Call caricust()
    End Sub
End Class