using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public UsuarioDto? Usuario { get; set; }
        public DateTime? ExpiracaoToken { get; set; }
    }

    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CadastroUsuarioDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }

    public class EmpresaDto
    {
        public Int32 IdEmpresa { get; set; }
        
        [Required(ErrorMessage = "Nome empresarial é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome empresarial deve ter no máximo 200 caracteres")]
        public String NomeEmpresarial { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Nome fantasia deve ter no máximo 200 caracteres")]
        public String NomeFantasia { get; set; } = string.Empty;
        
        [StringLength(18, ErrorMessage = "CNPJ deve ter no máximo 18 caracteres")]
        public String Cnpj { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Situação deve ter no máximo 50 caracteres")]
        public String Situacao { get; set; } = string.Empty;
        
        public String Abertura { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Tipo deve ter no máximo 100 caracteres")]
        public String Tipo { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Natureza jurídica deve ter no máximo 100 caracteres")]
        public String NaturezaJuridica { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Atividade principal deve ter no máximo 200 caracteres")]
        public String AtividadePrincipal { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Logradouro deve ter no máximo 200 caracteres")]
        public String Logradouro { get; set; } = string.Empty;
        
        [StringLength(10, ErrorMessage = "Número deve ter no máximo 10 caracteres")]
        public String Numero { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres")]
        public String Complemento { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Bairro deve ter no máximo 100 caracteres")]
        public String Bairro { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Município deve ter no máximo 100 caracteres")]
        public String Municipio { get; set; } = string.Empty;
        
        [StringLength(2, ErrorMessage = "UF deve ter exatamente 2 caracteres")]
        public String Uf { get; set; } = string.Empty;
        
        [StringLength(10, ErrorMessage = "CEP deve ter no máximo 10 caracteres")]
        public String Cep { get; set; } = string.Empty;
        
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public Int32 IdUsuario { get; set; }
    }
}
