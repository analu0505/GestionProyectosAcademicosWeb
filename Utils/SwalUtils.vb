Imports System
Imports System.Web.UI

Namespace Utils
    Public Class SwalUtils
        Public Shared Sub ShowSwal(page As Page, titulo As String, mensaje As String, icono As String)
            Dim js As String = $"Swal.fire({{title:'{titulo}', text:'{mensaje}', icon:'{icono}'}});"
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), js, True)
        End Sub
    End Class
End Namespace