Imports System.Data.SqlClient
Imports System.Net.Mail

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

    Public Sub RunTransaction(myConnString As String)
        Dim myConnection As New SqlConnection(myConnString)
        myConnection.Open()
        Dim myCommand As SqlCommand = myConnection.CreateCommand()
        Dim myTrans As SqlTransaction
        ' Start a local transaction
        myTrans = myConnection.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = myConnection
        myCommand.Transaction = myTrans
        Try
            myCommand.CommandText = "Insert into Test (id, desc) VALUES (100, 'Description')"
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = "Insert into Test (id, desc) VALUES (101, 'Description')"
            myCommand.ExecuteNonQuery()
            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")
        Catch e As Exception
            Try
                myTrans.Rollback()
            Catch ex As SqlException
                If Not myTrans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " + ex.GetType().ToString() + " was encountered while attempting to roll back the transaction.")
                End If
            End Try
            Console.WriteLine("An exception of type " + e.GetType().ToString() + "was encountered while inserting the data.")
            Console.WriteLine("Neither record was written to database.")
        Finally
            myConnection.Close()
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
End Class