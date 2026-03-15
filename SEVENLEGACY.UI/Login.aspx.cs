using SEVENLEGACY.LOGIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEVENLEGACY.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ProcesarLogin(string usuario, string password)
        {
            UsuarioLogic logica = new UsuarioLogic();
            var userObj = logica.ValidarAcceso(usuario, password);

            if (userObj != null)
            {
                // Creamos la Cookie de sesión
                HttpCookie authCookie = new HttpCookie("UsuarioSesion");
                authCookie.Value = userObj.Username;
                HttpContext.Current.Response.Cookies.Add(authCookie);

                return "OK";
            }
            return "Credenciales inválidas";
        }
    }
}