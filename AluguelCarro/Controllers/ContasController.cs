using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelCarro.Context;
using AluguelCarro.Models;
using AluguelCarro.AcessoDados.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AluguelCarro.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ContasController(IContaRepository contaRepository, IUsuarioRepository usuarioRepository) {
            _contaRepository = contaRepository;
            _usuarioRepository = usuarioRepository;
            }

        public async Task<IActionResult> Index()
        {
            
            return View(await _contaRepository.PegarTodos());
        }

    
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_usuarioRepository.PegarTodos(), "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContaId,UsuarioId,Saldo")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                await _contaRepository.Inserir(conta);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioRepository.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        public async Task<IActionResult> Edit(int id)
        {
        

            var conta = await _contaRepository.PegarPeloId(id);
            if (conta == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioRepository.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContaId,UsuarioId,Saldo")] Conta conta)
        {
            if (id != conta.ContaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    await _contaRepository.Atualizar(conta);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_usuarioRepository.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }
               
              
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _contaRepository.Excluir(id);
            return Json("Conta excluida");
        }

          }
}
