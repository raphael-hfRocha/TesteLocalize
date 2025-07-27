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

        public async Task<Empresa> ObterDadosCnpjBanco(Empresa empresa)
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
    }
}
