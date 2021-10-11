using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using AluguelCarro.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositories {
    public class NivelAcessoRepository : RepositoryGeneric<NiveisAcesso>, INivelAcessoRepository {

        private readonly RoleManager<NiveisAcesso> _gerenciadorNiveisAcesso;
        private readonly AppDbContext _context;

        public NivelAcessoRepository(RoleManager<NiveisAcesso> gerenciadorNiveisAcesso, AppDbContext context): base(context) {

            _gerenciadorNiveisAcesso = gerenciadorNiveisAcesso;
            _context = context;
            }

        public async Task<bool> NivelAcessoExiste(string nivelAcesso) {
            return await _gerenciadorNiveisAcesso.RoleExistsAsync(nivelAcesso);
            }

        public new async Task Atualizar(NiveisAcesso niveisAcesso) {
            var nv = await _context.NiveisAcessos.FindAsync(niveisAcesso.Id);
            nv.Name = niveisAcesso.Name;
            nv.NormalizedName = niveisAcesso.NormalizedName;
            nv.Descricao = niveisAcesso.Descricao;
            await _gerenciadorNiveisAcesso.UpdateAsync(nv);
            }

        }
    }
