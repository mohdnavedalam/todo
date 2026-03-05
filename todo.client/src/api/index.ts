import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7015',
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    }
});


// Add request interceptor to attach JWT token
api.interceptors.request.use(
    config => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    error => {
        console.error('Request error:', error);
        return Promise.reject(error);
    }
);

// Add response interceptor to handle errors and expired tokens
api.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        if (error.response) {
            // If 401, token expired or invalid — clear auth and reload
            if (error.response.status === 401 && localStorage.getItem('token')) {
                localStorage.removeItem('token');
                localStorage.removeItem('userName');
                window.location.reload();
            }
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
