Namespace Models
    Public Class Entrega
        Private _idEntrega As Integer
        Private _idProyecto As Integer
        Private _idUsuario As Integer
        Private _descripcion As String
        Private _nombreArchivo As String
        Private _calificacion As Decimal?

        Public Property IdEntrega As Integer
            Get
                Return _idEntrega
            End Get
            Set(value As Integer)
                _idEntrega = value
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

        Public Property Descripcion As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property NombreArchivo As String
            Get
                Return _nombreArchivo
            End Get
            Set(value As String)
                _nombreArchivo = value
            End Set
        End Property

        Public Property Calificacion As Decimal?
            Get
                Return _calificacion
            End Get
            Set(value As Decimal?)
                _calificacion = value
            End Set
        End Property
    End Class
End Namespace