Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporankalkulasiexpedisi
    Public namaform As String = "Tools-Laporan_kalkulasi_kirim"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim tabeltransfer As DataTable

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
    Private Sub flaporankalkulasiexpedisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        dtawal.MaxDate = Now
        dtakhir.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            '.Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With

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

        Call historysave("Membuka Laporan Kalkulasi Expedisi", "N/A", namaform)
    End Sub

    Sub grid()
        GridColumn1.Caption = "No Faktur"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_pengiriman"
        GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn2.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn3.Caption = "Kode Barang"
        GridColumn3.FieldName = "kode_barang"

        GridColumn4.Caption = "Nama Barang"
        GridColumn4.FieldName = "nama_barang"

        GridColumn5.Caption = "Panjang"
        GridColumn5.FieldName = "panjang_barang"

        GridColumn6.Caption = "Lebar"
        GridColumn6.FieldName = "lebar_barang"

        GridColumn7.Caption = "Tinggi"
        GridColumn7.FieldName = "tinggi_barang"

        GridColumn8.Caption = "Volume"
        GridColumn8.FieldName = "volume_barang"

        GridColumn9.Caption = "Qty"
        GridColumn9.FieldName = "qty"

        GridColumn6.Caption = "Total Volume"
        GridColumn6.FieldName = "total_volume"

        GridColumn7.Caption = "Harga Barang"
        GridColumn7.FieldName = "harga_barang"

        GridColumn8.Caption = "Harga + Ongkir"
        GridColumn8.FieldName = "harga_tambah_ongkir"

        GridColumn9.Caption = "Total Ongkir"
        GridColumn9.FieldName = "total_ongkir"

        GridColumn10.Caption = "Total Harga Barang"
        GridColumn10.FieldName = "total_harga_barang"

        GridColumn11.Caption = "Grand Total Barang"
        GridColumn11.FieldName = "grand_total_barang"

        GridColumn12.Caption = "User"
        GridColumn12.FieldName = "nama_user"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
            sql = "SELECT * FROM tb_pengiriman_detail AS tpd JOIN tb_pengiriman AS tp ON tp.id = tpd.pengiriman_id JOIN tb_user AS usr ON usr.id = tp.user_id WHERE DATE(tgl_pengiriman) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_pengiriman_detail AS tpd JOIN tb_pengiriman AS tp ON tp.id = tpd.pengiriman_id JOIN tb_user AS usr ON usr.id = tp.user_id WHERE tgl_pengiriman BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
        Call historysave("Merefresh Laporan Kalkulasi Expedisi", "N/A", namaform)
    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click

    End Sub

    Sub ExportToExcel()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

    End Sub

    Private Sub flaporankalkulasiexpedisi_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class