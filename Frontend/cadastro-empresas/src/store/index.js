import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        empresa: {},
        empresas: [],
        usuario: {}
    },
    getters: {
        getEmpresa: state => state.empresa,
        getEmpresas: state => state.empresas,
        getUsuario: state => state.usuario
    },
    mutations: {
        setEmpresa(state, empresa) {
            state.empresa = empresa;
        },
        setEmpresas(state, empresas) {
            state.empresas = empresas;
        },
        setUsuario(state, usuario) {
            state.usuario = usuario;
        }
    },
})