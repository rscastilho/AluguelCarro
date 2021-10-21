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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace AluguelCarro.Controllers
{
    [Authorize]
    public class CarrosController : Controller
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IHostingEnvironment _hostingEnviroment;

        public CarrosController(ICarroRepository carroRepository, IHostingEnvironment hostingEnviroment) {
            _carroRepository = carroRepository;
            _hostingEnviroment = hostingEnviroment;
            }

  
              public async Task<IActionResult> Index()
        {
            return View(await _carroRepository.PegarTodos().ToListAsync());
        }
      
      
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                if(arquivo != null) {
                    var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "Imagens");
                    using(FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create)) {

                        await arquivo.CopyToAsync(fileStream);
                        carro.Foto = "~/Imagens/" + arquivo.FileName;
                        }

                    }

                await _carroRepository.Inserir(carro);
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }


        public async Task<IActionResult> Edit(int id)
        {
           

            var carro = await _carroRepository.PegarPeloId(id);
            if (carro == null)
            {
                return NotFound();
            }
            TempData["FotoCarro"] = carro.Foto;
                return View(carro);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (id != carro.CarroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(arquivo != null) {
                    
                        var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "Imagens");
                        using(FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create)) {

                            await arquivo.CopyToAsync(fileStream);
                            carro.Foto = "~/Imagens/" + arquivo.FileName;
                            }
                        }
                else {
                    carro.Foto = TempData["FotoCarro"].ToString();
                    await _carroRepository.Atualizar(carro);

                    }
                
               
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }


           
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var carro = await _carroRepository.PegarPeloId(id);
            string fotoCarro = carro.Foto;
            fotoCarro = fotoCarro.Replace("~", "wwwroot");
            System.IO.File.Delete(fotoCarro);

            await _carroRepository.Excluir(id);
            return Json("Carro excluido");
        }

     
    }
}
