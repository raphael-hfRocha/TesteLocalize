import { shallowMount, createLocalVue } from '@vue/test-utils'
import Vuex from 'vuex'
import Usuario from '@/pages/Usuario/index.vue'
import BootstrapVue from 'bootstrap-vue'

const localVue = createLocalVue()
localVue.use(Vuex)
localVue.use(BootstrapVue)

describe('Usuario.vue', () => {
  let wrapper
  let store
  let actions
  let state

  beforeEach(() => {
    state = {
      usuario: {
        id: 1,
        nome: 'Teste User',
        email: 'teste@email.com'
      }
    }

    actions = {
      setUsuario: jest.fn()
    }

    store = new Vuex.Store({
      state,
      actions
    })

    wrapper = shallowMount(Usuario, {
      localVue,
      store,
      mocks: {
        $route: {
          path: '/usuario'
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

  it('tem o nome correto do componente', () => {
    expect(wrapper.vm.$options.name).toBe('UsuarioPage')
  })

  it('acessa corretamente os dados do store', () => {
    expect(wrapper.vm.usuario).toEqual(state.usuario)
  })

  it('chama método do lifecycle created', () => {
    const spy = jest.spyOn(wrapper.vm, 'carregaReturnClassUsuario')
    wrapper.vm.$options.created[0].call(wrapper.vm)
    expect(spy).toHaveBeenCalled()
  })
})
