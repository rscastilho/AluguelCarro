using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Models;
using AluguelCarro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Controllers {
    public class UsuariosController : Controller {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioRepository usuarioRepository, ILogger<UsuariosController> logger) {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            }

        public async Task<IActionResult> Index() {
            try {
                return View(await _usuarioRepository.PegarUsuarioLogado(User));
                }
            catch (Exception ex) {
                throw ex;
                }
            
            }


        public async Task<IActionResult> Registro() {
            if(User.Identity.IsAuthenticated)
                await _usuarioRepository.EfetuarLogOut();
            return View();

            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registro) {
            if(ModelState.IsValid) {
                var usuario = new Usuario {
                    UserName = registro.NomeUsuario,
                    Email = registro.Email,
                    CPF = registro.CPF,
                    Telefone = registro.Telefone,
                    Nome = registro.Nome,
                    PasswordHash = registro.Senha
                    };

                _logger.LogInformation("Tentando criar um usuário");
                var resultado = await _usuarioRepository.SalvarUsuario(usuario, registro.Senha);

                if(resultado.Succeeded) {
                    _logger.LogInformation("Novo usuário criado");
                    _logger.LogInformation("Atribuindo nível de acesso ao novo usuário");
                    var nivelAcesso = "Administrador";

                    await _usuarioRepository.AtribuirNivelAcesso(usuario, nivelAcesso);
                    _logger.LogInformation("Atribuição concluída");

                    _logger.LogInformation("Logando o usuário");
                    await _usuarioRepository.EfetuarLogin(usuario, false);
                    _logger.LogInformation("Usuário logado com sucesso");

                    return RedirectToAction("Index", "Usuarios");
                    }

                else {
                    _logger.LogError("Erro ao criar o usuário");

                    foreach(var erro in resultado.Errors)
                        ModelState.AddModelError("", erro.Description.ToString());
                    }

                }
            _logger.LogError("Informações de usuário inválidas");
            return View(registro);
            }

        public async Task<IActionResult> Login() {
               
                if(User.Identity.IsAuthenticated)
                    await _usuarioRepository.EfetuarLogOut();

                _logger.LogInformation("Entrando na página de login");
                return View();
                

            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login) {
            if(ModelState.IsValid) {
                _logger.LogInformation("Pegando o usuário pelo email");
                var usuario = await _usuarioRepository.PegarUsuarioPeloEmail(login.Email);
                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                if(usuario != null) {
                    _logger.LogInformation("Verificando informações do usuário");
                    if(passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, login.Senha) != PasswordVerificationResult.Failed) {
                        _logger.LogInformation("Informações corretas. Logando o usuário");
                        await _usuarioRepository.EfetuarLogin(usuario, false);

                        return RedirectToAction("Index", "home");
                        }

                    _logger.LogError("Informações inválidas");
                    ModelState.AddModelError("", "Email e/ou senha inválidos");
                    }

                _logger.LogError("Informações inválidas");
                ModelState.AddModelError("", "Email e/ou senha inválidos");
                }
            return View(login);
            }

        public async Task<IActionResult> LogOut() {
            await _usuarioRepository.EfetuarLogOut();

            return RedirectToAction("Login", "Usuarios");
            }


        public async Task<IActionResult> Atualizar(string UsuarioId) {
            _logger.LogInformation("Verificando se o usuário existe");
            var usuario = await _usuarioRepository.PegarPeloId(UsuarioId);

            var atualizarViewModel = new AtualizarViewModel {
                
                Id = usuario.Id,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                NomeUsuario = usuario.UserName
                };
            _logger.LogInformation("Atualizar usuário");
            return View(atualizarViewModel);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel atualizarViewModel) {
            if(ModelState.IsValid) {
                var usuario = await _usuarioRepository.PegarPeloId(atualizarViewModel.Id);

                usuario.Nome = atualizarViewModel.Nome;
                usuario.CPF = atualizarViewModel.CPF;
                usuario.UserName = atualizarViewModel.NomeUsuario;
                usuario.Email = atualizarViewModel.Email;
                usuario.Telefone = atualizarViewModel.Telefone;

                await _usuarioRepository.AtualizarUsuario(usuario);
                _logger.LogInformation("Atualizando usuário");

                return RedirectToAction("Index", "Usuarios");
                }
            _logger.LogError("Informações inválidas");

            return View(atualizarViewModel);
            }


        }
    }
