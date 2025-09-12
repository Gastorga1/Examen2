Public Class venta
    Private _id As Integer
    Private _idCliente As Integer
    Private _fecha As String
    Private _total As String

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, idCliente As Integer, fecha As String, total As String)
        Me._id = id
        Me._idCliente = idCliente
        Me._fecha = fecha
        Me._total = total

    End Sub

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            id = value


        End Set
    End Property
    Public Property idCliente() As Integer
        Get
            Return _idCliente

        End Get
        Set(value As Integer)
            idCliente = value

        End Set
    End Property
    Public Property fecha() As String
        Get
            Return _fecha
        End Get
        Set(value As String)
            idCliente = value
        End Set
    End Property
    Public Property total() As String
        Get
            Return _total
        End Get
        Set(value As String)
            _total = value
        End Set
    End Property





End Class
