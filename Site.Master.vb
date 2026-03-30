Public Class SiteMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' Ocultar navbar en Login
        Dim nombrePagina As String = System.IO.Path.GetFileName(Request.Path).ToLower()

        If nombrePagina = "login.aspx" Then
            pnlNavbar.Visible = False
            Return
        End If

        If Session("Nombre") IsNot Nothing AndAlso Session("Rol") IsNot Nothing Then
            lblUsuario.Text = Session("Nombre").ToString() & " (" & Session("Rol").ToString() & ")"

            ' Solo Coordinador ve Usuarios
            If Session("Rol").ToString() <> "Coordinador" Then
                liUsuarios.Visible = False
            Else
                liUsuarios.Visible = True
            End If
        Else
            lblUsuario.Text = ""
            liUsuarios.Visible = False
        End If

    End Sub
End Class