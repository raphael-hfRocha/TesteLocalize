<template>
  <div id="cadastro-empresas">
    Id do usuario: {{ usuario.idUsuario }}
    <h1>Cadastro de Empresas</h1>
    <div class="form">
      <b-form @submit.prevent="cadastrarEmpresa">
        <b-form-group id="input-group-1">
          <label for="cnpj">CNPJ:</label>
          <b-form-input
            id="input-1"
            v-model="empresa.cnpj"
            type="text"
            placeholder="Digite seu CNPJ"
            :maxlength="18"
            required
          ></b-form-input>
        </b-form-group>
        <b-button
          style="width: 180px; margin-right: 10px;"
          type="submit"
          variant="primary"
          >Enviar</b-button
        >
        <b-button
          style="width: 180px; margin-left: 10px"
          type="reset"
          variant="danger"
          @click="limparDadosEmpresa"
          >Limpar campo</b-button
        >
      </b-form>
    </div>
  </div>
</template>

<script src="./index.js" lang="js"></script>

<style scoped>
.form {
  max-width: 500px;
  margin: auto;
}
.form label {
  text-align: left;
  display: block;
  width: 100%;
}
</style>