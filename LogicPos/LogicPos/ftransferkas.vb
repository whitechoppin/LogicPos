Imports System.Data.Odbc

Public Class ftransferkas
    Dim kodetransfer, kodedarikas, kodekekas, viewketerangan, viewkodekekas, viewkodedarikas, viewkodesales As String
    Dim saldotransfer As Double
    Dim viewtglkas As DateTime
    Dim statusprint, statusposted As Boolean
    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Private Sub ftransferkas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
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
            cnn.Close()
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
    End Sub

    Sub simpan()
        kodetransfer = autonumber()

        Call koneksii()
        sql = "INSERT INTO tb_transfer_kas (kode_transfer_kas, kode_dari_kas, kode_ke_kas, kode_user, jenis_transfer_kas, tanggal_transfer_kas, saldo_transfer_kas, keterangan_transfer_kas, print_transfer_kas, posted_transfer_kas, created_by, updated_by,date_created, last_updated) VALUES ('" & kodetransfer & "','" & cmbdarikas.Text & "','" & cmbkekas.Text & "','" & cmbsales.Text & "','TRANSFER', '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & saldotransfer & "','" & txtketerangantransfer.Text & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        kodedarikas = cmbdarikas.Text

        If kodedarikas IsNot "" Then
            Call koneksii()
            sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_transfer_kas, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodedarikas & "','" & kodetransfer & "', 'KELUAR','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Transfer Kas Nomor " & kodetransfer & "','" & saldotransfer & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        kodekekas = cmbkekas.Text

        If kodekekas IsNot "" Then
            Call koneksii()
            sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_transfer_kas, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodekekas & "','" & kodetransfer & "', 'MASUK','" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Transfer Kas Nomor " & kodetransfer & "','" & 0 & "', '" & saldotransfer & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
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
        sql = "UPDATE tb_transfer_kas SET kode_user='" & cmbsales.Text & "', kode_dari_kas='" & cmbdarikas.Text & "', kode_ke_kas='" & cmbkekas.Text & "', tanggal_transfer_kas='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', saldo_transfer_kas='" & saldotransfer & "', keterangan_transfer_kas='" & txtketerangantransfer.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated=now()  WHERE  kode_transfer_kas='" & txtkodetransfer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        kodedarikas = cmbdarikas.Text

        If kodedarikas IsNot "" Then
            Call koneksii()
            sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbdarikas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangantransfer.Text & "', debet_kas='" & saldotransfer & "', kredit_kas='" & 0 & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_transfer_kas='" & txtkodetransfer.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        kodekekas = cmbkekas.Text

        If kodekekas IsNot "" Then
            Call koneksii()
            sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbkekas.Text & "', tanggal_transaksi='" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangantransfer.Text & "', debet_kas='" & 0 & "', kredit_kas='" & saldotransfer & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_transfer_kas='" & txtkodetransfer.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
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
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
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
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
        btnprint.Enabled = False
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Call cetak_faktur()
    End Sub
    Sub cetak_faktur()
        Dim faktur As String
        'Dim tabel_faktur As New DataTable
        'With tabel_faktur
        '    .Columns.Add("kode_stok")
        '    .Columns.Add("kode_barang")
        '    .Columns.Add("nama_barang")
        '    .Columns.Add("qty", GetType(Double))
        '    .Columns.Add("satuan_barang")
        '    .Columns.Add("jenis_barang")
        'End With

        'Dim baris As DataRow
        'For i As Integer = 0 To GridView1.RowCount - 1
        '    baris = tabel_faktur.NewRow
        '    baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
        '    baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
        '    baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
        '    baris("qty") = GridView1.GetRowCellValue(i, "qty")
        '    baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
        '    baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
        '    tabel_faktur.Rows.Add(baris)
        'Next

        rpt_faktur = New fakturtransferkas
        'rpt_faktur.SetDataSource(tabel_faktur)
        'rpt.SetParameterValue("total", total2)
        rpt_faktur.SetParameterValue("nofaktur", txtkodetransfer.Text)
        rpt_faktur.SetParameterValue("darikas", txtnamadarikas.Text)
        rpt_faktur.SetParameterValue("kekas", txtnamakekas.Text)
        rpt_faktur.SetParameterValue("tanggal", dttransaksi.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangantransfer.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)
        rpt_faktur.SetParameterValue("saldo", saldotransfer)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)

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

        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_transfer_kas WHERE kode_transfer_kas  = '" & txtkodetransfer.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
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
            cnn.Close()
        End Using
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