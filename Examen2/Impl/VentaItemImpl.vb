Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Public Class VentaItemImpl
    Implements IVentaItem

    Public Function Insertar(item As VentaItem) As Boolean Implements IVentaItem.Insertar
        Try
            Dim query As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@idventa, @idproducto, @preciounitario, @cantidad, @preciototal)"
            Dim parametros() As SqlParameter = {
                New SqlParameter("@idventa", item.IdVenta),
                New SqlParameter("@idproducto", item.IdProducto),
                New SqlParameter("@preciounitario", item.PrecioUnitario),
                New SqlParameter("@cantidad", item.Cantidad),
                New SqlParameter("@preciototal", item.PrecioTotal)
            }
            Return ConexionBD.EjecutarComando(query, parametros) > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function BuscarPorVenta(idVenta As Integer) As List(Of VentaItem) Implements IVentaItem.BuscarPorVenta
        Try
            Dim items As New List(Of VentaItem)()
            Dim query As String = "SELECT * FROM ventasitems WHERE IDVenta=@idventa"
            Dim parametros() As SqlParameter = {New SqlParameter("@idventa", idVenta)}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)
            For Each row As DataRow In dt.Rows
                items.Add(New VentaItem(row("ID"), row("IDVenta"), row("IDProducto"), row("PrecioUnitario"), row("Cantidad")))
            Next
            Return items
        Catch ex As Exception
            Return New List(Of VentaItem)()
        End Try
    End Function

    Public Function EliminarPorVenta(idVenta As Integer) As Boolean Implements IVentaItem.EliminarPorVenta
        Try
            Dim query As String = "DELETE FROM ventasitems WHERE IDVenta=@idventa"
            Dim parametros() As SqlParameter = {New SqlParameter("@idventa", idVenta)}
            Return ConexionBD.EjecutarComando(query, parametros) >= 0
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
