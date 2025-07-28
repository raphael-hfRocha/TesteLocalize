const axios = require('axios');

async function testarAPI() {
    console.log('🧪 Testando conectividade com a API...');
    
    try {
        // Teste 1: GET na rota base
        console.log('\n1️⃣ Testando GET /api...');
        const response1 = await axios.get('https://localhost:7220/api');
        console.log('✅ Status:', response1.status);
        console.log('📄 Data:', response1.data);
    } catch (error1) {
        console.log('❌ Erro na rota /api:', error1.message);
    }
    
    try {
        // Teste 2: GET na rota de usuário
        console.log('\n2️⃣ Testando GET /api/Usuario...');
        const response2 = await axios.get('https://localhost:7220/api/Usuario');
        console.log('✅ Status:', response2.status);
        console.log('📄 Data:', response2.data);
    } catch (error2) {
        console.log('❌ Erro na rota /api/Usuario:', error2.message);
        console.log('📊 Status:', error2.response?.status);
        console.log('📄 Data:', error2.response?.data);
    }
    
    try {
        // Teste 3: GET na rota de teste do banco
        console.log('\n3️⃣ Testando GET /api/TesteDatabase/test-connection...');
        const response3 = await axios.get('https://localhost:7220/api/TesteDatabase/test-connection');
        console.log('✅ Status:', response3.status);
        console.log('📄 Data:', response3.data);
    } catch (error3) {
        console.log('❌ Erro no GET /api/TesteDatabase/test-connection:', error3.message);
        console.log('📊 Status:', error3.response?.status);
        console.log('📄 Data:', error3.response?.data);
    }
    
    try {
        // Teste 4: POST na rota de usuário
        console.log('\n4️⃣ Testando POST /api/Usuario...');
        const userData = {
            nome: 'Teste',
            email: 'teste@teste.com',
            senha: '123456'
        };
        
        const response4 = await axios.post('https://localhost:7220/api/Usuario', userData, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        console.log('✅ Status:', response4.status);
        console.log('📄 Data:', response4.data);
    } catch (error4) {
        console.log('❌ Erro no POST /api/Usuario:', error4.message);
        console.log('📊 Status:', error4.response?.status);
        console.log('📄 Data:', error4.response?.data);
    }
}

testarAPI();
