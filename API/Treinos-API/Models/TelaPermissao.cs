using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TelaPermissao
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public string Home { get; set; }
        public string ContabilizarTreino { get; set; }
        public string Relatorio { get; set; }
        public string Treino { get; set; }
    }
}
