Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fpembelian
    Dim tabel As DataTable
    Dim harga, modal, ongkir As Double
    Dim total2 As Double
    Dim satuan, jenis, supplier As String
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
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print
        End With

        sql = "Delete from tb_pembelian_sementara" 'clear data
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
    End Sub
    Sub tabindex()
        txtkodeitem.TabIndex = 1
        txtbanyak.TabIndex = 2
        txtharga.TabIndex = 3
        btntambah.TabIndex = 4
    End Sub
    Sub tabel_utama()
        tabel = New DataTable
        With tabel
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))

        End With
        GridControl1.DataSource = tabel
        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Caption = "Kode Stok"
        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.Visible = False
        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn4.Caption = "Banyak"
        GridColumn4.FieldName = "banyak"
        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn7.FieldName = "harga"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.Caption = "Subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
    End Sub
    Sub isi_pembayaran()
        Call koneksii()
        cmbbayar.Items.Clear()
        cmbbayar.AutoCompleteCustomSource.Clear()
        cmbbayar.AutoCompleteCustomSource.Add("TUNAI")
        cmbbayar.Items.Add("TUNAI")
        sql = "SELECT * FROM tb_rekening_supplier WHERE kode_supplier='" & Strings.Right(cmbsupplier.Text, 5) & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbbayar.AutoCompleteCustomSource.Add(dr("nama_bank") + " - " + dr("nomor_rekening") + " - " + dr("nama_rekening"))
                cmbbayar.Items.Add(dr("nama_bank") + " - " + dr("nomor_rekening") + " - " + dr("nama_rekening"))
            End While
        End If
    End Sub
    Sub awal()
        Call tabindex()
        RichTextBox1.Enabled = False
        txtketerangan.Enabled = True
        txtketerangan.Clear()
        txtkodeitem.Focus()
        txtbanyak.Clear()
        txtharga.Clear()
        txtharga.Text = 0
        txtkodeitem.Clear()
        txtnonota.Enabled = False
        txtnama.Clear()
        txtnama.Enabled = False
        txtnonota.Text = autonumber()
        txtongkir.Enabled = False
        cbongkir.Checked = False
        DateTimePicker1.Value = Date.Now
        DateTimePicker1.MinDate = Date.Now
        Call tabel_utama()
        cmbsupplier.SelectedIndex = -1
        lblsatuan.Text = ""
    End Sub
    Sub cari()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodeitem.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnama.Text = dr("nama_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            jenis = dr("jenis_barang")
            modal = dr("modal_barang")
            txtharga.Text = Format(modal, "##,##0")
        Else
            txtnama.Text = ""
            lblsatuan.Text = ""
            txtharga.Text = 0
        End If
    End Sub
    Sub tambah()
        Dim kode_barang, nama_barang, satuan_barang, jenis_barang, kode_stok As String
        Dim counter_angka As String
        Dim total_karakter As Integer
        Dim tambah_counter As Integer
        Dim qty, harga_satuan, subtotal, nomor As Double
        Call koneksii()
        If txtnama.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Then
            Exit Sub
        End If

        If GridView1.RowCount = 0 Then
            If lblsatuan.Text = "Pcs" Then
                tabel.Rows.Add(txtkodeitem.Text, txtkodeitem.Text, txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                GridControl1.RefreshDataSource()
                lblsatuan.Text = ""
                txtkodeitem.Clear()
                txtnama.Clear()
                txtbanyak.Clear()
                txtharga.Text = 0
                txtnama.Enabled = False
                txtkodeitem.Focus()
            Else
                'MsgBox(txtkodeitem.Text + "1")
                sql = "SELECT * FROM tb_stok where kode_barang= '" & txtkodeitem.Text & "' order by date_created desc limit 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                If dr.HasRows Then
                    kode_stok = dr("kode_stok")
                    total_karakter = Len(kode_stok)
                    counter_angka = Microsoft.VisualBasic.Right(kode_stok, total_karakter - 8)
                    tambah_counter = counter_angka + 1
                    tabel.Rows.Add(txtkodeitem.Text + CStr(tambah_counter), txtkodeitem.Text, txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                    GridControl1.RefreshDataSource()
                    Call koneksii()
                    sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodeitem.Text + CStr(tambah_counter) & "', '" & txtkodeitem.Text & "', '" & txtnama.Text & "','" & Val(txtbanyak.Text) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(txtbanyak.Text) * Val(harga) & "' ,'1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    lblsatuan.Text = ""
                    txtkodeitem.Clear()
                    txtnama.Clear()
                    txtbanyak.Clear()
                    txtharga.Text = 0
                    txtnama.Enabled = False
                    txtkodeitem.Focus()
                Else

                    tabel.Rows.Add(txtkodeitem.Text + "1", txtkodeitem.Text, txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                    GridControl1.RefreshDataSource()
                    Call koneksii()
                    sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodeitem.Text + "1" & "', '" & txtkodeitem.Text & "', '" & txtnama.Text & "','" & Val(txtbanyak.Text) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(txtbanyak.Text) * Val(harga) & "' ,'1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    lblsatuan.Text = ""
                    txtkodeitem.Clear()
                    txtnama.Clear()
                    txtbanyak.Clear()
                    txtharga.Text = 0
                    txtnama.Enabled = False
                    txtkodeitem.Focus()
                End If


            End If
        Else 'data ada
            Dim lokasi As Integer = -1
            Dim qty1 As Integer
            If GridView1.RowCount <> 0 Then
                'MsgBox("data ada")
                If lblsatuan.Text = "Pcs" Then
                    'MsgBox("ini pcs")
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "kode_barang") = txtkodeitem.Text And GridView1.GetRowCellValue(i, "satuan_barang").Equals("Pcs") Then
                            MsgBox(GridView1.GetRowCellValue(i, "kode_barang"))
                            lokasi = i
                        End If
                    Next
                    If lokasi = -1 Then
                        tabel.Rows.Add(txtkodeitem.Text, txtkodeitem.Text, txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                        GridControl1.RefreshDataSource()
                        lblsatuan.Text = ""
                        txtkodeitem.Clear()
                        txtnama.Clear()
                        txtbanyak.Clear()
                        txtharga.Text = 0
                        txtnama.Enabled = False
                        txtkodeitem.Focus()
                    Else
                        qty1 = GridView1.GetRowCellValue(lokasi, "banyak")
                        GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                        tabel.Rows.Add(txtkodeitem.Text, txtkodeitem.Text, txtnama.Text, (Val(txtbanyak.Text) + qty1), satuan, jenis, Val(harga), (Val(txtbanyak.Text) + qty1) * Val(harga))
                        GridControl1.RefreshDataSource()

                        lblsatuan.Text = ""
                        txtkodeitem.Clear()
                        txtnama.Clear()
                        txtbanyak.Clear()
                        txtharga.Clear()
                        txtnama.Enabled = False
                        txtkodeitem.Focus()
                        Exit Sub
                    End If

                Else
                    'MsgBox("bukan Pcs")
                    Call koneksii()
                    sql = "SELECT * FROM tb_pembelian_sementara where kode_barang= '" & txtkodeitem.Text & "' order by nomor desc limit 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        total_karakter = Len(dr("kode_stok"))
                        counter_angka = Microsoft.VisualBasic.Right(dr("kode_stok"), total_karakter - 8)
                        tambah_counter = counter_angka + 1
                        tabel.Rows.Add(txtkodeitem.Text + CStr(tambah_counter), txtkodeitem.Text, txtnama.Text, (Val(txtbanyak.Text)), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                        GridControl1.RefreshDataSource()

                        sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodeitem.Text + CStr(tambah_counter) & "', '" & txtkodeitem.Text & " ', '" & txtnama.Text & "','" & Val(txtbanyak.Text) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(txtbanyak.Text) * Val(harga) & "', '" & tambah_counter & "')"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    Else
                        tabel.Rows.Add(txtkodeitem.Text + "1", txtkodeitem.Text, txtnama.Text, (Val(txtbanyak.Text)), satuan, jenis, Val(harga), Val(txtbanyak.Text) * Val(harga))
                        GridControl1.RefreshDataSource()

                        sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodeitem.Text + "1" & "', '" & txtkodeitem.Text & " ', '" & txtnama.Text & "','" & Val(txtbanyak.Text) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(txtbanyak.Text) * Val(harga) & "', '" & tambah_counter & "')"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If

                    'MsgBox(dr("kode_stok"))
                    'MsgBox("counter Angka= " + counter_angka)
                    'MsgBox(txtkodeitem.Text + CStr(tambah_counter))
                    'MsgBox("bisa di input")
                    lblsatuan.Text = ""
                    txtkodeitem.Clear()
                    txtnama.Clear()
                    txtbanyak.Clear()
                    txtharga.Clear()
                    txtnama.Enabled = False
                    txtkodeitem.Focus()
                    'sql = "SELECT * FROM tb_pembelian_sementara where kode_barang= '" & txtkodeitem.Text & "'"
                    'cmmd = New OdbcCommand(sql, cnn)
                    'dr = cmmd.ExecuteReader()
                    'If dr.HasRows Then
                    '    kode_barang = dr("kode_barang")
                    '    nama_barang = dr("nama_barang")
                    '    qty = dr("qty")
                    '    satuan_barang = dr("satuan_barang")
                    '    jenis_barang = dr("jenis_barang")
                    '    harga_satuan = dr("harga_satuan")
                    '    subtotal = dr("subtotal")
                    '    nomor = dr("nomor")
                    'End If
                End If
            End If
        End If
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
    End Sub
    Private Sub txtkodeitem_TextChanged(sender As Object, e As EventArgs) Handles txtkodeitem.TextChanged, txtnonota.TextChanged
        'isi = txtkodeitem.Text
        'isicari = isi
        'If Strings.Left(txtkodeitem.Text, 1) Like "[A-Z, a-z]" Then

        '    Call search()
        '    'fcaribarang.txtcari.Focus()
        '    'fcaribarang.txtcari.DeselectAll()
        'Else
        Call cari()
        'End If
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
        sql = "SELECT RIGHT(kode_pembelian,3) FROM tb_pembelian WHERE DATE_FORMAT(MID(`kode_pembelian`, 2 , 6), ' %y ')+ MONTH(MID(`kode_pembelian`,2 , 6)) + DAY(MID(`kode_pembelian`,2, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pembelian,3) DESC"
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
        total2 = GridView1.Columns("subtotal").SummaryItem.SummaryValue 'ambil isi summary gridview
        sql = "SELECT * FROM tb_supplier  WHERE '" & Strings.Right(cmbsupplier.Text, 5) & "' =  kode_supplier"
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
                    sql = "INSERT INTO tb_pembelian_detail (kode_pembelian, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,harga_beli, subtotal) VALUES ('" & autonumber() & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "', '" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "')"
                    cmmd = New OdbcCommand(sql, cnn)
                    cnn.Open()
                    dr = cmmd.ExecuteReader()
                Next
                sql = "INSERT INTO tb_pembelian (kode_pembelian,kode_supplier,keterangan_pembelian,harga_ongkir,tgl_pembelian,tgl_jatuhtempo_pembelian,total_pembelian,pembayaran_pembelian) VALUES ('" & txtnonota.Text & "','" & Strings.Right(cmbsupplier.Text, 5) & "', '" & txtketerangan.Text & "', '" & ongkir & "', NOW() ,'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & total2 & "', '" & cmbbayar.Text & "')"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                        sql = "select * from tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                        If dr.HasRows Then
                            sql = "UPDATE tb_stok SET jumlah_stok = stok + '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "'"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                        Else
                            sql = "insert into tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) values ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_stok") & "','OK', '" & GridView1.GetRowCellValue(i, "jumlah_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_gudang") & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                        End If

                        sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga_barang") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                    Else
                        sql = "insert into tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) values ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_stok") & "','1', '" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_gudang") & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        'sql = "UPDATE tb_barang SET stok = stok + '" & GridView1.GetRowCellValue(i, "Banyak") & "' WHERE kode = '" & GridView1.GetRowCellValue(i, "kode") & "'"
                        'cmmd = New OdbcCommand(sql, cnn)
                        'dr = cmmd.ExecuteReader()
                        sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
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
        cmmd = New OdbcCommand("SELECT * FROM tb_supplier", cnn)
        cmbsupplier.Items.Clear()
        cmbsupplier.AutoCompleteCustomSource.Clear()
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsupplier.AutoCompleteCustomSource.Add(dr("nama_supplier") + " - " + dr("kode_supplier"))
                cmbsupplier.Items.Add(dr("nama_supplier") + " - " + dr("kode_supplier"))
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
    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged
        Call isi_pembayaran()
    End Sub
    Private Sub txtongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtongkir_TextChanged(sender As Object, e As EventArgs) Handles txtongkir.TextChanged
        If txtongkir.Text = "" Then
            txtongkir.Text = 0
        Else
            ongkir = txtongkir.Text
            txtongkir.Text = Format(ongkir, "##,##0")
            txtongkir.SelectionStart = Len(txtongkir.Text)
        End If
    End Sub
    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        isi = txtkodeitem.Text
        Call search()
    End Sub
    Private Sub cmbongkir_CheckedChanged(sender As Object, e As EventArgs) Handles cbongkir.CheckedChanged
        If cbongkir.Checked = True Then
            txtongkir.Enabled = True
        Else
            txtongkir.Enabled = False
        End If
    End Sub
    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        If Not (Char.IsDigit(e.KeyChar)) And e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
End Class