Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Public Class VentaImpl
    Implements IVenta

    Public Function Insertar(venta As venta) As Integer Implements IVenta.Insertar
        Try
            Dim query As String = "INSERT INTO ventas (IDCliente, Fecha, Total) OUTPUT INSERTED.ID VALUES (@idcliente, @fecha, @total)"
            Dim parametros() As SqlParameter = {
                New SqlParameter("@idcliente", venta.idCliente),
                New SqlParameter("@fecha", venta.fecha),
                New SqlParameter("@total", venta.total)
            }
            Using conn As SqlConnection = ConexionBD.ObtenerConexion()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddRange(parametros)
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function BuscarPorID(id As Integer) As venta Implements IVenta.BuscarPorID
        Try
            Dim query As String = "SELECT * FROM ventas WHERE ID=@id"
            Dim parametros() As SqlParameter = {New SqlParameter("@id", id)}
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, parametros)
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New venta(row("ID"), row("IDCliente"), row("Fecha"), row("Total"))
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function BuscarTodas() As List(Of venta) Implements IVenta.BuscarTodas
        Try
            Dim ventas As New List(Of venta)()
            Dim query As String = "SELECT * FROM ventas ORDER BY Fecha DESC"
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, Nothing)
            For Each row As DataRow In dt.Rows
                ventas.Add(New venta(row("ID"), row("IDCliente"), row("Fecha"), row("Total")))
            Next
            Return ventas
        Catch ex As Exception
            Return New List(Of venta)()
        End Try
    End Function

    Public Function GeneraReporteVentas(venta As venta, cliente As Cliente) As Object Implements IVenta.GeneraReporteVentas
        Try
            Dim reporte As String = "----- REPORTE DE VENTA -----" & Environment.NewLine
            reporte &= $"ID Venta: {venta.id}" & Environment.NewLine
            reporte &= $"Cliente: {cliente.Nombre} (ID: {cliente.ID})" & Environment.NewLine
            reporte &= $"Fecha: {venta.fecha}" & Environment.NewLine
            reporte &= $"Total: ${venta.total}" & Environment.NewLine
            reporte &= "---------------------------"
            Return reporte
        Catch ex As Exception
            Return "Error generando reporte."
        End Try
    End Function
    Public Function CalcularTotalVentasMensuales() As Dictionary(Of String, Decimal)
        Dim totalesMensuales As New Dictionary(Of String, Decimal)()
        Try
            Dim query As String = "SELECT FORMAT(Fecha, 'yyyy-MM') AS Mes, SUM(Total) AS TotalVentas FROM ventas GROUP BY FORMAT(Fecha, 'yyyy-MM') ORDER BY Mes"
            Dim dt As DataTable = ConexionBD.EjecutarConsulta(query, Nothing)
            For Each row As DataRow In dt.Rows
                Dim mes As String = row("Mes").ToString()
                Dim total As Decimal = Convert.ToDecimal(row("TotalVentas"))
                totalesMensuales(mes) = total
            Next
        Catch ex As Exception
            ' Manejo de errores si es necesario
        End Try
        Return totalesMensuales
    End Function
End Class
