<template>
  <div class="home">
    <h1>Cadastro de Empresas</h1>
    <div class="opcao-usuario">
      <router-link
        :to="{
          name: 'login',
          params: $route.params.id,
        }"
      >
        <b-button size="lg" variant="primary" style="margin: 5px; width: 500px"
          >Login</b-button
        >
      </router-link>
      <router-link to="/cadastro-usuario">
        <b-button size="lg" variant="danger" style="margin: 5px; width: 500px"
          >Cadastre-se</b-button
        >
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
};
</script>

<style scoped>
.opcao-usuario {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
  margin-top: 20px;
}
</style>
