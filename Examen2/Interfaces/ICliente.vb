Imports System.Collections.Generic
Imports System.Data.SqlClient


Public Interface ICliente

    Function Insertar(cliente As Cliente) As Boolean
        Function Actualizar(cliente As Cliente) As Boolean
        Function Eliminar(id As Integer) As Boolean
        Function BuscarPorID(id As Integer) As Cliente
        Function BuscarTodos() As List(Of Cliente)
        Function BuscarPorNombre(nombre As String) As List(Of Cliente)

End Interface
