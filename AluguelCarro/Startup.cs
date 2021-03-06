using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.AcessoDados.Repositories;
using AluguelCarro.Context;

using AluguelCarro.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
        
            //conexao banco de dados
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("Connection"), builder =>
                builder.MigrationsAssembly("AluguelCarro")));
            //congfigura??o identity usuario e nivel de acessl
            services.AddIdentity<Usuario, NiveisAcesso>().AddDefaultUI().AddEntityFrameworkStores<AppDbContext>();

            //inicia a configura??o cookies
            services.ConfigureApplicationCookie(options => {
                //define o cokkie como http
                options.Cookie.HttpOnly = true;
                //tempo para expirar o cookie
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                //se expirar cria um novo cookie
                options.SlidingExpiration = true;
                
                }
            );

            //configura??o de senha
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddScoped<INivelAcessoRepository, NivelAcessoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<ICarroRepository, CarroRepository>();
            services.AddScoped<IAluguelRepository, AluguelRepository>();


            services.AddControllersWithViews();
            services.AddRazorPages();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuarios}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
            }
        }
    }
