Imports GestionProyectosAcademicosWeb.Utils

Public Class Proyectos
    Inherits System.Web.UI.Page

    Private dbP As New dbProyecto()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Rol") Is Nothing OrElse Session("IdUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        Dim rol As String = Session("Rol").ToString()

        ' Solo Profesor y Coordinador pueden crear proyectos
        pnlCrearProyecto.Visible = (rol = "Profesor" OrElse rol = "Coordinador")

        If Not IsPostBack Then
            Cargar()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden crear proyectos.", "warning")
            Exit Sub
        End If

        If Not Page.IsValid Then
            SwalUtils.ShowSwal(Me, "Atención", "Complete los campos requeridos.", "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtTitulo.Text) OrElse String.IsNullOrWhiteSpace(txtCurso.Text) Then
            SwalUtils.ShowSwal(Me, "Atención", "Título y curso son obligatorios.", "warning")
            Exit Sub
        End If

        Dim pr As New Models.Proyecto With {
            .Titulo = txtTitulo.Text.Trim(),
            .Curso = txtCurso.Text.Trim(),
            .Estado = ddlEstado.SelectedValue,
            .IdUsuarioCreador = Convert.ToInt32(Session("IdUsuario"))
        }

        If dbP.CrearProyecto(pr, err) Then
            txtTitulo.Text = ""
            txtCurso.Text = ""
            ddlEstado.SelectedIndex = 0
            Cargar()
            SwalUtils.ShowSwal(Me, "Éxito", "Proyecto insertado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo insertar el proyecto.", err), "error")
        End If
    End Sub

    Private Sub Cargar()
        Dim err As String = ""
        gvProyectos.DataSource = dbP.Listar(Session("Rol").ToString(), Convert.ToInt32(Session("IdUsuario")), err)
        gvProyectos.DataBind()
    End Sub

    Protected Sub gvProyectos_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rol As String = Session("Rol").ToString()
            Dim creador As String = DataBinder.Eval(e.Row.DataItem, "Creador").ToString()
            Dim nombreSesion As String = Session("Nombre").ToString()

            Dim btnEditar As LinkButton = TryCast(e.Row.FindControl("btnEditar"), LinkButton)
            Dim btnEliminar As LinkButton = TryCast(e.Row.FindControl("btnEliminar"), LinkButton)

            Dim puedeEditar As Boolean = (rol = "Coordinador") OrElse (rol = "Profesor" AndAlso creador = nombreSesion)

            If btnEditar IsNot Nothing Then btnEditar.Visible = puedeEditar
            If btnEliminar IsNot Nothing Then btnEliminar.Visible = puedeEditar
        End If
    End Sub

    Protected Sub gvProyectos_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvProyectos.EditIndex = e.NewEditIndex
        Cargar()
    End Sub

    Protected Sub gvProyectos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvProyectos.EditIndex = -1
        Cargar()
    End Sub

    Protected Sub gvProyectos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim err As String = ""
        Dim idProyecto As Integer = Convert.ToInt32(gvProyectos.DataKeys(e.RowIndex).Value)
        Dim row As GridViewRow = gvProyectos.Rows(e.RowIndex)

        Dim titulo As String = CType(row.Cells(1).Controls(0), TextBox).Text.Trim()
        Dim curso As String = CType(row.Cells(2).Controls(0), TextBox).Text.Trim()
        Dim ddlEstadoEdit As DropDownList = CType(row.FindControl("ddlEstadoEdit"), DropDownList)
        Dim estado As String = ddlEstadoEdit.SelectedValue

        If String.IsNullOrWhiteSpace(titulo) OrElse String.IsNullOrWhiteSpace(curso) Then
            SwalUtils.ShowSwal(Me, "Atención", "No deje campos vacíos.", "warning")
            Exit Sub
        End If

        Dim pr As New Models.Proyecto With {
            .IdProyecto = idProyecto,
            .Titulo = titulo,
            .Curso = curso,
            .Estado = estado
        }

        If dbP.Actualizar(pr, err) Then
            gvProyectos.EditIndex = -1
            Cargar()
            SwalUtils.ShowSwal(Me, "Actualizado", "Proyecto actualizado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo actualizar el proyecto.", err), "error")
        End If
    End Sub

    Protected Sub gvProyectos_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim err As String = ""
        Dim idProyecto As Integer = Convert.ToInt32(gvProyectos.DataKeys(e.RowIndex).Value)

        If dbP.Eliminar(idProyecto, err) Then
            Cargar()
            SwalUtils.ShowSwal(Me, "Eliminado", "Proyecto eliminado correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error al eliminar", If(err = "", "No se pudo eliminar el proyecto.", err), "error")
        End If
    End Sub
End Class