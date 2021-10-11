using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models {
    public class Carro {
        public int CarroId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "Digite menos caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "Digite menos caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Foto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        
        public int PrecoDiaria { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; }



        }
    }
