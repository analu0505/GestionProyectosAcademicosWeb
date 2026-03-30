Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class dbEntrega
    Private ReadOnly cn As String = ConfigurationManager.ConnectionStrings("CN").ConnectionString

    Public Function Crear(en As Models.Entrega, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    INSERT INTO Entrega (IdProyecto, IdUsuario, Descripcion, NombreArchivo)
                    VALUES (@IdProyecto, @IdUsuario, @Descripcion, @NombreArchivo)
                ", conn)

                    cmd.Parameters.AddWithValue("@IdProyecto", en.IdProyecto)
                    cmd.Parameters.AddWithValue("@IdUsuario", en.IdUsuario)
                    cmd.Parameters.AddWithValue("@Descripcion", en.Descripcion)
                    cmd.Parameters.AddWithValue("@NombreArchivo", If(String.IsNullOrWhiteSpace(en.NombreArchivo), DBNull.Value, CType(en.NombreArchivo, Object)))

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

    ' Estudiante: solo ve sus propias entregas
    ' Profesor/Coordinador: ven todas las entregas del proyecto
    Public Function ListarPorProyecto(idProyecto As Integer, rol As String, idUsuario As Integer, ByRef errorMessage As String) As DataTable
        Try
            Dim dt As New DataTable()

            Using conn As New SqlConnection(cn)
                Dim sql As String

                If rol = "Estudiante" Then
                    sql = "
                        SELECT E.IdEntrega,
                               E.Descripcion,
                               E.NombreArchivo,
                               E.Calificacion,
                               E.Fecha,
                               U.Nombre AS Autor
                        FROM Entrega E
                        INNER JOIN Usuario U ON U.IdUsuario = E.IdUsuario
                        WHERE E.IdProyecto = @IdProyecto
                          AND E.IdUsuario = @IdUsuario
                        ORDER BY E.IdEntrega DESC
                    "
                Else
                    sql = "
                        SELECT E.IdEntrega,
                               E.Descripcion,
                               E.NombreArchivo,
                               E.Calificacion,
                               E.Fecha,
                               U.Nombre AS Autor
                        FROM Entrega E
                        INNER JOIN Usuario U ON U.IdUsuario = E.IdUsuario
                        WHERE E.IdProyecto = @IdProyecto
                        ORDER BY E.IdEntrega DESC
                    "
                End If

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@IdProyecto", idProyecto)

                    If rol = "Estudiante" Then
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario)
                    End If

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

    Public Function ActualizarCalificacion(idEntrega As Integer, calificacion As Decimal, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    UPDATE Entrega
                    SET Calificacion = @Calificacion
                    WHERE IdEntrega = @Id
                ", conn)

                    cmd.Parameters.AddWithValue("@Calificacion", calificacion)
                    cmd.Parameters.AddWithValue("@Id", idEntrega)

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
                Using cmd As New SqlCommand("DELETE FROM Entrega WHERE IdEntrega = @Id", conn)
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