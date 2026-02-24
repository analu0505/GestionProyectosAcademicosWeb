<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Proyectos.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Proyectos" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Proyectos</title>
</head>
<body>
<form id="form1" runat="server">

    <h3>Gestión de Proyectos (Avance 1)</h3>

    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <br /><br />

    Título:
    <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfv1" runat="server"
        ControlToValidate="txtTitulo" ErrorMessage="* requerido" ForeColor="Red" />
    <br />

    Curso:
    <asp:TextBox ID="txtCurso" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfv2" runat="server"
        ControlToValidate="txtCurso" ErrorMessage="* requerido" ForeColor="Red" />
    <br />

    Estado:
    <asp:DropDownList ID="ddlEstado" runat="server">
        <asp:ListItem>Pendiente</asp:ListItem>
        <asp:ListItem>En Proceso</asp:ListItem>
        <asp:ListItem>Entregado</asp:ListItem>
    </asp:DropDownList>
    <br /><br />

    <asp:Button ID="btnGuardar" runat="server" Text="Insertar" />
    <asp:Button ID="btnCargar" runat="server" Text="Cargar" CausesValidation="False" />
    <br /><br />

    <asp:GridView ID="gvProyectos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdProyecto"
        OnRowCommand="gvProyectos_RowCommand">
        <Columns>
            <asp:BoundField DataField="IdProyecto" HeaderText="ID" />
            <asp:BoundField DataField="Titulo" HeaderText="Título" />
            <asp:BoundField DataField="Curso" HeaderText="Curso" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Eliminar"
                        CommandName="ELIMINAR"
                        CommandArgument='<%# Eval("IdProyecto") %>'
                        OnClientClick="return confirm('¿Eliminar?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</form>
</body>
</html>
