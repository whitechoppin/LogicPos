Public Class fretjual
    Public kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang As String
    Public banyak, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang, banyak_selisih, banyak_retur As Double
    Public idbarang, idstok As Integer

    Private Sub fretjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
    End Sub
    Sub awal()
        txtretur.Clear()
        lblnamaibarang.Text = "( " & kode_barang & " ) " & nama_barang
        txtretur.Focus()
    End Sub
    Sub proses_pindah()
        Dim lokasi, lokasi1, lokasi2 As Integer
        Dim banyak_dulu, banyak_potong As Integer
        lokasi = -1
        lokasi1 = -1
        lokasi2 = -1

        If freturjual.GridView2.RowCount = 0 Then
            'jika 0 di tambah
            freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, banyak_retur, satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, Val(harga_diskon * banyak_retur), Val((harga_diskon - modal_barang) * banyak_retur), modal_barang, idbarang, idstok)
            'freturjual.GridControl2.RefreshDataSource()

            For i As Integer = 0 To freturjual.GridView1.RowCount - 1
                If Val(freturjual.GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                    lokasi = i
                End If
            Next

            If lokasi > -1 Then
                If (banyak - banyak_retur).Equals(0) Then
                    freturjual.GridView1.DeleteRow(lokasi)
                    lokasi = -1
                    MsgBox("bagian 1")
                Else
                    freturjual.GridView1.SetRowCellValue(lokasi, "qty", banyak - banyak_retur)
                    freturjual.GridView1.SetRowCellValue(lokasi, "subtotal", harga_diskon * (banyak - banyak_retur))
                    freturjual.GridView1.SetRowCellValue(lokasi, "laba", (harga_diskon - modal_barang) * (banyak - banyak_retur))
                    lokasi = -1
                    MsgBox("bagian 2")
                End If
            End If
        Else
            'jika bukan 0 di cari
            For i As Integer = 0 To freturjual.GridView2.RowCount - 1
                'jika ada
                If Val(freturjual.GridView2.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                    lokasi = i
                End If
            Next

            MsgBox(lokasi)
            MsgBox(idstok)
            If lokasi > -1 Then
                banyak_dulu = Val(freturjual.GridView2.GetRowCellValue(lokasi, "qty"))
                freturjual.GridView2.SetRowCellValue(lokasi, "qty", banyak_dulu + banyak_retur)
                freturjual.GridView2.SetRowCellValue(lokasi, "subtotal", harga_diskon * (banyak_dulu + banyak_retur))
                freturjual.GridView2.SetRowCellValue(lokasi, "laba", (harga_diskon - modal_barang) * (banyak_dulu + banyak_retur))

                'hapus jika banyak = 0
                For a As Integer = 0 To freturjual.GridView1.RowCount - 1
                    If Val(freturjual.GridView1.GetRowCellValue(a, "stok_id")).Equals(idstok) Then
                        lokasi1 = a
                    End If
                Next

                If lokasi1 > -1 Then
                    banyak_potong = Val(freturjual.GridView1.GetRowCellValue(lokasi1, "qty"))
                    If (banyak_potong - banyak_retur).Equals(0) Then
                        freturjual.GridView1.DeleteRow(lokasi1)
                        lokasi1 = -1
                        MsgBox("bagian 3")
                    Else
                        freturjual.GridView1.SetRowCellValue(lokasi1, "qty", banyak_potong - banyak_retur)
                        freturjual.GridView1.SetRowCellValue(lokasi1, "subtotal", harga_diskon * (banyak_potong - banyak_retur))
                        freturjual.GridView1.SetRowCellValue(lokasi1, "laba", (harga_diskon - modal_barang) * (banyak - banyak_retur))

                        lokasi1 = -1
                        MsgBox("bagian 4")
                    End If
                End If
            Else
                freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, banyak_retur, satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, Val(harga_diskon * banyak_retur), Val((harga_diskon - modal_barang) * banyak_retur), modal_barang, idbarang, idstok)
                'freturjual.GridControl2.RefreshDataSource()

                For i As Integer = 0 To freturjual.GridView1.RowCount - 1
                    If Val(freturjual.GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                        lokasi2 = i
                    End If
                Next

                If lokasi2 > -1 Then
                    If (banyak - banyak_retur).Equals(0) Then
                        freturjual.GridView1.DeleteRow(lokasi2)
                        lokasi2 = -1
                        MsgBox("bagian 5")
                    Else
                        freturjual.GridView1.SetRowCellValue(lokasi2, "qty", banyak - banyak_retur)
                        freturjual.GridView1.SetRowCellValue(lokasi2, "subtotal", harga_diskon * (banyak - banyak_retur))
                        freturjual.GridView1.SetRowCellValue(lokasi2, "laba", (harga_diskon - modal_barang) * (banyak - banyak_retur))

                        lokasi2 = -1
                        MsgBox("bagian 6")
                    End If
                End If
            End If
        End If
        freturjual.GridView1.RefreshData()
        freturjual.GridView2.RefreshData()
        freturjual.statusnonota(False)
        Me.Close()
    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        If banyak_retur > banyak Then
            MsgBox("Retur Kelebihan !")
        Else
            banyak_selisih = banyak - banyak_retur
            Call proses_pindah()
        End If
    End Sub
    Private Sub txtretur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtretur.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtretur_TextChanged(sender As Object, e As EventArgs) Handles txtretur.TextChanged
        If txtretur.Text = "" Then
            txtretur.Text = 0
        Else
            banyak_retur = txtretur.Text
            txtretur.Text = Format(banyak_retur, "##,##0")
            txtretur.SelectionStart = Len(txtretur.Text)
        End If
    End Sub
End Class