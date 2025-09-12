Imports System.Collections.Generic
Imports System.Data.SqlClient
Public Interface IProducto
    Function Insertar(producto As Producto) As Boolean
    Function Actualizar(producto As Producto) As Boolean
    Function Eliminar(id As Integer) As Boolean
    Function BuscarPorID(id As Integer) As Producto
    Function BuscarTodos() As List(Of Producto)
    Function BuscarPorCategoria(categoria As String) As List(Of Producto)
End Interface
