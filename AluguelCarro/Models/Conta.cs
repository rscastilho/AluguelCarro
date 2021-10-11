using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models {
    public class Conta {

        public int ContaId { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Saldo  { get; set; }



        }
    }
