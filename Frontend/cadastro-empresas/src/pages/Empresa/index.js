export default {
    async created() {
        await this.verificaUsuarioLogado();
        this.usuario.isLoggedIn = true;
        await this.carregarMinhasEmpresas();
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
        async verificaUsuarioLogado() {
            if (this.usuario.idUsuario === null || this.usuario.idUsuario === undefined || this.usuario.idUsuario <= 0) {
                this.$router.push({ name: 'home' });
            }
        },

        async logout() {
            try {
                console.log('🚪 Fazendo logout...');
                
                // Usar a mutation do store para logout
                this.$store.commit('logout');
                
                // Limpar dados das empresas
                this.$store.commit('setEmpresa', {
                    idEmpresa: null,
                    nomeEmpresarial: '',
                    nomeFantasia: '',
                    cnpj: '',
                    situacao: '',
                    abertura: '',
                    tipo: '',
                    naturezaJuridica: '',
                    atividadePrincipal: '',
                    logradouro: '',
                    numero: '',
                    complemento: '',
                    bairro: '',
                    municipio: '',
                    uf: '',
                    cep: '',
                    usuario: {
                        idUsuario: null,
                        nome: '',
                        email: '',
                        senha: '',
                        token: null,
                        isLoggedIn: false
                    },
                    idUsuario: null
                });
                
                this.$store.commit('setEmpresas', []);
                
                console.log('✅ Logout realizado com sucesso!');
                
                // Redirecionar para a página inicial
                this.$router.push({ name: 'home' });
                
            } catch (error) {
                console.error('❌ Erro durante logout:', error);
                // Mesmo em caso de erro, usar a mutation e redirecionar
                this.$store.commit('logout');
                this.$router.push({ name: 'home' });
            }
        },

        async carregarMinhasEmpresas() {
            try {
                this.carregandoEmpresas = true;
                
                // Primeiro tentar debug
                console.log('🔍 Testando endpoint de debug...');
                await this.testarDebugEmpresas();
                
                const response = await this.$http.get(`api/${this.controllerAuth}/SearchEmpresasUsuario`);
                
                console.log('✅ Empresas carregadas:', response.data);
                
                this.items = response.data || [];
                this.empresas = response.data || [];
                
            } catch (error) {
                console.error('❌ Erro ao carregar empresas:', error);
                
                if (error.response && error.response.status === 401) {
                    console.log('🔐 Token inválido, você está sendo direcionado para login');
                    localStorage.removeItem('authToken');
                    this.$store.commit('setUsuario', {
                        idUsuario: null,
                        nome: '',
                        email: '',
                        senha: '',
                        token: null,
                        isLoggedIn: false
                    });
                    this.$router.push({ name: 'home' });
                } else if (error.response && error.response.status === 500) {
                    console.log('🛠️ Erro 500 detectado, tentando criar empresa de teste...');
                    await this.criarEmpresaTeste();
                }
                
                this.items = [];
                this.empresas = [];
            } finally {
                this.carregandoEmpresas = false;
            }
        },

        async testarDebugEmpresas() {
            try {
                console.log('🔍 Chamando endpoint de debug...');
                const response = await this.$http.get(`api/${this.controllerAuth}/debug-empresas`);
                console.log('🔍 Debug response:', response.data);
                return response.data;
            } catch (error) {
                console.error('❌ Erro no debug:', error);
                if (error.response) {
                    console.error('❌ Debug error response:', error.response.data);
                }
                throw error;
            }
        },

        async criarEmpresaTeste() {
            try {
                console.log('🏢 Criando empresa de teste...');
                const response = await this.$http.post(`api/${this.controllerAuth}/criar-empresa-teste`);
                console.log('🏢 Empresa de teste criada:', response.data);
                
                // Tentar carregar novamente após criar empresa de teste
                await this.carregarMinhasEmpresas();
                
                return response.data;
            } catch (error) {
                console.error('❌ Erro ao criar empresa de teste:', error);
                if (error.response) {
                    console.error('❌ Error response:', error.response.data);
                }
            }
        },
// eslint-disable-next-line
        async puxarDadosLista(idUsuario) {
            await this.carregarMinhasEmpresas();
        },

        getStatusVariant(situacao) {
            switch(situacao?.toLowerCase()) {
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
        }
    }
}