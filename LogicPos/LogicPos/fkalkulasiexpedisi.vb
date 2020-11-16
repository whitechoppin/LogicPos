Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class fkalkulasiexpedisi
    Public namaform As String = "tools-kalkulasi_expedisi"

    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam expedisi
    Public kodepengiriman As String
    Dim totalongkospengiriman, hargabarang, panjangbarang, lebarbarang, tinggibarang, volumebarang, banyakbarang, totalvolumebarang, ongkirbarang, totalongkirbarang, totalhargabarang, grandtotalbarang, grandtotalvolumebarang As Double

    'variabel bantuan view pengiriman
    Dim nomorpengiriman, nomoruser, namaexpedisi, alamatexpedisi, telpexpedisi, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim viewtglpengiriman As DateTime
    Dim viewtotalpengiriman As Double

    'variabel bantuan hitung gridview
    Dim ongkirbarangulang, totalongkirbarangulang, totalhargabarangulang, grandtotalbarangulang As Double

    Dim summarytotalvolumebefore, volumebarangbefore, qtybarangbefore, totalvolumebarangbefore As Double

    Dim rpt_faktur As New ReportDocument

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

    Private Sub fkalkulasiexpedisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodepengiriman = currentnumber()
        Call inisialisasi(kodepengiriman)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("total_volume").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_volume", "{0:n0}")
            .Columns("ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ongkos_kirim", "{0:C}")
            .Columns("total_ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_ongkos_kirim", "{0:C}")
            .Columns("total_harga_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_harga_barang", "{0:C}")
            .Columns("grand_total_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "grand_total_barang", "{0:C}")
        End With
    End Sub

    'Function autonumber()
    '    Call koneksii()
    '    sql = "SELECT RIGHT(kode_pengiriman,3) FROM tb_pengiriman WHERE DATE_FORMAT(MID(`kode_pengiriman`, 3 , 6), ' %y ')+ MONTH(MID(`kode_pengiriman`,3 , 6)) + DAY(MID(`kode_pengiriman`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pengiriman,3) DESC"
    '    Dim pesan As String = ""
    '    Try
    '        cmmd = New OdbcCommand(sql, cnn)
    '        dr = cmmd.ExecuteReader
    '        If dr.HasRows Then
    '            dr.Read()
    '            If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
    '                Return "PM" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '            Else
    '                If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
    '                    Return "PM" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                Else
    '                    If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
    '                        Return "PM" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Return "PM" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT id FROM tb_pengiriman ORDER BY id DESC LIMIT 1;"
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
        Finally
            'cnn.Close()
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT kode_pengiriman FROM tb_pengiriman WHERE date_created < (SELECT date_created FROM tb_pengiriman WHERE kode_pengiriman = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT kode_pengiriman FROM tb_pengiriman WHERE date_created > (SELECT date_created FROM tb_pengiriman WHERE kode_pengiriman = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
        Finally
            'cnn.Close()
        End Try
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call inisialisasi(txtnonota.Text)
    End Sub

    Private Sub btncaridata_Click(sender As Object, e As EventArgs) Handles btncaridata.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
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

        cmbuser.SelectedIndex = 0
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

        txttotalongkir.Clear()
        txttotalongkir.Text = 0
        txttotalongkir.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = True

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = True

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = True


        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnreset.Enabled = True
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
        sql = "SELECT * FROM tb_pengiriman_detail WHERE pengiriman_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("panjang_barang")), Val(dr("lebar_barang")), Val(dr("tinggi_barang")), Val(dr("volume_barang")), Val(dr("qty")), Val(dr("total_volume")), Val(dr("harga_barang")), Val(dr("ongkos_kirim")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("grand_total_barang")))
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

        txttotalongkir.Enabled = False

        dtpengiriman.Enabled = False
        dtpengiriman.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncaribarang.Enabled = False

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = False

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = False

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = False


        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = False

        btntambahbarang.Enabled = False
        btnreset.Enabled = False
        btnrefresh.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()


        If nomorkode IsNot "" Then
            sql = "SELECT * FROM tb_pengiriman WHERE id = '" & nomorkode.ToString & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                'header
                nomorpengiriman = dr("kode_pengiriman")
                nomoruser = dr("kode_user")

                namaexpedisi = dr("nama_expedisi")
                alamatexpedisi = dr("alamat_expedisi")
                telpexpedisi = dr("telp_expedisi")

                statusprint = dr("print_pengiriman")
                statusposted = dr("posted_pengiriman")

                viewtotalpengiriman = dr("total_pengiriman")
                viewtglpengiriman = dr("tgl_pengiriman")

                viewketerangan = dr("keterangan_pengiriman")

                'isi data pengiriman
                txtnonota.Text = nomorpengiriman
                cmbuser.Text = nomoruser

                txtnamaexpedisi.Text = namaexpedisi
                txtalamatexpedisi.Text = alamatexpedisi
                txttelpexpedisi.Text = telpexpedisi

                txttotalongkir.Text = viewtotalpengiriman
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
            cmbuser.Text = ""

            txtnamaexpedisi.Text = ""
            txtalamatexpedisi.Text = ""
            txttelpexpedisi.Text = ""

            dtpengiriman.Value = Date.Now
            txttotalongkir.Text = 0

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

        txttotalongkir.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = True

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = True

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnreset.Enabled = True
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
            .Columns.Add("panjang_barang", GetType(Double))
            .Columns.Add("lebar_barang", GetType(Double))
            .Columns.Add("tinggi_barang", GetType(Double))
            .Columns.Add("volume_barang", GetType(Double))
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("total_volume", GetType(Double))
            .Columns.Add("harga_barang", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("total_ongkos_kirim", GetType(Double))
            .Columns.Add("total_harga_barang", GetType(Double))
            .Columns.Add("grand_total_barang", GetType(Double))
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.Width = 40

        GridColumn3.FieldName = "panjang_barang"
        GridColumn3.Caption = "Panjang Barang"
        GridColumn3.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn3.DisplayFormat.FormatString = "{0:n0}"
        GridColumn3.Width = 5

        GridColumn4.FieldName = "lebar_barang"
        GridColumn4.Caption = "Lebar Barang"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 5

        GridColumn5.FieldName = "tinggi_barang"
        GridColumn5.Caption = "Tinggi Barang"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 5

        GridColumn6.FieldName = "volume_barang"
        GridColumn6.Caption = "Volume Barang"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 5

        GridColumn7.FieldName = "qty"
        GridColumn7.Caption = "Qty"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 5

        GridColumn8.FieldName = "total_volume"
        GridColumn8.Caption = "Total Volume"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20

        GridColumn9.FieldName = "harga_barang"
        GridColumn9.Caption = "Harga Barang"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:C}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "ongkos_kirim"
        GridColumn10.Caption = "Ongkos_kirim"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:C}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "total_ongkos_kirim"
        GridColumn11.Caption = "Total Ongkos Kirim"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:C}"
        GridColumn11.Width = 30

        GridColumn12.FieldName = "total_harga_barang"
        GridColumn12.Caption = "Total Harga Barang"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:C}"
        GridColumn12.Width = 30

        GridColumn13.FieldName = "grand_total_barang"
        GridColumn13.Caption = "Grand Total Barang"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:C}"
        GridColumn13.Width = 30

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtpanjangbarang.Clear()
        txtlebarbarang.Clear()
        txttinggibarang.Clear()
        txtbanyakbarang.Clear()
        txthargabarang.Text = 0
        txtkodebarang.Focus()
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" & cmbuser.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtuser.Text = dr("nama_user")
        Else
            txtuser.Text = ""
        End If
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
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans


        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            Exit Sub
        Else
            Try
                'tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("panjang_barang")), Val(dr("lebar_barang")), Val(dr("tinggi_barang")), Val(dr("volume_barang")), Val(dr("qty")), Val(dr("total_volume")), Val(dr("harga_barang")), Val(dr("ongkos_kirim")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("grand_total_barang")))

                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "INSERT INTO tb_pengiriman_detail (kode_pengiriman, kode_barang, nama_barang, panjang_barang, lebar_barang, tinggi_barang, volume_barang, qty, total_volume, harga_barang, ongkos_kirim, total_ongkos_kirim, total_harga_barang, grand_total_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepengiriman & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "panjang_barang") & "','" & GridView1.GetRowCellValue(i, "lebar_barang") & "','" & GridView1.GetRowCellValue(i, "tinggi_barang") & "','" & GridView1.GetRowCellValue(i, "volume_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "total_volume") & "', '" & GridView1.GetRowCellValue(i, "harga_barang") & "','" & GridView1.GetRowCellValue(i, "ongkos_kirim") & "','" & GridView1.GetRowCellValue(i, "total_ongkos_kirim") & "','" & GridView1.GetRowCellValue(i, "total_harga_barang") & "','" & GridView1.GetRowCellValue(i, "grand_total_barang") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next
                myCommand.CommandText = "INSERT INTO tb_pengiriman (kode_pengiriman, kode_user, nama_expedisi, alamat_expedisi, telp_expedisi, tgl_pengiriman, total_pengiriman, print_pengiriman, posted_pengiriman, keterangan_pengiriman, created_by, updated_by,date_created, last_updated) VALUES ('" & kodepengiriman & "','" & cmbuser.Text & "','" & txtnamaexpedisi.Text & "','" & txtalamatexpedisi.Text & "','" & txttelpexpedisi.Text & "','" & Format(dtpengiriman.Value, "yyyy-MM-dd HH:mm:ss") & "','" & totalongkospengiriman & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Sukses")

                'history user ==========
                Call historysave("Menyimpan Data Pengiriman Container Kode " + kodepengiriman, kodepengiriman, namaform)
                '========================

                Call inisialisasi(kodepengiriman)
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
                            If txttotalongkir.Text IsNot "" Or txttotalongkir.Text <= 0 Then
                                Call simpan()
                            Else
                                MsgBox("Isi Ongkos Kirim Expedisi")
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
            .Columns.Add("panjang_barang")
            .Columns.Add("lebar_barang")
            .Columns.Add("tinggi_barang")
            .Columns.Add("volume_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("total_volume", GetType(Double))
            .Columns.Add("harga_barang", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("total_ongkos_kirim", GetType(Double))
            .Columns.Add("total_harga_barang", GetType(Double))
            .Columns.Add("grand_total_barang", GetType(Double))

        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("panjang_barang") = GridView1.GetRowCellValue(i, "panjang_barang")
            baris("lebar_barang") = GridView1.GetRowCellValue(i, "lebar_barang")
            baris("tinggi_barang") = GridView1.GetRowCellValue(i, "tinggi_barang")
            baris("volume_barang") = GridView1.GetRowCellValue(i, "volume_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("total_volume") = GridView1.GetRowCellValue(i, "total_volume")
            baris("harga_barang") = GridView1.GetRowCellValue(i, "harga_barang")
            baris("ongkos_kirim") = GridView1.GetRowCellValue(i, "ongkos_kirim")
            baris("total_ongkos_kirim") = GridView1.GetRowCellValue(i, "total_ongkos_kirim")
            baris("total_harga_barang") = GridView1.GetRowCellValue(i, "total_harga_barang")
            baris("grand_total_barang") = GridView1.GetRowCellValue(i, "grand_total_barang")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturexpedisi
        rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", kodepengiriman)
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
        sql = "UPDATE tb_pengiriman SET print_pengiriman = 1 WHERE kode_pengiriman = '" & txtnonota.Text & "' "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        cbprinted.Checked = True

        Call historysave("Mencetak Data Expedisi Kode " + txtnonota.Text, txtnonota.Text, namaform)
    End Sub

    Sub perbarui(nomornota As String)

        'hapus tb_pembelian_detail
        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction


        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        'loop isi pembelian detail

        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            Exit Sub
        Else
            Try
                myCommand.CommandText = "DELETE FROM tb_pengiriman_detail WHERE kode_pengiriman = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "INSERT INTO tb_pengiriman_detail (kode_pengiriman, kode_barang, nama_barang, panjang_barang, lebar_barang, tinggi_barang, volume_barang, qty, total_volume, harga_barang, ongkos_kirim, total_ongkos_kirim, total_harga_barang, grand_total_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "panjang_barang") & "','" & GridView1.GetRowCellValue(i, "lebar_barang") & "','" & GridView1.GetRowCellValue(i, "tinggi_barang") & "','" & GridView1.GetRowCellValue(i, "volume_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "total_volume") & "', '" & GridView1.GetRowCellValue(i, "harga_barang") & "','" & GridView1.GetRowCellValue(i, "ongkos_kirim") & "','" & GridView1.GetRowCellValue(i, "total_ongkos_kirim") & "','" & GridView1.GetRowCellValue(i, "total_harga_barang") & "','" & GridView1.GetRowCellValue(i, "grand_total_barang") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                myCommand.CommandText = "UPDATE tb_pengiriman SET kode_user = '" & cmbuser.Text & "', nama_expedisi = '" & txtnamaexpedisi.Text & "', alamat_expedisi = '" & txtalamatexpedisi.Text & "', telp_expedisi = '" & txttelpexpedisi.Text & "', tgl_pengiriman = '" & Format(dtpengiriman.Value, "yyyy-MM-dd HH:mm:ss") & "', total_pengiriman='" & totalongkospengiriman & "', print_pengiriman = 0, posted_pengiriman = 1, keterangan_pengiriman = '" & txtketerangan.Text & "', updated_by = '" & fmenu.kodeuser.Text & "', last_updated = now() WHERE kode_pengiriman = '" & nomornota & "' "
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'history user ==========
                Call historysave("Mengedit Data Pengiriman Kode " + nomornota, nomornota, namaform)
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
                                    If txttotalongkir.Text IsNot "" Or txttotalongkir.Text <= 0 Then
                                        btnedit.Text = "Edit"
                                        'isi disini sub updatenya
                                        Call perbarui(txtnonota.Text)
                                    Else
                                        MsgBox("Isi Ongkos Kirim Expedisi")
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
            Call inisialisasi(kodepengiriman)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub txttotalongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txthargabarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargabarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtpanjangbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpanjangbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtlebarbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtlebarbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txttinggibarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttinggibarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txttotalongkir_TextChanged(sender As Object, e As EventArgs) Handles txttotalongkir.TextChanged
        If txttotalongkir.Text = "" Then
            txttotalongkir.Text = 0
        Else
            totalongkospengiriman = txttotalongkir.Text
            txttotalongkir.Text = Format(totalongkospengiriman, "##,##0")
            txttotalongkir.SelectionStart = Len(txttotalongkir.Text)
        End If
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

    Private Sub txtpanjangbarang_TextChanged(sender As Object, e As EventArgs) Handles txtpanjangbarang.TextChanged
        If txtpanjangbarang.Text = "" Then
            txtpanjangbarang.Text = 0
        Else
            panjangbarang = txtpanjangbarang.Text
            txtpanjangbarang.Text = Format(panjangbarang, "##,##0")
            txtpanjangbarang.SelectionStart = Len(txtpanjangbarang.Text)
        End If
    End Sub

    Private Sub txtlebarbarang_TextChanged(sender As Object, e As EventArgs) Handles txtlebarbarang.TextChanged
        If txtlebarbarang.Text = "" Then
            txtlebarbarang.Text = 0
        Else
            lebarbarang = txtlebarbarang.Text
            txtlebarbarang.Text = Format(lebarbarang, "##,##0")
            txtlebarbarang.SelectionStart = Len(txtlebarbarang.Text)
        End If
    End Sub

    Private Sub txttinggibarang_TextChanged(sender As Object, e As EventArgs) Handles txttinggibarang.TextChanged
        If txttinggibarang.Text = "" Then
            txttinggibarang.Text = 0
        Else
            tinggibarang = txttinggibarang.Text
            txttinggibarang.Text = Format(tinggibarang, "##,##0")
            txttinggibarang.SelectionStart = Len(txttinggibarang.Text)
        End If
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

    Private Sub cmbuser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbuser.SelectedIndexChanged
        Call cariuser()
    End Sub

    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodebarang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
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

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Sub tambah()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txthargabarang.Text = "" Or txtpanjangbarang.Text = "" Or txtlebarbarang.Text = "" Or txttinggibarang.Text = "" Or txtbanyakbarang.Text = "" Or banyakbarang <= 0 Then
            Exit Sub
        Else
            volumebarang = panjangbarang * lebarbarang * tinggibarang
            totalvolumebarang = volumebarang * banyakbarang
            totalhargabarang = hargabarang * banyakbarang

            ongkirbarang = 0
            totalongkirbarang = 0
            grandtotalbarang = 0

            tabel.Rows.Add(txtkodebarang.Text, txtnamabarang.Text, Val(panjangbarang), Val(lebarbarang), Val(tinggibarang), Val(volumebarang), Val(banyakbarang), Val(totalvolumebarang), Val(hargabarang), Val(ongkirbarang), Val(totalongkirbarang), Val(totalhargabarang), Val(grandtotalbarang))
            Call reload_tabel()
        End If
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Sub resetdata()
        For i As Integer = 0 To GridView1.RowCount - 1

            GridView1.SetRowCellValue(i, "ongkos_kirim", 0)
            GridView1.SetRowCellValue(i, "total_ongkos_kirim", 0)
            GridView1.SetRowCellValue(i, "grand_total_barang", 0)
        Next
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Call resetdata()
    End Sub

    Sub kalkulasi()
        grandtotalvolumebarang = Val(GridView1.Columns("total_volume").SummaryItem.SummaryValue)
        'tambahkan total barang ulang
        For i As Integer = 0 To GridView1.RowCount - 1
            'mendapatkan ongkir satuan barang
            ongkirbarangulang = totalongkospengiriman * (Val(GridView1.GetRowCellValue(i, "volume_barang")) / grandtotalvolumebarang)
            'mendapatkan total ongkir satuan barang  x qty
            totalongkirbarangulang = ongkirbarangulang * Val(GridView1.GetRowCellValue(i, "qty"))
            'mendapatkan total harga barang
            totalhargabarangulang = Val(GridView1.GetRowCellValue(i, "harga_barang")) * Val(GridView1.GetRowCellValue(i, "qty"))

            grandtotalbarangulang = totalhargabarangulang + totalongkirbarangulang

            GridView1.SetRowCellValue(i, "ongkos_kirim", ongkirbarangulang)
            GridView1.SetRowCellValue(i, "total_ongkos_kirim", totalongkirbarangulang)
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

                    GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(GridView1.GetRowCellValue(e.RowHandle, "volume_barang")) * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * 1)

                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(GridView1.GetRowCellValue(e.RowHandle, "volume_barang")) * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", Val(GridView1.GetRowCellValue(e.RowHandle, "harga_barang")) * e.Value)

            End If
            '============================================
        ElseIf e.Column.FieldName = "panjang_barang" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "panjang_barang", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "volume_barang", 1 * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang")))
                    GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(1 * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "volume_barang", e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang")))
                GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

            End If
            '==========================================
        ElseIf e.Column.FieldName = "lebar_barang" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "lebar_barang", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "volume_barang", 1 * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang")))
                    GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(1 * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "volume_barang", e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang")))
                GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "tinggi_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

            End If
            '===========================================
        ElseIf e.Column.FieldName = "tinggi_barang" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "tinggi_barang", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "volume_barang", 1 * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")))
                    GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(1 * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "volume_barang", e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang")))
                GridView1.SetRowCellValue(e.RowHandle, "total_volume", Val(e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "panjang_barang")) * Val(GridView1.GetRowCellValue(e.RowHandle, "lebar_barang"))) * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

            End If
            '===========================================
        ElseIf e.Column.FieldName = "harga_barang" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "harga_barang", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", 1 * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

                Catch ex As Exception

                End Try
            Else
                GridView1.SetRowCellValue(e.RowHandle, "total_harga_barang", e.Value * Val(GridView1.GetRowCellValue(e.RowHandle, "qty")))

            End If
        End If

        GridView1.RefreshData()
        Call resetdata()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub ritenumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritenumber.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub fkalkulasiexpedisi_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

End Class