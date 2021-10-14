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

namespace AluguelCarro.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IEnderecoRepository _enderecoRepositorio;

        public EnderecosController(IUsuarioRepository usuarioRepositorio, IEnderecoRepository enderecoRepositorio) {
            _usuarioRepositorio = usuarioRepositorio;
            _enderecoRepositorio = enderecoRepositorio;
            }

        public async Task<IActionResult> Create()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(User);
            var endereco = new Endereco { UsuarioId = usuario.Id };
            return View(endereco);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnderecoId,Rua,Numero,Bairro,Cidade,Estado,UsuarioId")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoRepositorio.Inserir(endereco);
                
                return RedirectToAction("index", "usuarios");
            }
            
            return View(endereco);
        }

      
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoRepositorio.PegarPeloId(id);
            if (endereco == null)
            {
                return NotFound();
            }
            
            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnderecoId,Rua,Numero,Bairro,Cidade,Estado,UsuarioId")] Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    await _enderecoRepositorio.Atualizar(endereco);
                return RedirectToAction("index", "usuarios");
                }
            return View(endereco);
                
             
        }
            
        
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _enderecoRepositorio.Excluir(id);
            return Json("Endereço excluido!");
        }

    }
}
