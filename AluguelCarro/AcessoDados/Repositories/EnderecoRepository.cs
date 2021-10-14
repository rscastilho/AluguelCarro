using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using AluguelCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {
    public class EnderecoRepository : RepositoryGeneric<Endereco>, IEnderecoRepository {
        public EnderecoRepository(AppDbContext context) : base(context) {


            }
        }
    }
