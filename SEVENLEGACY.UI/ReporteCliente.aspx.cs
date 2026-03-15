using Microsoft.Reporting.WebForms;
using SEVENLEGACY.ENTITIES;
using SEVENLEGACY.LOGIC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEVENLEGACY.UI
{
    public partial class ReporteCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string filtro = Request.QueryString["filtro"] ?? "";
                CargarInforme(filtro);
            }
        }

        private void CargarInforme(string filtro)
        {
            ClienteLogic logic = new ClienteLogic();

            DataTable dt = logic.Consultar(filtro);

            rvClientes.LocalReport.DataSources.Clear();
            rvClientes.LocalReport.ReportPath = Server.MapPath("~/rptClientes.rdlc");

            ReportDataSource rds = new ReportDataSource("dsClientesParams", dt);

            rvClientes.LocalReport.DataSources.Add(rds);
            rvClientes.LocalReport.Refresh();
        }
    }
}