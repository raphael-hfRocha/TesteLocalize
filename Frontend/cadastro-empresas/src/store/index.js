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
        empresas: [],
        usuario: {
            idUsuario: null,
            nome: '',
            email: '',
            senha: '',
            token: null,
            isLoggedIn: false
        }
    },
    getters: {
        getEmpresa: state => state.empresa,
        getEmpresas: state => state.empresas,
        getUsuario: state => state.usuario,
        isAuthenticated: state => state.usuario.isLoggedIn && state.usuario.token
    },
    mutations: {
        setEmpresa(state, empresa) {
            state.empresa = empresa;
        },
        setEmpresas(state, empresas) {
            state.empresas = empresas;
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
        }
    },
})