Public Class fretjual
    Public kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang As String
    Public banyak, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang, banyak_selisih As Double
    Private Sub fretjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
    End Sub
    Sub awal()
        txtretur.Clear()
        lblnamaibarang.Text = "( " + kode_barang + " ) " + nama_barang
    End Sub
    Sub proses_pindah()
        If freturjual.GridView2.RowCount = 0 Then
            'jika 0 di tambah
            freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, Val(txtretur.Text), satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            freturjual.GridControl2.RefreshDataSource()
        Else
            'jika bukan 0 di cari
            Dim lokasi As Integer

            For i As Integer = 0 To freturjual.GridView2.RowCount - 1
                'jika ada

                If freturjual.GridView2.GetRowCellValue(i, "kode_stok") = kode_stok Then
                    lokasi = i
                    freturjual.GridView2.SetRowCellValue(i, "banyak", txtretur.Text)
                    Me.Close()
                End If
            Next
            freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, Val(txtretur.Text), satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            freturjual.GridControl2.RefreshDataSource()
            Me.Close()
        End If

    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click

        If Val(txtretur.Text) > banyak Then
            Exit Sub
        Else
            banyak_selisih = banyak - Val(txtretur.Text)

            'freturjual.tabel1.Rows.Add(kode_stok, kode_barang, nama_barang, banyak_selisih, satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            'freturjual.GridControl2.RefreshDataSource()


            Call proses_pindah()

        End If


    End Sub
End Class