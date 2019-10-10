Public Class frekeningsupplier
    Private Sub frekening_supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub awal()
        txtnamabank.Enabled = False
        txtnamarek.Enabled = False
        txtnorek.Enabled = False

        txtnamabank.Clear()
        txtnamarek.Clear()
        txtnorek.Clear()

        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False
    End Sub
End Class