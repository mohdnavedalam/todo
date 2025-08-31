import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import Todo from './todo';

function App() {
    const [response, setResponse] = useState<any>(null);

    return (
        <>
            <Todo />
        </>
    );
}

export default App;