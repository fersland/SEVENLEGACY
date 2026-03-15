using SEVENLEGACY.ENTITIES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEVENLEGACY.DATA
{
    public class UsuarioData
    {
        public Usuario Login(string usuario, string password)
        {
            Usuario usuarioEncontrado = null;
            try
            {
                using (SqlConnection cn = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user", usuario);
                    cmd.Parameters.AddWithValue("@pass", password);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuarioEncontrado = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Username = dr["username"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return usuarioEncontrado;
        }
    }
}
