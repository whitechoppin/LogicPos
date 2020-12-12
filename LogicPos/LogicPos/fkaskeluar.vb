Imports System.Data.Odbc
Imports System.IO
Imports ZXing

Public Class fkaskeluar
    Public namaform As String = "administrasi-kas_keluar"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Dim idkaskeluar, idkas, iduser As Integer
    Dim viewketerangan, viewkodekas, viewkodesales As String
    Dim saldokeluar As Double
    Dim viewtglkas As DateTime
    Dim statusprint, statusposted As Boolean
    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument

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

    Private Sub fkaskeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("saldo_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "saldo_kas", "{0:n0}")
        End With

        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                printstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                printstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                printstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                printstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                printstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                printstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                printstatus = True
        End Select

        Call historysave("Membuka Administrasi Kas Keluar", "N/A", namaform)
    End Sub

    Sub comboboxuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsales.DataSource = ds.Tables(0)
        cmbsales.ValueMember = "id"
        cmbsales.DisplayMember = "kode_user"
    End Sub

    Sub comboboxkas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbkas.DataSource = ds.Tables(0)
        cmbkas.ValueMember = "id"
        cmbkas.DisplayMember = "kode_kas"
    End Sub

    Sub carikas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idkas = Val(dr("id"))
            txtnamakas.Text = dr("nama_kas")
        Else
            idkas = 0
            txtnamakas.Text = ""
        End If
    End Sub

    Sub carisales()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = Val(dr("id"))
            cmbsales.ForeColor = Color.Black
        Else
            iduser = 0
            cmbsales.ForeColor = Color.Red
        End If
    End Sub

    Sub awal()
        'button 
        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnprint.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        Call comboboxuser()
        Call comboboxkas()

        'bersihkan form
        txtkodekeluar.Clear()
        cmbsales.SelectedIndex = -1
        cmbkas.SelectedIndex = -1

        txtsaldokeluar.Clear()
        txtketerangan.Clear()

        cmbsales.Enabled = False
        cmbkas.Enabled = False
        txtsaldokeluar.Enabled = False
        dttransaksi.Value = Date.Now
        dttransaksi.Enabled = False
        txtketerangan.Enabled = False

        cbposted.Checked = False
        cbprinted.Checked = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        Call isitabel()

    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_kas_keluar JOIN tb_kas ON tb_kas.id = tb_kas_keluar.kas_id"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "id"
        GridColumn1.FieldName = "id"
        GridColumn1.Width = 10

        GridColumn2.Caption = "Kas"
        GridColumn2.FieldName = "kode_kas"
        GridColumn2.Width = 20

        GridColumn3.Caption = "Tanggal"
        GridColumn3.FieldName = "tanggal"
        GridColumn3.Width = 10

        GridColumn4.Caption = "Saldo Kas"
        GridColumn4.FieldName = "saldo_kas"
        GridColumn4.Width = 50
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
    End Sub

    Sub index()
        btntambah.TabIndex = 1
        btnedit.TabIndex = 2
        btnhapus.TabIndex = 3
        btnbatal.TabIndex = 4
        btnprint.TabIndex = 5
        cmbsales.TabIndex = 6
        cmbkas.TabIndex = 7
        dttransaksi.TabIndex = 8
        txtsaldokeluar.TabIndex = 9
        txtketerangan.TabIndex = 10
    End Sub

    Sub enable_text()
        cmbsales.Enabled = True
        cmbkas.Enabled = True
        dttransaksi.Enabled = True
        txtsaldokeluar.Enabled = True
        txtketerangan.Enabled = True
        cmbsales.Focus()
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()
                GridControl1.Enabled = False
            Else
                If cmbsales.SelectedIndex = -1 Then
                    MsgBox("Sales belum terisi !")
                Else
                    If cmbkas.SelectedIndex = -1 Then
                        MsgBox("Kas belum terisi !")
                    Else
                        If txtsaldokeluar.Text.Length = 0 Then
                            MsgBox("Saldo belum terisi !")
                        Else
                            Call simpan()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub simpan()
        Call koneksii()

        sql = "INSERT INTO tb_kas_keluar(kas_id, user_id, jenis_kas, tanggal, keterangan_kas, saldo_kas, print_kas, posted_kas, created_by, updated_by,date_created, last_updated) VALUES ('" & idkas & "','" & cmbsales.SelectedValue & "','Keluar', '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtketerangan.Text & "','" & saldokeluar & "','" & 0 & "','" & 1 & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
        cmmd = New OdbcCommand(sql, cnn)
        idkaskeluar = CInt(cmmd.ExecuteScalar())

        If idkas > 0 Then
            Call koneksii()
            sql = "INSERT INTO tb_transaksi_kas(kode_kas, kode_kas_keluar, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & idkas & "','" & idkaskeluar & "', 'KELUAR','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Kas Keluar Nomor " & idkaskeluar & "','" & saldokeluar & "', '" & 0 & "', '" & fmenu.kodeuser.Text & "', '" & fmenu.kodeuser.Text & "', now(), now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        'history user ==========
        Call historysave("Menyimpan Data Kas Keluar Kode " & idkaskeluar, idkaskeluar, namaform)
        '========================
        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        btntambah.Text = "Tambah"
        Me.Refresh()
        Call awal()
    End Sub

    Sub edit()
        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            myCommand.CommandText = "UPDATE tb_kas_keluar SET user_id='" & iduser & "',kas_id='" & idkas & "', tanggal='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', saldo_kas='" & saldokeluar & "', keterangan_kas='" & txtketerangan.Text & "',updated_by='" & fmenu.kodeuser.Text & "',last_updated=now() WHERE id='" & idkaskeluar & "'"
            myCommand.ExecuteNonQuery()

            idkas = cmbkas.SelectedValue

            If idkas > 0 Then
                myCommand.CommandText = "UPDATE tb_transaksi_kas SET kode_kas='" & idkas & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', debet_kas='" & saldokeluar & "', kredit_kas='" & 0 & "', updated_by='" & fmenu.kodeuser.Text & "', last_updated=now() WHERE id='" & idkaskeluar & "'"
                myCommand.ExecuteNonQuery()
            End If

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Mengedit Data Kas Keluar Kode " & idkaskeluar, idkaskeluar, namaform)
            '========================

            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"
            Me.Refresh()
            Call awal()
        Catch e As Exception
            Try
                myTrans.Rollback()
            Catch ex As OdbcException
                If Not myTrans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " + ex.GetType().ToString() + " was encountered while attempting to roll back the transaction.")
                End If
            End Try

            Console.WriteLine("An exception of type " + e.GetType().ToString() + "was encountered while inserting the data.")
            Console.WriteLine("Neither record was written to database.")
            MsgBox("Data Gagal Update", MsgBoxStyle.Information, "Gagal")
        End Try
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Simpan"
                btnhapus.Enabled = False
                Call enable_text()
                Call index()
                GridControl1.Enabled = False
            Else
                If cmbsales.SelectedIndex = -1 Then
                    MsgBox("Sales belum terisi !")
                Else
                    If cmbkas.SelectedIndex = -1 Then
                        MsgBox("Kas belum terisi !")
                    Else
                        If txtsaldokeluar.Text.Length = 0 Then
                            MsgBox("Saldo belum terisi !")
                        Else
                            Call edit()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If editstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus Data Kas Keluar " & Me.txtkodekeluar.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_kas_keluar WHERE id='" & txtkodekeluar.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                sql = "DELETE FROM tb_transaksi_kas WHERE kode_kas_keluar='" & txtkodekeluar.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                'history user ==========
                Call historysave("Menghapus Data Kas Keluar Kode " + txtkodekeluar.Text, txtkodekeluar.Text, namaform)
                '========================

                MessageBox.Show(txtkodekeluar.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Refresh()
                Call awal()
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
        btnprint.Enabled = False
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            If cekcetakan(txtkodekeluar.Text, namaform).Equals(True) Then
                statusizincetak = False
                passwordid = 16
                fpassword.kodetabel = txtkodekeluar.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()

                    sql = "UPDATE tb_kas_keluar SET print_kas = 1 WHERE id='" & txtkodekeluar.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Kas Keluar Kode " + txtkodekeluar.Text, txtkodekeluar.Text, namaform)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()

                sql = "UPDATE tb_kas_keluar SET print_kas = 1 WHERE kode_kas_keluar = '" & txtkodekeluar.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Kas Keluar Kode " + txtkodekeluar.Text, txtkodekeluar.Text, namaform)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub cetak_faktur()
        'barcode
        Dim tabel_barcode As New DataTable
        Dim baris_barcode As DataRow

        Dim writer As New BarcodeWriter
        Dim barcode As Image
        Dim ms As MemoryStream = New MemoryStream

        With tabel_barcode
            .Columns.Add("kode_barcode")
            .Columns.Add("gambar_barcode", GetType(Byte()))
        End With

        baris_barcode = tabel_barcode.NewRow
        baris_barcode("kode_barcode") = txtkodekeluar.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtkodekeluar.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        rpt_faktur = New fakturkaskeluar
        rpt_faktur.Database.Tables(1).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtkodekeluar.Text)
        rpt_faktur.SetParameterValue("kodekas", txtnamakas.Text)
        rpt_faktur.SetParameterValue("saldo", saldokeluar)
        rpt_faktur.SetParameterValue("tanggal", Format(dttransaksi.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.kodeuser.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Private Sub fkaskeluar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call carisales()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call carisales()
    End Sub

    Public Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
        Dim faktur As String
        Call koneksii()
        sql = "SELECT * FROM tb_printer WHERE nomor='2'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            faktur = dr("nama_printer")

        Else
            faktur = ""
        End If

        Try
            Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
            Dim PkSize As New System.Drawing.Printing.PaperSize
            ObjPrinterSetting.PrinterName = faktur
            For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                If ObjPrinterSetting.PaperSizes.Item(i).PaperName = mPaperSize.Trim Then
                    PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                    Exit For
                End If
            Next

            If PkSize IsNot Nothing Then
                Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = rpt_faktur.PrintOptions
                myAppPrintOptions.PrinterName = faktur
                myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                rpt_faktur.PrintOptions.PaperOrientation = IIf(PaperOrientation = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)
            End If
            PkSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub cari()
        idkaskeluar = GridView1.GetFocusedRowCellValue("id")
        txtkodekeluar.Text = idkaskeluar

        Call koneksii()
        sql = "SELECT * FROM tb_kas_keluar WHERE id='" & idkaskeluar & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            viewkodesales = dr("user_id")
            viewkodekas = dr("kas_id")
            viewtglkas = dr("tanggal")
            saldokeluar = dr("saldo_kas")
            viewketerangan = dr("keterangan_kas")
            statusprint = dr("print_kas")
            statusposted = dr("posted_kas")

            cmbsales.SelectedValue = viewkodesales
            cmbkas.SelectedValue = viewkodekas
            dttransaksi.Value = viewtglkas
            txtsaldokeluar.Text = Format(saldokeluar, "##,##0")
            txtketerangan.Text = viewketerangan
            cbprinted.Checked = statusprint
            cbposted.Checked = statusposted

            btnedit.Enabled = True
            btnbatal.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            btntambah.Text = "Tambah"
        End If
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Call cari()
        btnprint.Enabled = True
    End Sub

    Private Sub cmbkas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkas.SelectedIndexChanged
        Call carikas()
    End Sub

    Private Sub cmbkas_TextChanged(sender As Object, e As EventArgs) Handles cmbkas.TextChanged
        Call carikas()
    End Sub

    Private Sub txtsaldokeluar_TextChanged(sender As Object, e As EventArgs) Handles txtsaldokeluar.TextChanged
        If txtsaldokeluar.Text = "" Then
            txtsaldokeluar.Text = 0
        Else
            saldokeluar = txtsaldokeluar.Text
            txtsaldokeluar.Text = Format(saldokeluar, "##,##0")
            txtsaldokeluar.SelectionStart = Len(txtsaldokeluar.Text)
        End If
    End Sub

    Private Sub txtsaldokeluar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsaldokeluar.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class