<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="GestionProyectosAcademicosWeb.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Iniciar Sesión</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        body {
            min-height: 100vh;
            background: linear-gradient(135deg, #0d6efd, #6610f2);
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: Arial, sans-serif;
        }

        .login-card {
            background: white;
            border-radius: 20px;
            box-shadow: 0 12px 30px rgba(0,0,0,.20);
            padding: 35px;
            width: 100%;
            max-width: 430px;
        }

        .login-title {
            font-weight: 700;
            text-align: center;
            margin-bottom: 8px;
        }

        .login-subtitle {
            text-align: center;
            color: #6c757d;
            margin-bottom: 25px;
        }

        .icon-circle {
            width: 75px;
            height: 75px;
            border-radius: 50%;
            background: #e9f2ff;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 18px auto;
            font-size: 32px;
            color: #0d6efd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <div class="icon-circle">
                <i class="bi bi-person-circle"></i>
            </div>

            <h2 class="login-title">Iniciar Sesión</h2>
            <p class="login-subtitle">Sistema de Gestión de Proyectos Académicos</p>

            <div class="mb-3">
                <label class="form-label">Correo</label>
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server"
                    ControlToValidate="txtCorreo"
                    ErrorMessage="Ingrese el correo"
                    ForeColor="Red" />
            </div>

            <div class="mb-3">
                <label class="form-label">Clave</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvClave" runat="server"
                    ControlToValidate="txtClave"
                    ErrorMessage="Ingrese la clave"
                    ForeColor="Red" />
            </div>

            <div class="d-grid">
                <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>