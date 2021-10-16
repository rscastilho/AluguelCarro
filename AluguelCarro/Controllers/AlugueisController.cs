using AluguelCarro.AcessoDados.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Controllers {
    public class AlugueisController : Controller {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IAluguelRepository _aluguelRepository;

        public AlugueisController(IUsuarioRepository usuarioRepository, IContaRepository contaRepository, IAluguelRepository aluguelRepository) {
            _usuarioRepository = usuarioRepository;
            _contaRepository = contaRepository;
            _aluguelRepository = aluguelRepository;
            }

        public IActionResult Index() {
            return View();
            }
        }
    }
