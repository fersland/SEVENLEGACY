using SEVENLEGACY.DATA;
using SEVENLEGACY.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEVENLEGACY.LOGIC
{
    public class UsuarioLogic
    {
        private UsuarioData _obj = new UsuarioData();
        public Usuario ValidarAcceso(string user, string pass)
        {
            // Podrías agregar aquí lógica de bloqueo de cuenta si fallan 3 veces
            return _obj.Login(user, pass);
        }
    }
}
