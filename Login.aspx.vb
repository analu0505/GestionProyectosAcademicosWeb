Imports GestionProyectosAcademicosWeb.Utils

Public Class Login
    Inherits System.Web.UI.Page

    Private dbU As New dbUsuario()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Rol") IsNot Nothing Then
            Response.Redirect("Proyectos.aspx")
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim err As String = ""

        If Not Page.IsValid Then
            SwalUtils.ShowSwal(Me, "Atención", "Complete los campos requeridos.", "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtCorreo.Text) OrElse String.IsNullOrWhiteSpace(txtClave.Text) Then
            SwalUtils.ShowSwal(Me, "Atención", "Correo y clave son obligatorios.", "warning")
            Exit Sub
        End If

        Dim row = dbU.Login(txtCorreo.Text.Trim(), txtClave.Text.Trim(), err)

        If row Is Nothing Then
            SwalUtils.ShowSwal(Me, "Error", "Credenciales incorrectas.", "error")
            Exit Sub
        End If

        Session("IdUsuario") = Convert.ToInt32(row("IdUsuario"))
        Session("Nombre") = row("Nombre").ToString()
        Session("Rol") = row("Rol").ToString()

        Response.Redirect("Proyectos.aspx")
    End Sub
End Class