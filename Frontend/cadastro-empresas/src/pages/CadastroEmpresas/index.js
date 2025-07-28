export default {
    async created() {
        await this.limparDadosEmpresa();
        await this.verificaUsuarioLogado();
    },
    name: 'CadastroEmpresas',
    data() {
        return {
            controller: 'Empresa',
            controllerAuth: 'Auth'
        }
    },
    methods: {
        async verificaUsuarioLogado() {
            if (this.usuario.idUsuario === null || this.usuario.idUsuario === undefined || this.usuario.idUsuario <= 0) {
                this.$router.push({ name: 'home' });
            }
        },
        async cadastrarEmpresa() {
            const empresaData = {
                nomeEmpresarial: this.empresa.nomeEmpresarial || 'Nome Empresarial Exemplo',
                nomeFantasia: this.empresa.nomeFantasia || 'Nome Fantasia Exemplo',
                cnpj: this.empresa.cnpj || '',
                situacao: this.empresa.situacao || 'Ativa',
                abertura: this.empresa.abertura || '2023-01-01',
                tipo: this.empresa.tipo || 'MATRIZ',
                naturezaJuridica: this.empresa.naturezaJuridica || 'Sociedade Empresária Limitada',
                atividadePrincipal: this.empresa.atividadePrincipal || 'Comércio Varejista',
                logradouro: this.empresa.logradouro || 'Rua Exemplo',
                numero: this.empresa.numero || '123',
                complemento: this.empresa.complemento || 'Apto 1',
                bairro: this.empresa.bairro || 'Bairro Exemplo',
                municipio: this.empresa.municipio || 'Cidade Exemplo',
                uf: this.empresa.uf || 'SP',
                cep: this.empresa.cep || '12345-678',
                idUsuario: this.usuario.idUsuario || null
            };

            await this.$http
                .post(`api/${this.controllerAuth}/SearchEmpresasUsuario`,
                    empresaData)
                .then((response) => {
                    this.empresa = response.data;
                    console.log("Empresa cadastrada com sucesso:", this.empresa);
                    this.$store.commit('setEmpresa', this.empresa);
                    this.$router.push({ name: 'empresa' });
                }).catch((error) => {
                    console.error('Erro ao cadastrar usuário:', error);
                    this.$router.push({ name: 'cadastroEmpresa' });
                })
        },
    },
    async limparDadosEmpresa() {
        this.empresa.idEmpresa = null
        this.empresa.nomeEmpresarial = '',
            this.empresa.nomeFantasia = '',
            this.empresa.cnpj = '',
            this.empresa.situacao = '',
            this.empresa.abertura = '',
            this.empresa.tipo = '',
            this.empresa.naturezaJuridica = '',
            this.empresa.atividadePrincipal = '',
            this.empresa.logradouro = '',
            this.empresa.numero = '',
            this.empresa.complemento = '',
            this.empresa.bairro = '',
            this.empresa.municipio = '',
            this.empresa.uf = '',
            this.empresa.cep = ''
    },
    computed: {
        usuario: {
            get() {
                return this.$store.state.usuario;
            },
            set(value) {
                this.$store.commit('setUsuario', value);
            }
        },
        empresa: {
            get() {
                return this.$store.state.empresa;
            },
            set(value) {
                this.$store.commit('setEmpresa', value);
            }
        },
    }
}
