using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEVENLEGACY.ENTITIES
{
    public class Cliente
    {
        public int IdClie { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNac { get; set; }
        public int IdEstadoCivil { get; set; }

        public string EstadoCivil { get; set; }
    }
}
