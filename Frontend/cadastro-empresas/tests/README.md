# Testes Automatizados

Este projeto inclui testes automatizados usando Jest e Vue Test Utils.

## 📋 Pré-requisitos

Certifique-se de que todas as dependências estão instaladas:

```bash
npm install
```

## 🧪 Executando os Testes

### Executar todos os testes uma vez
```bash
npm test
```

### Executar testes em modo watch (re-executa quando arquivos mudam)
```bash
npm run test:watch
```

### Executar testes com relatório de cobertura
```bash
npm run test:coverage
```

## 📊 Estrutura dos Testes

```
tests/
├── setup.js          # Configuração global dos testes
├── App.spec.js        # Testes do componente principal
├── Home.spec.js       # Testes da página Home
├── Login.spec.js      # Testes da página Login
└── Router.spec.js     # Testes de integração do roteamento
```

## 🎯 Tipos de Testes Incluídos

### 1. **Testes Unitários**
- **App.vue**: Verifica renderização, links de navegação e estrutura
- **Home.vue**: Testa exibição de conteúdo e propriedades do componente
- **Login.vue**: Valida elementos da interface e funcionalidades

### 2. **Testes de Integração**
- **Router.spec.js**: Testa navegação entre páginas e funcionamento do Vue Router

## 📈 Cobertura de Testes

Os testes cobrem:
- ✅ Renderização de componentes
- ✅ Navegação entre páginas
- ✅ Exibição de conteúdo dinâmico
- ✅ Estrutura dos componentes
- ✅ Integração com Vue Router
- ✅ Mocks do Vuex Store

## 🐛 Debugging

Para debuggar testes específicos:

```bash
# Executar apenas um arquivo de teste
npm test -- tests/Login.spec.js

# Executar testes com mais detalhes
npm test -- --verbose

# Executar um teste específico
npm test -- --testNamePattern="renderiza corretamente"
```

## 📝 Adicionando Novos Testes

1. Crie um arquivo `.spec.js` na pasta `tests/`
2. Importe o componente que deseja testar
3. Use `shallowMount` para testes unitários ou `mount` para testes de integração
4. Adicione os mocks necessários (router, store, etc.)

### Exemplo de teste básico:
```javascript
import { shallowMount } from '@vue/test-utils'
import MeuComponente from '@/components/MeuComponente.vue'

describe('MeuComponente.vue', () => {
  it('renderiza corretamente', () => {
    const wrapper = shallowMount(MeuComponente)
    expect(wrapper.exists()).toBe(true)
  })
})
```

## 🔧 Configuração

A configuração dos testes está em:
- `jest.config.js` - Configuração principal do Jest
- `tests/setup.js` - Configuração global e mocks
- `package.json` - Scripts e dependências
