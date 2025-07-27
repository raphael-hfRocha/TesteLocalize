#!/bin/bash

echo "🧪 Executando Testes Automatizados do Projeto Vue.js"
echo "=================================================="

echo ""
echo "📦 Instalando dependências..."
npm install

echo ""
echo "🧪 Executando todos os testes..."
npm test

echo ""
echo "📊 Gerando relatório de cobertura..."
npm run test:coverage

echo ""
echo "✅ Testes concluídos!"
echo ""
echo "Para executar testes em modo watch:"
echo "npm run test:watch"
echo ""
echo "Para executar um teste específico:"
echo "npm test -- tests/Login.spec.js"
