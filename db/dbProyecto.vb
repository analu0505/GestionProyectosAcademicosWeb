Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class dbProyecto

    Private ReadOnly cn As String = ConfigurationManager.ConnectionStrings("CN").ConnectionString

    Public Function CrearProyecto(p As Models.Proyecto, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    INSERT INTO Proyecto (Titulo, Curso, Estado, IdUsuarioCreador)
                    VALUES (@Titulo, @Curso, @Estado, @IdUsuario)
                ", conn)

                    cmd.Parameters.AddWithValue("@Titulo", p.Titulo)
                    cmd.Parameters.AddWithValue("@Curso", p.Curso)
                    cmd.Parameters.AddWithValue("@Estado", p.Estado)
                    cmd.Parameters.AddWithValue("@IdUsuario", p.IdUsuarioCreador)

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

    Public Function Listar(rol As String, idUsuario As Integer, ByRef errorMessage As String) As DataTable
        Try
            Dim dt As New DataTable()

            Using conn As New SqlConnection(cn)
                Dim sql As String = "
                    SELECT P.IdProyecto,
                           P.Titulo,
                           P.Curso,
                           P.Estado,
                           U.Nombre AS Creador
                    FROM Proyecto P
                    INNER JOIN Usuario U ON U.IdUsuario = P.IdUsuarioCreador
                    ORDER BY P.IdProyecto DESC
                "

                Using cmd As New SqlCommand(sql, conn)
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

    Public Function Actualizar(p As Models.Proyecto, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    UPDATE Proyecto
                    SET Titulo = @Titulo,
                        Curso = @Curso,
                        Estado = @Estado
                    WHERE IdProyecto = @Id
                ", conn)

                    cmd.Parameters.AddWithValue("@Titulo", p.Titulo)
                    cmd.Parameters.AddWithValue("@Curso", p.Curso)
                    cmd.Parameters.AddWithValue("@Estado", p.Estado)
                    cmd.Parameters.AddWithValue("@Id", p.IdProyecto)

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
                Using cmd As New SqlCommand("DELETE FROM Proyecto WHERE IdProyecto = @Id", conn)
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

    Public Function ListarParaCombo(rol As String, idUsuario As Integer, ByRef errorMessage As String) As DataTable
        Try
            Dim dt As New DataTable()

            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    SELECT IdProyecto, Titulo
                    FROM Proyecto
                    ORDER BY IdProyecto DESC
                ", conn)

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

End Class