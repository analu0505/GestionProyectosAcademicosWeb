Namespace Models
    Public Class Comentario
        Private _idComentario As Integer
        Private _idProyecto As Integer
        Private _idUsuario As Integer
        Private _texto As String

        Public Property IdComentario As Integer
            Get
                Return _idComentario
            End Get
            Set(value As Integer)
                _idComentario = value
            End Set
        End Property

        Public Property IdProyecto As Integer
            Get
                Return _idProyecto
            End Get
            Set(value As Integer)
                _idProyecto = value
            End Set
        End Property

        Public Property IdUsuario As Integer
            Get
                Return _idUsuario
            End Get
            Set(value As Integer)
                _idUsuario = value
            End Set
        End Property

        Public Property Texto As String
            Get
                Return _texto
            End Get
            Set(value As String)
                _texto = value
            End Set
        End Property
    End Class
End Namespace