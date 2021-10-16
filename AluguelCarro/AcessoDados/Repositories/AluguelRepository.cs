using AluguelCarro.Context;
using AluguelCarro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {

    
    public class AluguelRepository : RepositoryGeneric<Aluguel>, IAluguelRepository {
        private readonly AppDbContext _context;

        public AluguelRepository(AppDbContext context) : base(context) {
            _context = context;

            }

        public async Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim) {
            return await _context.Alugueis.AnyAsync(a => a.UsuarioId == usuarioId && a.CarroId == carroId && a.Inicio == dataInicio &&
            a.Fim == dataFim);


            }


        }
    }
