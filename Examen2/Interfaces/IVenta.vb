Imports System.Collections.Generic
Imports System.Data.SqlClient
Public Interface IVenta
    Function Insertar(venta As venta) As Integer
    Function BuscarPorID(id As Integer) As venta
    Function BuscarTodas() As List(Of venta)

    Function GeneraReporteVentas(venta As venta, cliente As Cliente)
End Interface

