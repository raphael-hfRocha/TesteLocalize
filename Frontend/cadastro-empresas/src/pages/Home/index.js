export default {
  async created() {
    await this.returnClassUsuario();
    await this.returnClassEmpresa();
    console.log("Usuário verificado:", this.usuario);
  },
  name: 'Home',
  data() {
    return {
      controller: 'Empresa',
      controllerAuth: 'Auth'
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
