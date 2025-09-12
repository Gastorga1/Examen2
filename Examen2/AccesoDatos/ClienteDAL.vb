Imports System.Data.SqlClient

Public Class ClienteDAL
    Public Shared Function Insertar(cliente As Cliente) As Boolean
        Try
            Dim query As String = "INSERT INTO clientes (Cliente, Telefono, Correo) VALUES (@cliente, @telefono, @correo)"
            Dim parametros() As SqlParameter = {
                New SqlParameter("@cliente", cliente.Nombre),
                New SqlParameter("@telefono", cliente.Telefono),
                New SqlParameter("@correo", cliente.Correo)
            }
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Actualizar(cliente As Cliente) As Boolean
        Try
            Dim query As String = "UPDATE clientes SET Cliente=@cliente, Telefono=@telefono, Correo=@correo WHERE ID=@id"
            Dim parametros() As SqlParameter = {
                New SqlParameter("@cliente", cliente.Nombre),
                New SqlParameter("@telefono", cliente.Telefono),
                New SqlParameter("@correo", cliente.Correo),
                New SqlParameter("@id", cliente.ID)
            }
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Eliminar(id As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM clientes WHERE ID=@id"
            Dim parametros() As SqlParameter = {New SqlParameter("@id", id)}
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function BuscarPorID(id As Integer) As Cliente
        Try
            Dim query As String = "SELECT * FROM clientes WHERE ID=@id"
            Dim parametros() As SqlParameter = {New SqlParameter("@id", id)}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New Cliente(row("ID"), row("Cliente"), row("Telefono"), row("Correo"))
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function BuscarTodos() As List(Of Cliente)
        Try
            Dim clientes As New List(Of Cliente)()
            Dim query As String = "SELECT * FROM clientes ORDER BY Cliente"
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, Nothing)

            For Each row As DataRow In dt.Rows
                clientes.Add(New Cliente(row("ID"), row("Cliente"), row("Telefono"), row("Correo")))
            Next
            Return clientes
        Catch ex As Exception
            Return New List(Of Cliente)()
        End Try
    End Function

    Public Shared Function BuscarPorNombre(nombre As String) As List(Of Cliente)
        Try
            Dim clientes As New List(Of Cliente)()
            Dim query As String = "SELECT * FROM clientes WHERE Cliente LIKE @nombre ORDER BY Cliente"
            Dim parametros() As SqlParameter = {New SqlParameter("@nombre", "%" & nombre & "%")}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)

            For Each row As DataRow In dt.Rows
                clientes.Add(New Cliente(row("ID"), row("Cliente"), row("Telefono"), row("Correo")))
            Next
            Return clientes
        Catch ex As Exception
            Return New List(Of Cliente)()
        End Try
    End Function
End Class