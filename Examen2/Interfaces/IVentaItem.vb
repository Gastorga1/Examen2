Imports System.Collections.Generic
Imports System.Data.SqlClient
Public Interface IVentaItem
    Function Insertar(item As VentaItem) As Boolean
    Function BuscarPorVenta(idVenta As Integer) As List(Of VentaItem)
    Function EliminarPorVenta(idVenta As Integer) As Boolean
End Interface
