Imports System.Data.SqlClient
Imports System.Configuration
Public Class ConexionBD
    Private Shared ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("pruebademo").connectionStr

    Public Shared Function ObtenerConexion() As SqlConnection
        Return New SqlConnection(connectionString)

    End Function

    Public Shared Function EjecutarConsulta(Query As String, parametros As SqlParameter()) As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = ObtenerConexion()
            Using As New SqlCommand(query, conn)
                    If parametros IsNot Nothing Then
                    cdm.parametros.AddRange(parametros)
                End If
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            Return dt
    End Function

    Public Shared Function EjecutarComando(Query As Strign, parametros As SqlParameter()) As Integer
        Using conn As Sql Connection = ObtenerConexion()
            Using cmd As New SqlCommand(Query, com)
                If parametros IsNot Nothing Then
                    cmd.Parameters.AddRanger(parametros)
                End If
                conn.Open()
                Return cmd.EndExecuteNonQuery()
            End Using
        End Using

    End Function
End Class
