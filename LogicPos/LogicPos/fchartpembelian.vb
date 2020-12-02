Imports System.Data.Odbc

Public Class fchartpembelian
    Public namaform As String = "chart-pembelian"
    Dim idgudang, iduser, idsupplier As Integer

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
    Private Sub fchartpembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call comboboxsupplier()
        Call comboboxgudang()
        Call comboboxuser()

        cmbsales.Text = ""
        cmbgudang.Text = ""
        cmbsupplier.Text = ""

        Call historysave("Membuka Chart Pembelian", "N/A", namaform)
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

    Sub carisupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            idsupplier = Val(dr("id"))
        Else
            idsupplier = 0
        End If
    End Sub
    Sub carigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            idgudang = Val(dr("id"))
        Else
            idgudang = 0
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT id FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            iduser = Val(dr("id"))
        Else
            iduser = 0
        End If
    End Sub

    Sub LoadChart()
        Me.Cursor = Cursors.WaitCursor

        Call koneksii()
        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                End If
            End If
        Else
            If rbharian.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE(tgl_pembelian) as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE(tgl_pembelian)"
                        End If
                    End If
                End If
            ElseIf rbbulanan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y-%m') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y-%m')"
                        End If
                    End If
                End If
            ElseIf rbtahunan.Checked Then
                If iduser > 0 Then
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    End If
                Else
                    If idgudang > 0 Then
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        End If
                    Else
                        If idsupplier > 0 Then
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
                        Else
                            sql = "SELECT SUM(subtotal) AS total, DATE_FORMAT(tgl_pembelian,'%Y') as tgl FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' GROUP BY DATE_FORMAT(tgl_pembelian,'%Y')"
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

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        If rbharian.Checked Or rbbulanan.Checked Or rbtahunan.Checked Then
            Call LoadChart()
        Else
            MsgBox("Pilih grup harian, bulanan atau tahunan", vbInformation)
        End If
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

    End Sub

    Private Sub btncarisupplier_Click(sender As Object, e As EventArgs) Handles btncarisupplier.Click

    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click

    End Sub
End Class