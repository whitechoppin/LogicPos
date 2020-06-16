Module validasi
    Public isicari As String = ""
    Public isicari2 As String = ""
    Public tutupcaribarang As Integer
    Public kodes As String
    'String Pencarian kode gudang
    Public kodegudangcari As String
    'master
    Public tutupcus As Integer
    Public tutupsup As Integer
    Public tutupcaristok As Integer
    Public tutupgudang As Integer
    Public tutupkas As Integer
    '======================
    'transaksi
    Public tutupbeli As Integer
    Public tutupjual As Integer

    Public tutupreturbeli As Integer
    Public tutupreturjual As Integer
    'variable text go pada barang masuk dan barang keluar
    Public tutupcaribarangmasuk As Integer
    Public tutupcaribarangkeluar As Integer

    'variable text go pada transfer barang
    Public tutupcaritransferbarang As Integer

    '======================
    'pelunasan jual dan beli
    'variabel untuk buka pencarian nota penjualan dan pembelian pada form pelunasan utang dan piutang
    Public tutuplunasbeli As Integer
    Public kodelunassupplier As String
    Public tutuplunasjual As Integer
    Public kodelunascustomer As String
    'variabel text go pada pelunasan penjualan dan pelunasan pembelian
    Public tutupcaripelunasanutang As Integer
    Public tutupcaripelunasanpiutang As Integer
    '======================
    Public msgbox1 As Integer
    Public passwordid As Integer
End Module
