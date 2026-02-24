Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Proyectos
    Inherits System.Web.UI.Page

    Private ReadOnly cn As String =
        ConfigurationManager.ConnectionStrings("CN").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Cargar()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    INSERT INTO Proyecto (Titulo, Curso, Estado)
                    VALUES (@Titulo, @Curso, @Estado)
                ", conn)

                    cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim())
                    cmd.Parameters.AddWithValue("@Curso", txtCurso.Text.Trim())
                    cmd.Parameters.AddWithValue("@Estado", ddlEstado.SelectedValue)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            lblMensaje.ForeColor = Drawing.Color.Green
            lblMensaje.Text = "Insertado."

            txtTitulo.Text = ""
            txtCurso.Text = ""
            ddlEstado.SelectedIndex = 0

            Cargar()

        Catch ex As Exception
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Cargar()
    End Sub

    Private Sub Cargar()
        Try
            Dim dt As New DataTable()

            Using conn As New SqlConnection(cn)
                Using cmd As New SqlCommand("
                    SELECT IdProyecto, Titulo, Curso, Estado
                    FROM Proyecto
                    ORDER BY IdProyecto DESC
                ", conn)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            gvProyectos.DataSource = dt
            gvProyectos.DataBind()

        Catch ex As Exception
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Error al cargar: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProyectos_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ELIMINAR" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)

            Try
                Using conn As New SqlConnection(cn)
                    Using cmd As New SqlCommand("DELETE FROM Proyecto WHERE IdProyecto=@Id", conn)
                        cmd.Parameters.AddWithValue("@Id", id)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                lblMensaje.ForeColor = Drawing.Color.Green
                lblMensaje.Text = "Eliminado."
                Cargar()

            Catch ex As Exception
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Error al eliminar: " & ex.Message
            End Try
        End If
    End Sub
End Class