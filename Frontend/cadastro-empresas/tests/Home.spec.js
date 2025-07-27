import { shallowMount, createLocalVue } from '@vue/test-utils'
import Home from '@/pages/Home/index.vue'
import BootstrapVue from 'bootstrap-vue'

const localVue = createLocalVue()
localVue.use(BootstrapVue)

describe('Home.vue', () => {
  let wrapper

  beforeEach(() => {
    wrapper = shallowMount(Home, {
      localVue,
      mocks: {
        $route: {
          path: '/'
        },
        $router: {
          push: jest.fn()
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

  it('exibe o título da página home', () => {
    expect(wrapper.find('h2').text()).toBe('🏠 Página Home')
  })

  it('exibe mensagem de boas-vindas', () => {
    expect(wrapper.text()).toContain('Bem-vindo à página inicial do sistema!')
  })

  it('exibe informação de debug', () => {
    expect(wrapper.text()).toContain('Esta é a página inicial (rota: /)')
  })

  it('tem o componente correto definido', () => {
    expect(wrapper.vm.$options.name).toBe('HomePage')
  })
})
