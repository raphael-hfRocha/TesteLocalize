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

        async carregarMinhasEmpresas() {
            try {
                this.carregandoEmpresas = true;
                
                // Usar o endpoint correto do AuthController
                const response = await this.$http.get(`api/${this.controllerAuth}/SearchEmpresasUsuario`);
                
                console.log('✅ Empresas carregadas:', response.data);
                
                // Atualizar tanto items (para a tabela) quanto empresas (para o store)
                this.items = response.data || [];
                this.empresas = response.data || [];
                
            } catch (error) {
                console.error('❌ Erro ao carregar empresas:', error);
                
                // Se erro 401, redirecionar para login
                if (error.response && error.response.status === 401) {
                    console.log('Token inválido, redirecionando para login...');
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
                }
                
                // Limpar dados em caso de erro
                this.items = [];
                this.empresas = [];
            } finally {
                this.carregandoEmpresas = false;
            }
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

        async puxarDadosLista(idUsuario) {
            // Método legado - manter para compatibilidade, mas usar carregarMinhasEmpresas
            await this.carregarMinhasEmpresas();
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