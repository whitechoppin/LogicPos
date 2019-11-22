Imports System.Data.Odbc
Public Class fcaricust
    Dim pilih As String
    Dim kode As String
    Private Sub fcaricust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.MdiParent = fmenu
        'Call koneksii()
        Call awal()
        Call tabel()
    End Sub
    Sub awal()
        cmbcari.SelectedIndex = 1
        'txtcari.Clear()
        txtcari.Focus()
        txtcari.Text = isicari2

    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_pelanggan"
        GridColumn2.Caption = "Nama Pelanggan"
        GridColumn2.FieldName = "nama_pelanggan"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "Select tb_pelanggan.kode_pelanggan, tb_pelanggan.nama_pelanggan from tb_pelanggan"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
    End Sub
    Sub cari()
        'Call koneksii()
        If cmbcari.SelectedIndex = 0 Then
            pilih = "tb_pelanggan.kode_pelanggan"
        Else
            If cmbcari.SelectedIndex = 1 Then
                pilih = "tb_pelanggan.nama_pelanggan"
            End If
        End If
        Using cnn As New OdbcConnection(strConn)
            sql = "Select tb_pelanggan.kode_pelanggan, tb_pelanggan.nama_pelanggan from tb_pelanggan where " & pilih & " LIKE '%" & txtcari.Text & "%'  "
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using

    End Sub
    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub
    Private Sub btnmasuk_Click(sender As Object, e As EventArgs) Handles btnmasuk.Click
        If tutupcus = 1 Then
            fpricelist.txtkodecus.Text = Me.GridView1.GetFocusedRowCellValue("kode_pelanggan")
            'Else
            '    flaporanpenjualan.txtkodekasir.Text = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcus = 1 Then
            fpricelist.txtkodecus.Text = Me.GridView1.GetFocusedRowCellValue("kode_pelanggan")
        ElseIf tutupcus = 2 Then
            fpenjualan.cmbcustomer.Text = Me.GridView1.GetFocusedRowCellValue("kode_pelanggan")
        ElseIf tutupcus = 3 Then
            fbarangkeluar.cmbcustomer.Text = Me.GridView1.GetFocusedRowCellValue("kode_pelanggan")
        End If
        Me.Close()
    End Sub
End Class