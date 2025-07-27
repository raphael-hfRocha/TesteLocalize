using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UsuarioRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            var validaEmail = await _applicationDbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
            var validaSenha = await _applicationDbContext.Usuarios.FirstOrDefaultAsync(u => u.Senha == usuario.Senha);            
            return usuario;
        }

        public async Task<Usuario> ObterDadosUsuario(Usuario usuario)
        {
            var result = await _applicationDbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);
            return result;
        }

        public Usuario CadastrarUsuario(Usuario usuario)
        {
            _applicationDbContext.Usuarios.Add(usuario);
            _applicationDbContext.SaveChanges();
            return usuario;
        }
    }
}
