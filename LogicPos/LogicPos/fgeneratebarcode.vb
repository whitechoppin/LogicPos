Imports ZXing

Public Class fgeneratebarcode

    Dim WithEvents printDoc As New Printing.PrintDocument()

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim writer As New BarcodeWriter

        picBarcode.Image = Nothing

        If txtInput.TextLength > 0 Then
            If cbBarcodeType.SelectedIndex = 0 Then
                writer.Options.Height = 150
                writer.Options.Width = 380
                writer.Format = BarcodeFormat.CODE_128
                picBarcode.Image = writer.Write(txtInput.Text)
            ElseIf cbBarcodeType.SelectedIndex = 1 Then
                writer.Options.Height = 290
                writer.Options.Width = 380
                writer.Format = BarcodeFormat.QR_CODE
                picBarcode.Image = writer.Write(txtInput.Text)
            Else
                MsgBox("Pilih Jenis Barcode")
            End If
        Else
            MsgBox("Isi Text Barcode")
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        picBarcode.Image = Nothing
    End Sub

    Private Sub PrintImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        e.Graphics.DrawImage(picBarcode.Image, e.MarginBounds.Left, e.MarginBounds.Top)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printDoc.Print()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        With sfd
            .Title = "Save Image As"
            .Filter = "Jpg, Jpeg Images|*.jpg;*.jpeg|PNG Image|*.png|BMP Image|*.bmp"
            .AddExtension = True
            .DefaultExt = ".jpg"
            .FileName = "My Barcode.jpg"
            .ValidateNames = True
            '.OverwritePrompt = True
            '.InitialDirectory = Dir()
            '.RestoreDirectory = True

            If .ShowDialog = DialogResult.OK Then
                If .FilterIndex = 1 Then
                    picBarcode.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                ElseIf .FilterIndex = 2 Then
                    picBarcode.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png)
                ElseIf .FilterIndex = 3 Then
                    picBarcode.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                End If
            Else
                Return
            End If

        End With
    End Sub

    Private Sub fgeneratebarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        cbBarcodeType.SelectedIndex = 0
    End Sub
End Class