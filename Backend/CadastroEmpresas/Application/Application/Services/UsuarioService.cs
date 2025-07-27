using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Infrastructure.Repositories; // Certifique-se de que o projeto ou biblioteca "Infrastructure" está referenciado corretamente.
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(HttpClient httpClient, UsuarioRepository usuarioRepository) // Adicionado parâmetro para injetar a dependência.
        {
            _httpClient = httpClient;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "Dados do usuario nulos.");
            }

            var retorno = _usuarioRepository.CadastrarUsuario(usuario);

            return retorno;
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            var retorno = await _usuarioRepository.Login(usuario);

            if (usuario.Email == null && usuario.Senha == null)
            {
                throw new Exception("E-mail ou senha são nulos.");
            } 
            else if(usuario.Email == null || usuario.Senha == null)
            {
                throw new Exception("E-mail ou senha são nulos.");
            }

            return retorno;
        }

        public async Task<Usuario> ObterDadosUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "Usuario não pode ser nulo.");
            }

            var retorno = await _usuarioRepository.ObterDadosUsuario(usuario);

            return retorno;
        }
    }
}
