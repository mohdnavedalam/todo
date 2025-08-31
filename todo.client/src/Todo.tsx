import './Todo.css';

const Todo = () => {
  return (
    <div className="todo-container">
        <h1 className="todo-title">Todo List</h1>
        <div className="todo-input-group">
            <input 
                type="text" 
                placeholder="Add a todo" 
                className="todo-input"
            />
            <button className="todo-button">
                Add
            </button>
        </div>
    </div>
  );
}

export default Todo;