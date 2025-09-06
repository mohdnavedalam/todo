import { useState } from 'react';
import './Todo.css';

const Todo = () => {
    const [todos, setTodos] = useState<string[]>([]);
    const [inputValue, setInputValue] = useState<string>('');
    const handleAddTodo = () => {
        // Logic to add a todo item
        const newTodo = inputValue
        if (newTodo.trim() !== '') {
            setTodos([...todos, newTodo]);
            setInputValue('');
        }
    }
    const handleDeleteTodo = (index: number) => {
        // Logic to delete a todo item
        const newTodos = todos.filter((_, i) => i !== index);
        setTodos(newTodos);
    }
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
                <button className="todo-button" onClick={handleAddTodo}>
                    Add
                </button>
            </div>
            <ul className="todo-list">
                {todos.map((todo, index) => (
                    <li key={index} >
                        <div className="todo-item">
                            <div className='todo-card'>
                                {todo}
                            </div>
                            <button
                                className="todo-delete"
                                onClick={() => {handleDeleteTodo(index)}}
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