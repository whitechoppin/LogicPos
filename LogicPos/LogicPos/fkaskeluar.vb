Imports System.Data.Odbc

Public Class fkaskeluar
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Dim kodekeluar, kodekas, viewketerangan, viewkodekas, viewkodesales As String
    Dim saldokeluar As Double
    Dim viewtglkas As DateTime
    Dim statusprint, statusposted As Boolean
    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument

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

    Sub comboboxkas()
        Call koneksii()
        cmbkas.Items.Clear()
        cmbkas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbkas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbkas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub carikas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamakas.Text = dr("nama_kas")
        Else
            txtnamakas.Text = ""
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
        txtkodekeluar.Clear()
        cmbsales.SelectedIndex = -1
        cmbkas.SelectedIndex = -1
        txtnamakas.Clear()

        txtsaldokeluar.Clear()
        txtketerangan.Clear()

        Call koneksii()

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

        Call comboboxuser()
        Call comboboxkas()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode Kas Keluar"
        GridColumn1.FieldName = "kode_kas_keluar"
        GridColumn1.Width = "40"

        GridColumn2.Caption = "Kode Kas"
        GridColumn2.FieldName = "kode_kas"
        GridColumn2.Width = "40"

        GridColumn3.Caption = "Tanggal Transaksi"
        GridColumn3.FieldName = "tanggal_transaksi"
        GridColumn3.Width = "40"

        GridColumn4.Caption = "Saldo Kas"
        GridColumn4.FieldName = "saldo_kas"
        GridColumn4.Width = "60"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "SELECT * FROM tb_kas_keluar"
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
        cmbkas.TabIndex = 7
        dttransaksi.TabIndex = 8
        txtsaldokeluar.TabIndex = 9
        txtketerangan.TabIndex = 10
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_kas_keluar,3) FROM tb_kas_keluar WHERE DATE_FORMAT(MID(`kode_kas_keluar`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kas_keluar`,3 , 6)) + DAY(MID(`kode_kas_keluar`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kas_keluar,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "KK" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "KK" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "KK" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "KK" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
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
                txtkodekeluar.Text = autonumber()
                GridControl1.Enabled = False
            Else
                If txtkodekeluar.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
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
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub simpan()
        kodekeluar = autonumber()

        Call koneksii()
        sql = "INSERT INTO tb_kas_keluar (kode_kas_keluar, kode_kas, kode_user, jenis_kas, tanggal_transaksi, keterangan_kas, saldo_kas, print_kas, posted_kas, created_by, updated_by,date_created, last_updated) VALUES ('" & kodekeluar & "','" & cmbkas.Text & "','" & cmbsales.Text & "','KEluar', '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtketerangan.Text & "','" & saldokeluar & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        kodekas = cmbkas.Text

        If kodekas IsNot "" Then
            Call koneksii()
            sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_kas_keluar, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodekas & "','" & kodekeluar & "', 'MASUK','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Kas Keluar Nomor " & kodekeluar & "','" & saldokeluar & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        btntambah.Text = "Tambah"
        Me.Refresh()
        Call awal()

    End Sub

    Sub edit()
        Call koneksii()
        sql = "UPDATE tb_kas_keluar SET kode_user='" & cmbsales.Text & "',kode_kas='" & cmbkas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', saldo_kas='" & saldokeluar & "', keterangan_kas='" & txtketerangan.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated=now()  WHERE  kode_kas_keluar='" & txtkodekeluar.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        kodekas = cmbkas.Text

        If kodekas IsNot "" Then
            Call koneksii()
            sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbkas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', debet_kas='" & saldokeluar & "', kredit_kas='" & 0 & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_kas_keluar='" & txtkodekeluar.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
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
                If txtkodekeluar.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
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
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If editstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus Data Kas Keluar " & Me.txtkodekeluar.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_kas_keluar WHERE  kode_kas_keluar='" & txtkodekeluar.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

                sql = "DELETE FROM tb_transaksi_kas WHERE  kode_kas_keluar='" & txtkodekeluar.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader

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
            Call cetak_faktur()

            sql = "UPDATE tb_kas_keluar SET print_kas = 1 WHERE kode_kas_keluar = '" & txtkodekeluar.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub cetak_faktur()
        rpt_faktur = New fakturkaskeluar
        rpt_faktur.SetParameterValue("nofaktur", txtkodekeluar.Text)
        rpt_faktur.SetParameterValue("kodekas", txtnamakas.Text)
        rpt_faktur.SetParameterValue("saldo", saldokeluar)
        rpt_faktur.SetParameterValue("tanggal", dttransaksi.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Private Sub fkaskeluar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
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
        txtkodekeluar.Text = GridView1.GetFocusedRowCellValue("kode_kas_keluar")

        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_kas_keluar WHERE kode_kas_keluar  = '" & txtkodekeluar.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                viewkodesales = dr("kode_user")
                viewkodekas = dr("kode_kas")
                viewtglkas = dr("tanggal_transaksi")
                saldokeluar = dr("saldo_kas")
                viewketerangan = dr("keterangan_kas")
                statusprint = dr("print_kas")
                statusposted = dr("posted_kas")

                cmbsales.Text = viewkodesales
                cmbkas.Text = viewkodekas
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
            cnn.Close()
        End Using
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