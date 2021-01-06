Imports System.Data.Odbc

Public Class fchartpelunasanutang
    Public namaform As String = "chart-pelunasan_utang"
    Dim iduser, idsupplier, idkas As Integer
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
    Private Sub fchartpelunasanutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call comboboxsupplier()
        Call comboboxkas()
        Call comboboxuser()

        cmbsales.Text = ""
        cmbkas.Text = ""
        cmbsupplier.Text = ""

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

        Call historysave("Membuka Chart Pelunasan Utang", "N/A", namaform)
    End Sub

    Sub comboboxsupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsupplier.DataSource = ds.Tables(0)
        cmbsupplier.ValueMember = "id"
        cmbsupplier.DisplayMember = "kode_supplier"
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

    Sub carisupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            idsupplier = Val(dr("id"))
            txtnamasupplier.Text = dr("nama_supplier")
        Else
            idsupplier = 0
            txtnamasupplier.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
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

    Sub LoadChart()
        Me.Cursor = Cursors.WaitCursor

        Call koneksii()
        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                End If
            End If
        Else
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND gudang_id = '" & idkas & "'GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND user_id = '" & iduser & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                Else
                    If idkas > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND gudang_id = '" & idkas & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND supplier_id = '" & idsupplier & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
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
            ChartControl1.Series("Pembelian").Visible = True
            ChartControl1.Series("Pembelian").DataSource = ds.Tables(0)
            ChartControl1.Series("Pembelian").ValueDataMembersSerializable = "total"
            ChartControl1.Series("Pembelian").ArgumentDataMember = "tgl"

            'ChartControl1.Series("Series 2").Visible = True
            'ChartControl1.Series("Series 2").DataSource = ds.Tables(0)
            'ChartControl1.Series("Series 2").ValueDataMembersSerializable = "NETT"
            'ChartControl1.Series("Series 2").ArgumentDataMember = "TGL2"

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
            Call historysave("Mengexport Chart Pelunasan Utang", "N/A", namaform)
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            ChartControl1.ShowPrintPreview()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btncarikas_Click(sender As Object, e As EventArgs) Handles btncarikas.Click

    End Sub

    Private Sub btncarisupplier_Click(sender As Object, e As EventArgs) Handles btncarisupplier.Click

    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged

    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged

    End Sub

    Private Sub cmbkas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkas.SelectedIndexChanged

    End Sub

    Private Sub cmbkas_TextChanged(sender As Object, e As EventArgs) Handles cmbkas.TextChanged

    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged

    End Sub

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged

    End Sub
End Class