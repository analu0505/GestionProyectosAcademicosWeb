Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Namespace Utils

    Public Class DbHelper

        Private ReadOnly cn As String =
            ConfigurationManager.ConnectionStrings("CN").ConnectionString


        '========================================
        ' SELECT -> devuelve DataTable
        '========================================
        Public Function ExecuteQuery(query As String,
                                     parametros As Dictionary(Of String, Object),
                                     ByRef errorMessage As String) As DataTable

            Try

                Dim dt As New DataTable()

                Using conn As New SqlConnection(cn)

                    Using cmd As New SqlCommand(query, conn)

                        If parametros IsNot Nothing Then
                            For Each p In parametros
                                cmd.Parameters.AddWithValue(p.Key, If(p.Value, DBNull.Value))
                            Next
                        End If

                        Using da As New SqlDataAdapter(cmd)
                            da.Fill(dt)
                        End Using

                    End Using

                End Using

                Return dt

            Catch ex As Exception
                errorMessage = ex.Message
                Return Nothing
            End Try

        End Function


        '========================================
        ' INSERT / UPDATE / DELETE
        '========================================
        Public Function ExecuteNonQuery(query As String,
                                        parametros As Dictionary(Of String, Object),
                                        ByRef errorMessage As String) As Boolean

            Try

                Using conn As New SqlConnection(cn)

                    Using cmd As New SqlCommand(query, conn)

                        If parametros IsNot Nothing Then
                            For Each p In parametros
                                cmd.Parameters.AddWithValue(p.Key, If(p.Value, DBNull.Value))
                            Next
                        End If

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


        '========================================
        ' SCALAR (COUNT, IDENTITY, etc)
        '========================================
        Public Function ExecuteScalar(query As String,
                                      parametros As Dictionary(Of String, Object),
                                      ByRef errorMessage As String) As Object

            Try

                Using conn As New SqlConnection(cn)

                    Using cmd As New SqlCommand(query, conn)

                        If parametros IsNot Nothing Then
                            For Each p In parametros
                                cmd.Parameters.AddWithValue(p.Key, If(p.Value, DBNull.Value))
                            Next
                        End If

                        conn.Open()
                        Return cmd.ExecuteScalar()

                    End Using

                End Using

            Catch ex As Exception
                errorMessage = ex.Message
                Return Nothing
            End Try

        End Function

    End Class

End Namespace