import { shallowMount, createLocalVue } from '@vue/test-utils'
import Login from '@/pages/Login/index.vue'
import BootstrapVue from 'bootstrap-vue'

const localVue = createLocalVue()
localVue.use(BootstrapVue)

describe('Login.vue', () => {
  let wrapper

  beforeEach(() => {
    wrapper = shallowMount(Login, {
      localVue,
      mocks: {
        $route: {
          path: '/login'
        },
        $router: {
          push: jest.fn()
        },
        $store: {
          state: {
            usuario: {
              email: '',
              password: ''
            }
          },
          commit: jest.fn()
        }
      }
    })
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('renderiza corretamente', () => {
    expect(wrapper.exists()).toBe(true)
  })

  it('exibe o título da página', () => {
    expect(wrapper.find('h2').text()).toBe('🔐 Página de Login')
  })

  it('exibe a mensagem de debug', () => {
    expect(wrapper.text()).toContain('Se você está vendo isso, o roteamento está funcionando!')
  })

  it('exibe a rota atual', () => {
    expect(wrapper.text()).toContain('Rota atual: /login')
  })

  it('tem o componente correto definido', () => {
    expect(wrapper.vm.$options.name).toBe('LoginPage')
  })
})
