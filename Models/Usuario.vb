Namespace Models
    Public Class Usuario
        Private _idUsuario As Integer
        Private _nombre As String
        Private _correo As String
        Private _clave As String
        Private _rol As String

        Public Property IdUsuario As Integer
            Get
                Return _idUsuario
            End Get
            Set(value As Integer)
                _idUsuario = value
            End Set
        End Property

        Public Property Nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        Public Property Correo As String
            Get
                Return _correo
            End Get
            Set(value As String)
                _correo = value
            End Set
        End Property

        Public Property Clave As String
            Get
                Return _clave
            End Get
            Set(value As String)
                _clave = value
            End Set
        End Property

        Public Property Rol As String
            Get
                Return _rol
            End Get
            Set(value As String)
                _rol = value
            End Set
        End Property
    End Class
End Namespace