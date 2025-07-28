using Application.DTOs;
using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public AuthService(HttpClient httpClient, AuthRepository usuarioRepository, TokenService tokenService)
        {
            _httpClient = httpClient;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "Dados do usuário nulos.");
            }

            // Validar se email já existe
            var emailExiste = await _usuarioRepository.EmailJaExiste(usuario.Email);
            if (emailExiste)
            {
                throw new InvalidOperationException("Este email já está cadastrado.");
            }

            // Validações básicas
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                throw new ArgumentException("Nome é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                throw new ArgumentException("Email é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Senha))
            {
                throw new ArgumentException("Senha é obrigatória.");
            }

            if (usuario.Senha.Length < 6)
            {
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");
            }

            var retorno = _usuarioRepository.CadastrarUsuario(usuario);
            return retorno;
        }

        public async Task<(Usuario? usuario, string token)> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("Email e senha são obrigatórios.");
            }

            var usuario = new Usuario { Email = email, Senha = senha };
            var usuarioAutenticado = await _usuarioRepository.Login(usuario);

            if (usuarioAutenticado == null)
            {
                throw new UnauthorizedAccessException("Email ou senha incorretos.");
            }

            // Gerar token JWT
            var token = _tokenService.Generate(usuarioAutenticado);

            return (usuarioAutenticado, token);
        }

        public async Task<Usuario?> ObterDadosUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "Usuario não pode ser nulo.");
            }

            var retorno = await _usuarioRepository.ObterDadosUsuario(usuario);
            return retorno;
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email é obrigatório.");
            }

            return await _usuarioRepository.ObterUsuarioPorEmail(email);
        }
    }
}
