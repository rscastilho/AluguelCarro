using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewModels {
    public class LoginViewModel {

        [Required(ErrorMessage ="Campo Obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email invalido")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        }
    }
