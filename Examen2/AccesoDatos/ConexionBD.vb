Imports System.Data.SqlClient
Imports System.Data

Public Class ConexionBD
    ' ConnectionString temporal - CAMBIAR por tu servidor
    Private Shared ReadOnly connectionString As String = "Data Source=(local);Initial Catalog=pruebademo;Integrated Security=true;TrustServerCertificate=true"

    Public Shared Function ObtenerConexion() As SqlConnection
        Return New SqlConnection(connectionString)
    End Function

    Public Shared Function EjecutarConsulta(query As String, parametros As SqlParameter()) As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = ObtenerConexion()
            Using cmd As New SqlCommand(query, conn)
                If parametros IsNot Nothing Then
                    cmd.Parameters.AddRange(parametros)
                End If
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function EjecutarComando(query As String, parametros As SqlParameter()) As Integer
        Using conn As SqlConnection = ObtenerConexion()
            Using cmd As New SqlCommand(query, conn)
                If parametros IsNot Nothing Then
                    cmd.Parameters.AddRange(parametros)
                End If
                conn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function
End Class