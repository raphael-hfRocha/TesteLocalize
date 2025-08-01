import Vue from 'vue';
import Router from 'vue-router';
import Empresa from '@/pages/Empresa/index.vue';
import Login from '@/pages/Login/index.vue';
import CadastroEmpresa from '@/pages/CadastroEmpresas/index.vue';
import CadastroUsuario from '@/pages/CadastroUsuarios/index.vue';
import Home from '@/pages/Home/index.vue';

Vue.use(Router);

const router = new Router({
    mode: 'hash',
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/empresa',
            name: 'empresa',
            component: Empresa,
            beforeEnter: (to, from, next) => {
                const isAuthenticated = localStorage.getItem('authToken');
                if (!isAuthenticated) {
                    next({ name: 'login' });
                } else {
                    next();
                }
            }
        },
        {
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            path: '/cadastro-empresa',
            name: 'cadastroEmpresa',
            component: CadastroEmpresa,
            beforeEnter: (to, from, next) => {
                const isAuthenticated = localStorage.getItem('authToken');
                if (!isAuthenticated) {
                    next({ name: 'login' });
                } else {
                    next();
                }
            }
        },
        {
            path: '/cadastro-usuario',
            name: 'cadastroUsuario',
            component: CadastroUsuario
        }
    ]
})

export default router;
