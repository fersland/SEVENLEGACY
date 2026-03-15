using SEVENLEGACY.DATA;
using SEVENLEGACY.ENTITIES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SEVENLEGACY.LOGIC
{
    public class ClienteLogic
    {
        private ClienteData _obj = new ClienteData();

        public bool Guardar(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Cedula) || cliente.Cedula.Length != 10)
            {
                throw new Exception("La cédula debe tener 10 dígitos.");
            }

            if (!cliente.Cedula.All(char.IsDigit))
            {
                throw new Exception("La cédula solo debe contener números.");
            }

            if (string.IsNullOrEmpty(cliente.Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }

            if (!Regex.IsMatch(cliente.Nombre, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                throw new Exception("El nombre contiene caracteres no permitidos.");
            }

            return _obj.MantenimientoCliente(cliente, cliente.IdClie == 0 ? "INSERT" : "UPDATE");
        }

        public bool Eliminar(int id)
        {
            return _obj.MantenimientoCliente(new Cliente { IdClie = id }, "DELETE");
        }

        public DataTable Consultar(string filtro)
        {
            return _obj.ListarClientes(filtro);
        }

        public DataTable ListarEstados()
        {
            return _obj.ObtenerEstadoCivil();
        }
    }
}
