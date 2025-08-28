import { use, useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';

function App() {
    const [response, setResponse] = useState<any>(null);

    
    const fetchData = async () => {
        try {
            const result = await axios.get('https://localhost:7015/api/Test');
            setResponse(result.data);
            console.log(result.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <h1 className="text-3xl font-bold underline">
            {response}
        </h1>
    );
}

export default App;