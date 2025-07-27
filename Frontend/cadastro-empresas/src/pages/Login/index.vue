<template>
  <div id="login" v-if="$route.path === '/login'">
    <h2>🔐 Página de Login</h2>
    <p>Você está na página de login!</p>
    <div
      style="
        border: 2px solid #007bff;
        padding: 20px;
        margin: 20px;
        border-radius: 8px;
      "
    >
      <p>
        <strong>Debug:</strong> Se você está vendo isso, o roteamento está
        funcionando!
      </p>
    </div>
    <p><strong>Rota atual:</strong> {{ $route.path }}</p>
  </div>
</template>

<script src="./index.js" lang="js"></script>

<style scoped>
</style>