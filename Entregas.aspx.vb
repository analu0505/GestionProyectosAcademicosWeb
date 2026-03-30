Imports GestionProyectosAcademicosWeb.Utils
Imports System.IO

Public Class Entregas
    Inherits System.Web.UI.Page

    Private dbP As New dbProyecto()
    Private dbE As New dbEntrega()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Rol") Is Nothing OrElse Session("IdUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        Dim rol As String = Session("Rol").ToString()

        ' Solo el estudiante puede registrar entregas
        pnlRegistroEntrega.Visible = (rol = "Estudiante")

        ' Ocultar columna de asignar calificación al estudiante
        If gvEntregas.Columns.Count > 6 Then
            gvEntregas.Columns(6).Visible = (rol = "Profesor" OrElse rol = "Coordinador")
        End If

        If Not IsPostBack Then
            CargarProyectosCombo()
            CargarEntregas()
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

    Private Sub CargarEntregas()
        If ddlProyecto.Items.Count = 0 Then
            gvEntregas.DataSource = Nothing
            gvEntregas.DataBind()
            Exit Sub
        End If

        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()
        Dim idU As Integer = Convert.ToInt32(Session("IdUsuario"))
        Dim idProyecto As Integer = Convert.ToInt32(ddlProyecto.SelectedValue)

        gvEntregas.DataSource = dbE.ListarPorProyecto(idProyecto, rol, idU, err)
        gvEntregas.DataBind()
    End Sub

    Protected Sub gvEntregas_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rol As String = Session("Rol").ToString()
            Dim pnlCalificacion As Panel = TryCast(e.Row.FindControl("pnlCalificacion"), Panel)

            If pnlCalificacion IsNot Nothing Then
                pnlCalificacion.Visible = (rol = "Profesor" OrElse rol = "Coordinador")
            End If
        End If
    End Sub

    Protected Sub ddlProyecto_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarEntregas()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim err As String = ""
        Dim rol As String = Session("Rol").ToString()

        If rol <> "Estudiante" Then
            SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo los estudiantes pueden registrar entregas.", "warning")
            Exit Sub
        End If

        If Not Page.IsValid Then
            SwalUtils.ShowSwal(Me, "Atención", "Escriba la descripción de la entrega.", "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtDescripcion.Text) Then
            SwalUtils.ShowSwal(Me, "Atención", "La descripción no puede ir vacía.", "warning")
            Exit Sub
        End If

        Dim nombreArchivoGuardado As String = ""

        Try
            If fuArchivo.HasFile Then
                Dim extension As String = Path.GetExtension(fuArchivo.FileName).ToLower()

                If extension <> ".pdf" AndAlso extension <> ".docx" AndAlso extension <> ".xlsx" AndAlso extension <> ".pptx" AndAlso extension <> ".txt" Then
                    SwalUtils.ShowSwal(Me, "Atención", "Solo se permiten archivos PDF, DOCX, XLSX, PPTX o TXT.", "warning")
                    Exit Sub
                End If

                Dim carpeta As String = Server.MapPath("~/ArchivosEntregas/")
                If Not Directory.Exists(carpeta) Then
                    Directory.CreateDirectory(carpeta)
                End If

                nombreArchivoGuardado = Guid.NewGuid().ToString() & "_" & Path.GetFileName(fuArchivo.FileName)
                Dim rutaCompleta As String = Path.Combine(carpeta, nombreArchivoGuardado)

                fuArchivo.SaveAs(rutaCompleta)
            End If

            Dim en As New Models.Entrega With {
                .IdProyecto = Convert.ToInt32(ddlProyecto.SelectedValue),
                .IdUsuario = Convert.ToInt32(Session("IdUsuario")),
                .Descripcion = txtDescripcion.Text.Trim(),
                .NombreArchivo = nombreArchivoGuardado
            }

            If dbE.Crear(en, err) Then
                txtDescripcion.Text = ""
                CargarEntregas()
                SwalUtils.ShowSwal(Me, "Éxito", "Entrega registrada correctamente.", "success")
            Else
                SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo registrar la entrega.", err), "error")
            End If

        Catch ex As Exception
            SwalUtils.ShowSwal(Me, "Error", "Ocurrió un problema al subir el archivo: " & ex.Message, "error")
        End Try
    End Sub

    Protected Sub gvEntregas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Calificar" Then
            Dim rol As String = Session("Rol").ToString()

            If rol <> "Profesor" AndAlso rol <> "Coordinador" Then
                SwalUtils.ShowSwal(Me, "Acceso denegado", "Solo Profesor o Coordinador pueden asignar calificación.", "warning")
                Exit Sub
            End If

            Dim err As String = ""
            Dim idEntrega As Integer = Convert.ToInt32(e.CommandArgument)

            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim txtNota As TextBox = CType(row.FindControl("txtNota"), TextBox)

            If txtNota Is Nothing OrElse String.IsNullOrWhiteSpace(txtNota.Text) Then
                SwalUtils.ShowSwal(Me, "Atención", "Ingrese una nota válida.", "warning")
                Exit Sub
            End If

            Dim nota As Decimal
            If Not Decimal.TryParse(txtNota.Text.Trim(), nota) Then
                SwalUtils.ShowSwal(Me, "Atención", "La nota debe ser numérica.", "warning")
                Exit Sub
            End If

            If nota < 0 OrElse nota > 100 Then
                SwalUtils.ShowSwal(Me, "Atención", "La nota debe estar entre 0 y 100.", "warning")
                Exit Sub
            End If

            If dbE.ActualizarCalificacion(idEntrega, nota, err) Then
                CargarEntregas()
                SwalUtils.ShowSwal(Me, "Éxito", "Calificación asignada correctamente.", "success")
            Else
                SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo guardar la calificación.", err), "error")
            End If
        End If
    End Sub

    Protected Sub gvEntregas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim err As String = ""
        Dim id As Integer = Convert.ToInt32(gvEntregas.DataKeys(e.RowIndex).Value)

        If dbE.Eliminar(id, err) Then
            CargarEntregas()
            SwalUtils.ShowSwal(Me, "Eliminado", "Entrega eliminada correctamente.", "success")
        Else
            SwalUtils.ShowSwal(Me, "Error", If(err = "", "No se pudo eliminar la entrega.", err), "error")
        End If
    End Sub
End Class