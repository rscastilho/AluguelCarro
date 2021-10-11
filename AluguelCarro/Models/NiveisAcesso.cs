using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models {
    public class NiveisAcesso: IdentityRole {

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(400, ErrorMessage = "Digite menos caracteres")]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }
        
        }
    }
