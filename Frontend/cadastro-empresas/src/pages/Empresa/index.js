// eslint-disable-next-line
export default {
    async created() {
        await this.verificaUsuarioLogado();
        await this.recarregarEmpresas();
    },
    name: 'UsuarioPage',
    data() {
        return {
            controller: 'Empresa',
            controllerAuth: 'Auth',
            visualizando: true,
            carregandoEmpresas: false,
            header: [
                { key: 'idEmpresa', label: 'ID' },
                { key: 'nomeEmpresarial', label: 'Nome Empresarial' },
                { key: 'nomeFantasia', label: 'Nome Fantasia' },
                { key: 'cnpj', label: 'CNPJ' },
                { key: 'situacao', label: 'Situação' },
            ],
        }
    },
    methods: {
        limparDadosEmpresa() {
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
        limparDadosUsuario() {
            this.usuario.idUsuario = null
            this.usuario.nome = ''
            this.usuario.email = ''
            this.usuario.senha = ''
        },
        async returnClassEmpresa() {
            this.$http.get(`api/${this.controller}/ReturnClass`)
                .then((response) => {
                    this.empresa = response.data;
                    console.log("Return class Empresa: ", this.empresa);
                }).catch((error) => {
                    console.error('Erro ao retornar classe Empresa:', error);
                })
        },
        async returnClassUsuario() {
            this.$http.get(`api/${this.controllerAuth}/ReturnClass`)
                .then((response) => {
                    this.usuario = response.data;
                    console.log("Return class Usuário: ", this.usuario);
                }).catch((error) => {
                    console.error('Erro ao retornar classe Usuário:', error);
                })
        },
        async puxarUsuarioLogado() {
            await this.$http.get(`api/${this.controllerAuth}/me`)
                .then((response) => {
                    this.usuario = response.data;
                    console.log("Usuário logado:", this.usuario);
                }).catch((error) => {
                    console.error('Erro ao puxar usuário logado:', error);
                })
        },
        async verificaUsuarioLogado() {
            await this.puxarUsuarioLogado();
            if (this.usuario.idUsuario === null) {
                this.limparDadosEmpresa();
                await this.logout();
            }
        },

        async logout() {
            this.limparDadosUsuario();
            localStorage.removeItem('authToken');
            this.$router.push({ name: 'login' });
        },

        async carregarMinhasEmpresas() {
            await this.puxarUsuarioLogado();

            await this.$http.get(`api/${this.controllerAuth}/SearchEmpresasUsuario`)
                .then((response) => {
                    this.empresas = response.data;
                    console.log("Puxando dados de empresas: ", this.empresas);
                }).catch((error) => {
                    console.log("Erro ao carregar as empresas: ", error)
                })
        },
        async recarregarEmpresas() {
            await this.carregarMinhasEmpresas();
        },
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
        empresas: {
            get() {
                return this.$store.state.empresas;
            },
            set(value) {
                this.$store.commit('setEmpresas', value);
            }
        },
    }
}