<template>
  <div id="app" v-if="$route.path === '/'">
      <h1>Cadastro de Empresas</h1>
      <div class="opcao-usuario">
        <router-link 
          :to="{
            name: 'login',
            params: $route.params.id,
          }">
          <b-button size="lg" variant="primary" style="margin: 5px; width: 500px">Login</b-button>
        </router-link>
        <router-link to="/cadastro-usuario">
          <b-button size="lg" variant="danger" style="margin: 5px; width: 500px">Cadastre-se</b-button>
        </router-link>
      </div>
      <p><strong>Rota atual:</strong> {{ $route.path }}</p>
      <router-view name="login"></router-view>

    <!-- Conteúdo das páginas -->
  </div>
</template>

<script>
export default {
  props: ['id'],
  name: "App",
  components: {},
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.opcao-usuario {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
  margin-top: 20px;
}
</style>
