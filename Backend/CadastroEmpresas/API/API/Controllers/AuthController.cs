using Application.Services;

using CadastroEmpresas.src.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Linq;
using Security.PasswordHasher;
using CadastroEmpresas.Domain.Entities;
using API.DTOs;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação e gerenciamento de usuários
    /// </summary>
    /// <remarks>
    /// **SISTEMA DE AUTENTICAÇÃO JWT**
    /// 
    /// Este controller oferece um sistema completo de autenticação baseado em JWT (JSON Web Tokens).
    /// 
    /// **Fluxo básico de uso:**
    /// 1. **Cadastro:** POST /api/auth/cadastro - Criar nova conta
    /// 2. **Login:** POST /api/auth/login - Obter token de acesso
    /// 3. **Uso do Token:** Incluir "Authorization: Bearer {token}" nas requisições protegidas
    /// 4. **Perfil:** GET /api/auth/me - Obter dados do usuário logado
    /// 5. **Validação:** GET /api/auth/validate-token - Verificar se token ainda é válido
    /// 
    /// **Segurança implementada:**
    /// - Senhas são criptografadas com SHA256 + Salt
    /// - Tokens JWT têm expiração de 2 horas
    /// - Validação de email único no sistema
    /// - Endpoints protegidos requerem token válido
    /// 
    /// **Exemplo de uso com JavaScript:**
    /// ```javascript
    /// // 1. Login
    /// const loginResponse = await fetch('/api/auth/login', {
    ///   method: 'POST',
    ///   headers: { 'Content-Type': 'application/json' },
    ///   body: JSON.stringify({ email: 'user@example.com', senha: 'password' })
    /// });
    /// const { token } = await loginResponse.json();
    /// 
    /// // 2. Usar token em requisições
    /// const userResponse = await fetch('/api/auth/me', {
    ///   headers: { 'Authorization': `Bearer ${token}` }
    /// });
    /// ```
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _usuarioService;
        private readonly EmpresaService _empresaService;

        public AuthController(AuthService usuarioService, EmpresaService empresaService)
        {
            _usuarioService = usuarioService;
            _empresaService = empresaService;
        }

        /// <summary>
        /// Endpoint para login de usuários
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** POST /api/auth/login
        /// 
        /// **Body (JSON):**
        /// ```json
        /// {
        ///   "email": "usuario@example.com",
        ///   "senha": "suaSenhaSecreta"
        /// }
        /// ```
        /// 
        /// **Resposta de Sucesso (200):**
        /// ```json
        /// {
        ///   "sucesso": true,
        ///   "mensagem": "Login realizado com sucesso!",
        ///   "token": "eyJhbGciOiJIUzI1NiIs...",
        ///   "expiracaoToken": "2024-12-21T10:30:00Z",
        ///   "usuario": {
        ///     "idUsuario": 1,
        ///     "nome": "João Silva",
        ///     "email": "usuario@example.com"
        ///   }
        /// }
        /// ```
        /// 
        /// **Resposta de Erro (401):**
        /// ```json
        /// {
        ///   "sucesso": false,
        ///   "mensagem": "Email ou senha incorretos."
        /// }
        /// ```
        /// 
        /// **IMPORTANTE:** Salve o token retornado para usar em requisições autenticadas!
        /// </remarks>
        /// <param name="loginRequest">Dados de login (email e senha)</param>
        /// <returns>Token JWT e dados do usuário</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                // Validar dados de entrada
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new LoginResponseDto
                    {
                        Sucesso = false,
                        Mensagem = $"Dados inválidos: {string.Join(", ", errors)}"
                    });
                }

                // Tentar fazer login
                var (usuario, token) = await _usuarioService.Login(loginRequest.Email, loginRequest.Senha);

                // Criar resposta de sucesso
                var response = new LoginResponseDto
                {
                    Sucesso = true,
                    Mensagem = "Login realizado com sucesso!",
                    Token = token,
                    ExpiracaoToken = DateTime.UtcNow.AddHours(2),
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = usuario.IdUsuario,
                        Nome = usuario.Nome,
                        Email = usuario.Email
                    }
                };

                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Sucesso = false,
                    Mensagem = "Email ou senha incorretos."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new LoginResponseDto
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Sucesso = false,
                    Mensagem = "Erro interno do servidor."
                });
            }
        }

        /// <summary>
        /// Endpoint para cadastro de novos usuários
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** POST /api/auth/cadastro
        /// 
        /// **Body (JSON):**
        /// ```json
        /// {
        ///   "nome": "João Silva",
        ///   "email": "joao.silva@example.com",
        ///   "senha": "minhasenhasegura123"
        /// }
        /// ```
        /// 
        /// **Resposta de Sucesso (201):**
        /// ```json
        /// {
        ///   "idUsuario": 1,
        ///   "nome": "João Silva",
        ///   "email": "joao.silva@example.com"
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Email já existe (409):**
        /// ```json
        /// {
        ///   "sucesso": false,
        ///   "mensagem": "Este email já está cadastrado no sistema."
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Dados inválidos (400):**
        /// ```json
        /// {
        ///   "sucesso": false,
        ///   "mensagem": "Dados inválidos: Email deve ter um formato válido"
        /// }
        /// ```
        /// 
        /// **NOTA:** A senha será automaticamente criptografada antes de ser salva no banco.
        /// </remarks>
        /// <param name="cadastroRequest">Dados do novo usuário</param>
        /// <returns>Dados do usuário criado (sem senha)</returns>
        [HttpPost("cadastro")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioDto>> Cadastro([FromBody] CadastroUsuarioDto cadastroRequest)
        {
            try
            {
                // Validar dados de entrada
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new
                    {
                        Sucesso = false,
                        Mensagem = $"Dados inválidos: {string.Join(", ", errors)}"
                    });
                }

                // Criar usuário
                var novoUsuario = new Usuario
                {
                    Nome = cadastroRequest.Nome,
                    Email = cadastroRequest.Email,
                    Senha = cadastroRequest.Senha
                };

                var usuarioCriado = await _usuarioService.CadastrarUsuario(novoUsuario);

                // Retornar dados do usuário (sem senha)
                var usuarioResponse = new UsuarioDto
                {
                    IdUsuario = usuarioCriado.IdUsuario,
                    Nome = usuarioCriado.Nome,
                    Email = usuarioCriado.Email
                };

                return CreatedAtAction(
                    nameof(ObterUsuario),
                    new { id = usuarioCriado.IdUsuario },
                    usuarioResponse
                );
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("email já está cadastrado"))
            {
                return Conflict(new
                {
                    Sucesso = false,
                    Mensagem = "Este email já está cadastrado."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = "Erro interno do servidor."
                });
            }
        }

        /// <summary>
        /// Endpoint para obter dados do usuário logado
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** GET /api/auth/me
        /// 
        /// **Headers obrigatórios:**
        /// ```
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
        /// ```
        /// 
        /// **Resposta de Sucesso (200):**
        /// ```json
        /// {
        ///   "idUsuario": 1,
        ///   "nome": "João Silva",
        ///   "email": "joao.silva@example.com"
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Token inválido (401):**
        /// ```json
        /// {
        ///   "mensagem": "Token inválido."
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Usuário não encontrado (404):**
        /// ```json
        /// {
        ///   "mensagem": "Usuário não encontrado."
        /// }
        /// ```
        /// 
        /// **IMPORTANTE:** Este endpoint requer autenticação. Use o token obtido no login.
        /// </remarks>
        /// <returns>Dados do usuário atual</returns>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioDto>> ObterUsuarioLogado()
        {
            try
            {
                // Extrair email do token JWT
                var email = User.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { Mensagem = "Token inválido." });
                }

                var usuario = await _usuarioService.ObterUsuarioPorEmail(email);
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                var usuarioResponse = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };

                return Ok(usuarioResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor." });
            }
        }

        /// <summary>
        /// Endpoint para obter usuário por ID
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** GET /api/auth/{id}
        /// **Exemplo:** GET /api/auth/1
        /// 
        /// **Headers obrigatórios:**
        /// ```
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
        /// ```
        /// 
        /// **Resposta de Sucesso (200):**
        /// ```json
        /// {
        ///   "idUsuario": 1,
        ///   "nome": "João Silva",
        ///   "email": "joao.silva@example.com"
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Usuário não encontrado (404):**
        /// ```json
        /// {
        ///   "mensagem": "Usuário não encontrado."
        /// }
        /// ```
        /// 
        /// **IMPORTANTE:** Este endpoint requer autenticação e é usado para buscar dados de qualquer usuário pelo ID.
        /// </remarks>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioDto>> ObterUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterDadosUsuario(new Usuario { IdUsuario = id });
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                var usuarioResponse = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };

                return Ok(usuarioResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor." });
            }
        }



        /// <summary>
        /// Endpoint para validar token
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** GET /api/auth/validate-token
        /// 
        /// **Headers obrigatórios:**
        /// ```
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
        /// ```
        /// 
        /// **Resposta de Sucesso (200):**
        /// ```json
        /// {
        ///   "sucesso": true,
        ///   "mensagem": "Token válido.",
        ///   "usuario": "joao.silva@example.com"
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Token inválido/expirado (401):**
        /// ```json
        /// {
        ///   "message": "Unauthorized"
        /// }
        /// ```
        /// 
        /// **USO:** Este endpoint é útil para verificar se o token ainda é válido antes de fazer outras operações.
        /// </remarks>
        /// <returns>Status do token</returns>
        [HttpGet("validate-token")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult ValidarToken()
        {
            return Ok(new
            {
                Sucesso = true,
                Mensagem = "Token válido.",
                Usuario = User.Identity?.Name
            });
        }

        /// <summary>
        /// Endpoint para buscar empresas cadastradas pelo usuário logado
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** GET /api/auth/SearchEmpresasUsuario
        /// 
        /// **Headers obrigatórios:**
        /// ```
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
        /// ```
        /// 
        /// **Resposta de Sucesso (200):**
        /// ```json
        /// [
        ///   {
        ///     "idEmpresa": 1,
        ///     "nomeEmpresarial": "Empresa Exemplo LTDA",
        ///     "nomeFantasia": "Empresa Exemplo",
        ///     "cnpj": "12.345.678/0001-90",
        ///     "situacao": "Ativa",
        ///     "abertura": "01/01/2020",
        ///     "tipo": "MATRIZ"
        ///   }
        /// ]
        /// ```
        /// 
        /// **Resposta de Erro - Token inválido (401):**
        /// ```json
        /// {
        ///   "message": "Unauthorized"
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Usuário não encontrado (404):**
        /// ```json
        /// {
        ///   "mensagem": "Usuário não encontrado."
        /// }
        /// ```
        /// 
        /// **IMPORTANTE:** Este endpoint requer autenticação e retorna apenas as empresas do usuário logado.
        /// </remarks>
        /// <returns>Lista de empresas do usuário</returns>
        [HttpGet("SearchEmpresasUsuario")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObterMinhasEmpresas()
        {
            try
            {
                var email = User.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { Mensagem = "Token inválido." });
                }

                var usuario = await _usuarioService.ObterUsuarioPorEmail(email);
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                var empresas = await _empresaService.ObterEmpresasPorUsuario(usuario.IdUsuario);

                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Mensagem = "Erro interno do servidor.",
                    Detalhes = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        /// <summary>
        /// Endpoint de debug para testar conexão com banco
        /// </summary>
        [HttpGet("debug-empresas")]
        [Authorize]
        public async Task<ActionResult> DebugEmpresas()
        {
            try
            {
                var email = User.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { Mensagem = "Token inválido." });
                }

                var usuario = await _usuarioService.ObterUsuarioPorEmail(email);
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                // Debug: tentar contar empresas
                var totalEmpresas = await _empresaService.ObterEmpresasPorUsuario(usuario.IdUsuario);

                return Ok(new
                {
                    Usuario = new { usuario.IdUsuario, usuario.Nome, usuario.Email },
                    TotalEmpresas = totalEmpresas?.Count ?? 0,
                    Empresas = totalEmpresas,
                    Debug = "Endpoint funcionando corretamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Mensagem = "Erro interno do servidor.",
                    Detalhes = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message,
                    TipoErro = ex.GetType().Name
                });
            }
        }

        /// <summary>
        /// Endpoint para cadastrar nova empresa vinculada ao usuário logado
        /// </summary>
        /// <remarks>
        /// **Como usar este endpoint:**
        /// 
        /// **URL:** POST /api/auth/SearchEmpresasUsuario
        /// 
        /// **Headers obrigatórios:**
        /// ```
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
        /// ```
        /// 
        /// **Body (JSON):**
        /// ```json
        /// {
        ///   "nomeEmpresarial": "Minha Empresa LTDA",
        ///   "nomeFantasia": "Minha Empresa",
        ///   "cnpj": "12.345.678/0001-90",
        ///   "situacao": "Ativa",
        ///   "abertura": "01/01/2020",
        ///   "tipo": "MATRIZ",
        ///   "naturezaJuridica": "Sociedade Empresária Limitada",
        ///   "atividadePrincipal": "Comércio varejista",
        ///   "logradouro": "Rua das Flores",
        ///   "numero": "123",
        ///   "complemento": "Sala 1",
        ///   "bairro": "Centro",
        ///   "municipio": "São Paulo",
        ///   "uf": "SP",
        ///   "cep": "01000-000"
        /// }
        /// ```
        /// 
        /// **Resposta de Sucesso (201):**
        /// ```json
        /// {
        ///   "sucesso": true,
        ///   "mensagem": "Empresa cadastrada com sucesso!",
        ///   "empresa": {
        ///     "idEmpresa": 1,
        ///     "nomeEmpresarial": "Minha Empresa LTDA",
        ///     "nomeFantasia": "Minha Empresa",
        ///     "cnpj": "12.345.678/0001-90",
        ///     "situacao": "Ativa"
        ///   }
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Token inválido (401):**
        /// ```json
        /// {
        ///   "sucesso": false,
        ///   "mensagem": "Token inválido."
        /// }
        /// ```
        /// 
        /// **Resposta de Erro - Dados inválidos (400):**
        /// ```json
        /// {
        ///   "sucesso": false,
        ///   "mensagem": "Dados inválidos: Nome empresarial é obrigatório"
        /// }
        /// ```
        /// 
        /// **IMPORTANTE:** A empresa será automaticamente vinculada ao usuário logado através do token JWT.
        /// </remarks>
        /// <param name="empresaRequest">Dados da nova empresa</param>
        /// <returns>Dados da empresa criada</returns>
        [HttpPost("SearchEmpresasUsuario")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CadastrarEmpresaUsuario([FromBody] EmpresaDto empresaRequest)
        {
            try
            {
                // Validar dados de entrada
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new
                    {
                        Sucesso = false,
                        Mensagem = $"Dados inválidos: {string.Join(", ", errors)}"
                    });
                }

                // Obter usuário logado do token JWT
                var email = User.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new 
                    { 
                        Sucesso = false,
                        Mensagem = "Token inválido." 
                    });
                }

                var usuario = await _usuarioService.ObterUsuarioPorEmail(email);
                if (usuario == null)
                {
                    return NotFound(new 
                    { 
                        Sucesso = false,
                        Mensagem = "Usuário não encontrado." 
                    });
                }

                // Criar nova empresa com dados recebidos
                var novaEmpresa = new Empresa
                {
                    NomeEmpresarial = empresaRequest.NomeEmpresarial ?? "Nome Empresarial",
                    NomeFantasia = empresaRequest.NomeFantasia ?? "Nome Fantasia",
                    Cnpj = empresaRequest.Cnpj ?? "",
                    Situacao = empresaRequest.Situacao ?? "Ativa",
                    Abertura = empresaRequest.Abertura ?? DateTime.Now.ToString("yyyy-MM-dd"),
                    Tipo = empresaRequest.Tipo ?? "MATRIZ",
                    NaturezaJuridica = empresaRequest.NaturezaJuridica ?? "Sociedade Empresária Limitada",
                    AtividadePrincipal = empresaRequest.AtividadePrincipal ?? "Atividade Principal",
                    Logradouro = empresaRequest.Logradouro ?? "",
                    Numero = empresaRequest.Numero ?? "",
                    Complemento = empresaRequest.Complemento ?? "",
                    Bairro = empresaRequest.Bairro ?? "",
                    Municipio = empresaRequest.Municipio ?? "",
                    Uf = empresaRequest.Uf ?? "",
                    Cep = empresaRequest.Cep ?? "",
                    IdUsuario = usuario.IdUsuario  // Vincular ao usuário logado
                };

                // Cadastrar empresa no banco
                var empresaCriada = _empresaService.CadastrarEmpresa(novaEmpresa);

                // Criar resposta de sucesso
                var empresaResponse = new EmpresaDto
                {
                    IdEmpresa = empresaCriada.IdEmpresa,
                    NomeEmpresarial = empresaCriada.NomeEmpresarial,
                    NomeFantasia = empresaCriada.NomeFantasia,
                    Cnpj = empresaCriada.Cnpj,
                    Situacao = empresaCriada.Situacao,
                    Abertura = empresaCriada.Abertura,
                    Tipo = empresaCriada.Tipo,
                    NaturezaJuridica = empresaCriada.NaturezaJuridica,
                    AtividadePrincipal = empresaCriada.AtividadePrincipal,
                    Logradouro = empresaCriada.Logradouro,
                    Numero = empresaCriada.Numero,
                    Complemento = empresaCriada.Complemento,
                    Bairro = empresaCriada.Bairro,
                    Municipio = empresaCriada.Municipio,
                    Uf = empresaCriada.Uf,
                    Cep = empresaCriada.Cep,
                    IdUsuario = empresaCriada.IdUsuario
                };

                return CreatedAtAction(
                    nameof(ObterMinhasEmpresas),
                    new
                    {
                        Sucesso = true,
                        Mensagem = "Empresa cadastrada com sucesso!",
                        Empresa = empresaResponse
                    }
                );
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Mensagem = $"Dados obrigatórios não informados: {ex.ParamName}"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = "Erro interno do servidor.",
                    Detalhes = ex.Message
                });
            }
        }
    }
}
