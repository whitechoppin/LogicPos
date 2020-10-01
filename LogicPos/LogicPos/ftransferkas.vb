Imports System.Data.Odbc
Imports System.IO
Imports ZXing

Public Class ftransferkas
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Dim kodetransfer, kodedarikas, kodekekas, viewketerangan, viewkodekekas, viewkodedarikas, viewkodesales As String
    Dim saldotransfer As Double
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

    Private Sub ftransferkas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()

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
    End Sub

    Sub comboboxuser()
        Call koneksii()
        cmbsales.Items.Clear()
        cmbsales.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_user", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsales.AutoCompleteCustomSource.Add(dr("kode_user"))
                cmbsales.Items.Add(dr("kode_user"))
            End While
        End If
    End Sub

    Sub comboboxdarikas()
        Call koneksii()
        cmbdarikas.Items.Clear()
        cmbdarikas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbdarikas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbdarikas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub comboboxkekas()
        Call koneksii()
        cmbkekas.Items.Clear()
        cmbkekas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbkekas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbkekas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub caridarikas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbdarikas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamadarikas.Text = dr("nama_kas")
        Else
            txtnamadarikas.Text = ""
        End If
    End Sub

    Sub carikekas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkekas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamakekas.Text = dr("nama_kas")
        Else
            txtnamakekas.Text = ""
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

        'bersihkan form
        txtkodetransfer.Clear()
        cmbsales.SelectedIndex = -1
        cmbdarikas.SelectedIndex = -1
        txtnamadarikas.Clear()
        cmbkekas.SelectedIndex = -1
        txtnamakekas.Clear()

        txtsaldotransfer.Clear()
        txtketerangantransfer.Clear()

        Call koneksii()

        cmbsales.Enabled = False
        cmbdarikas.Enabled = False
        cmbkekas.Enabled = False
        txtsaldotransfer.Enabled = False
        dttransaksi.Value = Date.Now
        dttransaksi.Enabled = False
        txtketerangantransfer.Enabled = False

        cbposted.Checked = False
        cbprinted.Checked = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        Call isitabel()

        Call comboboxuser()
        Call comboboxdarikas()
        Call comboboxkekas()
    End Sub

    Sub kolom()
        GridColumn1.Caption = "Kode Transfer Kas"
        GridColumn1.FieldName = "kode_transfer_kas"
        GridColumn1.Width = "40"

        GridColumn2.Caption = "Kode Dari Kas"
        GridColumn2.FieldName = "kode_dari_kas"
        GridColumn2.Width = "40"

        GridColumn3.Caption = "Kode Ke Kas"
        GridColumn3.FieldName = "kode_ke_kas"
        GridColumn3.Width = "40"

        GridColumn4.Caption = "Kode User"
        GridColumn4.FieldName = "kode_user"
        GridColumn4.Width = "40"

        GridColumn5.Caption = "Tanggal Transfer"
        GridColumn5.FieldName = "tanggal_transfer_kas"
        GridColumn5.Width = "60"

        GridColumn6.Caption = "Saldo Transfer"
        GridColumn6.FieldName = "saldo_transfer_kas"
        GridColumn6.Width = "60"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "Rp ##,#0"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "SELECT * FROM tb_transfer_kas"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub

    Sub index()
        btntambah.TabIndex = 1
        btnedit.TabIndex = 2
        btnhapus.TabIndex = 3
        btnbatal.TabIndex = 4
        btnprint.TabIndex = 5
        cmbsales.TabIndex = 6
        cmbdarikas.TabIndex = 7
        cmbkekas.TabIndex = 7
        dttransaksi.TabIndex = 8
        txtsaldotransfer.TabIndex = 9
        txtketerangantransfer.TabIndex = 10
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_transfer_kas,3) FROM tb_transfer_kas WHERE DATE_FORMAT(MID(`kode_transfer_kas`, 3 , 6), ' %y ')+ MONTH(MID(`kode_transfer_kas`,3 , 6)) + DAY(MID(`kode_transfer_kas`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_transfer_kas,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "TK" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "TK" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "TK" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "TK" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            'cnn.Close()
        End Try
        Return pesan
    End Function
    Sub enable_text()
        cmbsales.Enabled = True
        cmbdarikas.Enabled = True
        cmbkekas.Enabled = True
        dttransaksi.Enabled = True
        txtsaldotransfer.Enabled = True
        txtketerangantransfer.Enabled = True
        cmbsales.Focus()
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()
                txtkodetransfer.Text = autonumber()
                GridControl1.Enabled = False
            Else
                If txtkodetransfer.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If cmbsales.SelectedIndex = -1 Then
                        MsgBox("Sales belum terisi !")
                    Else
                        If cmbdarikas.SelectedIndex = -1 And cmbkekas.SelectedIndex = -1 Then
                            MsgBox("Salah Satu Kas belum terisi !")
                        Else
                            If txtsaldotransfer.Text.Length = 0 Then
                                MsgBox("Saldo belum terisi !")
                            Else
                                Call simpan()
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub simpan()
        kodetransfer = autonumber()

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
            myCommand.CommandText = "INSERT INTO tb_transfer_kas (kode_transfer_kas, kode_dari_kas, kode_ke_kas, kode_user, jenis_transfer_kas, tanggal_transfer_kas, saldo_transfer_kas, keterangan_transfer_kas, print_transfer_kas, posted_transfer_kas, created_by, updated_by,date_created, last_updated) VALUES ('" & kodetransfer & "','" & cmbdarikas.Text & "','" & cmbkekas.Text & "','" & cmbsales.Text & "','TRANSFER', '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & saldotransfer & "','" & txtketerangantransfer.Text & "','" & 0 & "','" & 1 & "','" & fmenu.namauser.Text & "','" & fmenu.namauser.Text & "',now(),now())"
            myCommand.ExecuteNonQuery()

            kodedarikas = cmbdarikas.Text

            If kodedarikas IsNot "" Then
                myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_transfer_kas, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodedarikas & "','" & kodetransfer & "', 'KELUAR','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Transfer Kas Nomor " & kodetransfer & "','" & saldotransfer & "', '" & 0 & "', '" & fmenu.namauser.Text & "', '" & fmenu.namauser.Text & "', now(), now())"
                myCommand.ExecuteNonQuery()
            End If

            kodekekas = cmbkekas.Text

            If kodekekas IsNot "" Then
                myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_transfer_kas, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodekekas & "','" & kodetransfer & "', 'MASUK','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Transfer Kas Nomor " & kodetransfer & "','" & 0 & "', '" & saldotransfer & "', '" & fmenu.namauser.Text & "', '" & fmenu.namauser.Text & "', now(), now())"
                myCommand.ExecuteNonQuery()
            End If

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
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
            MsgBox("Data Gagal tersimpan", MsgBoxStyle.Information, "Gagal")
        End Try

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
            myCommand.CommandText = "UPDATE tb_transfer_kas SET kode_user='" & cmbsales.Text & "', kode_dari_kas='" & cmbdarikas.Text & "', kode_ke_kas='" & cmbkekas.Text & "', tanggal_transfer_kas='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', saldo_transfer_kas='" & saldotransfer & "', keterangan_transfer_kas='" & txtketerangantransfer.Text & "',updated_by='" & fmenu.namauser.Text & "',last_updated=now()  WHERE  kode_transfer_kas='" & txtkodetransfer.Text & "'"
            myCommand.ExecuteNonQuery()

            kodedarikas = cmbdarikas.Text

            If kodedarikas IsNot "" Then
                myCommand.CommandText = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbdarikas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangantransfer.Text & "', debet_kas='" & saldotransfer & "', kredit_kas='" & 0 & "', updated_by='" & fmenu.namauser.Text & "', last_updated=now() WHERE kode_transfer_kas='" & txtkodetransfer.Text & "'"
                myCommand.ExecuteNonQuery()
            End If

            kodekekas = cmbkekas.Text

            If kodekekas IsNot "" Then
                myCommand.CommandText = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbkekas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangantransfer.Text & "', debet_kas='" & 0 & "', kredit_kas='" & saldotransfer & "', updated_by='" & fmenu.namauser.Text & "', last_updated=now() WHERE kode_transfer_kas='" & txtkodetransfer.Text & "'"
                myCommand.ExecuteNonQuery()
            End If

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

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
                If txtkodetransfer.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If cmbsales.SelectedIndex = -1 Then
                        MsgBox("Sales belum terisi !")
                    Else
                        If cmbdarikas.SelectedIndex = -1 And cmbkekas.SelectedIndex = -1 Then
                            MsgBox("Salah Satu Kas belum terisi !")
                        Else
                            If txtsaldotransfer.Text.Length = 0 Then
                                MsgBox("Saldo belum terisi !")
                            Else
                                Call edit()
                            End If
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
            If MessageBox.Show("Hapus Data Kas Masuk " & Me.txtkodetransfer.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_transfer_kas WHERE  kode_transfer_kas='" & txtkodetransfer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                sql = "DELETE FROM tb_transaksi_kas WHERE  kode_transfer_kas='" & txtkodetransfer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                MessageBox.Show(txtkodetransfer.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            If cekcetakan(txtkodetransfer.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 17
                fpassword.kodetabel = txtkodetransfer.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()

                    sql = "UPDATE tb_transfer_kas SET print_transfer_kas = 1 WHERE kode_transfer_kas = '" & txtkodetransfer.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Transfer Kas Kode " + txtkodetransfer.Text, txtkodetransfer.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()

                sql = "UPDATE tb_transfer_kas SET print_transfer_kas = 1 WHERE kode_transfer_kas = '" & txtkodetransfer.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Transfer Kas Kode " + txtkodetransfer.Text, txtkodetransfer.Text)
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
        baris_barcode("kode_barcode") = txtkodetransfer.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtkodetransfer.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        rpt_faktur = New fakturtransferkas
        rpt_faktur.Database.Tables(1).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtkodetransfer.Text)
        rpt_faktur.SetParameterValue("darikas", txtnamadarikas.Text)
        rpt_faktur.SetParameterValue("kekas", txtnamakekas.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dttransaksi.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("keterangan", txtketerangantransfer.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.namauser.Text)
        rpt_faktur.SetParameterValue("saldo", saldotransfer)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)

    End Sub

    Private Sub ftransferkas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
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
        txtkodetransfer.Text = GridView1.GetFocusedRowCellValue("kode_transfer_kas")
        Call koneksii()
        sql = "SELECT * FROM tb_transfer_kas WHERE kode_transfer_kas  = '" & txtkodetransfer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            viewkodesales = dr("kode_user")
            viewkodedarikas = dr("kode_dari_kas")
            viewkodekekas = dr("kode_ke_kas")
            viewtglkas = dr("tanggal_transfer_kas")
            saldotransfer = dr("saldo_transfer_kas")
            viewketerangan = dr("keterangan_transfer_kas")
            statusprint = dr("print_transfer_kas")
            statusposted = dr("posted_transfer_kas")

            cmbsales.Text = viewkodesales
            cmbdarikas.Text = viewkodedarikas
            cmbkekas.Text = viewkodekekas
            dttransaksi.Value = viewtglkas
            txtsaldotransfer.Text = Format(saldotransfer, "##,##0")
            txtketerangantransfer.Text = viewketerangan
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

    Private Sub cmbdarikas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbdarikas.SelectedIndexChanged
        Call caridarikas()
    End Sub

    Private Sub cmbdarikas_TextChanged(sender As Object, e As EventArgs) Handles cmbdarikas.TextChanged
        Call caridarikas()
    End Sub

    Private Sub cmbkekas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkekas.SelectedIndexChanged
        Call carikekas()
    End Sub

    Private Sub cmbkekas_TextChanged(sender As Object, e As EventArgs) Handles cmbkekas.TextChanged
        Call carikekas()
    End Sub

    Private Sub txtsaldotransfer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsaldotransfer.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtsaldotransfer_TextChanged(sender As Object, e As EventArgs) Handles txtsaldotransfer.TextChanged
        If txtsaldotransfer.Text = "" Then
            txtsaldotransfer.Text = 0
        Else
            saldotransfer = txtsaldotransfer.Text
            txtsaldotransfer.Text = Format(saldotransfer, "##,##0")
            txtsaldotransfer.SelectionStart = Len(txtsaldotransfer.Text)
        End If
    End Sub

End Class