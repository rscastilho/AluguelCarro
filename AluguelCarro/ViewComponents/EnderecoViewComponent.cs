using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewComponents {
    public class EnderecoViewComponent: ViewComponent {

        private readonly AppDbContext _context;
        private readonly IUsuarioRepository _usuarioRepositorio;

        public EnderecoViewComponent(AppDbContext context, IUsuarioRepository usuarioRepositorio) {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
            }


        public async Task<IViewComponentResult> InvokeAsync() {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);
            return View(await _context.Enderecos.Where(e => e.UsuarioId == usuario.Id).ToListAsync());
            }

        }
    }
