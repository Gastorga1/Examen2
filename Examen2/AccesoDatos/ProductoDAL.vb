Imports System.Data.SqlClient

Public Class ProductoDAL
    Public Shared Function Insertar(producto As Producto) As Boolean
        Try
            Dim query As String = "INSERT INTO productos (Nombre, Precio, Categoria) VALUES (@nombre, @precio, @categoria)"
            Dim parametros() As SqlParameter = {
                    New SqlParameter("@nombre", producto.nombre),
                    New SqlParameter("@precio", producto.precio),
                    New SqlParameter("@categoria", producto.Categoria)
                }
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Actualizar(producto As Producto) As Boolean
        Try
            Dim query As String = "UPDATE productos SET Nombre=@nombre, Precio=@precio, Categoria=@categoria WHERE ID=@id"
            Dim parametros() As SqlParameter = {
                    New SqlParameter("@nombre", producto.nombre),
                    New SqlParameter("@precio", producto.precio),
                    New SqlParameter("@categoria", producto.Categoria),
                    New SqlParameter("@id", producto.id)
                }
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Eliminar(id As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM productos WHERE ID=@id"
            Dim parametros() As SqlParameter = {New SqlParameter("@id", id)}
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function BuscarPorID(id As Integer) As Producto
        Try
            Dim query As String = "SELECT * FROM productos WHERE ID=@id"
            Dim parametros() As SqlParameter = {New SqlParameter("@id", id)}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New Producto(row("ID"), row("Nombre"), row("Precio"), row("Categoria"))
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function BuscarTodos() As List(Of Producto)
        Try
            Dim productos As New List(Of Producto)()
            Dim query As String = "SELECT * FROM productos ORDER BY Nombre"
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, Nothing)

            For Each row As DataRow In dt.Rows
                productos.Add(New Producto(row("ID"), row("Nombre"), row("Precio"), row("Categoria")))
            Next
            Return productos
        Catch ex As Exception
            Return New List(Of Producto)()
        End Try
    End Function

    Public Shared Function BuscarPorCategoria(categoria As String) As List(Of Producto)
        Try
            Dim productos As New List(Of Producto)()
            Dim query As String = "SELECT * FROM productos WHERE Categoria LIKE @categoria ORDER BY Nombre"
            Dim parametros() As SqlParameter = {New SqlParameter("@categoria", "%" & categoria & "%")}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)

            For Each row As DataRow In dt.Rows
                productos.Add(New Producto(row("ID"), row("Nombre"), row("Precio"), row("Categoria")))
            Next
            Return productos
        Catch ex As Exception
            Return New List(Of Producto)()
        End Try
    End Function
End Class
