/* eslint-disable */
import { BTable, BTableSimple } from 'bootstrap-vue'

export default {
    async created() {
        await this.limparCamposUsuario();
        this.usuario.isLoggedIn = false;
    },
    name: 'CadastroUsuarios',
    components: {
        BTable,
        BTableSimple
    },
    data() {
        return {
            controller: 'Auth',
        }
    },
    methods: {
        async cadastrarUsuario() {
            await this.$http.post(`api/${this.controller}/cadastro`, this.usuario)
                .then((response) => {
                    this.usuario = response.data;
                    this.$router.push({ name: 'usuario' });
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