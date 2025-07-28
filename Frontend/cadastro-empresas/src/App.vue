<template>
  <div id="app">
    <!-- Conteúdo das páginas -->
    <router-view></router-view>
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
</style>
