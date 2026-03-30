Imports GestionProyectosAcademicosWeb.Utils

Public Class dbUsuario
    Private db As New DbHelper()

    Public Function CrearUsuario(u As Models.Usuario, ByRef errorMessage As String) As Boolean
        Dim q As String = "
            INSERT INTO Usuario (Nombre, Correo, Clave, Rol)
            VALUES (@Nombre, @Correo, @Clave, @Rol)
        "
        Dim p As New Dictionary(Of String, Object) From {
            {"@Nombre", u.Nombre},
            {"@Correo", u.Correo},
            {"@Clave", u.Clave},
            {"@Rol", u.Rol}
        }
        Return db.ExecuteNonQuery(q, p, errorMessage)
    End Function

    Public Function ListarUsuarios(ByRef errorMessage As String) As DataTable
        Dim q As String = "SELECT IdUsuario, Nombre, Correo, Rol FROM Usuario ORDER BY IdUsuario DESC"
        Return db.ExecuteQuery(q, Nothing, errorMessage)
    End Function

    Public Function ActualizarUsuario(u As Models.Usuario, ByRef errorMessage As String) As Boolean
        Dim q As String = "
            UPDATE Usuario
            SET Nombre=@Nombre, Correo=@Correo, Rol=@Rol
            WHERE IdUsuario=@Id
        "
        Dim p As New Dictionary(Of String, Object) From {
            {"@Nombre", u.Nombre},
            {"@Correo", u.Correo},
            {"@Rol", u.Rol},
            {"@Id", u.IdUsuario}
        }
        Return db.ExecuteNonQuery(q, p, errorMessage)
    End Function

    Public Function EliminarUsuario(id As Integer, ByRef errorMessage As String) As Boolean
        Dim q As String = "DELETE FROM Usuario WHERE IdUsuario=@Id"
        Dim p As New Dictionary(Of String, Object) From {{"@Id", id}}
        Return db.ExecuteNonQuery(q, p, errorMessage)
    End Function

    Public Function Login(correo As String, clave As String, ByRef errorMessage As String) As DataRow
        Dim q As String = "
            SELECT TOP 1 IdUsuario, Nombre, Correo, Rol
            FROM Usuario
            WHERE Correo=@Correo AND Clave=@Clave
        "
        Dim p As New Dictionary(Of String, Object) From {
            {"@Correo", correo},
            {"@Clave", clave}
        }
        Dim dt = db.ExecuteQuery(q, p, errorMessage)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then Return dt.Rows(0)
        Return Nothing
    End Function
End Class