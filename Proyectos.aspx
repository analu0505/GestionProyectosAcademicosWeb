<%@ Page Title="Proyectos" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyectos.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Proyectos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title"><i class="bi bi-folder2-open"></i> Gestión de Proyectos</h2>
    <p class="text-muted">Los proyectos académicos son creados por profesores o coordinadores.</p>

    <asp:Panel ID="pnlCrearProyecto" runat="server">
        <div class="row g-3 mb-4">
            <div class="col-md-4">
                <label class="form-label">Título</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server"
                    ControlToValidate="txtTitulo"
                    ErrorMessage="Ingrese el título"
                    ForeColor="Red"
                    ValidationGroup="vgP" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Curso</label>
                <asp:TextBox ID="txtCurso" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCurso" runat="server"
                    ControlToValidate="txtCurso"
                    ErrorMessage="Ingrese el curso"
                    ForeColor="Red"
                    ValidationGroup="vgP" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                    <asp:ListItem>Pendiente</asp:ListItem>
                    <asp:ListItem>En Proceso</asp:ListItem>
                    <asp:ListItem>Entregado</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="mb-4">
            <asp:Button ID="btnGuardar" runat="server"
                Text="Insertar Proyecto"
                CssClass="btn btn-primary"
                ValidationGroup="vgP"
                OnClick="btnGuardar_Click" />
        </div>
    </asp:Panel>

    <div class="table-responsive">
        <asp:GridView ID="gvProyectos" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover align-middle"
            DataKeyNames="IdProyecto"
            OnRowEditing="gvProyectos_RowEditing"
            OnRowCancelingEdit="gvProyectos_RowCancelingEdit"
            OnRowUpdating="gvProyectos_RowUpdating"
            OnRowDeleting="gvProyectos_RowDeleting"
            OnRowDataBound="gvProyectos_RowDataBound">

            <Columns>
                <asp:BoundField DataField="IdProyecto" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Titulo" HeaderText="Título" />
                <asp:BoundField DataField="Curso" HeaderText="Curso" />

                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Eval("Estado") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEstadoEdit" runat="server" CssClass="form-select form-select-sm">
                            <asp:ListItem>Pendiente</asp:ListItem>
                            <asp:ListItem>En Proceso</asp:ListItem>
                            <asp:ListItem>Entregado</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Creador" HeaderText="Creador" ReadOnly="True" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server"
                            CommandName="Edit"
                            CausesValidation="False"
                            CssClass="btn btn-sm btn-warning me-1">
                            <i class="bi bi-pencil-square"></i> Editar
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnEliminar" runat="server"
                            CommandName="Delete"
                            CausesValidation="False"
                            CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('¿Seguro que desea eliminar este proyecto?');">
                            <i class="bi bi-trash"></i> Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:LinkButton ID="btnGuardarEdit" runat="server"
                            CommandName="Update"
                            CausesValidation="False"
                            CssClass="btn btn-sm btn-success me-1">
                            <i class="bi bi-check-circle"></i> Guardar
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnCancelarEdit" runat="server"
                            CommandName="Cancel"
                            CausesValidation="False"
                            CssClass="btn btn-sm btn-secondary">
                            <i class="bi bi-x-circle"></i> Cancelar
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>