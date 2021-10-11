using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewModels {
    public class RegistroViewModel {

        [Required(ErrorMessage ="Campo obrigatorio")]
        [StringLength(100, ErrorMessage ="Use Menos caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        public string CPF { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatorio")]
        
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(100, ErrorMessage = "Use Menos caracteres")]
        public string NomeUsuario { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(100, ErrorMessage = "Use Menos caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email invalido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(100, ErrorMessage = "Use Menos caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        }
    }
