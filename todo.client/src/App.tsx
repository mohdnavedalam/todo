import { useState, useEffect } from 'react';
import './App.css';
import Todo from './Todo';
import AuthModal from './AuthModal';

function App() {
    const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
    const [userName, setUserName] = useState<string | null>(localStorage.getItem('userName'));

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