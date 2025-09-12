Public Class VentaItem
    Private _id As Integer
    Private _idVenta As Integer
    Private _idProducto As Integer
    Private _precioUnitario As Decimal
    Private _cantidad As Integer
    Private _precioTotal As Decimal
    Private _producto As Producto

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer)
        Me._id = id
        Me._idProducto = idProducto
        Me._cantidad = cantidad
        Me._idVenta = idVenta
        Me._precioUnitario = precioUnitario
    End Sub
    Public Sub CalcularPrecioTotal()
        _precioTotal = _precioUnitario * _cantidad
    End Sub

    Public Overrides Function ToString() As String
        Dim nombreProducto As String = If(ProductoObj IsNot Nothing, ProductoObj.Nombre, "Producto")
        Return $"{nombreProducto} - Cant: {Cantidad} - Total: ${PrecioTotal}"
    End Function
End Class
