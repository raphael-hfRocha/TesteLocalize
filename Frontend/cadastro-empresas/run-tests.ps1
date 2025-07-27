# Script PowerShell para executar testes
Write-Host "🧪 Executando Testes Automatizados do Projeto Vue.js" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan

Write-Host ""
Write-Host "📦 Instalando dependências..." -ForegroundColor Yellow
npm install

Write-Host ""
Write-Host "🧪 Executando todos os testes..." -ForegroundColor Yellow
npm test

Write-Host ""
Write-Host "📊 Gerando relatório de cobertura..." -ForegroundColor Yellow
npm run test:coverage

Write-Host ""
Write-Host "✅ Testes concluídos!" -ForegroundColor Green
Write-Host ""
Write-Host "Para executar testes em modo watch:" -ForegroundColor Blue
Write-Host "npm run test:watch" -ForegroundColor Blue
Write-Host ""
Write-Host "Para executar um teste específico:" -ForegroundColor Blue
Write-Host "npm test -- tests/Login.spec.js" -ForegroundColor Blue
