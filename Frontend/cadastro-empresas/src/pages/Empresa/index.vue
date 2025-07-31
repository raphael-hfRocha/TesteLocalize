<template>
  <div id="usuario">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h2>Página de Empresas</h2>
      <div>
        <b-button
          variant="danger"
          size="md"
          @click="logout"
          class="me-2"
        >
          Logout
        </b-button>
        <b-button
          variant="primary"
          @click="$router.push({ name: 'cadastroEmpresa' })"
        >
          Nova Empresa
        </b-button>
      </div>
    </div>

    <div class="usuario-info" v-if="usuario && usuario.nome">
      <b-card title="Informações do Usuário" class="mb-4">
        <b-card-text>
          <strong>Nome:</strong> {{ usuario.nome }}<br />
          <strong>Email:</strong> {{ usuario.email }}
        </b-card-text>
      </b-card>
    </div>

    <!-- Loading -->
    <div v-if="carregandoEmpresas" class="text-center mb-3">
      <b-spinner variant="primary"></b-spinner>
      <p class="mt-2">Carregando suas empresas...</p>
    </div>

    <!-- Tabela de empresas -->
    <div class="tabela-empresas" v-else>
      <div v-if="items.length === 0" class="text-center">
        <b-card>
          <b-card-text>
            <h5>Nenhuma empresa cadastrada</h5>
            <p>Você ainda não possui empresas cadastradas no sistema.</p>
            <b-button
              variant="primary"
              @click="$router.push({ name: 'cadastroEmpresa' })"
            >
              Cadastrar Primeira Empresa
            </b-button>
          </b-card-text>
        </b-card>
      </div>

      <div v-else>
        <b-table
          :items="empresas"
          :fields="header"
          striped
          hover
          small
          responsive
          class="mt-3"
        >
          <template #cell(situacao)="data">
            <b-badge :variant="getStatusVariant(data.value)">
              {{ data.value || "Não informado" }}
            </b-badge>
          </template>

          <template #cell(cnpj)="data">
            {{ data.value || "Não informado" }}
          </template>

          <template #cell(nomeFantasia)="data">
            {{ data.value || "Não informado" }}
          </template>

          <template #cell(actions)="data">
            <b-button-group size="sm">
              <b-button variant="outline-primary" @click="editarEmpresa(data.item)">
                <b-icon icon="pencil"></b-icon>
              </b-button>
              <b-button variant="outline-danger" @click="excluirEmpresa(data.item)">
                <b-icon icon="trash"></b-icon>
              </b-button>
            </b-button-group>
          </template>
        </b-table>

        <div class="d-flex justify-content-between align-items-center mt-3">
          <small class="text-muted">
            Total: {{ items.length }} empresa{{ items.length !== 1 ? 's' : '' }}
          </small>
        </div>
      </div>
    </div>
  </div>
</template>

<script src="./index.js" lang="js"></script>

<style scoped>
#usuario {
  padding: 20px;
}

.usuario-info {
  margin-bottom: 2rem;
}

.tabela-empresas {
  margin-bottom: 2rem;
}

/* Melhorar aparência da tabela */
.table {
  margin-bottom: 1rem;
}

.table th {
  border-top: none;
  background-color: #f8f9fa;
  font-weight: 600;
}

.table td {
  vertical-align: middle;
}

/* Estilo para badges de status */
.badge-success {
  background-color: #28a745 !important;
}

.badge-warning {
  background-color: #ffc107 !important;
  color: #212529 !important;
}

.badge-danger {
  background-color: #dc3545 !important;
}

.badge-secondary {
  background-color: #6c757d !important;
}

/* Botões de ação */
.btn-sm {
  padding: 0.25rem 0.5rem;
  font-size: 0.875rem;
}

/* Cartão de estado vazio */
.card {
  border: 1px solid #dee2e6;
  border-radius: 0.375rem;
}

.card-body {
  padding: 1.5rem;
}

/* Spinner de loading */
.spinner-border {
  width: 2rem;
  height: 2rem;
}

/* Responsividade para tabela */
@media (max-width: 768px) {
  .table-responsive {
    font-size: 0.875rem;
  }

  .btn-sm {
    padding: 0.125rem 0.25rem;
    font-size: 0.75rem;
  }
}

/* Estilos específicos para diferentes estados */
.table-hover tbody tr:hover {
  background-color: rgba(0, 0, 0, 0.075);
}

.text-center h5 {
  color: #6c757d;
  margin-bottom: 1rem;
}

.text-center p {
  color: #6c757d;
  margin-bottom: 1.5rem;
}
</style>
