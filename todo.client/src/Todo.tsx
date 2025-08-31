import { useState } from 'react';
import './Todo.css';

const Todo = () => {
    const [todos, setTodos] = useState<string[]>([]);
    const handleAddTodo = () => {
        // Logic to add a todo item
        const newTodo = (document.querySelector('.todo-input') as HTMLInputElement).value;
        if (newTodo.trim() !== '') {
            setTodos([...todos, newTodo]);
            (document.querySelector('.todo-input') as HTMLInputElement).value = '';
        }
    }
    return (
        <div className="todo-container">
            <h1 className="todo-title">Todo List</h1>
            <div className="todo-input-group">
                <input
                    type="text"
                    placeholder="Add a todo"
                    className="todo-input"
                />
                <button className="todo-button" onClick={handleAddTodo}>
                    Add
                </button>
            </div>
            <ul className="todo-list">
                {todos.map((todo, index) => (
                    <li key={index} className="todo-item">
                        {todo}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Todo;