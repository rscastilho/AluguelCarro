using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models {
    public class Aluguel {

        public int AluguelId { get; set; }
        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(150, ErrorMessage = "Digite menos caracteres")]
        public Usuario usuario { get; set; }

        public int CarroId  { get; set; }
        public Carro Carro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        
        public string Inicio { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        
        public string Fim { get; set; }

        public int PrecoTotal { get; set; }




        }
    }
