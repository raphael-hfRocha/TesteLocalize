import { mount, createLocalVue } from '@vue/test-utils'
import VueRouter from 'vue-router'
import App from '@/App.vue'
import Home from '@/pages/Home/index.vue'
import Login from '@/pages/Login/index.vue'
import BootstrapVue from 'bootstrap-vue'

const localVue = createLocalVue()
localVue.use(VueRouter)
localVue.use(BootstrapVue)

describe('Router Integration Tests', () => {
  let wrapper
  let router

  beforeEach(() => {
    router = new VueRouter({
      routes: [
        { path: '/', name: 'home', component: Home },
        { path: '/login', name: 'login', component: Login }
      ]
    })

    wrapper = mount(App, {
      localVue,
      router,
      mocks: {
        $store: {
          state: {
            usuario: {}
          },
          commit: jest.fn()
        }
      }
    })
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('navega para a página home por padrão', async () => {
    expect(wrapper.vm.$route.path).toBe('/')
  })

  it('navega para a página de login ao clicar no botão', async () => {
    const loginButton = wrapper.find('a[href="#/login"]')
    await loginButton.trigger('click')
    
    await wrapper.vm.$nextTick()
    
    expect(wrapper.vm.$route.path).toBe('/login')
  })

  it('exibe o conteúdo correto na página home', () => {
    expect(wrapper.html()).toContain('🏠 Página Home')
  })

  it('atualiza a exibição da rota atual quando navega', async () => {
    expect(wrapper.text()).toContain('Rota atual: /')
    
    await wrapper.vm.$router.push('/login')
    await wrapper.vm.$nextTick()
    
    expect(wrapper.text()).toContain('Rota atual: /login')
  })
})
