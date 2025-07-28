export default {
    async created() {
        await this.limparCamposLogin();
        this.usuario.isLoggedIn = false;
    },
    name: 'login',
    data() {
        return {
            controller: 'Auth'
        }
    },
    methods: {
        async fazerLogin() {
            this.$http.post(`api/${this.controller}/login`, {
                email: this.usuario.email,
                senha: this.usuario.senha
            }).then((response) => {
                if (response.data && response.data.sucesso) {
                    const usuarioLogado = {
                        idUsuario: response.data.usuario.idUsuario,
                        nome: response.data.usuario.nome,
                        email: response.data.usuario.email,
                        token: response.data.token
                    };
                    
                    localStorage.setItem('authToken', response.data.token);
                    
                    this.$store.commit('setUsuario', usuarioLogado);
                    
                    console.log('✅ Login realizado com sucesso!', usuarioLogado);
                    console.log('🔑 Token salvo no localStorage');
                    
                    this.$router.push({ name: 'empresa' });
                }
            }).catch((error) => {
                console.error('Erro ao fazer login:', error);
            });
        },
        async limparCampos() {
            this.usuario.idUsuario = null
            this.usuario.nome = ''
            this.usuario.email = ''
            this.usuario.senha = ''
        },

        async limparCamposLogin() {
            this.usuario.email = ''
            this.usuario.senha = ''
            localStorage.removeItem('authToken');
        }
    },
    computed: {
        usuario: {
            get() {
                return this.$store.state.usuario;
            },
            set(value) {
                this.$store.commit('setUsuario', value);
            }
        }
    }
}