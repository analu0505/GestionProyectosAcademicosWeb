Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class dbComentario
    Private ReadOnly cn As String = ConfigurationManager.ConnectionStrings("CN").ConnectionString

    Public Function Crear(c As Models.Comentario, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    INSERT INTO Comentario (IdProyecto, IdUsuario, Texto)
                    VALUES (@IdProyecto, @IdUsuario, @Texto)
                ", conn)

                    cmd.Parameters.AddWithValue("@IdProyecto", c.IdProyecto)
                    cmd.Parameters.AddWithValue("@IdUsuario", c.IdUsuario)
                    cmd.Parameters.AddWithValue("@Texto", c.Texto)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function

    Public Function ListarPorProyecto(idProyecto As Integer, ByRef errorMessage As String) As DataTable
        Try
            Dim dt As New DataTable()

            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    SELECT C.IdComentario, C.Texto, C.Fecha, U.Nombre AS Autor
                    FROM Comentario C
                    INNER JOIN Usuario U ON U.IdUsuario = C.IdUsuario
                    WHERE C.IdProyecto = @IdProyecto
                    ORDER BY C.IdComentario DESC
                ", conn)

                    cmd.Parameters.AddWithValue("@IdProyecto", idProyecto)

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            Return dt
        Catch ex As Exception
            errorMessage = ex.Message
            Return New DataTable()
        End Try
    End Function

    Public Function Actualizar(idComentario As Integer, texto As String, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    UPDATE Comentario
                    SET Texto = @Texto
                    WHERE IdComentario = @Id
                ", conn)

                    cmd.Parameters.AddWithValue("@Texto", texto)
                    cmd.Parameters.AddWithValue("@Id", idComentario)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function

    Public Function Eliminar(id As Integer, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("DELETE FROM Comentario WHERE IdComentario = @Id", conn)
                    cmd.Parameters.AddWithValue("@Id", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function
End Class