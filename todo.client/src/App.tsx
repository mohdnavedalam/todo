import { useState, useEffect } from 'react';
import './App.css';
import './AuthModal.css';
import Todo from './Todo';
import AuthModal from './AuthModal';
import api from './api';

function App() {
    const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
    const [userName, setUserName] = useState<string | null>(localStorage.getItem('userName'));
    const [validating, setValidating] = useState(true);

    // Validate session on app load
    useEffect(() => {
        const validateSession = async () => {
            const storedToken = localStorage.getItem('token');
            if (!storedToken) {
                setValidating(false);
                return;
            }

            try {
                const response = await api.get('/api/Auth/me');
                setUserName(response.data.name);
                setToken(storedToken);
            } catch {
                // Token is invalid or expired — clear it
                localStorage.removeItem('token');
                localStorage.removeItem('userName');
                setToken(null);
                setUserName(null);
            } finally {
                setValidating(false);
            }
        };

        validateSession();
    }, []);

    useEffect(() => {
        if (token) {
            localStorage.setItem('token', token);
        } else {
            localStorage.removeItem('token');
        }
    }, [token]);

    useEffect(() => {
        if (userName) {
            localStorage.setItem('userName', userName);
        } else {
            localStorage.removeItem('userName');
        }
    }, [userName]);

    const handleAuthenticated = (newToken: string, name: string) => {
        setToken(newToken);
        setUserName(name);
    };

    const handleLogout = () => {
        setToken(null);
        setUserName(null);
    };

    if (validating) {
        return (
            <div className="auth-overlay">
                <div className="auth-modal" style={{ textAlign: 'center' }}>
                    <h2 style={{ color: '#f0f0f0', margin: 0 }}>Loading...</h2>
                </div>
            </div>
        );
    }

    if (!token) {
        return <AuthModal onAuthenticated={handleAuthenticated} />;
    }

    return (
        <>
            <div className="auth-user-bar">
                <div className="auth-user-info">
                    Hello, <span>{userName}</span>
                </div>
                <button className="auth-logout" onClick={handleLogout}>
                    Sign Out
                </button>
            </div>
            <Todo />
        </>
    );
}

export default App;