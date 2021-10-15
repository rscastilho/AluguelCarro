using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using AluguelCarro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {
    public class ContaRepository : RepositoryGeneric<Conta>, IContaRepository {
        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context) : base(context) {
            _context = context;


            }

        public int PegarSaldoPeloId(string Id) {
            return _context.Contas.FirstOrDefault(c => c.UsuarioId == Id).Saldo;

            }

        public new async Task<IEnumerable<Conta>> PegarTodos() {
            return await _context.Contas.Include(c => c.Usuario).ToListAsync();
            }
        }
    }
