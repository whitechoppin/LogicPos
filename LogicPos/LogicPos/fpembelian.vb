Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fpembelian
    Dim tabel As DataTable
    Dim harga, modal As Double
    Dim total2 As Double
    Dim supplier As String
    Dim satuan As String = ""
    Dim kategori As String = ""

    Public isi As String
    Private Sub fpembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call awal()
        Call autonumber()
        Call comboboxsupplier()

        With GridView1
            '.OptionsView.ColumnAutoWidth = False ' agar muncul scrol bar
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("Subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Subtotal", "{0:n0}")
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print
        End With
    End Sub
    Sub tabindex()
        txtkodeitem.TabIndex = 1
        txtbanyak.TabIndex = 2
        txtharga.TabIndex = 3
        txtimei.TabIndex = 4
        btntambah.TabIndex = 5
    End Sub

    Sub awal()
        Call tabindex()
        RichTextBox1.Enabled = False
        txtkodeitem.Focus()
        txtbanyak.Clear()
        txtharga.Clear()
        txtharga.Text = 0
        txtkodeitem.Clear()
        txtnonota.Enabled = False
        txtnama.Clear()
        txtnama.Enabled = False
        txtimei.Clear()
        txtimei.Enabled = False

        txtnonota.Text = autonumber()

        DateTimePicker1.Value = Date.Now
        DateTimePicker1.MinDate = Date.Now

        cmbsupplier.SelectedIndex = -1

        tabel = New DataTable
        With tabel
            .Columns.Add("Nama")
            .Columns.Add("Banyak", GetType(Double))
            .Columns.Add("Satuan")
            .Columns.Add("kategori")
            .Columns.Add("imei")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("Subtotal", GetType(Double))
            .Columns.Add("kode")

        End With
        GridControl1.DataSource = tabel
        GridColumn1.FieldName = "Nama"
        GridColumn1.Caption = "Nama"
        GridColumn2.FieldName = "Banyak"
        GridColumn2.Caption = "Qty"
        GridColumn3.FieldName = "Satuan"
        GridColumn3.Caption = "Satuan"
        GridColumn4.FieldName = "kategori"
        GridColumn4.Caption = "Kategori"
        GridColumn5.Caption = "Imei"
        GridColumn5.FieldName = "imei"
        GridColumn6.FieldName = "harga"
        GridColumn6.Caption = "Harga Satuan"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.FieldName = "Subtotal"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"

        GridColumn8.FieldName = "kode"
        GridColumn8.Visible = False

    End Sub
    Sub cari()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode='" & txtkodeitem.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnama.Text = dr("nama")
            satuan = dr("satuan")
            kategori = dr("kategori")
            modal = dr("modal")
            txtharga.Text = Format(modal, "##,##0")
            If kategori = "HP" Then
                txtimei.Enabled = True
                txtbanyak.Enabled = False
            Else
                txtimei.Enabled = False
                txtbanyak.Enabled = True

            End If
        Else
            txtnama.Text = ""

        End If
    End Sub
    Sub tambah()
        Call koneksii()
        If txtnama.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Then
            Exit Sub
        End If

        For i As Integer = 0 To GridView1.RowCount - 1
            If txtkodeitem.Text = GridView1.GetRowCellValue(i, "kode") Then
                Dim byk As Integer
                byk = GridView1.GetRowCellValue(i, "Banyak")
                GridView1.DeleteRow(GridView1.GetRowHandle(i))
                tabel.Rows.Add(txtnama.Text, (Val(txtbanyak.Text) + byk), satuan, kategori, txtimei.Text, Val(harga), (Val(txtbanyak.Text) + byk) * Val(harga), txtkodeitem.Text)
                GridControl1.RefreshDataSource()

                txtkodeitem.Clear()
                txtnama.Clear()
                txtbanyak.Clear()
                txtharga.Clear()
                txtnama.Enabled = False
                txtkodeitem.Focus()
                Exit Sub
            End If
        Next

        tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text), satuan, kategori, txtimei.Text, Val(harga), Val(harga) * Val(txtbanyak.Text), txtkodeitem.Text)
        GridControl1.RefreshDataSource()
        txtkodeitem.Clear()
        txtnama.Clear()
        txtbanyak.Clear()
        txtharga.Text = 0
        txtimei.Clear()
        txtnama.Enabled = False
        txtkodeitem.Focus()
    End Sub
    Sub tambahHP()
        For i As Integer = 0 To GridView1.RowCount - 1
            If txtimei.Text = GridView1.GetRowCellValue(i, "imei") Then
                txtkodeitem.Clear()
                txtnama.Clear()
                txtbanyak.Clear()
                txtharga.Clear()
                txtnama.Enabled = False
                txtkodeitem.Focus()
                Exit Sub
            End If
        Next
        tabel.Rows.Add(txtnama.Text, "1", satuan, kategori, txtimei.Text, Val(harga), Val(harga), txtkodeitem.Text)
        GridControl1.RefreshDataSource()
        txtkodeitem.Clear()
        txtnama.Clear()
        txtbanyak.Clear()
        txtharga.Text = 0
        txtimei.Clear()
        txtnama.Enabled = False
        txtkodeitem.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If kategori = "HP" Then
            If txtimei.Text.Length = 0 Then
                MsgBox("Imei harus di isi!!!")
            Else
                Call tambahHP()
            End If
        Else
            Call tambah
        End If

    End Sub
    Private Sub txtkodeitem_TextChanged(sender As Object, e As EventArgs) Handles txtkodeitem.TextChanged, txtnonota.TextChanged
        isi = txtkodeitem.Text
        isicari = isi
        If Strings.Left(txtkodeitem.Text, 1) Like "[A-Z, a-z]" Then

            Call search()
            'fcaribarang.txtcari.Focus()
            'fcaribarang.txtcari.DeselectAll()
        Else
            Call cari()
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
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(idbeli,3) FROM tb_pembelian WHERE date_format (MID(`idbeli`, 2 , 6), ' %y ')+ MONTH(MID(`idbeli`,2 , 6)) + DAY(MID(`idbeli`,2, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(idbeli,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "N" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "N" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "N" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "N" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub simpan()
        total2 = GridView1.Columns("Subtotal").SummaryItem.SummaryValue 'ambil isi summary gridview
        Call koneksii()
        sql = "SELECT * FROM tb_supplier  WHERE '" & Strings.Right(cmbsupplier.Text, 5) & "' =  id"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = 0 Then
            MsgBox("Nama Supplier Tidak Di temukan!")

            Exit Sub
        Else
            If GridView1.RowCount = 0 Then
                MsgBox("Data masih kosong")
                Exit Sub
            Else

                For i As Integer = 0 To GridView1.RowCount - 1
                    sql = "INSERT INTO tb_pembelian_detail (idbeli,idbarang,namabarang,hrgbeli,qty,subtotal,imei) VALUES ('" & txtnonota.Text & "','" & GridView1.GetRowCellValue(i, "kode") & "', '" & GridView1.GetRowCellValue(i, "Nama") & "','" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "Banyak") & "', '" & GridView1.GetRowCellValue(i, "Subtotal") & "','" & txtimei.Text & "')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                Next

                sql = "INSERT INTO tb_pembelian (idbeli,idsupplier,tglbeli,tgljatuhtempo,total) VALUES ('" & txtnonota.Text & "','" & Strings.Right(cmbsupplier.Text, 5) & "', NOW() ,'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & total2 & "')"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "kategori") = "HP" Then
                        sql = "UPDATE tb_barang SET stok = stok + '" & GridView1.GetRowCellValue(i, "Banyak") & "' WHERE kode = '" & GridView1.GetRowCellValue(i, "kode") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        sql = "UPDATE tb_barang SET modal = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode = '" & GridView1.GetRowCellValue(i, "kode") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        sql = "insert into tb_imei( kodebrg,imei) values ('" & GridView1.GetRowCellValue(i, "kode") & "','" & GridView1.GetRowCellValue(i, "imei") & "')"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    Else
                        sql = "UPDATE tb_barang SET stok = stok + '" & GridView1.GetRowCellValue(i, "Banyak") & "' WHERE kode = '" & GridView1.GetRowCellValue(i, "kode") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        sql = "UPDATE tb_barang SET modal = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode = '" & GridView1.GetRowCellValue(i, "kode") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If

                Next

                MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
                'MsgBox(Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                Call awal()
            End If
        End If
    End Sub
    Private Sub btnproses_Click(sender As Object, e As EventArgs) Handles btnproses.Click
        Call simpan()
    End Sub
    Sub comboboxsupplier()
        Call koneksii()
        cmmd = New OdbcCommand("SELECT * FROM `tb_supplier` ", cnn)

        cmbsupplier.Items.Clear()
        cmbsupplier.AutoCompleteCustomSource.Clear()

        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsupplier.AutoCompleteCustomSource.Add(dr("nama") + " - " + dr("id"))
                cmbsupplier.Items.Add(dr("nama") + " - " + dr("id"))
            End While
        End If
    End Sub
    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "Banyak" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "Subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "Subtotal", 0)
            End Try
        End If
    End Sub
    Private Sub txtharga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtharga.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtkodeitem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodeitem.KeyDown
        If e.KeyCode = Keys.F2 Then
            Call awal()
        End If
    End Sub
    Private Sub txtharga_KeyDown(sender As Object, e As KeyEventArgs) Handles txtharga.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
        End If
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
    'Private Sub txtkodeitem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkodeitem.KeyPress
    '    If e.KeyChar Like "[A-Z,a-z]" Then
    '        fstok.Show()
    '        fstok.txtcari.Text = e.KeyChar
    '        fstok.txtcari.Focus()
    '        Me.txtkodeitem.Text = ""
    '    End If
    'End Sub
    Sub search()
        tutup = 2
        Dim panjang As Integer = txtkodeitem.Text.Length
        fcaribarang.Show()
        fcaribarang.txtcari.Focus()
        fcaribarang.txtcari.DeselectAll()
        fcaribarang.txtcari.SelectionStart = panjang
        Me.txtkodeitem.Clear()
    End Sub
    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click

        isi = txtkodeitem.Text
        Call search()

    End Sub
    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress, txtimei.KeyPress
        If Not (Char.IsDigit(e.KeyChar)) And e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
End Class