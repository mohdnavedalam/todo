import { useState, useEffect } from 'react';
import './Todo.css';
import axios from 'axios';
import api from './api';

interface Todo {
    id: number;
    task: string;
    isCompleted: boolean;
}

const Todo = () => {
    const [todos, setTodos] = useState<Todo[]>([]);
    const [inputValue, setInputValue] = useState<string>('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchTodos();
    }, []); // Remove todos from dependency to avoid infinite loop

    const fetchTodos = async () => {
        try {
            setLoading(true);
            setError(null);
            console.log('Fetching todos...');
            const response = await api.get<Todo[]>('/api/Todos');
            console.log('Todos response:', response);
            setTodos(response.data);
        } catch (err) {
            console.error('Detailed error:', err);
            if (axios.isAxiosError(err) && err.response) {
                setError(`Error: ${err.response.status} - ${err.response.statusText}`);
            } else {
                setError(err instanceof Error ? err.message : 'Failed to fetch todos');
            }
        } finally {
            setLoading(false);
        }
    };

    const handleAddTodo = async (inputValue: string) => {
        if (inputValue.trim() !== '') {
            try {
                const response = await api.post<Todo>('/api/Todos', {
                    task: inputValue.trim(),
                    isCompleted: false
                });
                setTodos([...todos, response.data]);
                setInputValue('');
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Failed to add todo');
                console.error('Error adding todo:', err);
            }
        }
    };

    const handleDeleteTodo = async (id: number) => {
        try {
            await api.delete(`/api/Todos/${id}`);
            // setTodos(todos.filter(todo => todo.id !== id));
            await fetchTodos(); // Refresh the list after deletion
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to delete todo');
            console.error('Error deleting todo:', err);
        }
    };
    return (
        <div className="todo-container">
            <h1 className="todo-title">Todo List</h1>
            <div className="todo-input-group">
                <input
                    type="text"
                    placeholder="Add a todo"
                    className="todo-input"
                    onChange={e => setInputValue(e.target.value)}
                    value={inputValue}
                />
                <button className="todo-button" onClick={() => handleAddTodo(inputValue)}>
                    Add
                </button>
            </div>
            <ul className="todo-list">
                {todos.map((todo, index) => (
                    <li key={index} >
                        <div className="todo-item">
                            <div className='todo-card'>
                                {todo.task}
                            </div>
                            <button
                                className="todo-delete"
                                onClick={() => {handleDeleteTodo(todo.id)}}
                                tabIndex={0}
                            >
                                Delete
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Todo;