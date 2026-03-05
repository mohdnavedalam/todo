import { useState, type FormEvent } from 'react';
import './AuthModal.css';
import api from './api';

interface AuthModalProps {
    onAuthenticated: (token: string, name: string) => void;
}

const AuthModal = ({ onAuthenticated }: AuthModalProps) => {
    const [mode, setMode] = useState<'signin' | 'signup'>('signin');
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            if (mode === 'signup') {
                const response = await api.post('/api/Auth/signup', {
                    name,
                    email,
                    password
                });
                onAuthenticated(response.data.token, response.data.name);
            } else {
                const response = await api.post('/api/Auth/signin', {
                    email,
                    password
                });
                onAuthenticated(response.data.token, response.data.name);
            }
        } catch (err: unknown) {
            if (err && typeof err === 'object' && 'response' in err) {
                const axiosErr = err as { response?: { data?: string; status?: number } };
                if (axiosErr.response?.data) {
                    setError(axiosErr.response.data);
                } else if (axiosErr.response?.status === 401) {
                    setError('Invalid email or password.');
                } else {
                    setError('Something went wrong. Please try again.');
                }
            } else {
                setError('Unable to connect to the server.');
            }
        } finally {
            setLoading(false);
        }
    };

    const switchMode = (newMode: 'signin' | 'signup') => {
        setMode(newMode);
        setError(null);
    };

    return (
        <div className="auth-overlay">
            <div className="auth-modal">
                <div className="auth-header">
                    <h2>Todo App</h2>
                    <p>{mode === 'signin' ? 'Welcome back!' : 'Create your account'}</p>
                </div>

                <div className="auth-tabs">
                    <button
                        className={`auth-tab ${mode === 'signin' ? 'active' : ''}`}
                        onClick={() => switchMode('signin')}
                    >
                        Sign In
                    </button>
                    <button
                        className={`auth-tab ${mode === 'signup' ? 'active' : ''}`}
                        onClick={() => switchMode('signup')}
                    >
                        Sign Up
                    </button>
                </div>

                {error && <div className="auth-error">{error}</div>}

                <form className="auth-form" onSubmit={handleSubmit}>
                    {mode === 'signup' && (
                        <div className="auth-field">
                            <label htmlFor="name">Name</label>
                            <input
                                id="name"
                                type="text"
                                placeholder="Your name"
                                value={name}
                                onChange={e => setName(e.target.value)}
                                required
                            />
                        </div>
                    )}

                    <div className="auth-field">
                        <label htmlFor="email">Email</label>
                        <input
                            id="email"
                            type="email"
                            placeholder="you@example.com"
                            value={email}
                            onChange={e => setEmail(e.target.value)}
                            required
                        />
                    </div>

                    <div className="auth-field">
                        <label htmlFor="password">Password</label>
                        <input
                            id="password"
                            type="password"
                            placeholder="Enter your password"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                            required
                            minLength={6}
                        />
                    </div>

                    <button type="submit" className="auth-submit" disabled={loading}>
                        {loading
                            ? 'Please wait...'
                            : mode === 'signin'
                                ? 'Sign In'
                                : 'Sign Up'}
                    </button>
                </form>
            </div>
        </div>
    );
};

export default AuthModal;
