import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7015',
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    }
});

// Add request interceptor to handle errors
api.interceptors.request.use(
    config => {
        return config;
    },
    error => {
        console.error('Request error:', error);
        return Promise.reject(error);
    }
);

// Add response interceptor to handle errors
api.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        if (error.response) {
            console.error('Response error:', error.response);
        } else if (error.request) {
            console.error('Request error:', error.request);
        } else {
            console.error('Error:', error.message);
        }
        return Promise.reject(error);
    }
);

export default api;
