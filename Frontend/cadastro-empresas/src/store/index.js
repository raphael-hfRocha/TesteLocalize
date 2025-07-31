import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        empresa: {
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
        },
        usuario: {
            idUsuario: null,
            nome: '',
            email: '',
            senha: '',
            token: null,
            isLoggedIn: false
        },
        empresas: []
    },
    getters: {
        getEmpresa: state => state.empresa,
        getUsuario: state => state.usuario,
        isAuthenticated: state => state.usuario.isLoggedIn && state.usuario.token,
        getEmpresas: state => state.empresas
    },
    mutations: {
        setEmpresa(state, empresa) {
            state.empresa = empresa;
        },
        setUsuario(state, usuario) {
            state.usuario = { ...state.usuario, ...usuario };
            if (usuario.token) {
                state.usuario.isLoggedIn = true;
            }
        },
        logout(state) {
            state.usuario = {
                idUsuario: null,
                nome: '',
                email: '',
                senha: '',
                token: null,
                isLoggedIn: false
            };
            localStorage.removeItem('authToken');
        },
        setEmpresas(state, empresas) {
            state.empresas = empresas;
        }
    },
})