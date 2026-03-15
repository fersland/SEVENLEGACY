<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SEVENLEGACY.UI.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SEVENLEGACY - Inicio</title>
    <link rel="stylesheet" href="resources/css/jquery-ui.css" />
    <link href="resources/css/Inicio.css" rel="stylesheet" type="text/css" />
    <script src="resources/js/jquery-3.6.0.min.js"></script>
    <script src="resources/js/jquery-ui.min.js"></script>
    <script src="resources/js/Inicio.js"></script>    
</head>
<body>
    <h1>SevenLegacy</h1>
    <hr />
    <h3>Mantenimiento de Clientes</h3>
    <div id="usuarioSesion">
        <div style="text-align: right; padding: 10px;">
    <span>Bienvenido, <%= Request.Cookies["UsuarioSesion"] != null ? Request.Cookies["UsuarioSesion"].Value : "" %></span>
    <button type="button" id="btnCerrarSesion">Cerrar Sesión</button>
</div>
    </div>
    <hr />

    <div class="container">
        <div class="formulario">
            <h3>Datos del Cliente</h3>
            <div class="form-group">
                <label>Cédula:</label>
                <input type="text" id="txtCedula" placeholder="Ingrese cédula 10 números máximo" autocomplete="off" minlength="10" maxlength="10" />
            </div>
            <div class="form-group">
                <label>Nombre:</label>
                <input type="text" id="txtNombre" placeholder="Nombre completo del cliente" autocomplete="off" />
            </div>
            <div class="form-group">
                <label>Género:</label>
                <select id="ddlGenero">
                    <option value="M">Masculino</option>
                    <option value="F">Femenino</option>
                </select>
            </div>
            <div class="form-group">
                <label>Fecha Nac.:</label>
                <input type="text" id="txtFechaNac" placeholder="Haga clic para seleccionar fecha" autocomplete="off" readonly />
            </div>
            <div class="form-group">
                <label>Estado Civil:</label>
                <select id="ddlEstadoCivil"></select>
            </div>
            <button type="button" id="btnGuardar">Guardar Registro</button>
            <button type="button" id="btnLimpiar">Limpiar</button>
            <button type="button" id="btnImprimir" class="btn-info">
                <i class="fa fa-print"></i> Imprimir Reporte
            </button>
        </div>

        <div class="listado">
            <h3>Consulta</h3>
            <div class="search-container">
                <input type="text" id="txtFiltro" placeholder="Filtrar por nombre o cédula..." />
                <span id="btnClear" class="clear-btn">&times;</span>
            </div>
            <br /><br />
            <table id="tablaClientes">
                <thead>
                    <tr>
                        <th>Cédula</th>
                        <th>Nombre</th>
                        <th>Género</th>
                        <th>Fecha Nac.</th>
                        <th>Estado Civil</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>