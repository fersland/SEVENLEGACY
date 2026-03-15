<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCliente.aspx.cs" Inherits="SEVENLEGACY.UI.ReporteCliente" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Reporte de Clientes</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 100%; height: 800px;">
            <rsweb:ReportViewer ID="rvClientes" runat="server" Width="100%" Height="100%" ZoomMode="PageWidth">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>