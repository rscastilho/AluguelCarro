using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewComponents {
    public class CarrosAlugadosViewComponent: ViewComponent {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly AppDbContext _context;

        public CarrosAlugadosViewComponent(IUsuarioRepository usuarioRepositorio, AppDbContext context) {
            _usuarioRepositorio = usuarioRepositorio;
            _context = context;
            }

        public async Task<IViewComponentResult> InvokeAsync() {
            var usuarioLogado = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _context.Alugueis.Include(a => a.Carro).Where(a => a.UsuarioId == usuarioLogado.Id).ToListAsync());
            }

        }
    }
