
using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using AluguelCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {
    public class CarroRepository : RepositoryGeneric<Carro>, ICarroRepository {
        public CarroRepository(AppDbContext context) : base(context) {
            }
        }
    }
