Imports System.Data.Odbc

Public Class fpricelist
    Public namaform As String = "master-pricelist"

    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean

    Dim idbarang, idpelanggan, idprice As Integer
    Dim hargabarang As Double = 0
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

        Call historysave("Membuka Master Pricelist", "N/A", namaform)
    End Sub
    Sub awal()
        Call comboboxpelanggan()

        cmbpelanggan.SelectedIndex = 0
        txtkode.Clear()
        txtnama.Clear()

        txtharga.Text = 0
        txtmodal.Text = 0

        txtkode.Focus()

        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False
    End Sub

    Sub resetbarang()
        txtkode.Clear()
        txtnama.Clear()
        txtharga.Text = 0
        txtmodal.Text = 0
        idbarang = 0

        txtkode.Focus()

        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False
    End Sub

    Sub datatable()
        Call koneksii()
        sql = "SELECT tb_barang.id as barang_id, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.jenis_barang ,tb_barang.satuan_barang, tb_price_group.harga_jual, tb_price_group.id FROM tb_price_group JOIN tb_barang ON tb_barang.id=tb_price_group.barang_id JOIN tb_pelanggan ON tb_pelanggan.id=tb_price_group.pelanggan_id WHERE tb_pelanggan.id = '" & idpelanggan & "' "
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Width = 15

        GridColumn2.Caption = "Nama Barang"
        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Width = 35

        GridColumn3.Caption = "Jenis"
        GridColumn3.FieldName = "jenis_barang"
        GridColumn3.Width = 15

        GridColumn4.Caption = "Satuan"
        GridColumn4.FieldName = "satuan_barang"
        GridColumn4.Width = 15

        GridColumn5.Caption = "Harga Jual"
        GridColumn5.FieldName = "harga_jual"
        GridColumn5.Width = 25
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "Rp ##,##0"

        GridColumn6.Caption = "id"
        GridColumn6.FieldName = "id"
        GridColumn6.Width = 10
        GridColumn6.Visible = False

        GridColumn7.Caption = "Id Barang"
        GridColumn7.FieldName = "barang_id"
        GridColumn7.Width = 10
        GridColumn7.Visible = False

        GridControl1.Visible = True
    End Sub

    Sub comboboxpelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbpelanggan.DataSource = ds.Tables(0)
        cmbpelanggan.ValueMember = "id"
        cmbpelanggan.DisplayMember = "kode_pelanggan"
    End Sub

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbpelanggan.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idpelanggan = Val(dr("id"))
            txtnamapelanggan.Text = dr("nama_pelanggan")
        Else
            idpelanggan = 0
            txtnamapelanggan.Text = ""
        End If

        Call datatable()
    End Sub
    Private Sub txtkode_TextChanged(sender As Object, e As EventArgs) Handles txtkode.TextChanged
        Call cari()
    End Sub
    Sub cari()
        Call koneksii()
        If idbarang > 0 Then
            sql = "SELECT tb_price_group.id, tb_barang.id as barang_id, tb_barang.nama_barang, tb_barang.modal_barang, tb_price_group.harga_jual FROM tb_price_group JOIN tb_barang ON tb_barang.id = tb_price_group.barang_id WHERE tb_barang.id='" & idbarang & "' AND tb_price_group.pelanggan_id='" & idpelanggan & "' LIMIT 1"
        Else
            sql = "SELECT tb_price_group.id, tb_barang.id as barang_id, tb_barang.nama_barang, tb_barang.modal_barang, tb_price_group.harga_jual FROM tb_price_group JOIN tb_barang ON tb_barang.id = tb_price_group.barang_id WHERE tb_barang.kode_barang='" & txtkode.Text & "' AND tb_price_group.pelanggan_id='" & idpelanggan & "' LIMIT 1"
        End If

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idprice = dr("id")
            idbarang = dr("barang_id")
            txtnama.Text = dr("nama_barang")
            txtmodal.Text = dr("modal_barang")

            hargabarang = dr("harga_jual")
            txtharga.Text = Format(hargabarang, "##,##0")

            btntambah.Enabled = False
            btnedit.Enabled = True
            btnhapus.Enabled = True
            btnbatal.Enabled = True
        Else
            If idbarang > 0 Then
                sql = "SELECT * FROM tb_barang WHERE id = '" & idbarang & "'"
            Else
                sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & txtkode.Text & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                idbarang = dr("id")
                txtnama.Text = dr("nama_barang")
                txtmodal.Text = dr("modal_barang")
            Else
                txtnama.Text = ""
                txtmodal.Text = ""
            End If

            txtharga.Text = 0

            btntambah.Enabled = True
            btnedit.Enabled = False
            btnhapus.Enabled = False
            btnbatal.Enabled = False
        End If
    End Sub
    Private Sub txtharga_TextChanged(sender As Object, e As EventArgs) Handles txtharga.TextChanged
        If txtharga.Text = "" Then
            txtharga.Text = 0
        Else
            hargabarang = txtharga.Text
            txtharga.Text = Format(hargabarang, "##,##0")
            txtharga.SelectionStart = Len(txtharga.Text)
        End If
    End Sub
    Sub save_new_item()
        Call koneksii()
        sql = "SELECT * FROM tb_price_group WHERE barang_id='" & idbarang & "' AND pelanggan_id='" & idpelanggan & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.Read Then
            MsgBox("Harga Sudah ada")
            Exit Sub
        Else
            Try
                Call koneksii()
                sql = "INSERT INTO tb_price_group(barang_id, pelanggan_id, harga_jual) VALUES('" & idbarang & "', '" & idpelanggan & "', '" & hargabarang & "')"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                'history user ==========
                Call historysave("Menyimpan Data Pricelist Kode " + txtkode.Text + " Pada Kode Customer " + cmbpelanggan.Text, txtkode.Text, namaform)
                '========================

                Call resetbarang()
                Call datatable()

                MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Success")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Sub save_exist_item()
        Try
            Call koneksii()
            sql = "UPDATE tb_price_group SET harga_jual='" & hargabarang & "',updated_by='" & fmenu.kodeuser.Text & "', last_updated=now() WHERE id='" & idprice & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            'history user ==========
            Call historysave("Mengedit Data Pricelist Kode " + txtkode.Text + " Pada Kode Customer " + cmbpelanggan.Text, txtkode.Text, namaform)
            '========================

            Call resetbarang()
            Call datatable()

            MsgBox("Data Terupdate", MsgBoxStyle.Information, "Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If idpelanggan > 0 Then
                MsgBox("Pelanggan Belum Di isi")
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode Barang Belum Di isi")
                Else
                    If txtharga.Text.Length = 0 Then
                        MsgBox("Harga jual Belum Di isi")
                    Else
                        If modalbarang >= hargabarang Then
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
        tutupcaribarang = 1
        fcaribarang.ShowDialog()
    End Sub
    Private Sub btncaricus_Click(sender As Object, e As EventArgs) Handles btncaricus.Click
        tutupcus = 1
        fcaripelanggan.ShowDialog()
    End Sub

    Sub hapus()
        If MessageBox.Show("Hapus data price barang " & txtkode.Text & " untuk pelanggan " & txtnamapelanggan.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Call koneksii()
                sql = "DELETE FROM tb_price_group WHERE id='" & idprice & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                MessageBox.Show(txtnama.Text + " Berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'history user ==========
                Call historysave("Menghapus Data Pricelist Kode " & txtkode.Text & " Pada Kode Customer " & cmbpelanggan.Text, txtkode.Text, namaform)
                '========================
                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        idbarang = GridView1.GetFocusedRowCellValue("barang_id")
        idprice = GridView1.GetFocusedRowCellValue("id")

        txtkode.Text = GridView1.GetFocusedRowCellValue("kode_barang")
        Call cari()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            sql = "SELECT * FROM tb_price_group WHERE id='" & idprice & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call hapus()
            Else
                MsgBox("Data tidak ada !")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call resetbarang()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call datatable()
    End Sub

    Private Sub cmbpelanggan_TextChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbpelanggan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If txthidden.Visible = True Then
            passwordid = 5
            fpassword.ShowDialog()
        ElseIf txthidden.Visible = False Then
            txthidden.Visible = True
        End If
    End Sub

    Private Sub fpricelist_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If idpelanggan > 0 Then
                MsgBox("Pelanggan Belum Di isi")
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Item Belum Di isi")
                Else
                    If txtharga.Text.Length = 0 Then
                        MsgBox("Harga jual Belum Di isi")
                    Else
                        If modalbarang >= hargabarang Then
                            MsgBox("Harga jual dibawah modal")
                        Else
                            Call save_exist_item()
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