using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Security.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PasswordHasherService _passwordHasher;

        public AuthRepository(ApplicationDbContext applicationDbContext, PasswordHasherService passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Usuario?> Login(Usuario usuario)
        {
            var usuarioEncontrado = await _applicationDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (usuarioEncontrado == null)
            {
                return null;
            }

            bool senhaValida = _passwordHasher.VerifyPassword(usuario.Senha, usuarioEncontrado.Senha);
            if (!senhaValida)
            {
                return null;
            }

            return usuarioEncontrado;
        }

        public async Task<Usuario?> ObterDadosUsuario(Usuario usuario)
        {
            var result = await _applicationDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);
            return result;
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            var result = await _applicationDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            usuario.Senha = _passwordHasher.HashPassword(usuario.Senha);   
            _applicationDbContext.Usuarios.Add(usuario);
            _applicationDbContext.SaveChanges();
            return usuario;
        }

        public async Task<bool> EmailJaExiste(string email)
        {
            return await _applicationDbContext.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        public List<Empresa> SearchEmpresasByUsuario(Usuario usuario)
        {
            var empresas = _applicationDbContext.Empresas
                .Where(e => e.IdUsuario == usuario.IdUsuario)
                .ToList();

            return empresas;
        }
    }
}
