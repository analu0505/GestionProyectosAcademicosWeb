<%@ Page Title="Observaciones" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comentarios.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Comentarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title"><i class="bi bi-chat-left-text-fill"></i> Observaciones / Comentarios</h2>
    <p class="text-muted">Las observaciones son registradas por profesores o coordinadores.</p>

    <div class="row g-3 mb-4">
        <div class="col-md-5">
            <label class="form-label">Proyecto</label>
            <asp:DropDownList ID="ddlProyecto" runat="server"
                CssClass="form-select"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <asp:Panel ID="pnlAgregarComentario" runat="server">
        <div class="row g-3 mb-4">
            <div class="col-md-8">
                <label class="form-label">Observación</label>
                <asp:TextBox ID="txtTexto" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTexto" runat="server"
                    ControlToValidate="txtTexto"
                    ErrorMessage="Ingrese una observación"
                    ForeColor="Red"
                    ValidationGroup="vgC" />
            </div>
        </div>

        <div class="mb-4">
            <asp:Button ID="btnAgregar" runat="server"
                Text="Agregar Observación"
                CssClass="btn btn-primary"
                ValidationGroup="vgC"
                OnClick="btnAgregar_Click" />
        </div>
    </asp:Panel>

    <div class="table-responsive">
        <asp:GridView ID="gvComentarios" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover align-middle"
            DataKeyNames="IdComentario"
            OnRowEditing="gvComentarios_RowEditing"
            OnRowCancelingEdit="gvComentarios_RowCancelingEdit"
            OnRowUpdating="gvComentarios_RowUpdating"
            OnRowDeleting="gvComentarios_RowDeleting"
            OnRowDataBound="gvComentarios_RowDataBound">

            <Columns>
                <asp:BoundField DataField="IdComentario" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Texto" HeaderText="Observación" />
                <asp:BoundField DataField="Autor" HeaderText="Autor" ReadOnly="True" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" />

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
                            OnClientClick="return confirm('¿Seguro que desea eliminar esta observación?');">
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