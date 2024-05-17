const axios = require('axios');
const APIUrl = 'https://localhost:7019/api/v1/';
export default { 
    getHeaders() {
        let token = window.localStorage.getItem('token');
        if (token == null) {
            return {}
        }
        return { Authorization: 'Bearer ' + token }
    },
    get(url) {
        return axios.get(APIUrl + url, { headers: this.getHeaders() })
    },
    post(url, data) {
        return axios.post(APIUrl + url, data, { headers: this.getHeaders() })
    }, 
    put(url, data){
        return axios.put(APIUrl + url, data, { headers: this.getHeaders() })
    }, 
    delete(url){
        return axios.delete(APIUrl + url, { headers: this.getHeaders()})
    }
}