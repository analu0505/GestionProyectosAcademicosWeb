<%@ Page Title="Usuarios" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="page-title"><i class="bi bi-people-fill"></i> Administración de Usuarios</h2>
    <p class="text-muted">Módulo exclusivo para el rol de Coordinador.</p>

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                ControlToValidate="txtNombre"
                ErrorMessage="Ingrese el nombre"
                ForeColor="Red"
                ValidationGroup="vgU" />
        </div>

        <div class="col-md-4">
            <label class="form-label">Correo</label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCorreo" runat="server"
                ControlToValidate="txtCorreo"
                ErrorMessage="Ingrese el correo"
                ForeColor="Red"
                ValidationGroup="vgU" />
        </div>

        <div class="col-md-4">
            <label class="form-label">Clave</label>
            <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvClave" runat="server"
                ControlToValidate="txtClave"
                ErrorMessage="Ingrese la clave"
                ForeColor="Red"
                ValidationGroup="vgU" />
        </div>

        <div class="col-md-4">
            <label class="form-label">Rol</label>
            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                <asp:ListItem>Estudiante</asp:ListItem>
                <asp:ListItem>Profesor</asp:ListItem>
                <asp:ListItem>Coordinador</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <div class="mb-4">
        <asp:Button ID="btnCrear" runat="server"
            Text="Crear Usuario"
            CssClass="btn btn-primary"
            ValidationGroup="vgU"
            OnClick="btnCrear_Click" />
    </div>

    <div class="table-responsive">
        <asp:GridView ID="gvUsuarios" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover align-middle"
            DataKeyNames="IdUsuario"
            OnRowEditing="gvUsuarios_RowEditing"
            OnRowCancelingEdit="gvUsuarios_RowCancelingEdit"
            OnRowUpdating="gvUsuarios_RowUpdating"
            OnRowDeleting="gvUsuarios_RowDeleting">

            <Columns>
                <asp:BoundField DataField="IdUsuario" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Correo" HeaderText="Correo" />
                <asp:BoundField DataField="Rol" HeaderText="Rol" />

                <asp:CommandField ShowEditButton="True"
                    CausesValidation="False"
                    EditText="Editar"
                    UpdateText="Guardar"
                    CancelText="Cancelar" />

                <asp:CommandField ShowDeleteButton="True"
                    CausesValidation="False"
                    DeleteText="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>