using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Models;
using AluguelCarro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Controllers {
    [Authorize]
    public class AlugueisController : Controller {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IAluguelRepository _aluguelRepository;

        public AlugueisController(IUsuarioRepository usuarioRepository, IContaRepository contaRepository, IAluguelRepository aluguelRepository) {
            _usuarioRepository = usuarioRepository;
            _contaRepository = contaRepository;
            _aluguelRepository = aluguelRepository;
            }

       
        public IActionResult Alugar(int carroId, int precoDiaria) {
            var aluguel = new AluguelViewModel {
                CarroId = carroId,
                PrecoDiaria = precoDiaria
                };
            return View(aluguel);
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Alugar(AluguelViewModel aluguel) {

            if(ModelState.IsValid) {
                var usuario = await _usuarioRepository.PegarUsuarioLogado(User);
                var saldo = _contaRepository.PegarSaldoPeloId(usuario.Id);
                if(await _aluguelRepository.VerificarReserva(usuario.Id, aluguel.CarroId, aluguel.Inicio, aluguel.Fim)) {
                    TempData["Aviso"] = "Você ja possui essa reserva";
                    return View(aluguel);
                    }
                else if(aluguel.PrecoTotal > saldo) {
                    TempData["Aviso"] = "Saldo insuficiente";
                    return View(aluguel);
                    }
                else {
                    Aluguel a = new Aluguel {
                        UsuarioId = usuario.Id,
                        CarroId = aluguel.CarroId,
                        Inicio = aluguel.Inicio,
                        Fim = aluguel.Fim,
                        PrecoTotal = aluguel.PrecoTotal

                        };
                    await _aluguelRepository.Inserir(a);

                    var saldoUsuario = await _contaRepository.PegarSaldoPeloUsuarioId(usuario.Id);
                    saldoUsuario.Saldo = saldoUsuario.Saldo - aluguel.PrecoTotal;
                    await _contaRepository.Atualizar(saldoUsuario);


                    return RedirectToAction("index", "Carros");

                    }
                }
            return View(aluguel);


            }

        }
    }
