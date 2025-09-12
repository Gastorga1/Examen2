Public Class VentaItem
    Private _id As Integer
    Private _idVenta As Integer
    Private _idProducto As Integer
    Private _precioUnitario As Decimal
    Private _cantidad As Integer
    Private _precioTotal As Decimal
    Private _producto As Producto

    ' Constructor vacío
    Public Sub New()
    End Sub

    ' Constructor con parámetros
    Public Sub New(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer)
        Me._id = id
        Me._idProducto = idProducto
        Me._cantidad = cantidad
        Me._idVenta = idVenta
        Me._precioUnitario = precioUnitario
        ' Calcular precio total automáticamente
        CalcularPrecioTotal()
    End Sub

    ' Propiedades Get/Set
    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property IdVenta As Integer
        Get
            Return _idVenta
        End Get
        Set(value As Integer)
            _idVenta = value
        End Set
    End Property

    Public Property IdProducto As Integer
        Get
            Return _idProducto
        End Get
        Set(value As Integer)
            _idProducto = value
        End Set
    End Property

    Public Property PrecioUnitario As Decimal
        Get
            Return _precioUnitario
        End Get
        Set(value As Decimal)
            _precioUnitario = value
            ' Recalcular precio total cuando cambie el precio unitario
            CalcularPrecioTotal()
        End Set
    End Property

    Public Property Cantidad As Integer
        Get
            Return _cantidad
        End Get
        Set(value As Integer)
            _cantidad = value
            ' Recalcular precio total cuando cambie la cantidad
            CalcularPrecioTotal()
        End Set
    End Property

    Public Property PrecioTotal As Decimal
        Get
            Return _precioTotal
        End Get
        Set(value As Decimal)
            _precioTotal = value
        End Set
    End Property

    Public Property ProductoObj As Producto
        Get
            Return _producto
        End Get
        Set(value As Producto)
            _producto = value
        End Set
    End Property

    ' Método para calcular precio total (CORREGIDO)
    Public Sub CalcularPrecioTotal()
        _precioTotal = _precioUnitario * _cantidad
    End Sub

    ' ToString mejorado
    Public Overrides Function ToString() As String
        Dim nombreProducto As String = If(ProductoObj IsNot Nothing, ProductoObj.Nombre, "Producto")
        Return $"{nombreProducto} - Cant: {Cantidad} - Total: ${PrecioTotal:F2}"
    End Function
End Class