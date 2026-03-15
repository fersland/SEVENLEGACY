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
    public class ClienteData
    {
        public DataTable ObtenerEstadoCivil()
        {
            using(SqlConnection _db = Conexion.conectar())
            {
                string query = "SELECT id_estado, descripcion FROM CAT_ESTADO_CIVIL";
                SqlDataAdapter _adpter = new SqlDataAdapter(query, _db);
                DataTable _dt = new DataTable();
                _adpter.Fill(_dt);
                return _dt;
            }
        }

        public bool MantenimientoCliente(Cliente obj, string accion)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection cn = Conexion.conectar())
                {
                    SqlCommand cmd = new SqlCommand("sp_MantenimientoCliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_clie", obj.IdClie);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@cedula", (object)obj.Cedula ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nombre", (object)obj.Nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@genero", (object)obj.Genero ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_estado_civil", obj.IdEstadoCivil);

                    if (obj.FechaNac == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@fecha_nac", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@fecha_nac", obj.FechaNac);

                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

        public DataTable ListarClientes(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = Conexion.conectar())
            {
                string query = "SELECT c.id_clie, c.cedula, c.nombre, c.genero, c.id_estado_civil, c.fecha_nac, e.descripcion as EstadoCivil " +
               "FROM SEVECLIE c INNER JOIN CAT_ESTADO_CIVIL e ON c.id_estado_civil = e.id_estado " +
               "WHERE c.nombre LIKE @filtro OR c.cedula LIKE @filtro";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
