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
        },
        {
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            path: '/cadastro-empresa',
            name: 'cadastroEmpresa',
            component: CadastroEmpresa
        },
        {
            path: '/cadastro-usuario',
            name: 'cadastroUsuario',
            component: CadastroUsuario
        }
    ]
})

// Redirecionar para a página de login se o usuário não estiver autenticado
router.beforeEach((to, from, next) => {
    // Verificar se a rota requer autenticação
    if (to.matched.some(record => record.meta.requiresAuth)) {
        // Verificar se existe token de autenticação
        const token = localStorage.getItem('authToken');
        
        if (!token) {
            // Não está autenticado, redirecionar para login
            console.log('🔒 Acesso negado. Redirecionando para login...');
            next({ name: 'login' });
        } else {
            // Token existe, permitir acesso
            console.log('✅ Usuário autenticado. Permitindo acesso...');
            next();
        }
    } else {
        // Rota não requer autenticação, permitir acesso
        next();
    }
});

export default router;