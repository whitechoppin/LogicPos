Imports ZXing

Public Class fgeneratebarcode
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim writer As New BarcodeWriter
        If cbBarcodeType.SelectedIndex = 0 Then
            writer.Format = BarcodeFormat.CODE_128
            picBarcode.Image = writer.Write(txtInput.Text)
        ElseIf cbBarcodeType.SelectedIndex = 1 Then
            writer.Format = BarcodeFormat.QR_CODE
            picBarcode.Image = writer.Write(txtInput.Text)
        Else
            MsgBox("Pilih Jenis Barcode")
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        picBarcode.Image = Nothing
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        sfd.ShowDialog()

        If sfd.FileName > "" Then
            picBarcode.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
        End If
    End Sub

    Private Sub fgeneratebarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class