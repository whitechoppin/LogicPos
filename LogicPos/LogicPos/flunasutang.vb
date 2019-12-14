Public Class flunasutang
    Private Sub flunasutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            '.Columns("Bayar").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Bayar", "{0:n0}")
        End With
    End Sub
End Class