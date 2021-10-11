using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models {
    public class Usuario: IdentityUser {

           
        [Required(ErrorMessage ="Campo obrigatório")]
        [StringLength(100, ErrorMessage ="Digite menos caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }






        }
    }
