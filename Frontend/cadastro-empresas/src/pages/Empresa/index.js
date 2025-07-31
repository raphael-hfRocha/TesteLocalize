// eslint-disable-next-line
export default {
    async created() {
        await this.verificaUsuarioLogado();
        this.usuario.isLoggedIn = true;
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
                { key: 'actions', label: 'Ações' }
            ],
            items: []
        }
    },
    methods: {
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
            if (this.usuario.idUsuario === null || this.usuario.idUsuario === undefined || this.usuario.idUsuario <= 0) {
                await this.logout();
                this.$router.push({ name: 'home' });
            }
        },

        async logout() {
            this.usuario.idUsuario = null,
                this.usuario.nome = '',
                this.usuario.email = '',
                this.usuario.senha = '',
                this.usuario.token = null,
                this.usuario.isLoggedIn = false
            this.$router.push({ name: 'home' });
        },

        async carregarMinhasEmpresas() {
            await this.puxarUsuarioLogado();

            await this.$http.get(`api/${this.controllerAuth}/SearchEmpresasUsuario`, { params: { idUsuario: this.usuario.idUsuario } })
                .then((response) => {
                    this.empresas = response.data;
                    console.log("Puxando dados de empresas: ", this.empresas);
                }).catch((error) => {
                    console.log("Erro ao carregar as empresas: ", error)
                })
        },
        getStatusVariant(situacao) {
            switch (situacao?.toLowerCase()) {
                case 'ativa':
                    return 'success';
                case 'inativa':
                    return 'danger';
                case 'suspensa':
                    return 'warning';
                default:
                    return 'secondary';
            }
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