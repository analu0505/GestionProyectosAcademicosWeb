Imports GestionProyectosAcademicosWeb.Utils

Public Class Comentarios
    Inherits System.Web.UI.Page

    Private dbP As New dbProyecto()
    Private dbC As New dbComentario()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Rol") Is Nothing OrElse Session("IdUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        Dim rol As String = Session("Rol").ToString()

        ' Solo Profesor y Coordinador agregan observaciones
        pnlAgregarComentario.Visible = (rol = "Profesor" OrElse rol = "Coordinador")

        If Not IsPostBack Then
            CargarProyectosCombo()
            CargarComentarios()
        End If
    End Sub

    Private Sub CargarProyectosCombo()
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()
        Dim idU As Integer = Convert.ToInt32(Session("IdUsuario"))

        Dim dt = dbP.ListarParaCombo(rol, idU, err)

        ddlProyecto.DataSource = dt
        ddlProyecto.DataTextField = "Titulo"
        ddlProyecto.DataValueField = "IdProyecto"
        ddlProyecto.DataBind()
    End Sub

    Private Sub CargarComentarios()
        If ddlProyecto.Items.Count = 0 Then
            gvComentarios.DataSource = Nothing
            gvComentarios.DataBind()
            Exit Sub
        End If

        Dim err As String = ""
        Dim idProyecto As Integer = Convert.ToInt32(ddlProyecto.SelectedValue)

        gvComentarios.DataSource = dbC.ListarPorProyecto(idProyecto, err)
        gvComentarios.DataBind()
    End Sub

    Protected Sub gvComentarios_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rol As String = Session("Rol").ToString()
            Dim btnEditar As LinkButton = TryCast(e.Row.FindControl("btnEditar"), LinkButton)
            Dim btnEliminar As LinkButton = TryCast(e.Row.FindControl("btnEliminar"), LinkButton)

            Dim puedeEditar As Boolean = (rol = "Profesor" OrElse rol = "Coordinador")

            If btnEditar IsNot Nothing Then btnEditar.Visible = puedeEditar
            If btnEliminar IsNot Nothing Then btnEliminar.Visible = puedeEditar
        End If
    End Sub

    Protected Sub ddlProyecto_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarComentarios()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden agregar observaciones.", "warning")
            Exit Sub
        End If

        If Not Page.IsValid Then
            SwalUtils.ShowSwal(Me, "Atención", "Escriba una observación.", "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtTexto.Text) Then
            SwalUtils.ShowSwal(Me, "Atención", "La observación no puede ir vacía.", "warning")
            Exit Sub
        End If

        Dim c As New Models.Comentario With {
            .IdProyecto = Convert.ToInt32(ddlProyecto.SelectedValue),
            .IdUsuario = Convert.ToInt32(Session("IdUsuario")),
            .Texto = txtTexto.Text.Trim()
        }

        If dbC.Crear(c, err) Then
            txtTexto.Text = ""
            CargarComentarios()
            SwalUtils.ShowSwal(Me, "Éxito", "Observación agregada correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo agregar la observación.", err), "error")
        End If
    End Sub

    Protected Sub gvComentarios_RowEditing(sender As Object, e As GridViewEditEventArgs)
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden editar observaciones.", "warning")
            Exit Sub
        End If

        gvComentarios.EditIndex = e.NewEditIndex
        CargarComentarios()
    End Sub

    Protected Sub gvComentarios_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvComentarios.EditIndex = -1
        CargarComentarios()
    End Sub

    Protected Sub gvComentarios_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden actualizar observaciones.", "warning")
            Exit Sub
        End If

        Dim idComentario As Integer = Convert.ToInt32(gvComentarios.DataKeys(e.RowIndex).Value)
        Dim row As GridViewRow = gvComentarios.Rows(e.RowIndex)
        Dim texto As String = CType(row.Cells(1).Controls(0), TextBox).Text.Trim()

        If String.IsNullOrWhiteSpace(texto) Then
            SwalUtils.ShowSwal(Me, "Atención", "La observación no puede ir vacía.", "warning")
            Exit Sub
        End If

        If dbC.Actualizar(idComentario, texto, err) Then
            gvComentarios.EditIndex = -1
            CargarComentarios()
            SwalUtils.ShowSwal(Me, "Actualizado", "Observación actualizada correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo actualizar la observación.", err), "error")
        End If
    End Sub

    Protected Sub gvComentarios_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden eliminar observaciones.", "warning")
            Exit Sub
        End If

        Dim id As Integer = Convert.ToInt32(gvComentarios.DataKeys(e.RowIndex).Value)

        If dbC.Eliminar(id, err) Then
            CargarComentarios()
            SwalUtils.ShowSwal(Me, "Eliminado", "Observación eliminada correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo eliminar la observación.", err), "error")
        End If
    End Sub
End Class