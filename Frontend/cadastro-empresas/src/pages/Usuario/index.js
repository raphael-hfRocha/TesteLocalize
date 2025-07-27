export default {
    async created() { 
        await this.carregaReturnClassUsuario();
    },
    name: 'UsuarioPage',
    data: () => ({
        controller1: 'Usuario',
    }),
    methods: {
        async carregaReturnClassUsuario() {
            await this.$http.get(`api/${this.controller1}/ReturnClass`)
                .then(response => {
                    this.usuario = response.data;
                }).error(error => {
                    console.error('Erro ao carregar usuário:', error);
                });
        }
    },
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