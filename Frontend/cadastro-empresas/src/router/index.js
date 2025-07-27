import Vue from 'vue';
import Router from 'vue-router';
import Usuario from '@/pages/Usuario/index.vue';
import Login from '@/pages/Login/index.vue';
import CadastroEmpresa from '@/pages/CadastroEmpresas/index.vue';
import CadastroUsuario from '@/pages/CadastroUsuarios/index.vue';
import App from '@/App.vue';

Vue.use(Router);

const router = new Router({
    mode: 'hash',
    routes: [
        {
            path: '/',
            name: 'home',
            components: {
                default: App,
                login: Login,
                cadastroUsuario: CadastroUsuario,
            }
        },
        {
            path: '/usuario',
            name: 'usuario',
            component: Usuario
        },
        {
            path: '/login',
            name: 'login',
            components: {
                default: Login,
            }
        },
        {
            path: '/cadastro-empresa',
            name: 'cadastro-empresa',
            component: CadastroEmpresa
        },
        {
            path: '/cadastro-usuario',
            name: 'cadastroUsuario',
            components: {
                default: CadastroUsuario,
            }
        }
    ]
})

export default router;