Imports GestionProyectosAcademicosWeb.Utils

Public Class Usuarios
    Inherits System.Web.UI.Page

    Private dbU As New dbUsuario()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Rol") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        If Session("Rol").ToString() <> "Coordinador" Then
            Response.Redirect("Proyectos.aspx")
        End If

        If Not IsPostBack Then
            Cargar()
        End If
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs)
        Dim err As String = ""

        If Not Page.IsValid Then
            SwalUtils.ShowSwal(Me, "Atención", "Complete los campos requeridos.", "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtNombre.Text) OrElse
           String.IsNullOrWhiteSpace(txtCorreo.Text) OrElse
           String.IsNullOrWhiteSpace(txtClave.Text) Then

            SwalUtils.ShowSwal(Me, "Atención", "No deje campos vacíos.", "warning")
            Exit Sub
        End If

        Dim u As New Models.Usuario With {
            .Nombre = txtNombre.Text.Trim(),
            .Correo = txtCorreo.Text.Trim(),
            .Clave = txtClave.Text.Trim(),
            .Rol = ddlRol.SelectedValue
        }

        If dbU.CrearUsuario(u, err) Then
            txtNombre.Text = ""
            txtCorreo.Text = ""
            txtClave.Text = ""
            ddlRol.SelectedIndex = 0

            Cargar()
            SwalUtils.ShowSwal(Me, "Éxito", "Usuario creado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo crear el usuario.", err), "error")
        End If
    End Sub

    Private Sub Cargar()
        Dim err As String = ""
        gvUsuarios.DataSource = dbU.ListarUsuarios(err)
        gvUsuarios.DataBind()
    End Sub

    Protected Sub gvUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvUsuarios.EditIndex = e.NewEditIndex
        Cargar()
    End Sub

    Protected Sub gvUsuarios_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvUsuarios.EditIndex = -1
        Cargar()
    End Sub

    Protected Sub gvUsuarios_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim err As String = ""
        Dim id As Integer = Convert.ToInt32(gvUsuarios.DataKeys(e.RowIndex).Value)
        Dim row = gvUsuarios.Rows(e.RowIndex)

        Dim nombre As String = CType(row.Cells(1).Controls(0), TextBox).Text.Trim()
        Dim correo As String = CType(row.Cells(2).Controls(0), TextBox).Text.Trim()
        Dim rol As String = CType(row.Cells(3).Controls(0), TextBox).Text.Trim()

        If String.IsNullOrWhiteSpace(nombre) OrElse
           String.IsNullOrWhiteSpace(correo) OrElse
           String.IsNullOrWhiteSpace(rol) Then

            SwalUtils.ShowSwal(Me, "Atención", "No deje campos vacíos.", "warning")
            Exit Sub
        End If

        Dim u As New Models.Usuario With {
            .IdUsuario = id,
            .Nombre = nombre,
            .Correo = correo,
            .Rol = rol
        }

        If dbU.ActualizarUsuario(u, err) Then
            gvUsuarios.EditIndex = -1
            Cargar()
            SwalUtils.ShowSwal(Me, "Actualizado", "Usuario actualizado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo actualizar el usuario.", err), "error")
        End If
    End Sub

    Protected Sub gvUsuarios_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim err As String = ""
        Dim id As Integer = Convert.ToInt32(gvUsuarios.DataKeys(e.RowIndex).Value)

        If dbU.EliminarUsuario(id, err) Then
            Cargar()
            SwalUtils.ShowSwal(Me, "Eliminado", "Usuario eliminado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo eliminar el usuario.", err), "error")
        End If
    End Sub
End Class