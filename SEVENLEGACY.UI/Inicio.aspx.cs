using SEVENLEGACY.ENTITIES;
using SEVENLEGACY.LOGIC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEVENLEGACY.UI
{
    public partial class Inicio : System.Web.UI.Page
    {
        private static ClienteLogic _logic = new ClienteLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UsuarioSesion"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        [WebMethod]
        public static List<object> listarEstadosCiviles()
        {
            List<object> lista = new List<object>();

            DataTable dt = _logic.ListarEstados();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new
                {
                    Id = row["id_estado"].ToString(),
                    Descripcion = row["descripcion"].ToString()
                });
            }

            return lista;
        }

        [WebMethod]
        public static string GuardarCliente(Cliente obj)
        {
            try
            {
                bool r = _logic.Guardar(obj);
                return r ? "OK" : "Error al procesar";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static List<Cliente> Listar(string filtro)
        {
            List<Cliente> lista = new List<Cliente>();
            DataTable dt = _logic.Consultar(filtro);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Cliente
                {
                    IdClie = Convert.ToInt32(row["id_clie"]),
                    Cedula = row["cedula"].ToString(),
                    Nombre = row["nombre"].ToString(),
                    Genero = row["genero"].ToString(),
                    FechaNac = Convert.ToDateTime(row["fecha_nac"]),
                    IdEstadoCivil = Convert.ToInt32(row["id_estado_civil"]),
                    EstadoCivil = row["EstadoCivil"].ToString()
                });
            }
            return lista;
        }

        [WebMethod]
        public static string EliminarCliente(int id)
        {
            try
            {
                bool r = _logic.Eliminar(id);
                return r ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Logout()
        {
            if (HttpContext.Current.Request.Cookies["UsuarioSesion"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UsuarioSesion");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            return "OK";
        }
    }
}