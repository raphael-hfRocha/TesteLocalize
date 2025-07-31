import Vue from 'vue';
import Axios from 'axios';

Vue.use({
    install(Vue) {
        Vue.prototype.$http = Axios.create({
            baseURL: 'https://localhost:7220',
            // timeout: 30000,
            validateStatus: function (status) {
                return status < 500;
            }
        })

        Vue.prototype.$http.interceptors.request.use(config => {
            const token = localStorage.getItem('authToken');
            if (token) {
                config.headers.Authorization = `Bearer ${token}`;
            }
            
            console.log(`${config.method?.toUpperCase()} ${config.url}`, {
                data: config.data,
                params: config.params,
                headers: config.headers
            });
            return config
        }, error => {
            console.error('Request Error:', error)
            return Promise.reject(error)
        })

        Vue.prototype.$http.interceptors.response.use(res => {
            console.log(`Response ${res.status}:`, res.data)
            return res
        }, error => {
            console.error('Response Error:', error)
            return Promise.reject(error)
        })
    }
})