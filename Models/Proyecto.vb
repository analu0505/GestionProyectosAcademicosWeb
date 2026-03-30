Namespace Models
    Public Class Proyecto
        Private _idProyecto As Integer
        Private _titulo As String
        Private _curso As String
        Private _estado As String
        Private _idUsuarioCreador As Integer

        Public Property IdProyecto As Integer
            Get
                Return _idProyecto
            End Get
            Set(value As Integer)
                _idProyecto = value
            End Set
        End Property

        Public Property Titulo As String
            Get
                Return _titulo
            End Get
            Set(value As String)
                _titulo = value
            End Set
        End Property

        Public Property Curso As String
            Get
                Return _curso
            End Get
            Set(value As String)
                _curso = value
            End Set
        End Property

        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                _estado = value
            End Set
        End Property

        Public Property IdUsuarioCreador As Integer
            Get
                Return _idUsuarioCreador
            End Get
            Set(value As Integer)
                _idUsuarioCreador = value
            End Set
        End Property
    End Class
End Namespace