/* eslint-disable */
export default {
    async created() {
        await this.returnClassUsuario();
        await this.returnClassEmpresa();
        await this.limparCamposUsuario();
        this.usuario.isLoggedIn = false;
    },
    name: 'CadastroUsuarios',
    data() {
        return {
            controller: 'Auth',
        }
    },
    methods: {
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
        async cadastrarUsuario() {
            await this.$http.post(`api/${this.controller}/cadastro`, this.usuario)
                .then((response) => {
                    this.usuario = response.data;
                    this.$router.push({ name: 'login' });
                }).catch((error) => {
                    console.error('Erro ao cadastrar usuário:', error);
                    this.$router.push({ name: 'cadastroUsuario' });
                })
        },
        async limparCamposUsuario() {
            this.usuario.idUsuario = null
            this.usuario.nome = ''
            this.usuario.email = ''
            this.usuario.senha = ''
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
        },
        empresa: {
            get() {
                return this.$store.state.empresa;
            },
            set(value) {
                this.$store.commit('setEmpresa', value);
            }
        }
    }
}