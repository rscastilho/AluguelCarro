using AluguelCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Interfaces {
    public interface INivelAcessoRepository : IRepositoryGeneric<NiveisAcesso> {
        Task<bool> NivelAcessoExiste(string nivelAcesso);
        new Task Atualizar(NiveisAcesso niveisAcesso);
        }
    }
