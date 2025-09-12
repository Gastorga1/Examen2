Public Class Producto

    Private _id As Integer
    Private _categoria As String
    Private _precio As Integer
    Private _nombre As String

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, categoria As String, Precio As Integer, nombre As String)
        _id = id
        _categoria = categoria
        _precio = Precio
        _nombre = nombre

    End Sub

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value

        End Set
    End Property

    Public Property Categoria() As String
        Get
            Return _categoria

        End Get
        Set(value As String)
            _categoria = value


        End Set
    End Property
    Public Property precio() As Integer
        Get
            Return _precio

        End Get
        Set(value As Integer)
            _precio = value

        End Set
    End Property

    Public Property Nombre()
        Get
            Return _nombre

        End Get
        Set(value)
            _nombre = value

        End Set
    End Property

End Class
