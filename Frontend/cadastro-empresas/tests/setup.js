import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'
import { createLocalVue } from '@vue/test-utils'

// Configurar Bootstrap Vue para testes
Vue.use(BootstrapVue)

// Configuração global para os testes
const localVue = createLocalVue()
localVue.use(BootstrapVue)

// Mock para o $route e $router se necessário
global.mockRouter = {
  push: jest.fn(),
  replace: jest.fn(),
  go: jest.fn(),
  back: jest.fn(),
  forward: jest.fn()
}

global.mockRoute = {
  path: '/',
  params: {},
  query: {}
}
