<%@ Page Title="Entregas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Entregas.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Entregas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title"><i class="bi bi-upload"></i> Registro de Entregas</h2>
    <p class="text-muted">Los estudiantes registran entregas y los profesores/coordinadores asignan calificación.</p>

    <asp:Panel ID="pnlRegistroEntrega" runat="server">
        <div class="row g-3 mb-4">
            <div class="col-md-4">
                <label class="form-label">Proyecto</label>
                <asp:DropDownList ID="ddlProyecto" runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
                    ControlToValidate="txtDescripcion"
                    ErrorMessage="Ingrese la descripción"
                    ForeColor="Red"
                    ValidationGroup="vgE" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Archivo</label>
                <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="mb-4">
            <asp:Button ID="btnAgregar" runat="server"
                Text="Registrar Entrega"
                CssClass="btn btn-primary"
                ValidationGroup="vgE"
                OnClick="btnAgregar_Click" />
        </div>
    </asp:Panel>

    <div class="table-responsive">
        <asp:GridView ID="gvEntregas" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover align-middle"
            DataKeyNames="IdEntrega"
            OnRowCommand="gvEntregas_RowCommand"
            OnRowDeleting="gvEntregas_RowDeleting"
            OnRowDataBound="gvEntregas_RowDataBound">

            <Columns>
                <asp:BoundField DataField="IdEntrega" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Autor" HeaderText="Autor" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" />

                <asp:TemplateField HeaderText="Archivo">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlArchivo" runat="server"
                            NavigateUrl='<%# "~/ArchivosEntregas/" & Eval("NombreArchivo") %>'
                            Text='<%# Eval("NombreArchivo") %>'
                            Target="_blank"
                            Visible='<%# Not String.IsNullOrEmpty(If(Eval("NombreArchivo"), "").ToString()) %>'>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nota">
                    <ItemTemplate>
                        <%# Eval("Calificacion") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asignar calificación">
                    <ItemTemplate>
                        <asp:Panel ID="pnlCalificacion" runat="server" CssClass="d-flex gap-2 align-items-center">
                            <asp:TextBox ID="txtNota" runat="server" CssClass="form-control form-control-sm" Width="90px"></asp:TextBox>

                            <asp:LinkButton ID="btnAsignarNota" runat="server"
                                CommandName="Calificar"
                                CommandArgument='<%# Eval("IdEntrega") %>'
                                CausesValidation="False"
                                CssClass="btn btn-sm btn-success">
                                <i class="bi bi-check-circle"></i> Asignar
                            </asp:LinkButton>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server"
                            CommandName="Delete"
                            CausesValidation="False"
                            CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('¿Seguro que desea eliminar esta entrega?');">
                            <i class="bi bi-trash"></i> Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>