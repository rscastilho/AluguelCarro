using AluguelCarro.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewComponents {
    public class AluguelViewComponent: ViewComponent {

        private readonly AppDbContext _context;

        public AluguelViewComponent(AppDbContext context) {
            _context = context;
            }

        public async Task<IViewComponentResult> InvokeAsync(int CarroId) {

            return View(await _context.Carros.FirstOrDefaultAsync(c => c.CarroId == CarroId));


            }

        }
    }
