export default {
    async created() { 
    },
    props: ['id'],
    name: 'login',
    data() {
        return {
            controller1: 'Usuario'
        }
    },
    methods: {
        
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