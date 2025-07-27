import { shallowMount, createLocalVue } from '@vue/test-utils'
import VueRouter from 'vue-router'
import App from '@/App.vue'
import BootstrapVue from 'bootstrap-vue'

const localVue = createLocalVue()
localVue.use(VueRouter)
localVue.use(BootstrapVue)

describe('App.vue', () => {
  let wrapper
  let router

  beforeEach(() => {
    router = new VueRouter({
      routes: [
        { path: '/', name: 'home' },
        { path: '/login', name: 'login' },
        { path: '/cadastro-usuario', name: 'cadastro-usuario' }
      ]
    })

    wrapper = shallowMount(App, {
      localVue,
      router
    })
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('renderiza corretamente', () => {
    expect(wrapper.exists()).toBe(true)
  })

  it('exibe o título principal', () => {
    expect(wrapper.find('h1').text()).toBe('Cadastro de Empresas')
  })

  it('tem link para login', () => {
    const loginLink = wrapper.find('router-link-stub[to="/login"]')
    expect(loginLink.exists()).toBe(true)
  })

  it('tem link para cadastro de usuário', () => {
    const cadastroLink = wrapper.find('router-link-stub[to="/cadastro-usuario"]')
    expect(cadastroLink.exists()).toBe(true)
  })

  it('tem link para home', () => {
    const homeLink = wrapper.find('router-link-stub[to="/"]')
    expect(homeLink.exists()).toBe(true)
  })

  it('tem router-view para exibir as páginas', () => {
    expect(wrapper.find('router-view-stub').exists()).toBe(true)
  })
})
