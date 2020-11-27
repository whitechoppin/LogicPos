Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Net.Mail
Imports System.Threading
Imports System.Globalization
Imports System.Net.NetworkInformation
Imports System.IO

Public Class ftokosejati
    Private Sub ftokosejati_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
    End Sub

    Private Sub ftokosejati_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

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

    Private Sub ftokosejati_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

    Public Sub saveuser()
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
            'benar
            myCommand.CommandText = "Insert into tb_pajak (pajak) VALUES (100)"
            myCommand.ExecuteNonQuery()
            'salah
            myCommand.CommandText = "Insert into tb_pajak (pajak) VALUES (2)"
            myCommand.ExecuteNonQuery()
            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")
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
        Finally
            'myConnection.Close()
        End Try
    End Sub


    Private Sub btnsend_Click(sender As Object, e As EventArgs) Handles btnsend.Click
        Try

            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()

            SmtpServer.UseDefaultCredentials = False
            SmtpServer.Credentials = New Net.NetworkCredential("logicpos@sjtsupplies.com", "17agustus1945")

            SmtpServer.Port = 587
            SmtpServer.Host = "mail.sjtsupplies.com"
            SmtpServer.EnableSsl = True
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            mail = New MailMessage()
            mail.From = New MailAddress("logicpos@sjtsupplies.com")
            mail.To.Add(txtemailto.Text)
            mail.Subject = "Testing Email From Logic Pos"
            mail.Body = "will give you data or document"
            SmtpServer.Send(mail)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub exportChart()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            ChartControl1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Sub LoadChart()
        Me.Cursor = Cursors.WaitCursor

        Call koneksii()
        sql = "SELECT SUM(total_penjualan) AS total, MONTH(tgl_penjualan) as bulan FROM tb_penjualan GROUP BY MONTH(tgl_penjualan)"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        Dim dtTemp As New DataTable

        Try
            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 'c%' GROUP BY TGL2 ORDER BY TGL2")

            ChartControl1.Series("Series 1").Visible = True
            ChartControl1.Series("Series 1").DataSource = ds.Tables(0)
            ChartControl1.Series("Series 1").ValueDataMembersSerializable = "total"
            ChartControl1.Series("Series 1").ArgumentDataMember = "bulan"

            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 't%' GROUP BY TGL2 ORDER BY TGL2")

            'ChartControl1.Series("Series 2").Visible = True
            'ChartControl1.Series("Series 2").DataSource = ds.Tables(0)
            'ChartControl1.Series("Series 2").ValueDataMembersSerializable = "NETT"
            'ChartControl1.Series("Series 2").ArgumentDataMember = "TGL2"


            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 'p%' GROUP BY TGL2 ORDER BY TGL2")

            'ChartControl1.Series("Series 3").Visible = True
            'ChartControl1.Series("Series 3").DataSource = ds.Tables(0)
            'ChartControl1.Series("Series 3").ValueDataMembersSerializable = "NETT"
            'ChartControl1.Series("Series 3").ArgumentDataMember = "TGL2"

            'ChartControl1.Titles(1).Text = "PERIODE : " & Format(tgl, "MMMM yyyy")

        Catch ex As Exception
            Dim strErr As String

            strErr = "Stack trace = " & ex.StackTrace & vbCrLf & "Soure = " & ex.Source & "Error = " & ex.Message
            'WriteToEventLog(strErr, EventLogEntryType.Error, "LoadChart")
            MsgBox("Ada kesalahan, silahkan tutup form lalu coba buka kembali." & vbCrLf & "Error : " & ex.Message, vbInformation)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Call saveuser()
        Call LoadChart()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim contoh As Double

        contoh = TextBox1.Text
        contoh.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
        MsgBox(contoh.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US")))
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        'For Each ci As CultureInfo In CultureInfo.GetCultures(CultureTypes.AllCultures)
        '    MsgBox(ci.EnglishName + " - " + ci.Name)
        'Next

        MsgBox(Thread.CurrentThread.CurrentCulture.Name)
        MsgBox(decimalSeparator)
    End Sub

    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from Win32_Processor")
        Dim cpu_ids As String = ""

        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu

        If cpu_ids.Length > 0 Then
            cpu_ids = cpu_ids.Substring(2)
        End If

        Return cpu_ids
    End Function

    Sub ProcessorSpeed()
        ' shows the processor name and speed of the computer
        Dim MyOBJ As Object
        Dim cpu As Object

        MyOBJ = GetObject("WinMgmts:").instancesof("Win32_Processor")
        For Each cpu In MyOBJ
            MsgBox(cpu.Name.ToString + " " + cpu.CurrentClockSpeed.ToString + " Mhz  CPU ID : " + cpu.ProcessorId, vbInformation)
        Next
    End Sub

    Function getMacAddress()
        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Return nics(1).GetPhysicalAddress.ToString
    End Function

    Private Function getMacAddressx() As String
        Try
            Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
            Dim adapter As NetworkInterface
            Dim myMac As String = String.Empty

            For Each adapter In adapters
                Select Case adapter.NetworkInterfaceType
                'Exclude Tunnels, Loopbacks and PPP
                    Case NetworkInterfaceType.Tunnel, NetworkInterfaceType.Loopback, NetworkInterfaceType.Ppp
                    Case Else
                        If Not adapter.GetPhysicalAddress.ToString = String.Empty And Not adapter.GetPhysicalAddress.ToString = "00000000000000E0" Then
                            myMac = adapter.GetPhysicalAddress.ToString
                            Exit For ' Got a mac so exit for
                        End If

                End Select
            Next adapter

            Return myMac
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MsgBox(CpuId() + " " + getMacAddress() + " " + getMacAddressx())
        Call ProcessorSpeed()
        Call exportChart()
        'MsgBox(CreateObject("WScript.Shell").RegRead("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0\ProcessorNameString"))
    End Sub
End Class