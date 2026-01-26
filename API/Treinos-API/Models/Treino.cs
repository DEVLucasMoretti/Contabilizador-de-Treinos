using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Treino
    {
        public Treino(){}

        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Data precisa ser preenchido")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo Dia da Semana precisa ser preenchido")]
        public string DiaDaSemana { get; set; }
        public string TreinoDoDia { get; set; }
        public double QuantidadeCaloria { get; set; }
        
    }
}
