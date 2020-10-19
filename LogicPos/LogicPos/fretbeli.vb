Public Class fretbeli
    Public kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang As String
    Public banyak, harga_beli, subtotal, banyak_selisih, banyak_retur As Double
    Public idbarang, idstok As Integer

    Private Sub fretbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
    End Sub
    Sub awal()
        txtretur.Clear()
        lblnamaibarang.Text = "( " & kode_barang & " ) " & nama_barang
        txtretur.Focus()
    End Sub
    Sub proses_pindah()
        If freturbeli.GridView2.RowCount = 0 Then
            Dim Lokasi As Integer
            'jika 0 di tambah
            freturbeli.tabel2.Rows.Add(kode_stok, kode_barang, nama_barang, banyak_retur, satuan_barang, jenis_barang, harga_beli, harga_beli * banyak_retur)
            freturbeli.GridControl2.RefreshDataSource()
            If (banyak - banyak_retur).Equals(0) Then
                For i As Integer = 0 To freturbeli.GridView1.RowCount - 1
                    If freturbeli.GridView1.GetRowCellValue(i, "kode_stok").Equals(kode_stok) Then
                        Lokasi = i
                    End If
                Next

                If Lokasi > -1 Then
                    freturbeli.GridView1.DeleteRow(Lokasi)
                    Lokasi = -1
                    Me.Close()
                End If
                'MsgBox("bagian 1")
            Else
                For i As Integer = 0 To freturbeli.GridView1.RowCount - 1
                    If freturbeli.GridView1.GetRowCellValue(i, "kode_stok").Equals(kode_stok) Then
                        freturbeli.GridView1.SetRowCellValue(i, "qty", banyak - banyak_retur)
                        freturbeli.GridView1.SetRowCellValue(i, "subtotal", (banyak - banyak_retur) * harga_beli)
                        Me.Close()
                    End If
                Next
                'MsgBox("bagian 2")
            End If
        Else
            'jika bukan 0 di cari
            Dim lokasi, lokasi1 As Integer
            For i As Integer = 0 To freturbeli.GridView2.RowCount - 1
                'jika ada
                If freturbeli.GridView2.GetRowCellValue(i, "kode_stok") = kode_stok Then
                    lokasi = i
                    Dim banyak As Integer = freturbeli.GridView2.GetRowCellValue(lokasi, "qty")
                    freturbeli.GridView2.SetRowCellValue(i, "qty", banyak + banyak_retur)
                    freturbeli.GridView2.SetRowCellValue(i, "subtotal", (banyak + banyak_retur) * harga_beli)

                    'hapus jika banyak = 0
                    For a As Integer = 0 To freturbeli.GridView1.RowCount - 1
                        If freturbeli.GridView1.GetRowCellValue(a, "kode_stok") = kode_stok Then
                            lokasi1 = a
                            Dim banyak_potong As Integer = freturbeli.GridView1.GetRowCellValue(lokasi1, "banyak")
                            If (banyak_potong - banyak_retur).Equals(0) Then
                                freturbeli.GridView1.DeleteRow(lokasi1)
                                'MsgBox("bagian 3")
                            Else
                                freturbeli.GridView1.SetRowCellValue(lokasi1, "qty", freturbeli.GridView1.GetRowCellValue(lokasi1, "qty") - banyak_retur)
                                freturbeli.GridView1.SetRowCellValue(lokasi1, "subtototal", (freturbeli.GridView1.GetRowCellValue(lokasi1, "qty") - banyak_retur) * harga_beli)
                                'MsgBox("bagian 4")
                            End If
                        End If
                    Next
                    'tambah jika tidak 0
                    'For a As Integer = 0 To freturbeli.GridView1.RowCount - 1
                    '    If freturbeli.GridView1.GetRowCellValue(a, "kode_stok") = kode_stok Then
                    '        lokasi1 = a
                    '        Dim banyak_potong As Integer = freturbeli.GridView1.GetRowCellValue(lokasi1, "qty")
                    '        If banyak_potong - txtretur.Text <> 0 Then
                    '            freturbeli.GridView1.SetRowCellValue(lokasi1, "qty", freturbeli.GridView1.GetRowCellValue(lokasi1, "qty") - txtretur.Text)
                    '            freturbeli.GridView1.SetRowCellValue(lokasi1, "subtototal", (freturbeli.GridView1.GetRowCellValue(lokasi1, "qty") - txtretur.Text) * harga_beli)
                    '        End If
                    '    End If
                    'Next
                    Me.Close()
                    Exit Sub
                End If
            Next

            freturbeli.tabel2.Rows.Add(kode_stok, kode_barang, nama_barang, banyak_retur, satuan_barang, jenis_barang, harga_beli, harga_beli * banyak_retur)
            freturbeli.GridControl2.RefreshDataSource()

            For i As Integer = 0 To freturbeli.GridView1.RowCount - 1
                Dim lokasi2 As Integer
                If freturbeli.GridView1.GetRowCellValue(i, "kode_stok") = kode_stok Then
                    lokasi2 = i
                    If banyak - banyak_retur = 0 Then
                        freturbeli.GridView1.DeleteRow(lokasi2)
                    Else
                        freturbeli.GridView1.SetRowCellValue(lokasi2, "qty", banyak - banyak_retur)
                        freturbeli.GridView1.SetRowCellValue(lokasi2, "subtotal", (banyak - banyak_retur) * harga_beli)

                    End If
                    Me.Close()
                    Exit Sub
                End If
            Next
            Me.Close()
        End If
    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click

        If banyak_retur > banyak Or txtretur.Text.Trim.Length = 0 Then
            MsgBox("Retur Kelebihan !")
            'Exit Sub
        Else
            banyak_selisih = banyak - banyak_retur
            'freturbeli.tabel1.Rows.Add(kode_stok, kode_barang, nama_barang, banyak_selisih, satuan_barang, jenis_barang, harga_satuan, diskon_persen, diskon_nominal, harga_diskon, subtotal, laba, modal_barang)
            'freturbeli.GridControl2.RefreshDataSource()
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