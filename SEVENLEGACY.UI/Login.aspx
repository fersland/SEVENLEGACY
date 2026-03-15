<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SEVENLEGACY.UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SEVENLEGACY</title>
    <script src="resources/js/jquery-3.6.0.min.js"></script>
    <script src="resources/js/jquery-ui.min.js"></script>
    <script src="resources/js/Login.js"></script>
    <link href="resources/css/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="container">
        <div class="login-wrapper">
            <div class="login-box">
                <h2>Inicio de Sesión</h2>
                <div class="form-group">
                    <label>Usuario:</label>
                    <input type="text" id="txtusername" title="Ingrese el usuario" placeholder="Ingrese nombre de usuario" />
                </div>

                <div class="form-group">
                    <label>Password:</label>
                    <input type="password" id="txtpassword" title="Ingrese la clave" placeholder="Ingrese la clave" />
                </div>

                <button type="button" id="btnIngresar">Entrar</button>
                <div id="mensaje" style="color:red; margin-top:10px;"></div>
            </div>
        </div>
    </div>
</body>
</html>