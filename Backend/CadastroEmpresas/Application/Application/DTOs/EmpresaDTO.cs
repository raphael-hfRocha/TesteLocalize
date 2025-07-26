using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using System.Reflection;

namespace CadastroEmpresas.Application.DTOs
{
    [Serializable]
    public class EmpresaDTO
    {
        private Int32 _idEmpresa;
        private String _nomeEmpresarial;
        private String _nomeFantasia;
        private String _cnpj;
        private String _situacao;
        private String _abertura;
        private String _tipo;
        private String _naturezaJuridica;
        private String _atividadePrincipal;
        private String _logradouro;
        private Int32 _numero;
        private String _complemento;
        private String _bairro;
        private String _municipio;
        private String _uf;
        private String _cep;
        private Usuario _usuario;

        public Int32 IdEmpresa { get => getIdEmpresa(); set => setIdEmpresa(value); }
        public String NomeEmpresarial { get => getNomeEmpresarial(); set => setNomeEmpresarial(value); }
        public String NomeFantasia { get => getNomeFantasia(); set => setNomeFantasia(value); }
        public String Cnpj { get => getCnpj(); set => setCnpj(value); }
        public String Situacao { get => getSituacao(); set => setSituacao(value); }
        public String Abertura { get => getAbertura(); set => setAbertura(value); }
        public String Tipo { get => getTipo(); set => setTipo(value); }
        public String NaturezaJuridica { get => getNaturezaJuridica(); set => setNaturezaJuridica(value); }
        public String AtividadePrincipal { get => getAtividadePrincipal(); set => setAtividadePrincipal(value); }
        public String Logradouro { get => getLogradouro(); set => setLogradouro(value); }
        public Int32 Numero { get => getNumero(); set => setNumero(value); }
        public String Complemento { get => getComplemento(); set => setComplemento(value); }
        public String Bairro { get => getBairro(); set => setBairro(value); }
        public String Municipio { get => getMunicipio(); set => setMunicipio(value); }
        public String Uf { get => getUf(); set => setUf(value); }
        public String Cep { get => getCep(); set => setCep(value); }
        public Usuario Usuario { get => getUsuario(); set => setUsuario(value); }

        private Int32 getIdEmpresa()
        {
            return _idEmpresa;
        }
        private void setIdEmpresa(Int32 valor)
        {
            _idEmpresa = valor;
        }
        private String getNomeEmpresarial()
        {
            return _nomeEmpresarial;
        }
        private void setNomeEmpresarial(String valor)
        {
            _nomeEmpresarial = valor;
        }
        private String getNomeFantasia()
        {
            return _nomeFantasia;
        }
        private void setNomeFantasia(String valor)
        {
            _nomeFantasia = valor;
        }
        private String getCnpj()
        {
            return _cnpj;
        }
        private void setCnpj(String valor)
        {
            _cnpj = valor;
        }
        private String getSituacao()
        {
            return _situacao;
        }
        private void setSituacao(String valor)
        {
            _situacao = valor;
        }
        private String getAbertura()
        {
            return _abertura;
        }
        private void setAbertura(String valor)
        {
            _abertura = valor;
        }
        private String getTipo()
        {
            return _tipo;
        }
        private void setTipo(String valor)
        {
            _tipo = valor;
        }
        private String getNaturezaJuridica()
        {
            return _naturezaJuridica;
        }
        private void setNaturezaJuridica(String valor)
        {
            _naturezaJuridica = valor;
        }
        private String getAtividadePrincipal()
        {
            return _atividadePrincipal;
        }
        private void setAtividadePrincipal(String valor)
        {
            _atividadePrincipal = valor;
        }
        private String getLogradouro()
        {
            return _logradouro;
        }
        private void setLogradouro(String valor)
        {
            _logradouro = valor;
        }
        private Int32 getNumero()
        {
            return _numero;
        }
        private void setNumero(Int32 valor)
        {
            _numero = valor;
        }
        private String getComplemento()
        {
            return _complemento;
        }
        private void setComplemento(String valor)
        {
            _complemento = valor;
        }
        private String getBairro()
        {
            return _bairro;
        }
        private void setBairro(String valor)
        {
            _bairro = valor;
        }
        private String getMunicipio()
        {
            return _municipio;
        }
        private void setMunicipio(String valor)
        {
            _municipio = valor;
        }
        private String getUf()
        {
            return _uf;
        }
        private void setUf(String valor)
        {
            _uf = valor;
        }
        private String getCep()
        {
            return _cep;
        }
        private void setCep(String valor)
        {
            _cep = valor;
        }
        private Usuario getUsuario()
        {
            return _usuario;
        }
        private void setUsuario(Usuario valor)
        {
            _usuario = valor;
        }
    }
}
