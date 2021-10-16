using AluguelCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Interfaces {
    public interface IAluguelRepository: IRepositoryGeneric<Aluguel> {
        Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim);

        }
    }
