<template>
  <div id="login" v-if="$route.path === '/login'">
    <b-modal v-show="error === true" id="modal-1" title="Erro">
      <strong class="my-4">Erro ao realizar login</strong>
    </b-modal>
    <h1>Login</h1>
    <div class="form">
      <b-form @submit.prevent="fazerLogin">
        <b-form-group id="input-group-1">
          <label for="email">E-mail:</label>
          <b-form-input
            id="input-1"
            v-model="usuario.email"
            type="email"
            placeholder="Digite seu e-mail"
            required
          ></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2">
          <label for="senha">Senha:</label>
          <b-form-input
            id="input-2"
            v-model="usuario.senha"
            type="password"
            placeholder="Digite sua senha"
            required
          ></b-form-input>
        </b-form-group>
        <b-button
          v-b-modal.modal-1
          style="width: 180px; margin-right: 10px"
          type="submit"
          variant="primary"
          >Login</b-button
        >
        <b-button
          style="width: 180px; margin-left: 10px"
          type="button"
          variant="danger"
          @click="limparCampos"
          >Limpar campos</b-button
        >
      </b-form>
    </div>
  </div>
</template>

<script src="./index.js" lang="js"></script>

<style scoped>
#login {
  margin: 20px;
}
.form {
  max-width: 500px;
  margin: auto;
}
.form label {
  text-align: left;
  display: block;
  width: 100%;
}
.form b-button {
  width: 500px;
}
</style>