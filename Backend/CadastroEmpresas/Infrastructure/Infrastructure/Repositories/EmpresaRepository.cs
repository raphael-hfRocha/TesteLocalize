using CadastroEmpresas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmpresaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EmpresaRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Empresa?> ObterDadosCnpjBanco(Empresa empresa)
        {
            var result = await _applicationDbContext.Empresas.FirstOrDefaultAsync(e => e.Cnpj == empresa.Cnpj);
            return result;
        }

        public Empresa CadastrarEmpresa(Empresa empresa) 
        {
            _applicationDbContext.Empresas.Add(empresa);
            _applicationDbContext.SaveChanges();
            return empresa;
        }

        public List<Empresa> ObterEmpresas()
        {
            return _applicationDbContext.Empresas.ToList();
        }

        public async Task<List<Empresa>> ObterEmpresasPorUsuario(int idUsuario)
        {
            return await _applicationDbContext.Empresas
                .Where(e => e.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task<int> ContarEmpresasTotal()
        {
            return await _applicationDbContext.Empresas.CountAsync();
        }

        public async Task<List<Empresa>> ObterTodasEmpresas()
        {
            return await _applicationDbContext.Empresas.ToListAsync();
        }
    }
}
