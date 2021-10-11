using AluguelCarro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AluguelCarro.Context {
    public class AppDbContext : IdentityDbContext<Usuario, NiveisAcesso, string> {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {
            }
        public DbSet<Usuario> Usuarios  { get; set; }
        public DbSet<NiveisAcesso> NiveisAcessos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        }
    }
