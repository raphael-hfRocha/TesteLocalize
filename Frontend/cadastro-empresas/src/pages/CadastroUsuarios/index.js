export default {
    created() { },
    name: 'CadastroUsuarios',
    props: ['id'],
    data() {
        return {}
    },
    methods: {},
    computed: {
        usuario: {
            get() {
                return this.$store.state.usuario;
            },
            set(usuario) {
                this.$store.commit('setUsuario', usuario);
            }
        },
        empresa: {
            get() {
                return this.$store.state.empresa;
            },
            set(empresa) {
                this.$store.commit('setEmpresa', empresa);
            }
        }
    }
}