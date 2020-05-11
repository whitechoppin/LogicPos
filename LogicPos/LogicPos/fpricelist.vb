Imports System.Data.Odbc

Public Class fpricelist
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Public isi As String
    Public isi2 As String
    Dim harga As Double = 0
    Dim orderan As Double = 0
    Dim modalbarang As Double = 0

    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim RatioHeight As Double = (Me.Height - CuRHeight) / CuRHeight
        Dim RatioWidth As Double = (Me.Width - CuRWidth) / CuRWidth

        For Each ctrl As Control In Controls
            ctrl.Width += ctrl.Width * RatioWidth
            ctrl.Left += ctrl.Left * RatioWidth
            ctrl.Top += ctrl.Top * RatioHeight
            ctrl.Height += ctrl.Height * RatioHeight
        Next

        CuRHeight = Me.Height
        CuRWidth = Me.Width
    End Sub

    '=======================

    Private Sub fpricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        fcaribarang.Visible = False
        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                hapusstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                hapusstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                hapusstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                hapusstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                hapusstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                hapusstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                hapusstatus = True
        End Select

        Call historysave("Membuka Master Pricelist", "N/A")
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
    Sub awaledit()
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
            sql = "SELECT tb_barang.kode_barang, tb_barang.nama_barang , tb_barang.jenis_barang ,tb_barang.satuan_barang , tb_price_group.harga_jual, tb_pelanggan.kode_pelanggan, tb_pelanggan.nama_pelanggan AS cust FROM tb_price_group JOIN tb_barang ON tb_barang.kode_barang=tb_price_group.kode_barang JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan=tb_price_group.kode_pelanggan WHERE tb_pelanggan.kode_pelanggan = '" & txtkodecus.Text & "' "
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
        sql = "SELECT tb_pelanggan.nama_pelanggan FROM tb_pelanggan WHERE tb_pelanggan.kode_pelanggan = '" & txtkodecus.Text & "' "
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

        sql = "SELECT * FROM tb_price_group JOIN tb_barang ON tb_barang.kode_barang = tb_price_group.kode_barang  WHERE tb_price_group.kode_barang='" & txtkode.Text & "' AND tb_price_group.kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            btnedit.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            txtnama.Text = dr("nama_barang")
            txtmodal.Text = dr("modal_barang")
            harga = dr("harga_jual")
            txtharga.Text = Format(harga, "##,##0")
            'txtharga.SelectionStart = Len(txtharga.Text)
        Else
            sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtnama.Text = dr("nama_barang")
                txtmodal.Text = dr("modal_barang")
            Else
                txtnama.Text = ""
                txtmodal.Text = ""
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
        sql = "SELECT * FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "' AND kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.Read Then
            MsgBox("Harga Sudah ada")
            Exit Sub
        Else
            Call koneksii()
            sql = "INSERT INTO tb_price_group (kode_barang, kode_pelanggan, harga_jual) VALUES ('" & txtkode.Text & "', '" & txtkodecus.Text & "', '" & harga & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MsgBox("Data Tersimpan")

            'history user ==========
            Call historysave("Menyimpan Data Pricelist Kode " + txtkode.Text + " Pada Kode Customer " + txtkodecus.Text, txtkode.Text)
            '========================

            Call clearbrg()
        End If

        Call caricust()
    End Sub

    Sub save_exist_item()
        Call koneksii()
        sql = "UPDATE tb_price_group SET harga_jual='" & harga & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_barang='" & txtkode.Text & "' AND kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        'history user ==========
        Call historysave("Mengedit Data Pricelist Kode " + txtkode.Text + " Pada Kode Customer " + txtkodecus.Text, txtkode.Text)
        '========================

        MsgBox("Data Terupdate", MsgBoxStyle.Information, "Success")
    End Sub
    Sub clearbrg()
        txtnama.Clear()
        txtharga.Clear()
        txtkode.Clear()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If txtkodecus.Text.Length = 0 Then
                MsgBox("Customer Belum Di pilih")
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Item Belum Di isi")
                Else
                    If txtharga.Text.Length = 0 Then
                        MsgBox("Harga jual Belum Di isi")
                    Else
                        If modalbarang >= harga Then
                            MsgBox("Harga jual dibawah modal")
                        Else
                            Call save_new_item()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
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
        sql = "SELECT * FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "' AND kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            btnedit.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False

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
                sql = "DELETE FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "' AND kode_pelanggan='" & txtkodecus.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                cnn.Close()
                MessageBox.Show(txtnama.Text + " Berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'history user ==========
                Call historysave("Menghapus Data Pricelist Kode " + txtkode.Text + " Pada Kode Customer " + txtkodecus.Text, txtkode.Text)
                '========================
                Me.Refresh()
                Call awal()
            End Using
        End If

    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        txtkode.Text = GridView1.GetFocusedRowCellValue("kode_barang")
        Call cari()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            sql = "SELECT * FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "'AND kode_pelanggan='" & txtkodecus.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call hapus()
            Else
                Exit Sub
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub edit()
        Call koneksii()
        sql = "SELECT * FROM tb_price_group WHERE kode_barang='" & txtkode.Text & "' AND kode_pelanggan='" & txtkodecus.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If txthidden.Visible = True Then
            passwordid = 5
            fpassword.Show()
            'txthidden.Visible = False
        ElseIf txthidden.Visible = False Then
            txthidden.Visible = True
        End If
    End Sub

    Private Sub fpricelist_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If txtkodecus.Text.Length = 0 Then
                MsgBox("Customer Belum Di pilih")
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Item Belum Di isi")
                Else
                    If txtharga.Text.Length = 0 Then
                        MsgBox("Harga jual Belum Di isi")
                    Else
                        If modalbarang >= harga Then
                            MsgBox("Harga jual dibawah modal")
                        Else
                            Call save_exist_item()
                            Call awaledit()
                            Call caricust()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub txtmodal_TextChanged(sender As Object, e As EventArgs) Handles txtmodal.TextChanged
        If txtmodal.Text = "" Then
            txtmodal.Text = 0
        Else
            modalbarang = txtmodal.Text
            txtmodal.Text = Format(modalbarang, "##,##0")
            txtmodal.SelectionStart = Len(txtmodal.Text)
        End If
    End Sub
End Class