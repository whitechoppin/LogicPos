Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class fkalkulasipengiriman
    Public namaform As String = "tools-kalkulasi_pengiriman"

    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam expedisi
    Public idpengiriman, iduser, idbarang As String
    Dim hargakubik, hargabarang, banyakbarang, hargatambahongkir, ongkirbarang, totalongkirbarang, totalhargabarang, grandtotalbarang As Double
    Dim kubikbarang As Decimal
    'variabel bantuan view pengiriman
    Dim nomorpengiriman, nomoruser, namaexpedisi, alamatexpedisi, telpexpedisi, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim viewtglpengiriman As DateTime
    Dim viewhargacolly As Double

    'variabel bantuan hitung gridview
    Dim ongkirbarangulang, hargatambahongkirulang, totalongkirbarangulang, totalhargabarangulang, grandtotalbarangulang As Double

    Dim summarytotalvolumebefore, volumebarangbefore, qtybarangbefore, totalvolumebarangbefore As Double

    Dim rpt_faktur As New ReportDocument

    Private Sub fkalkulasipengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        idpengiriman = currentnumber()
        Call inisialisasi(idpengiriman)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("kubik").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "kubik", "{0:N}")
            '.Columns("ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ongkos_kirim", "{0:n0}")
            .Columns("total_ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_ongkos_kirim", "{0:C}")
            .Columns("total_harga_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_harga_barang", "{0:C}")
            .Columns("grand_total_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "grand_total_barang", "{0:C}")
        End With
    End Sub

    Sub comboboxuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbuser.DataSource = ds.Tables(0)
        cmbuser.ValueMember = "id"
        cmbuser.DisplayMember = "kode_user"
    End Sub

    'Function autonumber()
    '    Call koneksii()
    '    sql = "SELECT RIGHT(kode_kirim,3) FROM tb_kirim WHERE DATE_FORMAT(MID(`kode_kirim`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kirim`,3 , 6)) + DAY(MID(`kode_kirim`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kirim,3) DESC"
    '    Dim pesan As String = ""
    '    Try
    '        cmmd = New OdbcCommand(sql, cnn)
    '        dr = cmmd.ExecuteReader
    '        If dr.HasRows Then
    '            dr.Read()
    '            If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
    '                Return "KM" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '            Else
    '                If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
    '                    Return "KM" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                Else
    '                    If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
    '                        Return "KM" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Return "KM" + Format(Now.Date, "yyMMdd") + "001"
    '        End If

    '    Catch ex As Exception
    '        pesan = ex.Message.ToString
    '    Finally
    '        'cnn.Close()
    '    End Try
    '    Return pesan
    'End Function


    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_kirim ORDER BY id DESC LIMIT 1;"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Return dr.Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_kirim WHERE date_created < (SELECT date_created FROM tb_kirim WHERE id = '" & previousnumber & "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    Call inisialisasi(previousnumber)
                    hitnumber = hitnumber + 1
                Else
                    MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
                End If
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            'cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_kirim WHERE date_created > (SELECT date_created FROM tb_kirim WHERE id = '" & nextingnumber & "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    Call inisialisasi(nextingnumber)
                    hitnumber = hitnumber + 1
                Else
                    MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
                End If
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
    End Sub

    Sub awalbaru()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = True
        btnprint.Enabled = False
        btnedit.Enabled = False
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgodata.Enabled = False
        btncaridata.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxuser()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbuser.SelectedIndex = -1
        cmbuser.Enabled = True

        txtnamaexpedisi.Clear()
        txtnamaexpedisi.Enabled = True

        txtalamatexpedisi.Clear()
        txtalamatexpedisi.Enabled = True

        txttelpexpedisi.Clear()
        txttelpexpedisi.Enabled = True

        cbprinted.Checked = False
        cbposted.Checked = False

        dtpengiriman.Enabled = True
        dtpengiriman.Value = Date.Now

        txthargakubik.Clear()
        txthargakubik.Text = 0
        txthargakubik.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnrefresh.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()

    End Sub

    Sub previewpengiriman(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_kirim_detail WHERE kirim_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("qty")), Val(dr("harga_barang")), Val(dr("kubik")), Val(dr("ongkos_kirim")), Val(dr("harga_tambah_ongkir")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("grand_total_barang")), dr("barang_id"))
            GridControl1.RefreshDataSource()
        End While
    End Sub

    Sub inisialisasi(nomorkode As String)

        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = True
        btnsimpan.Enabled = False
        btnprint.Enabled = True
        btnedit.Enabled = True
        btnbatal.Enabled = False

        'button navigations
        btnprev.Enabled = True
        btngo.Enabled = True
        txtgodata.Enabled = True
        btncaridata.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxuser()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbuser.Enabled = False

        txtnamaexpedisi.Enabled = False
        txtalamatexpedisi.Enabled = False
        txttelpexpedisi.Enabled = False

        txthargakubik.Enabled = False

        dtpengiriman.Enabled = False
        dtpengiriman.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncaribarang.Enabled = False

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = False

        btntambahbarang.Enabled = False
        btnrefresh.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode IsNot "" Then
            sql = "SELECT * FROM tb_kirim WHERE id = '" & nomorkode.ToString & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                'header
                nomorpengiriman = dr("id")
                nomoruser = dr("user_id")

                namaexpedisi = dr("nama_expedisi")
                alamatexpedisi = dr("alamat_expedisi")
                telpexpedisi = dr("telp_expedisi")

                statusprint = dr("print_kirim")
                statusposted = dr("posted_kirim")

                viewhargacolly = dr("harga_kubik")
                viewtglpengiriman = dr("tgl_kirim")

                viewketerangan = dr("keterangan_kirim")

                'isi data pengiriman
                txtnonota.Text = nomorpengiriman
                cmbuser.Text = nomoruser

                txtnamaexpedisi.Text = namaexpedisi
                txtalamatexpedisi.Text = alamatexpedisi
                txttelpexpedisi.Text = telpexpedisi

                txthargakubik.Text = viewhargacolly
                txtketerangan.Text = viewketerangan

                dtpengiriman.Value = viewtglpengiriman

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted


                'isi tabel view pengiriman

                Call previewpengiriman(nomorkode)

                'total tabel pembelian
            End If
        Else
            txtnonota.Clear()
            cmbuser.SelectedIndex = -1

            txtnamaexpedisi.Text = ""
            txtalamatexpedisi.Text = ""
            txttelpexpedisi.Text = ""

            dtpengiriman.Value = Date.Now
            txthargakubik.Text = 0

            cbprinted.Checked = False
            cbposted.Checked = False

            txtketerangan.Text = ""
        End If

    End Sub
    Sub awaledit()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = False
        btnprint.Enabled = False
        btnedit.Enabled = True
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgodata.Enabled = False
        btncaridata.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxuser()

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False
        'cmbsales.SelectedIndex = -1
        cmbuser.Enabled = True

        txtnamaexpedisi.Enabled = True
        txtalamatexpedisi.Enabled = True
        txttelpexpedisi.Enabled = True

        dtpengiriman.Enabled = True

        txthargakubik.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnrefresh.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        'txtketerangan.Clear()


    End Sub

    Sub tabel_utama()
        tabel = New DataTable

        With tabel
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("harga_barang", GetType(Double))
            .Columns.Add("kubik", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("harga_tambah_ongkir", GetType(Double))
            .Columns.Add("total_ongkos_kirim", GetType(Double))
            .Columns.Add("total_harga_barang", GetType(Double))
            .Columns.Add("grand_total_barang", GetType(Double))
            .Columns.Add("barang_id")
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.Width = 40

        GridColumn3.FieldName = "qty"
        GridColumn3.Caption = "Qty"
        GridColumn3.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn3.DisplayFormat.FormatString = "{0:n0}"
        GridColumn3.Width = 5

        GridColumn4.FieldName = "harga_barang"
        GridColumn4.Caption = "Harga Barang"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:C}"
        GridColumn4.Width = 30

        GridColumn5.FieldName = "kubik"
        GridColumn5.Caption = "kubik"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:N}"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "ongkos_kirim"
        GridColumn6.Caption = "Ongkos_kirim"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:C}"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "harga_tambah_ongkir"
        GridColumn7.Caption = "harga_tambah_ongkir"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:C}"
        GridColumn7.Width = 30

        GridColumn8.FieldName = "total_ongkos_kirim"
        GridColumn8.Caption = "Total Ongkos Kirim"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:C}"
        GridColumn8.Width = 30

        GridColumn9.FieldName = "total_harga_barang"
        GridColumn9.Caption = "Total Harga Barang"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:C}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "grand_total_barang"
        GridColumn10.Caption = "Grand Total Barang"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:C}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "barang_id"
        GridColumn11.Caption = "id barang"
        GridColumn11.Width = 20

    End Sub


    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txtkubik.Clear()
        txthargabarang.Clear()
        txtkodebarang.Focus()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub

    Sub simpan()
        'kodepengiriman = autonumber()

        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction


        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            'Exit Sub
        Else
            'tabel.Rows.Add(txtkodebarang.Text, txtnamabarang.Text, Val(banyakbarang), Val(hargabarang), Val(kubikbarang), Val(ongkirbarang), Val(hargatambahongkir), Val(totalongkirbarang), Val(totalhargabarang), Val(grandtotalbarang))
            Dim nilaikubik As Double
            Dim hurufkubik As String

            Try
                Call koneksii()
                sql = "INSERT INTO tb_kirim (user_id, nama_expedisi, alamat_expedisi, telp_expedisi, tgl_kirim, harga_kubik, print_kirim, posted_kirim, keterangan_kirim, created_by, updated_by, date_created, last_updated) VALUES ('" & iduser & "','" & txtnamaexpedisi.Text & "','" & txtalamatexpedisi.Text & "','" & txttelpexpedisi.Text & "','" & Format(dtpengiriman.Value, "yyyy-MM-dd HH:mm:ss") & "','" & hargakubik & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                cmmd = New OdbcCommand(sql, cnn)
                idpengiriman = CInt(cmmd.ExecuteScalar())

                For i As Integer = 0 To GridView1.RowCount - 1
                    nilaikubik = Val(GridView1.GetRowCellValue(i, "kubik"))
                    hurufkubik = nilaikubik.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))

                    myCommand.CommandText = "INSERT INTO tb_kirim_detail (kirim_id, barang_id, kode_barang, nama_barang, qty, harga_barang, kubik, ongkos_kirim, harga_tambah_ongkir, total_ongkos_kirim, total_harga_barang, grand_total_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & idpengiriman & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & Val(GridView1.GetRowCellValue(i, "qty")) & "','" & Val(GridView1.GetRowCellValue(i, "harga_barang")) & "','" & hurufkubik & "','" & Val(GridView1.GetRowCellValue(i, "ongkos_kirim")) & "','" & Val(GridView1.GetRowCellValue(i, "harga_tambah_ongkir")) & "','" & Val(GridView1.GetRowCellValue(i, "total_ongkos_kirim")) & "','" & Val(GridView1.GetRowCellValue(i, "total_harga_barang")) & "','" & Val(GridView1.GetRowCellValue(i, "grand_total_barang")) & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Sukses")

                'history user ==========
                Call historysave("Menyimpan Data Kirim Kode " & idpengiriman, idpengiriman, namaform)
                '========================
                Call inisialisasi(idpengiriman)
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
            End Try


        End If
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call kalkulasi()

        If GridView1.DataRowCount > 0 Then
            If txtuser.Text IsNot "" Then
                If txtnamaexpedisi.Text IsNot "" Then
                    If txtalamatexpedisi.Text IsNot "" Then
                        If txttelpexpedisi.Text IsNot "" Then
                            If txthargakubik.Text IsNot "" Or hargakubik <= 0 Then
                                Call simpan()
                            Else
                                MsgBox("Isi biaya kubikasi")
                            End If
                        Else
                            MsgBox("Isi Telepon Expedisi")
                        End If
                    Else
                        MsgBox("Isi Alamat Expedisi")
                    End If
                Else
                    MsgBox("Isi Nama Expedisi")
                End If
            Else
                MsgBox("Isi User")
            End If
        Else
            MsgBox("Keranjang Masih Kosong")
        End If
    End Sub

    Public Sub cetak_faktur()
        Dim tabel_faktur As New DataTable

        With tabel_faktur
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("harga_barang", GetType(Double))
            .Columns.Add("kubik", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("harga_tambah_ongkir", GetType(Double))
            .Columns.Add("total_ongkos_kirim", GetType(Double))
            .Columns.Add("total_harga_barang", GetType(Double))
            .Columns.Add("grand_total_barang", GetType(Double))

        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("harga_barang") = GridView1.GetRowCellValue(i, "harga_barang")
            baris("kubik") = GridView1.GetRowCellValue(i, "kubik")
            baris("ongkos_kirim") = GridView1.GetRowCellValue(i, "ongkos_kirim")
            baris("harga_tambah_ongkir") = GridView1.GetRowCellValue(i, "harga_tambah_ongkir")
            baris("total_ongkos_kirim") = GridView1.GetRowCellValue(i, "total_ongkos_kirim")
            baris("total_harga_barang") = GridView1.GetRowCellValue(i, "total_harga_barang")
            baris("grand_total_barang") = GridView1.GetRowCellValue(i, "grand_total_barang")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturpengiriman
        rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", idpengiriman)
        rpt_faktur.SetParameterValue("user", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("namaexpedisi", txtnamaexpedisi.Text)
        rpt_faktur.SetParameterValue("teleponexpedisi", txttelpexpedisi.Text)
        rpt_faktur.SetParameterValue("alamatexpedisi", txtalamatexpedisi.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpengiriman.Value, "dd MMMM yyyy HH:mm:ss").ToString)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Call cetak_faktur()

        Call koneksii()
        sql = "UPDATE tb_kirim SET print_kirim = 1 WHERE id = '" & txtnonota.Text & "' "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        cbprinted.Checked = True

        Call historysave("Mencetak Data Expedisi Kode " & txtnonota.Text, txtnonota.Text, namaform)
    End Sub

    Sub perbarui(nomornota As String)
        'hapus tb_pembelian_detail
        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction


        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            Exit Sub
        Else
            Dim nilaikubik As Double
            Dim hurufkubik As String

            Try
                myCommand.CommandText = "DELETE FROM tb_kirim_detail WHERE kirim_id = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                For i As Integer = 0 To GridView1.RowCount - 1
                    nilaikubik = Val(GridView1.GetRowCellValue(i, "kubik"))
                    hurufkubik = nilaikubik.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))

                    myCommand.CommandText = "INSERT INTO tb_kirim_detail (kirim_id, barang_id, kode_barang, nama_barang, qty, harga_barang, kubik, ongkos_kirim, harga_tambah_ongkir, total_ongkos_kirim, total_harga_barang, grand_total_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & Val(GridView1.GetRowCellValue(i, "qty")) & "','" & Val(GridView1.GetRowCellValue(i, "harga_barang")) & "','" & hurufkubik & "','" & Val(GridView1.GetRowCellValue(i, "ongkos_kirim")) & "','" & Val(GridView1.GetRowCellValue(i, "harga_tambah_ongkir")) & "','" & Val(GridView1.GetRowCellValue(i, "total_ongkos_kirim")) & "','" & Val(GridView1.GetRowCellValue(i, "total_harga_barang")) & "','" & Val(GridView1.GetRowCellValue(i, "grand_total_barang")) & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                Call koneksii()
                myCommand.CommandText = "UPDATE tb_kirim SET user_id = '" & iduser & "', nama_expedisi = '" & txtnamaexpedisi.Text & "', alamat_expedisi = '" & txtalamatexpedisi.Text & "', telp_expedisi = '" & txttelpexpedisi.Text & "', tgl_kirim = '" & Format(dtpengiriman.Value, "yyyy-MM-dd HH:mm:ss") & "', harga_kubik='" & hargakubik & "', print_kirim = 0, posted_kirim = 1, keterangan_kirim = '" & txtketerangan.Text & "', updated_by = '" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id = '" & nomornota & "' "
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'history user ==========
                Call historysave("Mengedit Data Kirim Kode " + nomornota, nomornota, namaform)
                '========================

                MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
                Call inisialisasi(nomornota)

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
            End Try

        End If
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Update"
            Call awaledit()
        Else
            If btnedit.Text = "Update" Then
                Call kalkulasi()

                If GridView1.DataRowCount > 0 Then
                    If txtuser.Text IsNot "" Then
                        If txtnamaexpedisi.Text IsNot "" Then
                            If txtalamatexpedisi.Text IsNot "" Then
                                If txttelpexpedisi.Text IsNot "" Then
                                    If txthargakubik.Text IsNot "" Or hargakubik <= 0 Then
                                        btnedit.Text = "Edit"
                                        'isi disini sub updatenya
                                        Call perbarui(txtnonota.Text)
                                    Else
                                        MsgBox("Isi Biaya Kubikasi")
                                    End If
                                Else
                                    MsgBox("Isi Telepon Expedisi")
                                End If
                            Else
                                MsgBox("Isi Alamat Expedisi")
                            End If
                        Else
                            MsgBox("Isi Nama Expedisi")
                        End If
                    Else
                        MsgBox("Isi User")
                    End If
                Else
                    MsgBox("Keranjang Masih Kosong")
                End If

            End If
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(idpengiriman)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub btncaridata_Click(sender As Object, e As EventArgs) Handles btncaridata.Click
        tutupcarikalkulasipengiriman = 2
        fcarikalkulasipengiriman.ShowDialog()
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call inisialisasi(txtnonota.Text)
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" & cmbuser.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = dr("id")
            txtuser.Text = dr("nama_user")
        Else
            iduser = "0"
            txtuser.Text = ""
        End If
    End Sub

    Private Sub cmbuser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbuser.SelectedIndexChanged
        Call cariuser()
    End Sub

    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodebarang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idbarang = dr("id")
            txtnamabarang.Text = dr("nama_barang")
            hargabarang = dr("modal_barang")
            txthargabarang.Text = Format(hargabarang, "##,##0")
        Else
            txtnamabarang.Text = ""
            txthargabarang.Text = 0
        End If
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        Call caribarang()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Private Sub txthargacolly_TextChanged(sender As Object, e As EventArgs) Handles txthargakubik.TextChanged
        If txthargakubik.Text = "" Then
            txthargakubik.Text = 0
        Else
            hargakubik = txthargakubik.Text
            txthargakubik.Text = Format(hargakubik, "##,##0")
            txthargakubik.SelectionStart = Len(txthargakubik.Text)
        End If
    End Sub

    Private Sub txthargacolly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargakubik.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txthargabarang_TextChanged(sender As Object, e As EventArgs) Handles txthargabarang.TextChanged
        If txthargabarang.Text = "" Then
            txthargabarang.Text = 0
        Else
            hargabarang = txthargabarang.Text
            txthargabarang.Text = Format(hargabarang, "##,##0")
            txthargabarang.SelectionStart = Len(txthargabarang.Text)
        End If
    End Sub

    Private Sub txthargabarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargabarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtbanyakbarang_TextChanged(sender As Object, e As EventArgs) Handles txtbanyakbarang.TextChanged
        If txtbanyakbarang.Text = "" Then
            txtbanyakbarang.Text = 0
        Else
            banyakbarang = txtbanyakbarang.Text
            txtbanyakbarang.Text = Format(banyakbarang, "##,##0")
            txtbanyakbarang.SelectionStart = Len(txtbanyakbarang.Text)
        End If
    End Sub

    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtkubik_TextChanged(sender As Object, e As EventArgs) Handles txtkubik.TextChanged
        If txtkubik.Text = "" Then
            txtkubik.Text = 0
        Else
            kubikbarang = txtkubik.Text
            'txtkubik.Text = Format(kubikbarang, "##,##0")
            'txtkubik.SelectionStart = Len(txtkubik.Text)
        End If
    End Sub

    Private Sub txtkubik_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkubik.KeyPress
        e.Handled = ValidDesimal(e)
    End Sub

    Sub tambah()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txthargabarang.Text = "" Or txtbanyakbarang.Text = "" Or banyakbarang <= 0 Or txtkubik.Text = "" Or kubikbarang <= 0 Then
            Exit Sub
        Else
            totalongkirbarang = hargakubik * kubikbarang
            ongkirbarang = totalongkirbarang / banyakbarang
            hargatambahongkir = hargabarang + ongkirbarang
            totalhargabarang = hargabarang * banyakbarang
            grandtotalbarang = totalongkirbarang + totalhargabarang

            tabel.Rows.Add(txtkodebarang.Text, txtnamabarang.Text, Val(banyakbarang), Val(hargabarang), Val(kubikbarang), Val(ongkirbarang), Val(hargatambahongkir), Val(totalongkirbarang), Val(totalhargabarang), Val(grandtotalbarang), idbarang)
            Call reload_tabel()
        End If
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Sub kalkulasi()
        'tambahkan total barang ulang
        For i As Integer = 0 To GridView1.RowCount - 1
            'mendapatkan ongkir satuan barang
            totalongkirbarangulang = hargakubik * Val(GridView1.GetRowCellValue(i, "kubik"))
            'mendapatkan total ongkir satuan barang  x qty
            ongkirbarangulang = totalongkirbarangulang / Val(GridView1.GetRowCellValue(i, "qty"))
            hargatambahongkirulang = Val(GridView1.GetRowCellValue(i, "harga_barang")) + ongkirbarangulang
            'mendapatkan total harga barang
            totalhargabarangulang = Val(GridView1.GetRowCellValue(i, "harga_barang")) * Val(GridView1.GetRowCellValue(i, "qty"))
            grandtotalbarangulang = totalhargabarangulang + totalongkirbarangulang


            GridView1.SetRowCellValue(i, "ongkos_kirim", ongkirbarangulang)
            GridView1.SetRowCellValue(i, "harga_tambah_ongkir", hargatambahongkirulang)
            GridView1.SetRowCellValue(i, "total_ongkos_kirim", totalongkirbarangulang)
            GridView1.SetRowCellValue(i, "total_harga_barang", totalhargabarangulang)
            GridView1.SetRowCellValue(i, "grand_total_barang", grandtotalbarangulang)
        Next

        GridView1.RefreshData()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call kalkulasi()
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging

        If e.Column.FieldName = "qty" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "qty", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "total_ongkos_kirim", Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "grand_total_barang", (Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) * 1) + (Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * 1))
                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "total_ongkos_kirim", Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "grand_total_barang", (Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) * e.Value) + (Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * e.Value))
            End If
            '============================================

        ElseIf e.Column.FieldName = "harga_barang" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "harga_barang", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "harga_tambah_ongkir", Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) + 1)
                    GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "qty")) * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "grand_total_barang", (Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) + 1) + Val(GridView1.GetRowCellValue(e.RowHandle, "qty")) * 1)
                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "harga_tambah_ongkir", Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) + e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "qty")) * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "grand_total_barang", (Val(GridView1.GetRowCellValue(e.RowHandle, "ongkos_kirim")) + e.Value) + Val(GridView1.GetRowCellValue(e.RowHandle, "qty")) * e.Value)
            End If
        End If

        GridView1.RefreshData()
    End Sub

    Private Sub ritedesimal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritedesimal.KeyPress
        e.Handled = ValidDesimal(e)
    End Sub
End Class