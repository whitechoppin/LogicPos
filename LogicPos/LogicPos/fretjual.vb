Public Class fretjual
    Public kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang As String
    Public banyak, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang, banyak_selisih As Double
    Private Sub fretjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
    End Sub
    Sub awal()
        txtretur.Clear()
        lblnamaibarang.Text = "( " + kode_barang + " ) " + nama_barang
        txtretur.Focus()
    End Sub
    Sub proses_pindah()
        If freturjual.GridView2.RowCount = 0 Then
            Dim Lokasi As Integer
            'jika 0 di tambah
            freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, Val(txtretur.Text), satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, Val(harga_diskon * Val(txtretur.Text)), laba, modal_barang)
            freturjual.GridControl2.RefreshDataSource()

            If (banyak - Val(txtretur.Text)).Equals(0) Then
                For i As Integer = 0 To freturjual.GridView1.RowCount - 1
                    If freturjual.GridView1.GetRowCellValue(i, "kode_stok").Equals(kode_stok) Then
                        'freturjual.GridView1.DeleteRow(i)
                        'Me.Close()
                        Lokasi = i
                    End If
                Next

                If Lokasi > -1 Then
                    freturjual.GridView1.DeleteRow(Lokasi)
                    Lokasi = -1
                    Me.Close()
                End If
                'MsgBox("bagian 1")
            Else
                For i As Integer = 0 To freturjual.GridView1.RowCount - 1
                    If freturjual.GridView1.GetRowCellValue(i, "kode_stok").Equals(kode_stok) Then
                        freturjual.GridView1.SetRowCellValue(i, "banyak", banyak - Val(txtretur.Text))
                        freturjual.GridView1.SetRowCellValue(i, "subtotal", harga_diskon * (banyak - Val(txtretur.Text)))
                        Me.Close()
                    End If
                Next
                'MsgBox("bagian 2")
            End If
        Else
            'jika bukan 0 di cari
            Dim lokasi, lokasi1 As Integer
            For i As Integer = 0 To freturjual.GridView2.RowCount - 1
                'jika ada
                If freturjual.GridView2.GetRowCellValue(i, "kode_stok") = kode_stok Then
                    lokasi = i
                    Dim banyak As Integer = freturjual.GridView2.GetRowCellValue(lokasi, "banyak")
                    freturjual.GridView2.SetRowCellValue(i, "banyak", banyak + Val(txtretur.Text))
                    freturjual.GridView2.SetRowCellValue(i, "subtotal", harga_diskon * (banyak + Val(txtretur.Text)))
                    'hapus jika banyak = 0
                    For a As Integer = 0 To freturjual.GridView1.RowCount - 1
                        If freturjual.GridView1.GetRowCellValue(a, "kode_stok") = kode_stok Then
                            lokasi1 = a
                            Dim banyak_potong As Integer = freturjual.GridView1.GetRowCellValue(lokasi1, "banyak")
                            If (banyak_potong - txtretur.Text).Equals(0) Then
                                freturjual.GridView1.DeleteRow(lokasi1)

                                'MsgBox("bagian 3")
                            Else
                                freturjual.GridView1.SetRowCellValue(lokasi1, "banyak", freturjual.GridView1.GetRowCellValue(lokasi1, "banyak") - Val(txtretur.Text))
                                freturjual.GridView1.SetRowCellValue(lokasi1, "subtotal", harga_diskon * (freturjual.GridView1.GetRowCellValue(lokasi1, "banyak") - Val(txtretur.Text)))

                                'MsgBox("bagian 4")
                            End If
                        End If
                    Next
                    'tambah jika tidak 0
                    'For a As Integer = 0 To freturjual.GridView1.RowCount - 1
                    '    If freturjual.GridView1.GetRowCellValue(a, "kode_stok") = kode_stok Then
                    '        lokasi1 = a
                    '        Dim banyak_potong As Integer = freturjual.GridView1.GetRowCellValue(lokasi1, "banyak")
                    '        If banyak_potong - txtretur.Text <> 0 Then
                    '            freturjual.GridView1.SetRowCellValue(lokasi1, "banyak", freturjual.GridView1.GetRowCellValue(lokasi1, "banyak") - Val(txtretur.Text))
                    '            freturjual.GridView1.SetRowCellValue(i, "subtotal", harga_diskon * (banyak - freturjual.GridView1.GetRowCellValue(lokasi1, "banyak") - Val(txtretur.Text)))
                    '        End If
                    '    End If
                    'Next
                    Me.Close()

                    Exit Sub
                End If
            Next

            freturjual.tabel2.Rows.Add(kode_barang, kode_stok, nama_barang, Val(txtretur.Text), satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            freturjual.GridControl2.RefreshDataSource()

            For i As Integer = 0 To freturjual.GridView1.RowCount - 1
                Dim lokasi2 As Integer
                If freturjual.GridView1.GetRowCellValue(i, "kode_stok") = kode_stok Then
                    lokasi2 = i
                    If banyak - Val(txtretur.Text) = 0 Then
                        freturjual.GridView1.DeleteRow(lokasi2)
                    Else
                        freturjual.GridView1.SetRowCellValue(lokasi2, "banyak", banyak - Val(txtretur.Text))
                        freturjual.GridView1.SetRowCellValue(lokasi2, "subtotal", harga_diskon * (banyak - Val(txtretur.Text)))
                    End If
                    Me.Close()
                    Exit Sub
                End If
            Next
            Me.Close()
        End If
    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click

        If Val(txtretur.Text) > banyak Or txtretur.Text.Trim.Length = 0 Then
            Exit Sub
        Else
            banyak_selisih = banyak - Val(txtretur.Text)
            'freturjual.tabel1.Rows.Add(kode_stok, kode_barang, nama_barang, banyak_selisih, satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            'freturjual.GridControl2.RefreshDataSource()
            Call proses_pindah()
            'Me.Close()
        End If
    End Sub
    Private Sub txtretur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtretur.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

End Class