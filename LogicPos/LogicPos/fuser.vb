Imports System.Data.Odbc

Public Class fuser
    Dim kode As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean

    'master
    Dim cekmasterbarang, cekmasterkategori, cekmastergudang, cekmastercustomer, cekmastersupplier, cekmasteruser, cekmasterkas, cekmasterpricelist, cekmasterreksupp, cekmasterrekcust As Integer
    Dim aksesbarang, akseskategori, aksesgudang, aksescustomer, aksessupplier, aksesuser, akseskas, aksespricelist, aksesreksupp, aksesrekcust As Integer
    'transaksi
    Dim cekpembelian, cekpenjualan, cekreturbeli, cekreturjual, cekbarangmasuk, cekbarangkeluar, cektransferbarang As Integer
    Dim aksespembelian, aksespenjualan, aksesreturbeli, aksesreturjual, aksesbarangmasuk, aksesbarangkeluar, aksestransferbarang As Integer
    'administrasi
    Dim ceklunasutang, ceklunaspiutang, cektransferkas, cekakunmasuk, cekakunkeluar As Integer
    Dim akseslunasutang, akseslunaspiutang, aksestransferkas, aksesakunmasuk, aksesakunkeluar As Integer
    'laporan
    Dim ceklappricelist, ceklappembelian, ceklappenjualan, ceklapreturbeli, ceklapreturjual, ceklapbarangmasuk, ceklapbarangkeluar, ceklaputang, ceklappiutang, ceklapstokbarang, ceklapakunmasuk, ceklapakunkeluar, ceklaptransferkas, ceklaptransferbarang, ceklaptransaksikas As Integer
    Dim akseslappricelist, akseslappembelian, akseslappenjualan, akseslapreturbeli, akseslapreturjual, akseslapbarangmasuk, akseslapbarangkeluar, akseslaputang, akseslappiutang, akseslapstokbarang, akseslapakunmasuk, akseslapakunkeluar, akseslaptransferkas, akseslaptransferbarang, akseslaptransaksikas As Integer

    Private Sub fuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Sub awal()
        txtkode.Clear()
        txtnama.Clear()
        txtpassword.Clear()
        txtalamat.Clear()
        cmbjabatan.SelectedIndex = -1
        txtemail.Clear()
        txttelp.Clear()
        txtalamat.Clear()
        txtketerangan.Clear()

        'akses user
        'master
        cbmasterbarang.Checked = False
        cbmasterkategori.Checked = False
        cbmastergudang.Checked = False
        cbmastercustomer.Checked = False
        cbmastersupplier.Checked = False
        cbmasteruser.Checked = False
        cbmasterkas.Checked = False
        cbmasterpricelist.Checked = False
        cbmasterreksupp.Checked = False
        cbmasterrekcust.Checked = False

        'transaksi
        cbpembelian.Checked = False
        cbpenjualan.Checked = False
        cbreturbeli.Checked = False
        cbreturjual.Checked = False
        cbbarangmasuk.Checked = False
        cbbarangkeluar.Checked = False
        cbtransferbarang.Checked = False

        'administrasi
        cblunasutang.Checked = False
        cblunaspiutang.Checked = False
        cbtransferkas.Checked = False
        cbakunmasuk.Checked = False
        cbakunkeluar.Checked = False

        'laporan
        cblappricelist.Checked = False
        cblappembelian.Checked = False
        cblappenjualan.Checked = False
        cblapreturbeli.Checked = False
        cblapreturjual.Checked = False
        cblapbarangmasuk.Checked = False
        cblapbarangkeluar.Checked = False
        cblaputang.Checked = False
        cblappiutang.Checked = False
        cblapstokbarang.Checked = False
        cblapakunmasuk.Checked = False
        cblapakunkeluar.Checked = False
        cblaptransferkas.Checked = False
        cblaptransferbarang.Checked = False
        cblaptransaksikas.Checked = False

        For id As Integer = 0 To 2
            'master
            clbmasterkategori.SetItemChecked(id, False)
            clbmasterbarang.SetItemChecked(id, False)
            clbmastergudang.SetItemChecked(id, False)
            clbmastercustomer.SetItemChecked(id, False)
            clbmastersupplier.SetItemChecked(id, False)
            clbmasteruser.SetItemChecked(id, False)
            clbmasterkas.SetItemChecked(id, False)
            clbmasterpricelist.SetItemChecked(id, False)
            clbmasterreksupp.SetItemChecked(id, False)
            clbmasterrekcust.SetItemChecked(id, False)

            'transaksi
            clbpembelian.SetItemChecked(id, False)
            clbpenjualan.SetItemChecked(id, False)
            clbreturbeli.SetItemChecked(id, False)
            clbreturjual.SetItemChecked(id, False)
            clbbarangmasuk.SetItemChecked(id, False)
            clbbarangkeluar.SetItemChecked(id, False)
            clbtransferbarang.SetItemChecked(id, False)

            'administrasi
            clblunasutang.SetItemChecked(id, False)
            clblunaspiutang.SetItemChecked(id, False)
            clbtransferkas.SetItemChecked(id, False)
            clbakunmasuk.SetItemChecked(id, False)
            clbakunkeluar.SetItemChecked(id, False)

            'laporan
            If id <= 1 Then
                clblappricelist.SetItemChecked(id, False)
                clblappembelian.SetItemChecked(id, False)
                clblappenjualan.SetItemChecked(id, False)
                clblapreturbeli.SetItemChecked(id, False)
                clblapreturjual.SetItemChecked(id, False)
                clblapbarangmasuk.SetItemChecked(id, False)
                clblapbarangkeluar.SetItemChecked(id, False)
                clblaputang.SetItemChecked(id, False)
                clblappiutang.SetItemChecked(id, False)
                clblapstokbarang.SetItemChecked(id, False)
                clblapakunmasuk.SetItemChecked(id, False)
                clblapakunkeluar.SetItemChecked(id, False)
                clblaptransferkas.SetItemChecked(id, False)
                clblaptransferbarang.SetItemChecked(id, False)
                clblaptransaksikas.SetItemChecked(id, False)
            End If
        Next

        'combo box 
        'master
        cbmasterbarang.Enabled = False
        cbmasterkategori.Enabled = False
        cbmastergudang.Enabled = False
        cbmastercustomer.Enabled = False
        cbmastersupplier.Enabled = False
        cbmasteruser.Enabled = False
        cbmasterkas.Enabled = False
        cbmasterpricelist.Enabled = False
        cbmasterreksupp.Enabled = False
        cbmasterrekcust.Enabled = False

        'transaksi
        cbpembelian.Enabled = False
        cbpenjualan.Enabled = False
        cbreturbeli.Enabled = False
        cbreturjual.Enabled = False
        cbbarangmasuk.Enabled = False
        cbbarangkeluar.Enabled = False
        cbtransferbarang.Enabled = False

        'administrasi
        cblunasutang.Enabled = False
        cblunaspiutang.Enabled = False
        cbtransferkas.Enabled = False
        cbakunmasuk.Enabled = False
        cbakunkeluar.Enabled = False

        'laporan
        cblappricelist.Enabled = False
        cblappembelian.Enabled = False
        cblappenjualan.Enabled = False
        cblapreturbeli.Enabled = False
        cblapreturjual.Enabled = False
        cblapbarangmasuk.Enabled = False
        cblapbarangkeluar.Enabled = False
        cblaputang.Enabled = False
        cblappiutang.Enabled = False
        cblapstokbarang.Enabled = False
        cblapakunmasuk.Enabled = False
        cblapakunkeluar.Enabled = False
        cblaptransferkas.Enabled = False
        cblaptransferbarang.Enabled = False
        cblaptransaksikas.Enabled = False

        'combo box list
        clbmasterbarang.Enabled = False
        clbmasterkategori.Enabled = False
        clbmastergudang.Enabled = False
        clbmastercustomer.Enabled = False
        clbmastersupplier.Enabled = False
        clbmasteruser.Enabled = False
        clbmasterkas.Enabled = False
        clbmasterpricelist.Enabled = False
        clbmasterreksupp.Enabled = False
        clbmasterrekcust.Enabled = False

        'transaksi
        clbpembelian.Enabled = False
        clbpenjualan.Enabled = False
        clbreturbeli.Enabled = False
        clbreturjual.Enabled = False
        clbbarangmasuk.Enabled = False
        clbbarangkeluar.Enabled = False
        clbtransferbarang.Enabled = False

        'administrasi
        clblunasutang.Enabled = False
        clblunaspiutang.Enabled = False
        clbtransferkas.Enabled = False
        clbakunmasuk.Enabled = False
        clbakunkeluar.Enabled = False

        'laporan
        clblappricelist.Enabled = False
        clblappembelian.Enabled = False
        clblappenjualan.Enabled = False
        clblapreturbeli.Enabled = False
        clblapreturjual.Enabled = False
        clblapbarangmasuk.Enabled = False
        clblapbarangkeluar.Enabled = False
        clblaputang.Enabled = False
        clblappiutang.Enabled = False
        clblapstokbarang.Enabled = False
        clblapakunmasuk.Enabled = False
        clblapakunkeluar.Enabled = False
        clblaptransferkas.Enabled = False
        clblaptransferbarang.Enabled = False
        clblaptransaksikas.Enabled = False


        Call koneksii()

        txtkode.Enabled = False
        txtnama.Enabled = False
        txtpassword.Enabled = False
        cmbjabatan.Enabled = False
        txtemail.Enabled = False
        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl.Enabled = True
        Call isitabel()

        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                hapusstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                hapusstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                hapusstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                hapusstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                hapusstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                hapusstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                hapusstatus = True
        End Select
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode User"
        GridColumn1.Width = 40
        GridColumn1.FieldName = "kode_user"
        GridColumn2.Caption = "Nama user"
        GridColumn2.Width = 60
        GridColumn2.FieldName = "nama_user"
        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 80
        GridColumn3.FieldName = "alamat_user"
        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 40
        GridColumn4.FieldName = "telepon_user"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 70
        GridColumn5.FieldName = "keterangan_user"
    End Sub

    Sub isitabel()
        'Call koneksii()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        txtnama.TabIndex = 2
        txtpassword.TabIndex = 3
        cmbjabatan.TabIndex = 4
        txtemail.TabIndex = 5
        txttelp.TabIndex = 6
        txtalamat.TabIndex = 7
        txtketerangan.TabIndex = 8
    End Sub
    Sub enable_text()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cmbjabatan.Enabled = True
        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True

        'akses user
        'master
        cbmasterbarang.Enabled = True
        cbmasterkategori.Enabled = True
        cbmastergudang.Enabled = True
        cbmastercustomer.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekcust.Enabled = True

        'transaksi
        cbpembelian.Enabled = True
        cbpenjualan.Enabled = True
        cbreturbeli.Enabled = True
        cbreturjual.Enabled = True
        cbbarangmasuk.Enabled = True
        cbbarangkeluar.Enabled = True
        cbtransferbarang.Enabled = True

        'administrasi
        cblunasutang.Enabled = True
        cblunaspiutang.Enabled = True
        cbtransferkas.Enabled = True
        cbakunmasuk.Enabled = True
        cbakunkeluar.Enabled = True

        'laporan
        cblappricelist.Enabled = True
        cblappembelian.Enabled = True
        cblappenjualan.Enabled = True
        cblapreturbeli.Enabled = True
        cblapreturjual.Enabled = True
        cblapbarangmasuk.Enabled = True
        cblapbarangkeluar.Enabled = True
        cblaputang.Enabled = True
        cblappiutang.Enabled = True
        cblapstokbarang.Enabled = True
        cblapakunmasuk.Enabled = True
        cblapakunkeluar.Enabled = True
        cblaptransferkas.Enabled = True
        cblaptransferbarang.Enabled = True
        cblaptransaksikas.Enabled = True
        'batas akses user

        txtkode.Focus()
    End Sub

    Sub enable_text_edit()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cmbjabatan.Enabled = True
        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True

        'akses user
        'master
        cbmasterbarang.Enabled = True
        cbmasterkategori.Enabled = True
        cbmastergudang.Enabled = True
        cbmastercustomer.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekcust.Enabled = True

        'transaksi
        cbpembelian.Enabled = True
        cbpenjualan.Enabled = True
        cbreturbeli.Enabled = True
        cbreturjual.Enabled = True
        cbbarangmasuk.Enabled = True
        cbbarangkeluar.Enabled = True
        cbtransferbarang.Enabled = True

        'administrasi
        cblunasutang.Enabled = True
        cblunaspiutang.Enabled = True
        cbtransferkas.Enabled = True
        cbakunmasuk.Enabled = True
        cbakunkeluar.Enabled = True

        'laporan
        cblappricelist.Enabled = True
        cblappembelian.Enabled = True
        cblappenjualan.Enabled = True
        cblapreturbeli.Enabled = True
        cblapreturjual.Enabled = True
        cblapbarangmasuk.Enabled = True
        cblapbarangkeluar.Enabled = True
        cblaputang.Enabled = True
        cblappiutang.Enabled = True
        cblapstokbarang.Enabled = True
        cblapakunmasuk.Enabled = True
        cblapakunkeluar.Enabled = True
        cblaptransferkas.Enabled = True
        cblaptransferbarang.Enabled = True
        cblaptransaksikas.Enabled = True

        'combo box list
        'master barang 
        If aksesbarang > 0 Then
            clbmasterbarang.Enabled = True
        Else
            clbmasterbarang.Enabled = False
        End If

        If akseskategori > 0 Then
            clbmasterkategori.Enabled = True
        Else
            clbmasterkategori.Enabled = False
        End If

        If aksesgudang > 0 Then
            clbmastergudang.Enabled = True
        Else
            clbmastergudang.Enabled = False
        End If

        If aksescustomer > 0 Then
            clbmastercustomer.Enabled = True
        Else
            clbmastercustomer.Enabled = False
        End If

        If aksessupplier > 0 Then
            clbmastersupplier.Enabled = True
        Else
            clbmastersupplier.Enabled = False
        End If

        If aksesuser > 0 Then
            clbmasteruser.Enabled = True
        Else
            clbmasteruser.Enabled = False
        End If

        If akseskas > 0 Then
            clbmasterkas.Enabled = True
        Else
            clbmasterkas.Enabled = False
        End If

        If aksespricelist > 0 Then
            clbmasterpricelist.Enabled = True
        Else
            clbmasterpricelist.Enabled = False
        End If

        If aksesreksupp > 0 Then
            clbmasterreksupp.Enabled = True
        Else
            clbmasterreksupp.Enabled = False
        End If

        If aksesrekcust > 0 Then
            clbmasterrekcust.Enabled = True
        Else
            clbmasterrekcust.Enabled = False
        End If

        'transaksi
        If aksespembelian > 0 Then
            clbpembelian.Enabled = True
        Else
            clbpembelian.Enabled = False
        End If

        If aksespenjualan > 0 Then
            clbpenjualan.Enabled = True
        Else
            clbpenjualan.Enabled = False
        End If

        If aksesreturbeli > 0 Then
            clbreturbeli.Enabled = True
        Else
            clbreturbeli.Enabled = False
        End If

        If aksesreturjual > 0 Then
            clbreturjual.Enabled = True
        Else
            clbreturjual.Enabled = False
        End If

        If aksesbarangmasuk > 0 Then
            clbbarangmasuk.Enabled = True
        Else
            clbbarangmasuk.Enabled = False
        End If

        If aksesbarangkeluar > 0 Then
            clbbarangkeluar.Enabled = True
        Else
            clbbarangkeluar.Enabled = False
        End If

        If aksestransferbarang > 0 Then
            clbtransferbarang.Enabled = True
        Else
            clbtransferbarang.Enabled = False
        End If

        'administrasi
        If akseslunasutang > 0 Then
            clblunasutang.Enabled = True
        Else
            clblunasutang.Enabled = False
        End If

        If akseslunaspiutang > 0 Then
            clblunaspiutang.Enabled = True
        Else
            clblunaspiutang.Enabled = False
        End If

        If aksestransferkas > 0 Then
            clbtransferkas.Enabled = True
        Else
            clbtransferkas.Enabled = False
        End If

        If aksesakunmasuk > 0 Then
            clbakunmasuk.Enabled = True
        Else
            clbakunmasuk.Enabled = False
        End If

        If aksesakunkeluar > 0 Then
            clbakunkeluar.Enabled = True
        Else
            clbakunkeluar.Enabled = False
        End If

        'laporan
        If akseslappricelist > 0 Then
            clblappricelist.Enabled = True
        Else
            clblappricelist.Enabled = False
        End If

        If akseslappembelian > 0 Then
            clblappembelian.Enabled = True
        Else
            clblappembelian.Enabled = False
        End If

        If akseslappenjualan > 0 Then
            clblappenjualan.Enabled = True
        Else
            clblappenjualan.Enabled = False
        End If

        If akseslapreturbeli > 0 Then
            clblapreturbeli.Enabled = True
        Else
            clblapreturbeli.Enabled = False
        End If

        If akseslapreturjual > 0 Then
            clblapreturjual.Enabled = True
        Else
            clblapreturjual.Enabled = False
        End If

        If akseslapbarangmasuk > 0 Then
            clblapbarangmasuk.Enabled = True
        Else
            clblapbarangmasuk.Enabled = False
        End If

        If akseslapbarangkeluar > 0 Then
            clblapbarangkeluar.Enabled = True
        Else
            clblapbarangkeluar.Enabled = False
        End If

        If akseslaputang > 0 Then
            clblaputang.Enabled = True
        Else
            clblaputang.Enabled = False
        End If

        If akseslappiutang > 0 Then
            clblappiutang.Enabled = True
        Else
            clblappiutang.Enabled = False
        End If

        If akseslapstokbarang > 0 Then
            clblapstokbarang.Enabled = True
        Else
            clblapstokbarang.Enabled = False
        End If

        If akseslapakunmasuk > 0 Then
            clblapakunmasuk.Enabled = True
        Else
            clblapakunmasuk.Enabled = False
        End If

        If akseslapakunkeluar > 0 Then
            clblapakunkeluar.Enabled = True
        Else
            clblapakunkeluar.Enabled = False
        End If

        If akseslaptransferkas > 0 Then
            clblaptransferkas.Enabled = True
        Else
            clblaptransferkas.Enabled = False
        End If

        If akseslaptransferbarang > 0 Then
            clblaptransferbarang.Enabled = True
        Else
            clblaptransferbarang.Enabled = False
        End If

        If akseslaptransaksikas > 0 Then
            clblaptransaksikas.Enabled = True
        Else
            clblaptransaksikas.Enabled = False
        End If

        'batas akses user

        txtkode.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()
                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtpassword.Text.Length = 0 Then
                            MsgBox("Password belum terisi !")
                        Else
                            If cmbjabatan.Text.Length = 0 Then
                                MsgBox("Jabatan belum terisi !")
                            Else
                                If txtemail.Text.Length = 0 Then
                                    MsgBox("Email belum terisi !")
                                Else
                                    If txttelp.Text.Length = 0 Then
                                        MsgBox("Telepon belum terisi !")
                                    Else
                                        If txtalamat.Text.Length = 0 Then
                                            MsgBox("Alamat belum terisi !")
                                        Else
                                            Call simpan()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub aksesadmin()
        'master
        cekmasterbarang = 0
        cekmasterkategori = 0
        cekmastergudang = 0
        cekmastercustomer = 0
        cekmastersupplier = 0
        cekmasteruser = 0
        cekmasterkas = 0
        cekmasterpricelist = 0
        cekmasterreksupp = 0
        cekmasterrekcust = 0

        'transaksi
        cekpembelian = 0
        cekpenjualan = 0
        cekreturbeli = 0
        cekreturjual = 0
        cekbarangmasuk = 0
        cekbarangkeluar = 0
        cektransferbarang = 0

        'administrasi
        ceklunasutang = 0
        ceklunaspiutang = 0
        cektransferkas = 0
        cekakunmasuk = 0
        cekakunkeluar = 0

        'laporan
        ceklappricelist = 0
        ceklappembelian = 0
        ceklappenjualan = 0
        ceklapreturbeli = 0
        ceklapreturjual = 0
        ceklapbarangmasuk = 0
        ceklapbarangkeluar = 0
        ceklaputang = 0
        ceklappiutang = 0
        ceklapstokbarang = 0
        ceklapakunmasuk = 0
        ceklapakunkeluar = 0
        ceklaptransferkas = 0
        ceklaptransferbarang = 0
        ceklaptransaksikas = 0

        'We will run through each indice

        For i = 0 To 2
            'master ===
            'barang
            If clbmasterbarang.GetItemChecked(i) Then
                If clbmasterbarang.Items(i).Equals("Tambah") Then
                    cekmasterbarang = cekmasterbarang + 1
                ElseIf clbmasterbarang.Items(i).Equals("Edit") Then
                    cekmasterbarang = cekmasterbarang + 3
                ElseIf clbmasterbarang.Items(i).Equals("Hapus") Then
                    cekmasterbarang = cekmasterbarang + 5
                End If
            Else
                cekmasterbarang = cekmasterbarang + 0
            End If
            'kategori
            If clbmasterkategori.GetItemChecked(i) Then
                If clbmasterkategori.Items(i).Equals("Tambah") Then
                    cekmasterkategori = cekmasterkategori + 1
                ElseIf clbmasterkategori.Items(i).Equals("Edit") Then
                    cekmasterkategori = cekmasterkategori + 3
                ElseIf clbmasterkategori.Items(i).Equals("Hapus") Then
                    cekmasterkategori = cekmasterkategori + 5
                End If
            Else
                cekmasterkategori = cekmasterkategori + 0
            End If
            'gudang
            If clbmastergudang.GetItemChecked(i) Then
                If clbmastergudang.Items(i).Equals("Tambah") Then
                    cekmastergudang = cekmastergudang + 1
                ElseIf clbmastergudang.Items(i).Equals("Edit") Then
                    cekmastergudang = cekmastergudang + 3
                ElseIf clbmastergudang.Items(i).Equals("Hapus") Then
                    cekmastergudang = cekmastergudang + 5
                End If
            Else
                cekmastergudang = cekmastergudang + 0
            End If
            'customer
            If clbmastercustomer.GetItemChecked(i) Then
                If clbmastercustomer.Items(i).Equals("Tambah") Then
                    cekmastercustomer = cekmastercustomer + 1
                ElseIf clbmastercustomer.Items(i).Equals("Edit") Then
                    cekmastercustomer = cekmastercustomer + 3
                ElseIf clbmastercustomer.Items(i).Equals("Hapus") Then
                    cekmastercustomer = cekmastercustomer + 5
                End If
            Else
                cekmastercustomer = cekmastercustomer + 0
            End If
            'supplier
            If clbmastersupplier.GetItemChecked(i) Then
                If clbmastersupplier.Items(i).Equals("Tambah") Then
                    cekmastersupplier = cekmastersupplier + 1
                ElseIf clbmastersupplier.Items(i).Equals("Edit") Then
                    cekmastersupplier = cekmastersupplier + 3
                ElseIf clbmastersupplier.Items(i).Equals("Hapus") Then
                    cekmastersupplier = cekmastersupplier + 5
                End If
            Else
                cekmastersupplier = cekmastersupplier + 0
            End If
            'user
            If clbmasteruser.GetItemChecked(i) Then
                If clbmasteruser.Items(i).Equals("Tambah") Then
                    cekmasteruser = cekmasteruser + 1
                ElseIf clbmasteruser.Items(i).Equals("Edit") Then
                    cekmasteruser = cekmasteruser + 3
                ElseIf clbmasteruser.Items(i).Equals("Hapus") Then
                    cekmasteruser = cekmasteruser + 5
                End If
            Else
                cekmasteruser = cekmasteruser + 0
            End If
            'kas
            If clbmasterkas.GetItemChecked(i) Then
                If clbmasterkas.Items(i).Equals("Tambah") Then
                    cekmasterkas = cekmasterkas + 1
                ElseIf clbmasterkas.Items(i).Equals("Edit") Then
                    cekmasterkas = cekmasterkas + 3
                ElseIf clbmasterkas.Items(i).Equals("Hapus") Then
                    cekmasterkas = cekmasterkas + 5
                End If
            Else
                cekmasterkas = cekmasterkas + 0
            End If
            'pricelist
            If clbmasterpricelist.GetItemChecked(i) Then
                If clbmasterpricelist.Items(i).Equals("Tambah") Then
                    cekmasterpricelist = cekmasterpricelist + 1
                ElseIf clbmasterpricelist.Items(i).Equals("Edit") Then
                    cekmasterpricelist = cekmasterpricelist + 3
                ElseIf clbmasterpricelist.Items(i).Equals("Hapus") Then
                    cekmasterpricelist = cekmasterpricelist + 5
                End If
            Else
                cekmasterpricelist = cekmasterpricelist + 0
            End If
            'rekening supp
            If clbmasterreksupp.GetItemChecked(i) Then
                If clbmasterreksupp.Items(i).Equals("Tambah") Then
                    cekmasterreksupp = cekmasterreksupp + 1
                ElseIf clbmasterreksupp.Items(i).Equals("Edit") Then
                    cekmasterreksupp = cekmasterreksupp + 3
                ElseIf clbmasterreksupp.Items(i).Equals("Hapus") Then
                    cekmasterreksupp = cekmasterreksupp + 5
                End If
            Else
                cekmasterreksupp = cekmasterreksupp + 0
            End If
            'rekening cust
            If clbmasterrekcust.GetItemChecked(i) Then
                If clbmasterrekcust.Items(i).Equals("Tambah") Then
                    cekmasterrekcust = cekmasterrekcust + 1
                ElseIf clbmasterrekcust.Items(i).Equals("Edit") Then
                    cekmasterrekcust = cekmasterrekcust + 3
                ElseIf clbmasterrekcust.Items(i).Equals("Hapus") Then
                    cekmasterrekcust = cekmasterrekcust + 5
                End If
            Else
                cekmasterrekcust = cekmasterrekcust + 0
            End If

            'transaksi ===
            'pembelian
            If clbpembelian.GetItemChecked(i) Then
                If clbpembelian.Items(i).Equals("Tambah") Then
                    cekpembelian = cekpembelian + 1
                ElseIf clbpembelian.Items(i).Equals("Edit") Then
                    cekpembelian = cekpembelian + 3
                ElseIf clbpembelian.Items(i).Equals("Print") Then
                    cekpembelian = cekpembelian + 5
                End If
            Else
                cekpembelian = cekpembelian + 0
            End If

            'penjualan
            If clbpenjualan.GetItemChecked(i) Then
                If clbpenjualan.Items(i).Equals("Tambah") Then
                    cekpenjualan = cekpenjualan + 1
                ElseIf clbpenjualan.Items(i).Equals("Edit") Then
                    cekpenjualan = cekpenjualan + 3
                ElseIf clbpenjualan.Items(i).Equals("Print") Then
                    cekpenjualan = cekpenjualan + 5
                End If
            Else
                cekpenjualan = cekpenjualan + 0
            End If

            'returbeli
            If clbreturbeli.GetItemChecked(i) Then
                If clbreturbeli.Items(i).Equals("Tambah") Then
                    cekreturbeli = cekreturbeli + 1
                ElseIf clbreturbeli.Items(i).Equals("Edit") Then
                    cekreturbeli = cekreturbeli + 3
                ElseIf clbreturbeli.Items(i).Equals("Print") Then
                    cekreturbeli = cekreturbeli + 5
                End If
            Else
                cekreturbeli = cekreturbeli + 0
            End If

            'returjual
            If clbreturjual.GetItemChecked(i) Then
                If clbreturjual.Items(i).Equals("Tambah") Then
                    cekreturjual = cekreturjual + 1
                ElseIf clbreturjual.Items(i).Equals("Edit") Then
                    cekreturjual = cekreturjual + 3
                ElseIf clbreturjual.Items(i).Equals("Print") Then
                    cekreturjual = cekreturjual + 5
                End If
            Else
                cekreturjual = cekreturjual + 0
            End If

            'barangmasuk
            If clbbarangmasuk.GetItemChecked(i) Then
                If clbbarangmasuk.Items(i).Equals("Tambah") Then
                    cekbarangmasuk = cekbarangmasuk + 1
                ElseIf clbbarangmasuk.Items(i).Equals("Edit") Then
                    cekbarangmasuk = cekbarangmasuk + 3
                ElseIf clbbarangmasuk.Items(i).Equals("Print") Then
                    cekbarangmasuk = cekbarangmasuk + 5
                End If
            Else
                cekbarangmasuk = cekbarangmasuk + 0
            End If

            'barangkeluar
            If clbbarangkeluar.GetItemChecked(i) Then
                If clbbarangkeluar.Items(i).Equals("Tambah") Then
                    cekbarangkeluar = cekbarangkeluar + 1
                ElseIf clbbarangkeluar.Items(i).Equals("Edit") Then
                    cekbarangkeluar = cekbarangkeluar + 3
                ElseIf clbbarangkeluar.Items(i).Equals("Print") Then
                    cekbarangkeluar = cekbarangkeluar + 5
                End If
            Else
                cekbarangkeluar = cekbarangkeluar + 0
            End If

            'transferbarang
            If clbtransferbarang.GetItemChecked(i) Then
                If clbtransferbarang.Items(i).Equals("Tambah") Then
                    cektransferbarang = cektransferbarang + 1
                ElseIf clbtransferbarang.Items(i).Equals("Edit") Then
                    cektransferbarang = cektransferbarang + 3
                ElseIf clbtransferbarang.Items(i).Equals("Print") Then
                    cektransferbarang = cektransferbarang + 5
                End If
            Else
                cektransferbarang = cektransferbarang + 0
            End If

            'administrasi ===
            'lunasutang
            If clblunasutang.GetItemChecked(i) Then
                If clblunasutang.Items(i).Equals("Tambah") Then
                    ceklunasutang = ceklunasutang + 1
                ElseIf clblunasutang.Items(i).Equals("Edit") Then
                    ceklunasutang = ceklunasutang + 3
                ElseIf clblunasutang.Items(i).Equals("Print") Then
                    ceklunasutang = ceklunasutang + 5
                End If
            Else
                ceklunasutang = ceklunasutang + 0
            End If

            'lunaspiutang
            If clblunaspiutang.GetItemChecked(i) Then
                If clblunaspiutang.Items(i).Equals("Tambah") Then
                    ceklunaspiutang = ceklunaspiutang + 1
                ElseIf clblunaspiutang.Items(i).Equals("Edit") Then
                    ceklunaspiutang = ceklunaspiutang + 3
                ElseIf clblunaspiutang.Items(i).Equals("Print") Then
                    ceklunaspiutang = ceklunaspiutang + 5
                End If
            Else
                ceklunaspiutang = ceklunaspiutang + 0
            End If

            'transferkas
            If clbtransferkas.GetItemChecked(i) Then
                If clbtransferkas.Items(i).Equals("Tambah") Then
                    cektransferkas = cektransferkas + 1
                ElseIf clbtransferkas.Items(i).Equals("Edit") Then
                    cektransferkas = cektransferkas + 3
                ElseIf clbtransferkas.Items(i).Equals("Print") Then
                    cektransferkas = cektransferkas + 5
                End If
            Else
                cektransferkas = cektransferkas + 0
            End If

            'akunmasuk
            If clbakunmasuk.GetItemChecked(i) Then
                If clbakunmasuk.Items(i).Equals("Tambah") Then
                    cekakunmasuk = cekakunmasuk + 1
                ElseIf clbakunmasuk.Items(i).Equals("Edit") Then
                    cekakunmasuk = cekakunmasuk + 3
                ElseIf clbakunmasuk.Items(i).Equals("Print") Then
                    cekakunmasuk = cekakunmasuk + 5
                End If
            Else
                cekakunmasuk = cekakunmasuk + 0
            End If

            'akunkeluar
            If clbakunkeluar.GetItemChecked(i) Then
                If clbakunkeluar.Items(i).Equals("Tambah") Then
                    cekakunkeluar = cekakunkeluar + 1
                ElseIf clbakunkeluar.Items(i).Equals("Edit") Then
                    cekakunkeluar = cekakunkeluar + 3
                ElseIf clbakunkeluar.Items(i).Equals("Print") Then
                    cekakunkeluar = cekakunkeluar + 5
                End If
            Else
                cekakunkeluar = cekakunkeluar + 0
            End If

            'laporan ===
            If i <= 1 Then
                'lappricelist
                If clblappricelist.GetItemChecked(i) Then
                    If clblappricelist.Items(i).Equals("Print") Then
                        ceklappricelist = ceklappricelist + 1
                    ElseIf clblappricelist.Items(i).Equals("Export") Then
                        ceklappricelist = ceklappricelist + 3
                    End If
                Else
                    ceklappricelist = ceklappricelist + 0
                End If
                'lappembelian
                If clblappembelian.GetItemChecked(i) Then
                    If clblappembelian.Items(i).Equals("Print") Then
                        ceklappembelian = ceklappembelian + 1
                    ElseIf clblappembelian.Items(i).Equals("Export") Then
                        ceklappembelian = ceklappembelian + 3
                    End If
                Else
                    ceklappembelian = ceklappembelian + 0
                End If
                'lappenjualan
                If clblappenjualan.GetItemChecked(i) Then
                    If clblappenjualan.Items(i).Equals("Print") Then
                        ceklappenjualan = ceklappenjualan + 1
                    ElseIf clblappenjualan.Items(i).Equals("Export") Then
                        ceklappenjualan = ceklappenjualan + 3
                    End If
                Else
                    ceklappenjualan = ceklappenjualan + 0
                End If

                'lapreturbeli
                If clblapreturbeli.GetItemChecked(i) Then
                    If clblapreturbeli.Items(i).Equals("Print") Then
                        ceklapreturbeli = ceklapreturbeli + 1
                    ElseIf clblapreturbeli.Items(i).Equals("Export") Then
                        ceklapreturbeli = ceklapreturbeli + 3
                    End If
                Else
                    ceklapreturbeli = ceklapreturbeli + 0
                End If

                'lapreturjual
                If clblapreturjual.GetItemChecked(i) Then
                    If clblapreturjual.Items(i).Equals("Print") Then
                        ceklapreturjual = ceklapreturjual + 1
                    ElseIf clblapreturjual.Items(i).Equals("Export") Then
                        ceklapreturjual = ceklapreturjual + 3
                    End If
                Else
                    ceklapreturjual = ceklapreturjual + 0
                End If

                'lapbarangmasuk
                If clblapbarangmasuk.GetItemChecked(i) Then
                    If clblapbarangmasuk.Items(i).Equals("Print") Then
                        ceklapbarangmasuk = ceklapbarangmasuk + 1
                    ElseIf clblapbarangmasuk.Items(i).Equals("Export") Then
                        ceklapbarangmasuk = ceklapbarangmasuk + 3
                    End If
                Else
                    ceklapbarangmasuk = ceklapbarangmasuk + 0
                End If

                'lapbarangkeluar
                If clblapbarangkeluar.GetItemChecked(i) Then
                    If clblapbarangkeluar.Items(i).Equals("Print") Then
                        ceklapbarangkeluar = ceklapbarangkeluar + 1
                    ElseIf clblapbarangkeluar.Items(i).Equals("Export") Then
                        ceklapbarangkeluar = ceklapbarangkeluar + 3
                    End If
                Else
                    ceklapbarangkeluar = ceklapbarangkeluar + 0
                End If

                'laputang
                If clblaputang.GetItemChecked(i) Then
                    If clblaputang.Items(i).Equals("Print") Then
                        ceklaputang = ceklaputang + 1
                    ElseIf clblaputang.Items(i).Equals("Export") Then
                        ceklaputang = ceklaputang + 3
                    End If
                Else
                    ceklaputang = ceklaputang + 0
                End If

                'lappiutang
                If clblappiutang.GetItemChecked(i) Then
                    If clblappiutang.Items(i).Equals("Print") Then
                        ceklappiutang = ceklappiutang + 1
                    ElseIf clblappiutang.Items(i).Equals("Export") Then
                        ceklappiutang = ceklappiutang + 3
                    End If
                Else
                    ceklappiutang = ceklappiutang + 0
                End If

                'lapstokbarang
                If clblapstokbarang.GetItemChecked(i) Then
                    If clblapstokbarang.Items(i).Equals("Print") Then
                        ceklapstokbarang = ceklapstokbarang + 1
                    ElseIf clblapstokbarang.Items(i).Equals("Export") Then
                        ceklapstokbarang = ceklapstokbarang + 3
                    End If
                Else
                    ceklapstokbarang = ceklapstokbarang + 0
                End If

                'lapakunmasuk
                If clblapakunmasuk.GetItemChecked(i) Then
                    If clblapakunmasuk.Items(i).Equals("Print") Then
                        ceklapakunmasuk = ceklapakunmasuk + 1
                    ElseIf clblapakunmasuk.Items(i).Equals("Export") Then
                        ceklapakunmasuk = ceklapakunmasuk + 3
                    End If
                Else
                    ceklapakunmasuk = ceklapakunmasuk + 0
                End If

                'lapakunkeluar
                If clblapakunkeluar.GetItemChecked(i) Then
                    If clblapakunkeluar.Items(i).Equals("Print") Then
                        ceklapakunkeluar = ceklapakunkeluar + 1
                    ElseIf clblapakunkeluar.Items(i).Equals("Export") Then
                        ceklapakunkeluar = ceklapakunkeluar + 3
                    End If
                Else
                    ceklapakunkeluar = ceklapakunkeluar + 0
                End If

                'laptransferkas
                If clblaptransferkas.GetItemChecked(i) Then
                    If clblaptransferkas.Items(i).Equals("Print") Then
                        ceklaptransferkas = ceklaptransferkas + 1
                    ElseIf clblaptransferkas.Items(i).Equals("Export") Then
                        ceklaptransferkas = ceklaptransferkas + 3
                    End If
                Else
                    ceklaptransferkas = ceklaptransferkas + 0
                End If

                'laptransferbarang
                If clblaptransferbarang.GetItemChecked(i) Then
                    If clblaptransferbarang.Items(i).Equals("Print") Then
                        ceklaptransferbarang = ceklaptransferbarang + 1
                    ElseIf clblaptransferbarang.Items(i).Equals("Export") Then
                        ceklaptransferbarang = ceklaptransferbarang + 3
                    End If
                Else
                    ceklaptransferbarang = ceklaptransferbarang + 0
                End If

                'laptransaksikas
                If clblaptransaksikas.GetItemChecked(i) Then
                    If clblaptransaksikas.Items(i).Equals("Print") Then
                        ceklaptransaksikas = ceklaptransaksikas + 1
                    ElseIf clblaptransaksikas.Items(i).Equals("Export") Then
                        ceklaptransaksikas = ceklaptransaksikas + 3
                    End If
                Else
                    ceklaptransaksikas = ceklaptransaksikas + 0
                End If
            End If

        Next


        'laporan
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode User Sudah ada dengan nama " + dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            Call aksesadmin()
            sql = "INSERT INTO tb_user (kode_user, nama_user, password_user, jabatan_user, email_user, telepon_user, alamat_user, keterangan_user, 
                    master_barang, master_kategori, master_gudang, master_customer, master_supplier, master_user, master_kas, master_pricelist, master_rek_supp, master_rek_cust, 
                    pembelian, penjualan, retur_beli, retur_jual, barang_masuk, barang_keluar, transfer_barang,
                    lunas_utang, lunas_piutang, transfer_kas, akun_masuk, akun_keluar, 
                    lap_pricelist, lap_pembelian, lap_penjualan, lap_returbeli, lap_returjual, lap_barang_masuk, lap_barang_keluar, lap_utang, lap_piutang, lap_stok_barang, lap_akun_masuk, lap_akun_keluar, lap_transfer_kas, lap_transfer_barang, lap_transaksi_kas,
                    created_by, updated_by,date_created, last_updated) 
                    VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & cmbjabatan.Text & "', '" & txtemail.Text & "', '" & txttelp.Text & "','" & txtalamat.Text & "','" & txtketerangan.Text & "',
                    '" & cekmasterbarang & "','" & cekmasterkategori & "','" & cekmastergudang & "','" & cekmastercustomer & "','" & cekmastersupplier & "','" & cekmasteruser & "','" & cekmasterkas & "','" & cekmasterpricelist & "','" & cekmasterreksupp & "','" & cekmasterrekcust & "',
                    '" & cekpembelian & "','" & cekpenjualan & "','" & cekreturbeli & "','" & cekreturjual & "','" & cekbarangmasuk & "','" & cekbarangkeluar & "','" & cektransferbarang & "',
                    '" & ceklunasutang & "','" & ceklunaspiutang & "','" & cektransferkas & "','" & cekakunmasuk & "','" & cekakunkeluar & "',
                    '" & ceklappricelist & "','" & ceklappembelian & "','" & ceklappenjualan & "','" & ceklapreturbeli & "','" & ceklapreturjual & "','" & ceklapbarangmasuk & "','" & ceklapbarangkeluar & "','" & ceklaputang & "','" & ceklappiutang & "','" & ceklapstokbarang & "','" & ceklapakunmasuk & "','" & ceklapakunkeluar & "','" & ceklaptransferkas & "','" & ceklaptransferbarang & "','" & ceklaptransaksikas & "',
                    '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If
        'Call koneksii()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Simpan"
                btnhapus.Enabled = False
                Call enable_text_edit()
                Call index()
                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtpassword.Text.Length = 0 Then
                            MsgBox("Password belum terisi !")
                        Else
                            If cmbjabatan.Text.Length = 0 Then
                                MsgBox("Jabatan belum terisi !")
                            Else
                                If txtemail.Text.Length = 0 Then
                                    MsgBox("Email belum terisi !")
                                Else
                                    If txttelp.Text.Length = 0 Then
                                        MsgBox("Telepon belum terisi !")
                                    Else
                                        If txtalamat.Text.Length = 0 Then
                                            MsgBox("Alamat belum terisi !")
                                        Else
                                            Call edit()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub edit()
        If txtkode.Text.Equals(kode) Then
            Call aksesadmin()
            Using cnn As New OdbcConnection(strConn)
                sql = "UPDATE tb_user SET nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, master_kategori=?, master_gudang=?, master_customer=?, master_supplier=?, master_user=?, master_kas=?, master_pricelist=?, master_rek_supp=?, master_rek_cust=?,
                        pembelian=? ,penjualan=?, retur_beli=?, retur_jual=?, barang_masuk=?, barang_keluar=?, transfer_barang=?,
                        lunas_utang=?, lunas_piutang=?, transfer_kas=?, akun_masuk=?, akun_keluar=?, 
                        lap_pricelist=?, lap_pembelian=?, lap_penjualan=?, lap_returbeli=?, lap_returjual=?, lap_barang_masuk=?, lap_barang_keluar=?, lap_utang=?, lap_piutang=?, lap_stok_barang=?, lap_akun_masuk=?, lap_akun_keluar=?, lap_transfer_kas=?, lap_transfer_barang=?, lap_transaksi_kas=?,
                        updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
                cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
                cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
                cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
                cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
                cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
                'akses 
                'master
                cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
                cmmd.Parameters.AddWithValue("@master_kategori", cekmasterkategori)
                cmmd.Parameters.AddWithValue("@master_gudang", cekmastergudang)
                cmmd.Parameters.AddWithValue("@master_customer", cekmastercustomer)
                cmmd.Parameters.AddWithValue("@master_supplier", cekmastersupplier)
                cmmd.Parameters.AddWithValue("@master_user", cekmasteruser)
                cmmd.Parameters.AddWithValue("@master_kas", cekmasterkas)
                cmmd.Parameters.AddWithValue("@master_pricelist", cekmasterpricelist)
                cmmd.Parameters.AddWithValue("@master_rek_supp", cekmasterreksupp)
                cmmd.Parameters.AddWithValue("@master_rek_cust", cekmasterrekcust)
                'transaksi
                cmmd.Parameters.AddWithValue("@pembelian", cekpembelian)
                cmmd.Parameters.AddWithValue("@penjualan", cekpenjualan)
                cmmd.Parameters.AddWithValue("@retur_beli", cekreturbeli)
                cmmd.Parameters.AddWithValue("@retur_jual", cekreturjual)
                cmmd.Parameters.AddWithValue("@barang_masuk", cekbarangmasuk)
                cmmd.Parameters.AddWithValue("@barang_keluar", cekbarangkeluar)
                cmmd.Parameters.AddWithValue("@transfer_barang", cektransferbarang)
                'administrasi
                cmmd.Parameters.AddWithValue("@lunas_utang", ceklunasutang)
                cmmd.Parameters.AddWithValue("@lunas_piutang", ceklunaspiutang)
                cmmd.Parameters.AddWithValue("@transfer_kas", cektransferkas)
                cmmd.Parameters.AddWithValue("@akun_masuk", cekakunmasuk)
                cmmd.Parameters.AddWithValue("@akun_keluar", cekakunkeluar)
                'laporan
                cmmd.Parameters.AddWithValue("@lap_pricelist", ceklappricelist)
                cmmd.Parameters.AddWithValue("@lap_pembelian", ceklappembelian)
                cmmd.Parameters.AddWithValue("@lap_penjualan", ceklappenjualan)
                cmmd.Parameters.AddWithValue("@lap_returbeli", ceklapreturbeli)
                cmmd.Parameters.AddWithValue("@lap_returjual", ceklapreturjual)
                cmmd.Parameters.AddWithValue("@lap_barang_masuk", ceklapbarangmasuk)
                cmmd.Parameters.AddWithValue("@lap_barang_keluar", ceklapbarangkeluar)
                cmmd.Parameters.AddWithValue("@lap_utang", ceklaputang)
                cmmd.Parameters.AddWithValue("@lap_piutang", ceklappiutang)
                cmmd.Parameters.AddWithValue("@lap_stok_barang", ceklapstokbarang)
                cmmd.Parameters.AddWithValue("@lap_akun_masuk", ceklapakunmasuk)
                cmmd.Parameters.AddWithValue("@lap_akun_keluar", ceklapakunkeluar)
                cmmd.Parameters.AddWithValue("@lap_transfer_kas", ceklaptransferkas)
                cmmd.Parameters.AddWithValue("@lap_transfer_barang", ceklaptransferbarang)
                cmmd.Parameters.AddWithValue("@lap_transaksi_kas", ceklaptransaksikas)
                ' end akses
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cnn.Open()
                cmmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Data Terupdate", MsgBoxStyle.Information, "Berhasil")
                btnedit.Text = "Edit"
                cnn.Close()
                Me.Refresh()
                Call awal()
            End Using
        Else

            sql = "SELECT * FROM tb_user WHERE kode_user  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode User Sudah ada dengan nama " + dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
            Else
                Call aksesadmin()
                Using cnn As New OdbcConnection(strConn)
                    sql = "UPDATE tb_user SET kode_user=?, nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, master_kategori=?, master_gudang=?, master_customer=?, master_supplier=?, master_user=?, master_kas=?, master_pricelist=?, master_rek_supp=?, master_rek_cust=?, 
                            pembelian=?, penjualan=?, retur_beli=?, retur_jual=?, barang_masuk=?, barang_keluar=?, transfer_barang=?,
                            lunas_utang=?, lunas_piutang=?, transfer_kas=?, akun_masuk=?, akun_keluar=?,
                            lap_pricelist=?, lap_pembelian=?, lap_penjualan=?, lap_returbeli=?, lap_returjual=?, lap_barang_masuk=?, lap_barang_keluar=?, lap_utang=?, lap_piutang=?, lap_stok_barang=?, lap_akun_masuk=?, lap_akun_keluar=?, lap_transfer_kas=?, lap_transfer_barang=?, lap_transaksi_kas=?,
                            updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    cmmd.Parameters.AddWithValue("@kode_user", txtkode.Text)
                    cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
                    cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
                    cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
                    cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
                    cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
                    cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
                    cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
                    'akses 
                    'master
                    cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
                    cmmd.Parameters.AddWithValue("@master_kategori", cekmasterkategori)
                    cmmd.Parameters.AddWithValue("@master_gudang", cekmastergudang)
                    cmmd.Parameters.AddWithValue("@master_customer", cekmastercustomer)
                    cmmd.Parameters.AddWithValue("@master_supplier", cekmastersupplier)
                    cmmd.Parameters.AddWithValue("@master_user", cekmasteruser)
                    cmmd.Parameters.AddWithValue("@master_kas", cekmasterkas)
                    cmmd.Parameters.AddWithValue("@master_pricelist", cekmasterpricelist)
                    cmmd.Parameters.AddWithValue("@master_rek_supp", cekmasterreksupp)
                    cmmd.Parameters.AddWithValue("@master_rek_cust", cekmastercustomer)
                    'transaksi
                    cmmd.Parameters.AddWithValue("@pembelian", cekpembelian)
                    cmmd.Parameters.AddWithValue("@penjualan", cekpenjualan)
                    cmmd.Parameters.AddWithValue("@retur_beli", cekreturbeli)
                    cmmd.Parameters.AddWithValue("@retur_jual", cekreturjual)
                    cmmd.Parameters.AddWithValue("@barang_masuk", cekbarangmasuk)
                    cmmd.Parameters.AddWithValue("@barang_keluar", cekbarangkeluar)
                    cmmd.Parameters.AddWithValue("@transfer_barang", cektransferbarang)
                    'administrasi
                    cmmd.Parameters.AddWithValue("@lunas_utang", ceklunasutang)
                    cmmd.Parameters.AddWithValue("@lunas_piutang", ceklunaspiutang)
                    cmmd.Parameters.AddWithValue("@transfer_kas", cektransferkas)
                    cmmd.Parameters.AddWithValue("@akun_masuk", cekakunmasuk)
                    cmmd.Parameters.AddWithValue("@akun_keluar", cekakunkeluar)
                    'laporan
                    cmmd.Parameters.AddWithValue("@lap_pricelist", ceklappricelist)
                    cmmd.Parameters.AddWithValue("@lap_pembelian", ceklappembelian)
                    cmmd.Parameters.AddWithValue("@lap_penjualan", ceklappenjualan)
                    cmmd.Parameters.AddWithValue("@lap_returbeli", ceklapreturbeli)
                    cmmd.Parameters.AddWithValue("@lap_returjual", ceklapreturjual)
                    cmmd.Parameters.AddWithValue("@lap_barang_masuk", ceklapbarangmasuk)
                    cmmd.Parameters.AddWithValue("@lap_barang_keluar", ceklapbarangkeluar)
                    cmmd.Parameters.AddWithValue("@lap_utang", ceklaputang)
                    cmmd.Parameters.AddWithValue("@lap_piutang", ceklappiutang)
                    cmmd.Parameters.AddWithValue("@lap_stok_barang", ceklapstokbarang)
                    cmmd.Parameters.AddWithValue("@lap_akun_masuk", ceklapakunmasuk)
                    cmmd.Parameters.AddWithValue("@lap_akun_keluar", ceklapakunkeluar)
                    cmmd.Parameters.AddWithValue("@lap_transfer_kas", ceklaptransferkas)
                    cmmd.Parameters.AddWithValue("@lap_transfer_barang", ceklaptransferbarang)
                    cmmd.Parameters.AddWithValue("@lap_transaksi_kas", ceklaptransaksikas)
                    ' end akses
                    cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                    cmmd.Parameters.AddWithValue("@last_updated", Date.Now)

                    cnn.Open()
                    cmmd.ExecuteNonQuery()
                    cnn.Close()
                    MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
                    btnedit.Text = "Edit"
                    cnn.Close()
                    Me.Refresh()
                    Call awal()
                End Using
            End If
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_user WHERE  kode_user='" & txtkode.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Refresh()
                Call awal()
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Sub cari()

        txtkode.Text = GridView.GetFocusedRowCellValue("kode_user")
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_user WHERE kode_user  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                kode = dr("kode_user")
                txtnama.Text = dr("nama_user")
                txtpassword.Text = dr("password_user")
                cmbjabatan.Text = dr("jabatan_user")
                txtemail.Text = dr("email_user")
                txttelp.Text = dr("telepon_user")
                txtalamat.Text = dr("alamat_user")
                txtketerangan.Text = dr("keterangan_user")

                'akses user
                'master
                aksesbarang = Val(dr("master_barang"))
                akseskategori = Val(dr("master_kategori"))
                aksesgudang = Val(dr("master_gudang"))
                aksescustomer = Val(dr("master_customer"))
                aksessupplier = Val(dr("master_supplier"))
                aksesuser = Val(dr("master_user"))
                akseskas = Val(dr("master_kas"))
                aksespricelist = Val(dr("master_pricelist"))
                aksesreksupp = Val(dr("master_rek_supp"))
                aksesrekcust = Val(dr("master_rek_cust"))

                'transaksi
                aksespembelian = Val(dr("pembelian"))
                aksespenjualan = Val(dr("penjualan"))
                aksesreturbeli = Val(dr("retur_beli"))
                aksesreturjual = Val(dr("retur_jual"))
                aksesbarangmasuk = Val(dr("barang_masuk"))
                aksesbarangkeluar = Val(dr("barang_keluar"))
                aksestransferbarang = Val(dr("transfer_barang"))

                'administrasi
                akseslunasutang = Val(dr("lunas_utang"))
                akseslunaspiutang = Val(dr("lunas_piutang"))
                aksestransferkas = Val(dr("transfer_kas"))
                aksesakunmasuk = Val(dr("akun_masuk"))
                aksesakunkeluar = Val(dr("akun_keluar"))

                'laporan
                akseslappricelist = Val(dr("lap_pricelist"))
                akseslappembelian = Val(dr("lap_pembelian"))
                akseslappenjualan = Val(dr("lap_penjualan"))
                akseslapreturbeli = Val(dr("lap_returbeli"))
                akseslapreturjual = Val(dr("lap_returjual"))
                akseslapbarangmasuk = Val(dr("lap_barang_masuk"))
                akseslapbarangkeluar = Val(dr("lap_barang_keluar"))
                akseslaputang = Val(dr("lap_utang"))
                akseslappiutang = Val(dr("lap_piutang"))
                akseslapstokbarang = Val(dr("lap_stok_barang"))
                akseslapakunmasuk = Val(dr("lap_akun_masuk"))
                akseslapakunkeluar = Val(dr("lap_akun_keluar"))
                akseslaptransferkas = Val(dr("lap_transfer_kas"))
                akseslaptransferbarang = Val(dr("lap_transfer_barang"))
                akseslaptransaksikas = Val(dr("lap_transaksi_kas"))

                '== mulai case ==
                'master
                Select Case akseskategori
                    Case 0
                        cbmasterkategori.Checked = False
                        clbmasterkategori.SetItemChecked(0, False)
                        clbmasterkategori.SetItemChecked(1, False)
                        clbmasterkategori.SetItemChecked(2, False)
                    Case 1
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, True)
                        clbmasterkategori.SetItemChecked(1, False)
                        clbmasterkategori.SetItemChecked(2, False)
                    Case 3
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, False)
                        clbmasterkategori.SetItemChecked(1, True)
                        clbmasterkategori.SetItemChecked(2, False)
                    Case 5
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, False)
                        clbmasterkategori.SetItemChecked(1, False)
                        clbmasterkategori.SetItemChecked(2, True)
                    Case 4
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, True)
                        clbmasterkategori.SetItemChecked(1, True)
                        clbmasterkategori.SetItemChecked(2, False)
                    Case 6
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, True)
                        clbmasterkategori.SetItemChecked(1, False)
                        clbmasterkategori.SetItemChecked(2, True)
                    Case 8
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, False)
                        clbmasterkategori.SetItemChecked(1, True)
                        clbmasterkategori.SetItemChecked(2, True)
                    Case 9
                        cbmasterkategori.Checked = True
                        clbmasterkategori.SetItemChecked(0, True)
                        clbmasterkategori.SetItemChecked(1, True)
                        clbmasterkategori.SetItemChecked(2, True)
                End Select

                Select Case aksesbarang
                    Case 0
                        cbmasterbarang.Checked = False
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 1
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 3
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 5
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 4
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 6
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 8
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 9
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, True)
                End Select

                Select Case aksesgudang
                    Case 0
                        cbmastergudang.Checked = False
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 1
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 3
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 5
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 4
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 6
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 8
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 9
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, True)
                End Select

                Select Case aksescustomer
                    Case 0
                        cbmastercustomer.Checked = False
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 1
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 3
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 5
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 4
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 6
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 8
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 9
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, True)
                End Select

                Select Case aksessupplier
                    Case 0
                        cbmastersupplier.Checked = False
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 1
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 3
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 5
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 4
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 6
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 8
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 9
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, True)
                End Select

                Select Case aksesuser
                    Case 0
                        cbmasteruser.Checked = False
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 1
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 3
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 5
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 4
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 6
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 8
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 9
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, True)
                End Select

                Select Case akseskas
                    Case 0
                        cbmasterkas.Checked = False
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 1
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 3
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 5
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 4
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 6
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 8
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 9
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, True)
                End Select

                Select Case aksespricelist
                    Case 0
                        cbmasterpricelist.Checked = False
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 1
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 3
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 5
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 4
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 6
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 8
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 9
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, True)
                End Select

                Select Case aksesreksupp
                    Case 0
                        cbmasterreksupp.Checked = False
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 1
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 3
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 5
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 4
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 6
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 8
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 9
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, True)
                End Select

                Select Case aksesrekcust
                    Case 0
                        cbmasterrekcust.Checked = False
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 1
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 3
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 5
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 4
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 6
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 8
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 9
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, True)
                End Select

                'transaksi
                Select Case aksespembelian
                    Case 0
                        cbpembelian.Checked = False
                        clbpembelian.SetItemChecked(0, False)
                        clbpembelian.SetItemChecked(1, False)
                        clbpembelian.SetItemChecked(2, False)
                    Case 1
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, True)
                        clbpembelian.SetItemChecked(1, False)
                        clbpembelian.SetItemChecked(2, False)
                    Case 3
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, False)
                        clbpembelian.SetItemChecked(1, True)
                        clbpembelian.SetItemChecked(2, False)
                    Case 5
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, False)
                        clbpembelian.SetItemChecked(1, False)
                        clbpembelian.SetItemChecked(2, True)
                    Case 4
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, True)
                        clbpembelian.SetItemChecked(1, True)
                        clbpembelian.SetItemChecked(2, False)
                    Case 6
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, True)
                        clbpembelian.SetItemChecked(1, False)
                        clbpembelian.SetItemChecked(2, True)
                    Case 8
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, False)
                        clbpembelian.SetItemChecked(1, True)
                        clbpembelian.SetItemChecked(2, True)
                    Case 9
                        cbpembelian.Checked = True
                        clbpembelian.SetItemChecked(0, True)
                        clbpembelian.SetItemChecked(1, True)
                        clbpembelian.SetItemChecked(2, True)
                End Select

                Select Case aksespenjualan
                    Case 0
                        cbpenjualan.Checked = False
                        clbpenjualan.SetItemChecked(0, False)
                        clbpenjualan.SetItemChecked(1, False)
                        clbpenjualan.SetItemChecked(2, False)
                    Case 1
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, True)
                        clbpenjualan.SetItemChecked(1, False)
                        clbpenjualan.SetItemChecked(2, False)
                    Case 3
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, False)
                        clbpenjualan.SetItemChecked(1, True)
                        clbpenjualan.SetItemChecked(2, False)
                    Case 5
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, False)
                        clbpenjualan.SetItemChecked(1, False)
                        clbpenjualan.SetItemChecked(2, True)
                    Case 4
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, True)
                        clbpenjualan.SetItemChecked(1, True)
                        clbpenjualan.SetItemChecked(2, False)
                    Case 6
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, True)
                        clbpenjualan.SetItemChecked(1, False)
                        clbpenjualan.SetItemChecked(2, True)
                    Case 8
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, False)
                        clbpenjualan.SetItemChecked(1, True)
                        clbpenjualan.SetItemChecked(2, True)
                    Case 9
                        cbpenjualan.Checked = True
                        clbpenjualan.SetItemChecked(0, True)
                        clbpenjualan.SetItemChecked(1, True)
                        clbpenjualan.SetItemChecked(2, True)
                End Select

                Select Case aksesreturbeli
                    Case 0
                        cbreturbeli.Checked = False
                        clbreturbeli.SetItemChecked(0, False)
                        clbreturbeli.SetItemChecked(1, False)
                        clbreturbeli.SetItemChecked(2, False)
                    Case 1
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, True)
                        clbreturbeli.SetItemChecked(1, False)
                        clbreturbeli.SetItemChecked(2, False)
                    Case 3
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, False)
                        clbreturbeli.SetItemChecked(1, True)
                        clbreturbeli.SetItemChecked(2, False)
                    Case 5
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, False)
                        clbreturbeli.SetItemChecked(1, False)
                        clbreturbeli.SetItemChecked(2, True)
                    Case 4
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, True)
                        clbreturbeli.SetItemChecked(1, True)
                        clbreturbeli.SetItemChecked(2, False)
                    Case 6
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, True)
                        clbreturbeli.SetItemChecked(1, False)
                        clbreturbeli.SetItemChecked(2, True)
                    Case 8
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, False)
                        clbreturbeli.SetItemChecked(1, True)
                        clbreturbeli.SetItemChecked(2, True)
                    Case 9
                        cbreturbeli.Checked = True
                        clbreturbeli.SetItemChecked(0, True)
                        clbreturbeli.SetItemChecked(1, True)
                        clbreturbeli.SetItemChecked(2, True)
                End Select

                Select Case aksesreturjual
                    Case 0
                        cbreturjual.Checked = False
                        clbreturjual.SetItemChecked(0, False)
                        clbreturjual.SetItemChecked(1, False)
                        clbreturjual.SetItemChecked(2, False)
                    Case 1
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, True)
                        clbreturjual.SetItemChecked(1, False)
                        clbreturjual.SetItemChecked(2, False)
                    Case 3
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, False)
                        clbreturjual.SetItemChecked(1, True)
                        clbreturjual.SetItemChecked(2, False)
                    Case 5
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, False)
                        clbreturjual.SetItemChecked(1, False)
                        clbreturjual.SetItemChecked(2, True)
                    Case 4
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, True)
                        clbreturjual.SetItemChecked(1, True)
                        clbreturjual.SetItemChecked(2, False)
                    Case 6
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, True)
                        clbreturjual.SetItemChecked(1, False)
                        clbreturjual.SetItemChecked(2, True)
                    Case 8
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, False)
                        clbreturjual.SetItemChecked(1, True)
                        clbreturjual.SetItemChecked(2, True)
                    Case 9
                        cbreturjual.Checked = True
                        clbreturjual.SetItemChecked(0, True)
                        clbreturjual.SetItemChecked(1, True)
                        clbreturjual.SetItemChecked(2, True)
                End Select

                Select Case aksesbarangmasuk
                    Case 0
                        cbbarangmasuk.Checked = False
                        clbbarangmasuk.SetItemChecked(0, False)
                        clbbarangmasuk.SetItemChecked(1, False)
                        clbbarangmasuk.SetItemChecked(2, False)
                    Case 1
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, True)
                        clbbarangmasuk.SetItemChecked(1, False)
                        clbbarangmasuk.SetItemChecked(2, False)
                    Case 3
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, False)
                        clbbarangmasuk.SetItemChecked(1, True)
                        clbbarangmasuk.SetItemChecked(2, False)
                    Case 5
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, False)
                        clbbarangmasuk.SetItemChecked(1, False)
                        clbbarangmasuk.SetItemChecked(2, True)
                    Case 4
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, True)
                        clbbarangmasuk.SetItemChecked(1, True)
                        clbbarangmasuk.SetItemChecked(2, False)
                    Case 6
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, True)
                        clbbarangmasuk.SetItemChecked(1, False)
                        clbbarangmasuk.SetItemChecked(2, True)
                    Case 8
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, False)
                        clbbarangmasuk.SetItemChecked(1, True)
                        clbbarangmasuk.SetItemChecked(2, True)
                    Case 9
                        cbbarangmasuk.Checked = True
                        clbbarangmasuk.SetItemChecked(0, True)
                        clbbarangmasuk.SetItemChecked(1, True)
                        clbbarangmasuk.SetItemChecked(2, True)
                End Select

                Select Case aksesbarangkeluar
                    Case 0
                        cbbarangkeluar.Checked = False
                        clbbarangkeluar.SetItemChecked(0, False)
                        clbbarangkeluar.SetItemChecked(1, False)
                        clbbarangkeluar.SetItemChecked(2, False)
                    Case 1
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, True)
                        clbbarangkeluar.SetItemChecked(1, False)
                        clbbarangkeluar.SetItemChecked(2, False)
                    Case 3
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, False)
                        clbbarangkeluar.SetItemChecked(1, True)
                        clbbarangkeluar.SetItemChecked(2, False)
                    Case 5
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, False)
                        clbbarangkeluar.SetItemChecked(1, False)
                        clbbarangkeluar.SetItemChecked(2, True)
                    Case 4
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, True)
                        clbbarangkeluar.SetItemChecked(1, True)
                        clbbarangkeluar.SetItemChecked(2, False)
                    Case 6
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, True)
                        clbbarangkeluar.SetItemChecked(1, False)
                        clbbarangkeluar.SetItemChecked(2, True)
                    Case 8
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, False)
                        clbbarangkeluar.SetItemChecked(1, True)
                        clbbarangkeluar.SetItemChecked(2, True)
                    Case 9
                        cbbarangkeluar.Checked = True
                        clbbarangkeluar.SetItemChecked(0, True)
                        clbbarangkeluar.SetItemChecked(1, True)
                        clbbarangkeluar.SetItemChecked(2, True)
                End Select

                Select Case aksestransferbarang
                    Case 0
                        cbtransferbarang.Checked = False
                        clbtransferbarang.SetItemChecked(0, False)
                        clbtransferbarang.SetItemChecked(1, False)
                        clbtransferbarang.SetItemChecked(2, False)
                    Case 1
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, True)
                        clbtransferbarang.SetItemChecked(1, False)
                        clbtransferbarang.SetItemChecked(2, False)
                    Case 3
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, False)
                        clbtransferbarang.SetItemChecked(1, True)
                        clbtransferbarang.SetItemChecked(2, False)
                    Case 5
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, False)
                        clbtransferbarang.SetItemChecked(1, False)
                        clbtransferbarang.SetItemChecked(2, True)
                    Case 4
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, True)
                        clbtransferbarang.SetItemChecked(1, True)
                        clbtransferbarang.SetItemChecked(2, False)
                    Case 6
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, True)
                        clbtransferbarang.SetItemChecked(1, False)
                        clbtransferbarang.SetItemChecked(2, True)
                    Case 8
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, False)
                        clbtransferbarang.SetItemChecked(1, True)
                        clbtransferbarang.SetItemChecked(2, True)
                    Case 9
                        cbtransferbarang.Checked = True
                        clbtransferbarang.SetItemChecked(0, True)
                        clbtransferbarang.SetItemChecked(1, True)
                        clbtransferbarang.SetItemChecked(2, True)
                End Select

                'administrasi

                Select Case akseslunasutang
                    Case 0
                        cblunasutang.Checked = False
                        clblunasutang.SetItemChecked(0, False)
                        clblunasutang.SetItemChecked(1, False)
                        clblunasutang.SetItemChecked(2, False)
                    Case 1
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, True)
                        clblunasutang.SetItemChecked(1, False)
                        clblunasutang.SetItemChecked(2, False)
                    Case 3
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, False)
                        clblunasutang.SetItemChecked(1, True)
                        clblunasutang.SetItemChecked(2, False)
                    Case 5
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, False)
                        clblunasutang.SetItemChecked(1, False)
                        clblunasutang.SetItemChecked(2, True)
                    Case 4
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, True)
                        clblunasutang.SetItemChecked(1, True)
                        clblunasutang.SetItemChecked(2, False)
                    Case 6
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, True)
                        clblunasutang.SetItemChecked(1, False)
                        clblunasutang.SetItemChecked(2, True)
                    Case 8
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, False)
                        clblunasutang.SetItemChecked(1, True)
                        clblunasutang.SetItemChecked(2, True)
                    Case 9
                        cblunasutang.Checked = True
                        clblunasutang.SetItemChecked(0, True)
                        clblunasutang.SetItemChecked(1, True)
                        clblunasutang.SetItemChecked(2, True)
                End Select

                Select Case akseslunaspiutang
                    Case 0
                        cblunaspiutang.Checked = False
                        clblunaspiutang.SetItemChecked(0, False)
                        clblunaspiutang.SetItemChecked(1, False)
                        clblunaspiutang.SetItemChecked(2, False)
                    Case 1
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, True)
                        clblunaspiutang.SetItemChecked(1, False)
                        clblunaspiutang.SetItemChecked(2, False)
                    Case 3
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, False)
                        clblunaspiutang.SetItemChecked(1, True)
                        clblunaspiutang.SetItemChecked(2, False)
                    Case 5
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, False)
                        clblunaspiutang.SetItemChecked(1, False)
                        clblunaspiutang.SetItemChecked(2, True)
                    Case 4
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, True)
                        clblunaspiutang.SetItemChecked(1, True)
                        clblunaspiutang.SetItemChecked(2, False)
                    Case 6
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, True)
                        clblunaspiutang.SetItemChecked(1, False)
                        clblunaspiutang.SetItemChecked(2, True)
                    Case 8
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, False)
                        clblunaspiutang.SetItemChecked(1, True)
                        clblunaspiutang.SetItemChecked(2, True)
                    Case 9
                        cblunaspiutang.Checked = True
                        clblunaspiutang.SetItemChecked(0, True)
                        clblunaspiutang.SetItemChecked(1, True)
                        clblunaspiutang.SetItemChecked(2, True)
                End Select

                Select Case aksestransferkas
                    Case 0
                        cbtransferkas.Checked = False
                        clbtransferkas.SetItemChecked(0, False)
                        clbtransferkas.SetItemChecked(1, False)
                        clbtransferkas.SetItemChecked(2, False)
                    Case 1
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, True)
                        clbtransferkas.SetItemChecked(1, False)
                        clbtransferkas.SetItemChecked(2, False)
                    Case 3
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, False)
                        clbtransferkas.SetItemChecked(1, True)
                        clbtransferkas.SetItemChecked(2, False)
                    Case 5
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, False)
                        clbtransferkas.SetItemChecked(1, False)
                        clbtransferkas.SetItemChecked(2, True)
                    Case 4
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, True)
                        clbtransferkas.SetItemChecked(1, True)
                        clbtransferkas.SetItemChecked(2, False)
                    Case 6
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, True)
                        clbtransferkas.SetItemChecked(1, False)
                        clbtransferkas.SetItemChecked(2, True)
                    Case 8
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, False)
                        clbtransferkas.SetItemChecked(1, True)
                        clbtransferkas.SetItemChecked(2, True)
                    Case 9
                        cbtransferkas.Checked = True
                        clbtransferkas.SetItemChecked(0, True)
                        clbtransferkas.SetItemChecked(1, True)
                        clbtransferkas.SetItemChecked(2, True)
                End Select

                Select Case aksesakunmasuk
                    Case 0
                        cbakunmasuk.Checked = False
                        clbakunmasuk.SetItemChecked(0, False)
                        clbakunmasuk.SetItemChecked(1, False)
                        clbakunmasuk.SetItemChecked(2, False)
                    Case 1
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, True)
                        clbakunmasuk.SetItemChecked(1, False)
                        clbakunmasuk.SetItemChecked(2, False)
                    Case 3
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, False)
                        clbakunmasuk.SetItemChecked(1, True)
                        clbakunmasuk.SetItemChecked(2, False)
                    Case 5
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, False)
                        clbakunmasuk.SetItemChecked(1, False)
                        clbakunmasuk.SetItemChecked(2, True)
                    Case 4
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, True)
                        clbakunmasuk.SetItemChecked(1, True)
                        clbakunmasuk.SetItemChecked(2, False)
                    Case 6
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, True)
                        clbakunmasuk.SetItemChecked(1, False)
                        clbakunmasuk.SetItemChecked(2, True)
                    Case 8
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, False)
                        clbakunmasuk.SetItemChecked(1, True)
                        clbakunmasuk.SetItemChecked(2, True)
                    Case 9
                        cbakunmasuk.Checked = True
                        clbakunmasuk.SetItemChecked(0, True)
                        clbakunmasuk.SetItemChecked(1, True)
                        clbakunmasuk.SetItemChecked(2, True)
                End Select

                Select Case aksesakunkeluar
                    Case 0
                        cbakunkeluar.Checked = False
                        clbakunkeluar.SetItemChecked(0, False)
                        clbakunkeluar.SetItemChecked(1, False)
                        clbakunkeluar.SetItemChecked(2, False)
                    Case 1
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, True)
                        clbakunkeluar.SetItemChecked(1, False)
                        clbakunkeluar.SetItemChecked(2, False)
                    Case 3
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, False)
                        clbakunkeluar.SetItemChecked(1, True)
                        clbakunkeluar.SetItemChecked(2, False)
                    Case 5
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, False)
                        clbakunkeluar.SetItemChecked(1, False)
                        clbakunkeluar.SetItemChecked(2, True)
                    Case 4
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, True)
                        clbakunkeluar.SetItemChecked(1, True)
                        clbakunkeluar.SetItemChecked(2, False)
                    Case 6
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, True)
                        clbakunkeluar.SetItemChecked(1, False)
                        clbakunkeluar.SetItemChecked(2, True)
                    Case 8
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, False)
                        clbakunkeluar.SetItemChecked(1, True)
                        clbakunkeluar.SetItemChecked(2, True)
                    Case 9
                        cbakunkeluar.Checked = True
                        clbakunkeluar.SetItemChecked(0, True)
                        clbakunkeluar.SetItemChecked(1, True)
                        clbakunkeluar.SetItemChecked(2, True)
                End Select
                'laporan
                Select Case akseslappricelist
                    Case 0
                        cblappricelist.Checked = False
                        clblappricelist.SetItemChecked(0, False)
                        clblappricelist.SetItemChecked(1, False)
                    Case 1
                        cblappricelist.Checked = True
                        clblappricelist.SetItemChecked(0, True)
                        clblappricelist.SetItemChecked(1, False)
                    Case 3
                        cblappricelist.Checked = True
                        clblappricelist.SetItemChecked(0, False)
                        clblappricelist.SetItemChecked(1, True)
                    Case 4
                        cblappricelist.Checked = True
                        clblappricelist.SetItemChecked(0, True)
                        clblappricelist.SetItemChecked(1, True)
                End Select

                Select Case akseslappembelian
                    Case 0
                        cblappembelian.Checked = False
                        clblappembelian.SetItemChecked(0, False)
                        clblappembelian.SetItemChecked(1, False)
                    Case 1
                        cblappembelian.Checked = True
                        clblappembelian.SetItemChecked(0, True)
                        clblappembelian.SetItemChecked(1, False)
                    Case 3
                        cblappembelian.Checked = True
                        clblappembelian.SetItemChecked(0, False)
                        clblappembelian.SetItemChecked(1, True)
                    Case 4
                        cblappembelian.Checked = True
                        clblappembelian.SetItemChecked(0, True)
                        clblappembelian.SetItemChecked(1, True)
                End Select

                Select Case akseslappenjualan
                    Case 0
                        cblappenjualan.Checked = False
                        clblappenjualan.SetItemChecked(0, False)
                        clblappenjualan.SetItemChecked(1, False)
                    Case 1
                        cblappenjualan.Checked = True
                        clblappenjualan.SetItemChecked(0, True)
                        clblappenjualan.SetItemChecked(1, False)
                    Case 3
                        cblappenjualan.Checked = True
                        clblappenjualan.SetItemChecked(0, False)
                        clblappenjualan.SetItemChecked(1, True)
                    Case 4
                        cblappenjualan.Checked = True
                        clblappenjualan.SetItemChecked(0, True)
                        clblappenjualan.SetItemChecked(1, True)
                End Select

                Select Case akseslapreturbeli
                    Case 0
                        cblapreturbeli.Checked = False
                        clblapreturbeli.SetItemChecked(0, False)
                        clblapreturbeli.SetItemChecked(1, False)
                    Case 1
                        cblapreturbeli.Checked = True
                        clblapreturbeli.SetItemChecked(0, True)
                        clblapreturbeli.SetItemChecked(1, False)
                    Case 3
                        cblapreturbeli.Checked = True
                        clblapreturbeli.SetItemChecked(0, False)
                        clblapreturbeli.SetItemChecked(1, True)
                    Case 4
                        cblapreturbeli.Checked = True
                        clblapreturbeli.SetItemChecked(0, True)
                        clblapreturbeli.SetItemChecked(1, True)
                End Select

                Select Case akseslapreturjual
                    Case 0
                        cblapreturjual.Checked = False
                        clblapreturjual.SetItemChecked(0, False)
                        clblapreturjual.SetItemChecked(1, False)
                    Case 1
                        cblapreturjual.Checked = True
                        clblapreturjual.SetItemChecked(0, True)
                        clblapreturjual.SetItemChecked(1, False)
                    Case 3
                        cblapreturjual.Checked = True
                        clblapreturjual.SetItemChecked(0, False)
                        clblapreturjual.SetItemChecked(1, True)
                    Case 4
                        cblapreturjual.Checked = True
                        clblapreturjual.SetItemChecked(0, True)
                        clblapreturjual.SetItemChecked(1, True)
                End Select

                Select Case akseslapbarangmasuk
                    Case 0
                        cblapbarangmasuk.Checked = False
                        clblapbarangmasuk.SetItemChecked(0, False)
                        clblapbarangmasuk.SetItemChecked(1, False)
                    Case 1
                        cblapbarangmasuk.Checked = True
                        clblapbarangmasuk.SetItemChecked(0, True)
                        clblapbarangmasuk.SetItemChecked(1, False)
                    Case 3
                        cblapbarangmasuk.Checked = True
                        clblapbarangmasuk.SetItemChecked(0, False)
                        clblapbarangmasuk.SetItemChecked(1, True)
                    Case 4
                        cblapbarangmasuk.Checked = True
                        clblapbarangmasuk.SetItemChecked(0, True)
                        clblapbarangmasuk.SetItemChecked(1, True)
                End Select

                Select Case akseslapbarangkeluar
                    Case 0
                        cblapbarangkeluar.Checked = False
                        clblapbarangkeluar.SetItemChecked(0, False)
                        clblapbarangkeluar.SetItemChecked(1, False)
                    Case 1
                        cblapbarangkeluar.Checked = True
                        clblapbarangkeluar.SetItemChecked(0, True)
                        clblapbarangkeluar.SetItemChecked(1, False)
                    Case 3
                        cblapbarangkeluar.Checked = True
                        clblapbarangkeluar.SetItemChecked(0, False)
                        clblapbarangkeluar.SetItemChecked(1, True)
                    Case 4
                        cblapbarangkeluar.Checked = True
                        clblapbarangkeluar.SetItemChecked(0, True)
                        clblapbarangkeluar.SetItemChecked(1, True)
                End Select

                Select Case akseslaputang
                    Case 0
                        cblaputang.Checked = False
                        clblaputang.SetItemChecked(0, False)
                        clblaputang.SetItemChecked(1, False)
                    Case 1
                        cblaputang.Checked = True
                        clblaputang.SetItemChecked(0, True)
                        clblaputang.SetItemChecked(1, False)
                    Case 3
                        cblaputang.Checked = True
                        clblaputang.SetItemChecked(0, False)
                        clblaputang.SetItemChecked(1, True)
                    Case 4
                        cblaputang.Checked = True
                        clblaputang.SetItemChecked(0, True)
                        clblaputang.SetItemChecked(1, True)
                End Select

                Select Case akseslappiutang
                    Case 0
                        cblappiutang.Checked = False
                        clblappiutang.SetItemChecked(0, False)
                        clblappiutang.SetItemChecked(1, False)
                    Case 1
                        cblappiutang.Checked = True
                        clblappiutang.SetItemChecked(0, True)
                        clblappiutang.SetItemChecked(1, False)
                    Case 3
                        cblappiutang.Checked = True
                        clblappiutang.SetItemChecked(0, False)
                        clblappiutang.SetItemChecked(1, True)
                    Case 4
                        cblappiutang.Checked = True
                        clblappiutang.SetItemChecked(0, True)
                        clblappiutang.SetItemChecked(1, True)
                End Select

                Select Case akseslapstokbarang
                    Case 0
                        cblapstokbarang.Checked = False
                        clblapstokbarang.SetItemChecked(0, False)
                        clblapstokbarang.SetItemChecked(1, False)
                    Case 1
                        cblapstokbarang.Checked = True
                        clblapstokbarang.SetItemChecked(0, True)
                        clblapstokbarang.SetItemChecked(1, False)
                    Case 3
                        cblapstokbarang.Checked = True
                        clblapstokbarang.SetItemChecked(0, False)
                        clblapstokbarang.SetItemChecked(1, True)
                    Case 4
                        cblapstokbarang.Checked = True
                        clblapstokbarang.SetItemChecked(0, True)
                        clblapstokbarang.SetItemChecked(1, True)
                End Select

                Select Case akseslapakunmasuk
                    Case 0
                        cblapakunmasuk.Checked = False
                        clblapakunmasuk.SetItemChecked(0, False)
                        clblapakunmasuk.SetItemChecked(1, False)
                    Case 1
                        cblapakunmasuk.Checked = True
                        clblapakunmasuk.SetItemChecked(0, True)
                        clblapakunmasuk.SetItemChecked(1, False)
                    Case 3
                        cblapakunmasuk.Checked = True
                        clblapakunmasuk.SetItemChecked(0, False)
                        clblapakunmasuk.SetItemChecked(1, True)
                    Case 4
                        cblapakunmasuk.Checked = True
                        clblapakunmasuk.SetItemChecked(0, True)
                        clblapakunmasuk.SetItemChecked(1, True)
                End Select

                Select Case akseslapakunkeluar
                    Case 0
                        cblapakunkeluar.Checked = False
                        clblapakunkeluar.SetItemChecked(0, False)
                        clblapakunkeluar.SetItemChecked(1, False)
                    Case 1
                        cblapakunkeluar.Checked = True
                        clblapakunkeluar.SetItemChecked(0, True)
                        clblapakunkeluar.SetItemChecked(1, False)
                    Case 3
                        cblapakunkeluar.Checked = True
                        clblapakunkeluar.SetItemChecked(0, False)
                        clblapakunkeluar.SetItemChecked(1, True)
                    Case 4
                        cblapakunkeluar.Checked = True
                        clblapakunkeluar.SetItemChecked(0, True)
                        clblapakunkeluar.SetItemChecked(1, True)
                End Select

                Select Case akseslaptransferkas
                    Case 0
                        cblaptransferkas.Checked = False
                        clblaptransferkas.SetItemChecked(0, False)
                        clblaptransferkas.SetItemChecked(1, False)
                    Case 1
                        cblaptransferkas.Checked = True
                        clblaptransferkas.SetItemChecked(0, True)
                        clblaptransferkas.SetItemChecked(1, False)
                    Case 3
                        cblaptransferkas.Checked = True
                        clblaptransferkas.SetItemChecked(0, False)
                        clblaptransferkas.SetItemChecked(1, True)
                    Case 4
                        cblaptransferkas.Checked = True
                        clblaptransferkas.SetItemChecked(0, True)
                        clblaptransferkas.SetItemChecked(1, True)
                End Select

                Select Case akseslaptransferbarang
                    Case 0
                        cblaptransferbarang.Checked = False
                        clblaptransferbarang.SetItemChecked(0, False)
                        clblaptransferbarang.SetItemChecked(1, False)
                    Case 1
                        cblaptransferbarang.Checked = True
                        clblaptransferbarang.SetItemChecked(0, True)
                        clblaptransferbarang.SetItemChecked(1, False)
                    Case 3
                        cblaptransferbarang.Checked = True
                        clblaptransferbarang.SetItemChecked(0, False)
                        clblaptransferbarang.SetItemChecked(1, True)
                    Case 4
                        cblaptransferbarang.Checked = True
                        clblaptransferbarang.SetItemChecked(0, True)
                        clblaptransferbarang.SetItemChecked(1, True)
                End Select

                Select Case akseslaptransaksikas
                    Case 0
                        cblaptransaksikas.Checked = False
                        clblaptransaksikas.SetItemChecked(0, False)
                        clblaptransaksikas.SetItemChecked(1, False)
                    Case 1
                        cblaptransaksikas.Checked = True
                        clblaptransaksikas.SetItemChecked(0, True)
                        clblaptransaksikas.SetItemChecked(1, False)
                    Case 3
                        cblaptransaksikas.Checked = True
                        clblaptransaksikas.SetItemChecked(0, False)
                        clblaptransaksikas.SetItemChecked(1, True)
                    Case 4
                        cblaptransaksikas.Checked = True
                        clblaptransaksikas.SetItemChecked(0, True)
                        clblaptransaksikas.SetItemChecked(1, True)
                End Select

                '==batas case==

                'master
                clbmasterbarang.Enabled = False
                clbmasterkategori.Enabled = False
                clbmastergudang.Enabled = False
                clbmastercustomer.Enabled = False
                clbmastersupplier.Enabled = False
                clbmasteruser.Enabled = False
                clbmasterkas.Enabled = False
                clbmasterpricelist.Enabled = False
                clbmasterreksupp.Enabled = False
                clbmasterrekcust.Enabled = False

                'transaksi
                clbpembelian.Enabled = False
                clbpenjualan.Enabled = False
                clbreturbeli.Enabled = False
                clbreturjual.Enabled = False
                clbbarangmasuk.Enabled = False
                clbbarangkeluar.Enabled = False
                clbtransferbarang.Enabled = False

                'administrasi
                clblunasutang.Enabled = False
                clblunaspiutang.Enabled = False
                clbtransferkas.Enabled = False
                clbakunmasuk.Enabled = False
                clbakunkeluar.Enabled = False

                'laporan
                clblappricelist.Enabled = False
                clblappembelian.Enabled = False
                clblappenjualan.Enabled = False
                clblapreturbeli.Enabled = False
                clblapreturjual.Enabled = False
                clblapbarangmasuk.Enabled = False
                clblapbarangkeluar.Enabled = False
                clblaputang.Enabled = False
                clblappiutang.Enabled = False
                clblapstokbarang.Enabled = False
                clblapakunmasuk.Enabled = False
                clblapakunkeluar.Enabled = False
                clblaptransferkas.Enabled = False
                clblaptransferbarang.Enabled = False
                clblaptransaksikas.Enabled = False

                'end

                btnedit.Enabled = True
                btnbatal.Enabled = True
                btnhapus.Enabled = True
                btntambah.Enabled = False
                btntambah.Text = "Tambah"
                cnn.Close()
            End If
        End Using
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Call cari()
    End Sub

    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    'master menu
    Private Sub cbmasterbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterbarang.CheckedChanged
        If cbmasterbarang.Checked = True Then
            clbmasterbarang.Enabled = True
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, True)
            Next
        ElseIf cbmasterbarang.Checked = False Then
            clbmasterbarang.Enabled = False
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterkategori_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterkategori.CheckedChanged
        If cbmasterkategori.Checked = True Then
            clbmasterkategori.Enabled = True
            For id As Integer = 0 To clbmasterkategori.Items.Count - 1
                Me.clbmasterkategori.SetItemChecked(id, True)
            Next
        ElseIf cbmasterkategori.Checked = False Then
            clbmasterkategori.Enabled = False
            For id As Integer = 0 To clbmasterkategori.Items.Count - 1
                Me.clbmasterkategori.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastergudang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastergudang.CheckedChanged
        If cbmastergudang.Checked = True Then
            clbmastergudang.Enabled = True
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, True)
            Next
        ElseIf cbmastergudang.Checked = False Then
            clbmastergudang.Enabled = False
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastercustomer_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastercustomer.CheckedChanged
        If cbmastercustomer.Checked = True Then
            clbmastercustomer.Enabled = True
            For id As Integer = 0 To clbmastercustomer.Items.Count - 1
                Me.clbmastercustomer.SetItemChecked(id, True)
            Next
        ElseIf cbmastercustomer.Checked = False Then
            clbmastercustomer.Enabled = False
            For id As Integer = 0 To clbmastercustomer.Items.Count - 1
                Me.clbmastercustomer.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastersupplier_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastersupplier.CheckedChanged
        If cbmastersupplier.Checked = True Then
            clbmastersupplier.Enabled = True
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, True)
            Next
        ElseIf cbmastersupplier.Checked = False Then
            clbmastersupplier.Enabled = False
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasteruser_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasteruser.CheckedChanged
        If cbmasteruser.Checked = True Then
            clbmasteruser.Enabled = True
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, True)
            Next
        ElseIf cbmasteruser.Checked = False Then
            clbmasteruser.Enabled = False
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterkas_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterkas.CheckedChanged
        If cbmasterkas.Checked = True Then
            clbmasterkas.Enabled = True
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, True)
            Next
        ElseIf cbmasterkas.Checked = False Then
            clbmasterkas.Enabled = False
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterpricelist_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterpricelist.CheckedChanged
        If cbmasterpricelist.Checked = True Then
            clbmasterpricelist.Enabled = True
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, True)
            Next
        ElseIf cbmasterpricelist.Checked = False Then
            clbmasterpricelist.Enabled = False
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterreksupp_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterreksupp.CheckedChanged
        If cbmasterreksupp.Checked = True Then
            clbmasterreksupp.Enabled = True
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, True)
            Next
        ElseIf cbmasterreksupp.Checked = False Then
            clbmasterreksupp.Enabled = False
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterrekcust_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterrekcust.CheckedChanged
        If cbmasterrekcust.Checked = True Then
            clbmasterrekcust.Enabled = True
            For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
                Me.clbmasterrekcust.SetItemChecked(id, True)
            Next
        ElseIf cbmasterrekcust.Checked = False Then
            clbmasterrekcust.Enabled = False
            For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
                Me.clbmasterrekcust.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'transaksi
    Private Sub cbpembelian_CheckedChanged(sender As Object, e As EventArgs) Handles cbpembelian.CheckedChanged
        If cbpembelian.Checked = True Then
            clbpembelian.Enabled = True
            For id As Integer = 0 To clbpembelian.Items.Count - 1
                Me.clbpembelian.SetItemChecked(id, True)
            Next
        ElseIf cbpembelian.Checked = False Then
            clbpembelian.Enabled = False
            For id As Integer = 0 To clbpembelian.Items.Count - 1
                Me.clbpembelian.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbpenjualan_CheckedChanged(sender As Object, e As EventArgs) Handles cbpenjualan.CheckedChanged
        If cbpenjualan.Checked = True Then
            clbpenjualan.Enabled = True
            For id As Integer = 0 To clbpenjualan.Items.Count - 1
                Me.clbpenjualan.SetItemChecked(id, True)
            Next
        ElseIf cbpenjualan.Checked = False Then
            clbpenjualan.Enabled = False
            For id As Integer = 0 To clbpenjualan.Items.Count - 1
                Me.clbpenjualan.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbreturbeli_CheckedChanged(sender As Object, e As EventArgs) Handles cbreturbeli.CheckedChanged
        If cbreturbeli.Checked = True Then
            clbreturbeli.Enabled = True
            For id As Integer = 0 To clbreturbeli.Items.Count - 1
                Me.clbreturbeli.SetItemChecked(id, True)
            Next
        ElseIf cbreturbeli.Checked = False Then
            clbreturbeli.Enabled = False
            For id As Integer = 0 To clbreturbeli.Items.Count - 1
                Me.clbreturbeli.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbreturjual_CheckedChanged(sender As Object, e As EventArgs) Handles cbreturjual.CheckedChanged
        If cbreturjual.Checked = True Then
            clbreturjual.Enabled = True
            For id As Integer = 0 To clbreturjual.Items.Count - 1
                Me.clbreturjual.SetItemChecked(id, True)
            Next
        ElseIf cbreturjual.Checked = False Then
            clbreturjual.Enabled = False
            For id As Integer = 0 To clbreturjual.Items.Count - 1
                Me.clbreturjual.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbbarangmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cbbarangmasuk.CheckedChanged
        If cbbarangmasuk.Checked = True Then
            clbbarangmasuk.Enabled = True
            For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
                Me.clbbarangmasuk.SetItemChecked(id, True)
            Next
        ElseIf cbbarangmasuk.Checked = False Then
            clbbarangmasuk.Enabled = False
            For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
                Me.clbbarangmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbbarangkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cbbarangkeluar.CheckedChanged
        If cbbarangkeluar.Checked = True Then
            clbbarangkeluar.Enabled = True
            For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
                Me.clbbarangkeluar.SetItemChecked(id, True)
            Next
        ElseIf cbbarangkeluar.Checked = False Then
            clbbarangkeluar.Enabled = False
            For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
                Me.clbbarangkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbtransferbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cbtransferbarang.CheckedChanged
        If cbtransferbarang.Checked = True Then
            clbtransferbarang.Enabled = True
            For id As Integer = 0 To clbtransferbarang.Items.Count - 1
                Me.clbtransferbarang.SetItemChecked(id, True)
            Next
        ElseIf cbtransferbarang.Checked = False Then
            clbtransferbarang.Enabled = False
            For id As Integer = 0 To clbtransferbarang.Items.Count - 1
                Me.clbtransferbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    'administrasi
    Private Sub cblunasutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblunasutang.CheckedChanged
        If cblunasutang.Checked = True Then
            clblunasutang.Enabled = True
            For id As Integer = 0 To clblunasutang.Items.Count - 1
                Me.clblunasutang.SetItemChecked(id, True)
            Next
        ElseIf cblunasutang.Checked = False Then
            clblunasutang.Enabled = False
            For id As Integer = 0 To clblunasutang.Items.Count - 1
                Me.clblunasutang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblunaspiutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblunaspiutang.CheckedChanged
        If cblunaspiutang.Checked = True Then
            clblunaspiutang.Enabled = True
            For id As Integer = 0 To clblunaspiutang.Items.Count - 1
                Me.clblunaspiutang.SetItemChecked(id, True)
            Next
        ElseIf cblunaspiutang.Checked = False Then
            clblunaspiutang.Enabled = False
            For id As Integer = 0 To clblunaspiutang.Items.Count - 1
                Me.clblunaspiutang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbakunmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cbakunmasuk.CheckedChanged
        If cbakunmasuk.Checked = True Then
            clbakunmasuk.Enabled = True
            For id As Integer = 0 To clbakunmasuk.Items.Count - 1
                Me.clbakunmasuk.SetItemChecked(id, True)
            Next
        ElseIf cbakunmasuk.Checked = False Then
            clbakunmasuk.Enabled = False
            For id As Integer = 0 To clbakunmasuk.Items.Count - 1
                Me.clbakunmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbakunkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cbakunkeluar.CheckedChanged
        If cbakunkeluar.Checked = True Then
            clbakunkeluar.Enabled = True
            For id As Integer = 0 To clbakunkeluar.Items.Count - 1
                Me.clbakunkeluar.SetItemChecked(id, True)
            Next
        ElseIf cbakunkeluar.Checked = False Then
            clbakunkeluar.Enabled = False
            For id As Integer = 0 To clbakunkeluar.Items.Count - 1
                Me.clbakunkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbtransferkas_CheckedChanged(sender As Object, e As EventArgs) Handles cbtransferkas.CheckedChanged
        If cbtransferkas.Checked = True Then
            clbtransferkas.Enabled = True
            For id As Integer = 0 To clbtransferkas.Items.Count - 1
                Me.clbtransferkas.SetItemChecked(id, True)
            Next
        ElseIf cbtransferkas.Checked = False Then
            clbtransferkas.Enabled = False
            For id As Integer = 0 To clbtransferkas.Items.Count - 1
                Me.clbtransferkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'laporan
    Private Sub cblappricelist_CheckedChanged(sender As Object, e As EventArgs) Handles cblappricelist.CheckedChanged
        If cblappricelist.Checked = True Then
            clblappricelist.Enabled = True
            For id As Integer = 0 To clblappricelist.Items.Count - 1
                Me.clblappricelist.SetItemChecked(id, True)
            Next
        ElseIf cblappricelist.Checked = False Then
            clblappricelist.Enabled = False
            For id As Integer = 0 To clblappricelist.Items.Count - 1
                Me.clblappricelist.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappembelian_CheckedChanged(sender As Object, e As EventArgs) Handles cblappembelian.CheckedChanged
        If cblappembelian.Checked = True Then
            clblappembelian.Enabled = True
            For id As Integer = 0 To clblappembelian.Items.Count - 1
                Me.clblappembelian.SetItemChecked(id, True)
            Next
        ElseIf cblappembelian.Checked = False Then
            clblappembelian.Enabled = False
            For id As Integer = 0 To clblappembelian.Items.Count - 1
                Me.clblappembelian.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappenjualan_CheckedChanged(sender As Object, e As EventArgs) Handles cblappenjualan.CheckedChanged
        If cblappenjualan.Checked = True Then
            clblappenjualan.Enabled = True
            For id As Integer = 0 To clblappenjualan.Items.Count - 1
                Me.clblappenjualan.SetItemChecked(id, True)
            Next
        ElseIf cblappenjualan.Checked = False Then
            clblappenjualan.Enabled = False
            For id As Integer = 0 To clblappenjualan.Items.Count - 1
                Me.clblappenjualan.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapreturbeli_CheckedChanged(sender As Object, e As EventArgs) Handles cblapreturbeli.CheckedChanged
        If cblapreturbeli.Checked = True Then
            clblapreturbeli.Enabled = True
            For id As Integer = 0 To clblapreturbeli.Items.Count - 1
                Me.clblapreturbeli.SetItemChecked(id, True)
            Next
        ElseIf cblapreturbeli.Checked = False Then
            clblapreturbeli.Enabled = False
            For id As Integer = 0 To clblapreturbeli.Items.Count - 1
                Me.clblapreturbeli.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapreturjual_CheckedChanged(sender As Object, e As EventArgs) Handles cblapreturjual.CheckedChanged
        If cblapreturjual.Checked = True Then
            clblapreturjual.Enabled = True
            For id As Integer = 0 To clblapreturjual.Items.Count - 1
                Me.clblapreturjual.SetItemChecked(id, True)
            Next
        ElseIf cblapreturjual.Checked = False Then
            clblapreturjual.Enabled = False
            For id As Integer = 0 To clblapreturjual.Items.Count - 1
                Me.clblapreturjual.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapbarangmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cblapbarangmasuk.CheckedChanged
        If cblapbarangmasuk.Checked = True Then
            clblapbarangmasuk.Enabled = True
            For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
                Me.clblapbarangmasuk.SetItemChecked(id, True)
            Next
        ElseIf cblapbarangmasuk.Checked = False Then
            clblapbarangmasuk.Enabled = False
            For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
                Me.clblapbarangmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapbarangkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cblapbarangkeluar.CheckedChanged
        If cblapbarangkeluar.Checked = True Then
            clblapbarangkeluar.Enabled = True
            For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
                Me.clblapbarangkeluar.SetItemChecked(id, True)
            Next
        ElseIf cblapbarangkeluar.Checked = False Then
            clblapbarangkeluar.Enabled = False
            For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
                Me.clblapbarangkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaputang_CheckedChanged(sender As Object, e As EventArgs) Handles cblaputang.CheckedChanged
        If cblaputang.Checked = True Then
            clblaputang.Enabled = True
            For id As Integer = 0 To clblaputang.Items.Count - 1
                Me.clblaputang.SetItemChecked(id, True)
            Next
        ElseIf cblaputang.Checked = False Then
            clblaputang.Enabled = False
            For id As Integer = 0 To clblaputang.Items.Count - 1
                Me.clblaputang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappiutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblappiutang.CheckedChanged
        If cblappiutang.Checked = True Then
            clblappiutang.Enabled = True
            For id As Integer = 0 To clblappiutang.Items.Count - 1
                Me.clblappiutang.SetItemChecked(id, True)
            Next
        ElseIf cblappiutang.Checked = False Then
            clblappiutang.Enabled = False
            For id As Integer = 0 To clblappiutang.Items.Count - 1
                Me.clblappiutang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblapstokbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblapstokbarang.CheckedChanged
        If cblapstokbarang.Checked = True Then
            clblapstokbarang.Enabled = True
            For id As Integer = 0 To clblapstokbarang.Items.Count - 1
                Me.clblapstokbarang.SetItemChecked(id, True)
            Next
        ElseIf cblapstokbarang.Checked = False Then
            clblapstokbarang.Enabled = False
            For id As Integer = 0 To clblapstokbarang.Items.Count - 1
                Me.clblapstokbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapakunmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cblapakunmasuk.CheckedChanged
        If cblapakunmasuk.Checked = True Then
            clblapakunmasuk.Enabled = True
            For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
                Me.clblapakunmasuk.SetItemChecked(id, True)
            Next
        ElseIf cblapakunmasuk.Checked = False Then
            clblapakunmasuk.Enabled = False
            For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
                Me.clblapakunmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblapakunkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cblapakunkeluar.CheckedChanged
        If cblapakunkeluar.Checked = True Then
            clblapakunkeluar.Enabled = True
            For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
                Me.clblapakunkeluar.SetItemChecked(id, True)
            Next
        ElseIf cblapakunkeluar.Checked = False Then
            clblapakunkeluar.Enabled = False
            For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
                Me.clblapakunkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblaptransferkas_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransferkas.CheckedChanged
        If cblaptransferkas.Checked = True Then
            clblaptransferkas.Enabled = True
            For id As Integer = 0 To clblaptransferkas.Items.Count - 1
                Me.clblaptransferkas.SetItemChecked(id, True)
            Next
        ElseIf cblaptransferkas.Checked = False Then
            clblaptransferkas.Enabled = False
            For id As Integer = 0 To clblaptransferkas.Items.Count - 1
                Me.clblaptransferkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaptransferbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransferbarang.CheckedChanged
        If cblaptransferbarang.Checked = True Then
            clblaptransferbarang.Enabled = True
            For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
                Me.clblaptransferbarang.SetItemChecked(id, True)
            Next
        ElseIf cblaptransferbarang.Checked = False Then
            clblaptransferbarang.Enabled = False
            For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
                Me.clblaptransferbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaptransaksikas_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransaksikas.CheckedChanged
        If cblaptransaksikas.Checked = True Then
            clblaptransaksikas.Enabled = True
            For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
                Me.clblaptransaksikas.SetItemChecked(id, True)
            Next
        ElseIf cblaptransaksikas.Checked = False Then
            clblaptransaksikas.Enabled = False
            For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
                Me.clblaptransaksikas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'combobox list
    'master
    Private Sub clbmasterbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterbarang.MouseDown
        Dim Index As Integer = clbmasterbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterbarang.SetItemChecked(Index, Not clbmasterbarang.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterbarang.Items.Count - 1
            If clbmasterbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterbarang.Enabled = False
            cbmasterbarang.Checked = False
        End If
    End Sub

    Private Sub clbmasterkategori_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterkategori.MouseDown
        Dim Index As Integer = clbmasterkategori.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterkategori.SetItemChecked(Index, Not clbmasterkategori.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterkategori.Items.Count - 1
            If clbmasterkategori.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterkategori.Enabled = False
            cbmasterkategori.Checked = False
        End If
    End Sub

    Private Sub clbmastergudang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastergudang.MouseDown
        Dim Index As Integer = clbmastergudang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastergudang.SetItemChecked(Index, Not clbmastergudang.GetItemChecked(Index))

        For id As Integer = 0 To clbmastergudang.Items.Count - 1
            If clbmastergudang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastergudang.Enabled = False
            cbmastergudang.Checked = False
        End If
    End Sub

    Private Sub clbmastercustomer_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastercustomer.MouseDown
        Dim Index As Integer = clbmastercustomer.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastercustomer.SetItemChecked(Index, Not clbmastercustomer.GetItemChecked(Index))

        For id As Integer = 0 To clbmastercustomer.Items.Count - 1
            If clbmastercustomer.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastercustomer.Enabled = False
            cbmastercustomer.Checked = False
        End If
    End Sub

    Private Sub clbmastersupplier_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastersupplier.MouseDown
        Dim Index As Integer = clbmastersupplier.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastersupplier.SetItemChecked(Index, Not clbmastersupplier.GetItemChecked(Index))

        For id As Integer = 0 To clbmastersupplier.Items.Count - 1
            If clbmastersupplier.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastersupplier.Enabled = False
            cbmastersupplier.Checked = False
        End If
    End Sub

    Private Sub clbmasteruser_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasteruser.MouseDown
        Dim Index As Integer = clbmasteruser.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasteruser.SetItemChecked(Index, Not clbmasteruser.GetItemChecked(Index))

        For id As Integer = 0 To clbmasteruser.Items.Count - 1
            If clbmasteruser.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasteruser.Enabled = False
            cbmasteruser.Checked = False
        End If
    End Sub

    Private Sub clbmasterkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterkas.MouseDown
        Dim Index As Integer = clbmasterkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterkas.SetItemChecked(Index, Not clbmasterkas.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterkas.Items.Count - 1
            If clbmasterkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterkas.Enabled = False
            cbmasterkas.Checked = False
        End If
    End Sub

    Private Sub clbmasterpricelist_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterpricelist.MouseDown
        Dim Index As Integer = clbmasterpricelist.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterpricelist.SetItemChecked(Index, Not clbmasterpricelist.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
            If clbmasterpricelist.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterpricelist.Enabled = False
            cbmasterpricelist.Checked = False
        End If
    End Sub

    Private Sub clbmasterreksupp_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterreksupp.MouseDown
        Dim Index As Integer = clbmasterreksupp.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterreksupp.SetItemChecked(Index, Not clbmasterreksupp.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
            If clbmasterreksupp.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterreksupp.Enabled = False
            cbmasterreksupp.Checked = False
        End If
    End Sub

    Private Sub clbmasterrekcust_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterrekcust.MouseDown
        Dim Index As Integer = clbmasterrekcust.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterrekcust.SetItemChecked(Index, Not clbmasterrekcust.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
            If clbmasterrekcust.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterrekcust.Enabled = False
            cbmasterrekcust.Checked = False
        End If
    End Sub

    'transaksi
    Private Sub clbpembelian_MouseDown(sender As Object, e As MouseEventArgs) Handles clbpembelian.MouseDown
        Dim Index As Integer = clbpembelian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbpembelian.SetItemChecked(Index, Not clbpembelian.GetItemChecked(Index))

        For id As Integer = 0 To clbpembelian.Items.Count - 1
            If clbpembelian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbpembelian.Enabled = False
            cbpembelian.Checked = False
        End If
    End Sub

    Private Sub clbpenjualan_MouseDown(sender As Object, e As MouseEventArgs) Handles clbpenjualan.MouseDown
        Dim Index As Integer = clbpenjualan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbpenjualan.SetItemChecked(Index, Not clbpenjualan.GetItemChecked(Index))

        For id As Integer = 0 To clbpenjualan.Items.Count - 1
            If clbpenjualan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbpenjualan.Enabled = False
            cbpenjualan.Checked = False
        End If
    End Sub

    Private Sub clbreturbeli_MouseDown(sender As Object, e As MouseEventArgs) Handles clbreturbeli.MouseDown
        Dim Index As Integer = clbreturbeli.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbreturbeli.SetItemChecked(Index, Not clbreturbeli.GetItemChecked(Index))

        For id As Integer = 0 To clbreturbeli.Items.Count - 1
            If clbreturbeli.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbreturbeli.Enabled = False
            cbreturbeli.Checked = False
        End If
    End Sub

    Private Sub clbreturjual_MouseDown(sender As Object, e As MouseEventArgs) Handles clbreturjual.MouseDown
        Dim Index As Integer = clbreturjual.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbreturjual.SetItemChecked(Index, Not clbreturjual.GetItemChecked(Index))

        For id As Integer = 0 To clbreturjual.Items.Count - 1
            If clbreturjual.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbreturjual.Enabled = False
            cbreturjual.Checked = False
        End If
    End Sub

    Private Sub clbbarangmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clbbarangmasuk.MouseDown
        Dim Index As Integer = clbbarangmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbbarangmasuk.SetItemChecked(Index, Not clbbarangmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
            If clbbarangmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbbarangmasuk.Enabled = False
            cbbarangmasuk.Checked = False
        End If
    End Sub

    Private Sub clbbarangkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clbbarangkeluar.MouseDown
        Dim Index As Integer = clbbarangkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbbarangkeluar.SetItemChecked(Index, Not clbbarangkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
            If clbbarangkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbbarangkeluar.Enabled = False
            cbbarangkeluar.Checked = False
        End If
    End Sub

    Private Sub clbtransferbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbtransferbarang.MouseDown
        Dim Index As Integer = clbtransferbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbtransferbarang.SetItemChecked(Index, Not clbtransferbarang.GetItemChecked(Index))

        For id As Integer = 0 To clbtransferbarang.Items.Count - 1
            If clbtransferbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbtransferbarang.Enabled = False
            cbtransferbarang.Checked = False
        End If
    End Sub

    'administrasi

    Private Sub clblunasutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblunasutang.MouseDown
        Dim Index As Integer = clblunasutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblunasutang.SetItemChecked(Index, Not clblunasutang.GetItemChecked(Index))

        For id As Integer = 0 To clblunasutang.Items.Count - 1
            If clblunasutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblunasutang.Enabled = False
            cblunasutang.Checked = False
        End If
    End Sub

    Private Sub clblunaspiutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblunaspiutang.MouseDown
        Dim Index As Integer = clblunaspiutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblunaspiutang.SetItemChecked(Index, Not clblunaspiutang.GetItemChecked(Index))

        For id As Integer = 0 To clblunaspiutang.Items.Count - 1
            If clblunaspiutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblunaspiutang.Enabled = False
            cblunaspiutang.Checked = False
        End If
    End Sub

    Private Sub clbtransferkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clbtransferkas.MouseDown
        Dim Index As Integer = clbtransferkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbtransferkas.SetItemChecked(Index, Not clbtransferkas.GetItemChecked(Index))

        For id As Integer = 0 To clbtransferkas.Items.Count - 1
            If clbtransferkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbtransferkas.Enabled = False
            cbtransferkas.Checked = False
        End If
    End Sub

    Private Sub clbakunmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clbakunmasuk.MouseDown
        Dim Index As Integer = clbakunmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbakunmasuk.SetItemChecked(Index, Not clbakunmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clbakunmasuk.Items.Count - 1
            If clbakunmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbakunmasuk.Enabled = False
            cbakunmasuk.Checked = False
        End If
    End Sub

    Private Sub clbakunkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clbakunkeluar.MouseDown
        Dim Index As Integer = clbakunkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbakunkeluar.SetItemChecked(Index, Not clbakunkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clbakunkeluar.Items.Count - 1
            If clbakunkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbakunkeluar.Enabled = False
            cbakunkeluar.Checked = False
        End If
    End Sub

    'laporan
    Private Sub clblappricelist_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappricelist.MouseDown
        Dim Index As Integer = clblappricelist.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappricelist.SetItemChecked(Index, Not clblappricelist.GetItemChecked(Index))

        For id As Integer = 0 To clblappricelist.Items.Count - 1
            If clblappricelist.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappricelist.Enabled = False
            cblappricelist.Checked = False
        End If
    End Sub
    Private Sub clblappembelian_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappembelian.MouseDown
        Dim Index As Integer = clblappembelian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappembelian.SetItemChecked(Index, Not clblappembelian.GetItemChecked(Index))

        For id As Integer = 0 To clblappembelian.Items.Count - 1
            If clblappembelian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappembelian.Enabled = False
            cblappembelian.Checked = False
        End If
    End Sub

    Private Sub clblappenjualan_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappenjualan.MouseDown
        Dim Index As Integer = clblappenjualan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappenjualan.SetItemChecked(Index, Not clblappenjualan.GetItemChecked(Index))

        For id As Integer = 0 To clblappenjualan.Items.Count - 1
            If clblappenjualan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappenjualan.Enabled = False
            cblappenjualan.Checked = False
        End If
    End Sub

    Private Sub clblapreturbeli_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapreturbeli.MouseDown
        Dim Index As Integer = clblapreturbeli.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapreturbeli.SetItemChecked(Index, Not clblapreturbeli.GetItemChecked(Index))

        For id As Integer = 0 To clblapreturbeli.Items.Count - 1
            If clblapreturbeli.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapreturbeli.Enabled = False
            cblapreturbeli.Checked = False
        End If
    End Sub

    Private Sub clblapreturjual_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapreturjual.MouseDown
        Dim Index As Integer = clblapreturjual.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapreturjual.SetItemChecked(Index, Not clblapreturjual.GetItemChecked(Index))

        For id As Integer = 0 To clblapreturjual.Items.Count - 1
            If clblapreturjual.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapreturjual.Enabled = False
            cblapreturjual.Checked = False
        End If
    End Sub

    Private Sub clblapbarangmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapbarangmasuk.MouseDown
        Dim Index As Integer = clblapbarangmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapbarangmasuk.SetItemChecked(Index, Not clblapbarangmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
            If clblapbarangmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapbarangmasuk.Enabled = False
            cblapbarangmasuk.Checked = False
        End If
    End Sub

    Private Sub clblapbarangkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapbarangkeluar.MouseDown
        Dim Index As Integer = clblapbarangkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapbarangkeluar.SetItemChecked(Index, Not clblapbarangkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
            If clblapbarangkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapbarangkeluar.Enabled = False
            cblapbarangkeluar.Checked = False
        End If
    End Sub

    Private Sub clblaputang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaputang.MouseDown
        Dim Index As Integer = clblaputang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaputang.SetItemChecked(Index, Not clblaputang.GetItemChecked(Index))

        For id As Integer = 0 To clblaputang.Items.Count - 1
            If clblaputang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaputang.Enabled = False
            cblaputang.Checked = False
        End If
    End Sub

    Private Sub clblappiutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappiutang.MouseDown
        Dim Index As Integer = clblappiutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappiutang.SetItemChecked(Index, Not clblappiutang.GetItemChecked(Index))

        For id As Integer = 0 To clblappiutang.Items.Count - 1
            If clblappiutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappiutang.Enabled = False
            cblappiutang.Checked = False
        End If
    End Sub

    Private Sub clblapstokbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapstokbarang.MouseDown
        Dim Index As Integer = clblapstokbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapstokbarang.SetItemChecked(Index, Not clblapstokbarang.GetItemChecked(Index))

        For id As Integer = 0 To clblapstokbarang.Items.Count - 1
            If clblapstokbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapstokbarang.Enabled = False
            cblapstokbarang.Checked = False
        End If
    End Sub

    Private Sub clblapakunmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapakunmasuk.MouseDown
        Dim Index As Integer = clblapakunmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapakunmasuk.SetItemChecked(Index, Not clblapakunmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
            If clblapakunmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapakunmasuk.Enabled = False
            cblapakunmasuk.Checked = False
        End If
    End Sub

    Private Sub clblapakunkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapakunkeluar.MouseDown
        Dim Index As Integer = clblapakunkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapakunkeluar.SetItemChecked(Index, Not clblapakunkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
            If clblapakunkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapakunkeluar.Enabled = False
            cblapakunkeluar.Checked = False
        End If
    End Sub

    Private Sub clblaptransferkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransferkas.MouseDown
        Dim Index As Integer = clblaptransferkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransferkas.SetItemChecked(Index, Not clblaptransferkas.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransferkas.Items.Count - 1
            If clblaptransferkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransferkas.Enabled = False
            cblaptransferkas.Checked = False
        End If
    End Sub

    Private Sub clblaptransferbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransferbarang.MouseDown
        Dim Index As Integer = clblaptransferbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransferbarang.SetItemChecked(Index, Not clblaptransferbarang.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
            If clblaptransferbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransferbarang.Enabled = False
            cblaptransferbarang.Checked = False
        End If
    End Sub

    Private Sub clblaptransaksikas_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransaksikas.MouseDown
        Dim Index As Integer = clblaptransaksikas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransaksikas.SetItemChecked(Index, Not clblaptransaksikas.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
            If clblaptransaksikas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransaksikas.Enabled = False
            cblaptransaksikas.Checked = False
        End If
    End Sub
End Class