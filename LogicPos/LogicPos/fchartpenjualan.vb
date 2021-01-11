Imports System.Data.Odbc

Public Class fchartpenjualan
    Public namaform As String = "chart-penjualan"
    Dim idgudang, iduser, idpelanggan, idstok, idbarang As Integer
    Public kodeakses As Integer

    Dim exportstatus, printstatus As Boolean
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
    Private Sub fchartpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call comboboxpelanggan()
        Call comboboxgudang()
        Call comboboxuser()

        cmbsales.Text = ""
        cmbgudang.Text = ""
        cmbpelanggan.Text = ""

        Select Case kodeakses
            Case 1
                printstatus = True
                exportstatus = False
            Case 3
                printstatus = False
                exportstatus = True
            Case 4
                printstatus = True
                exportstatus = True
        End Select

        If fmenu.jabatanuser.Text.Equals("Owner") Then
            cbkeuntungan.Enabled = True
        Else
            cbkeuntungan.Enabled = False
        End If

        Call historysave("Membuka Chart Penjualan", "N/A", namaform)
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
    Sub comboboxgudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbgudang.DataSource = ds.Tables(0)
        cmbgudang.ValueMember = "id"
        cmbgudang.DisplayMember = "kode_gudang"
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
    End Sub

    Sub carigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang ='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idgudang = Val(dr("id"))
            txtnamagudang.Text = dr("nama_gudang")
        Else
            idgudang = 0
            txtnamagudang.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = Val(dr("id"))
            txtnamasales.Text = dr("nama_user")
        Else
            iduser = 0
            txtnamasales.Text = ""
        End If
    End Sub

    Sub caristok()
        Call koneksii()
        sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang, tb_barang.modal_barang FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idstok = Val(dr("idstok"))
            idbarang = Val(dr("idbarang"))

            txtnamabarang.Text = dr("nama_barang")
        Else
            idstok = 0
            idbarang = 0

            txtnamabarang.Text = ""
        End If
    End Sub

    Sub LoadChart()
        Me.Cursor = Cursors.WaitCursor

        Call koneksii()
        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    End If
                End If
            End If
        Else
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE(tgl_penjualan)"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE(tgl_penjualan) as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_penjualan)"
                            End If
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y-%m') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y-%m')"
                            End If
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idgudang & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    Else
                        If idpelanggan > 0 Then
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND pelanggan_id = '" & idpelanggan & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        Else
                            If idstok > 0 Then
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND stok_id = '" & idstok & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            Else
                                sql = "SELECT SUM(subtotal) as total, SUM(keuntungan) as keuntungan, DATE_FORMAT(tgl_penjualan,'%Y') as tgl FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE DATE(tgl_penjualan) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_penjualan,'%Y')"
                            End If
                        End If
                    End If
                End If
            End If
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        Try
            ChartControl1.Series("Penjualan").Visible = True
            ChartControl1.Series("Penjualan").DataSource = ds.Tables(0)
            ChartControl1.Series("Penjualan").ValueDataMembersSerializable = "total"
            ChartControl1.Series("Penjualan").ArgumentDataMember = "tgl"

            If cbkeuntungan.Checked = True Then
                ChartControl1.Series("Keuntungan").Visible = True
                ChartControl1.Series("Keuntungan").DataSource = ds.Tables(0)
                ChartControl1.Series("Keuntungan").ValueDataMembersSerializable = "keuntungan"
                ChartControl1.Series("Keuntungan").ArgumentDataMember = "tgl"
            End If

            ChartControl1.Titles(0).Text = "PERIODE : " & Format(DateTimePicker1.Value, "dd MMMM yyyy") & " S/D " & Format(DateTimePicker2.Value, "dd MMMM yyyy")
        Catch ex As Exception
            Dim strErr As String

            strErr = "Stack trace = " & ex.StackTrace & vbCrLf & "Soure = " & ex.Source & "Error = " & ex.Message
            'WriteToEventLog(strErr, EventLogEntryType.Error, "LoadChart")
            MsgBox("Ada kesalahan, silahkan tutup form lalu coba buka kembali." & vbCrLf & "Error : " & ex.Message, vbInformation)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        If rbharian.Checked Or rbbulanan.Checked Or rbtahunan.Checked Then
            Call LoadChart()
        Else
            MsgBox("Pilih grup harian, bulanan atau tahunan", vbInformation)
        End If
    End Sub

    Sub ExportToExcel()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            ChartControl1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " & yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub
    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If exportstatus.Equals(True) Then
            ExportToExcel()
            Call historysave("Mengexport Chart Penjualan", "N/A", namaform)
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click

    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click

    End Sub


    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call cariuser()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call cariuser()
    End Sub

    Private Sub cmbpelanggan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            ChartControl1.ShowPrintPreview()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub cmbpelanggan_TextChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        Call carigudang()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caristok()
    End Sub
End Class