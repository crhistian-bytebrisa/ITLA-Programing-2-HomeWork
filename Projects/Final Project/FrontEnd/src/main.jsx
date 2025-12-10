import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import 'bootstrap/dist/css/bootstrap.min.css'  

// Se busca la ruta para ver donde esta el div con el id root y renderiza APP
ReactDOM.createRoot(document.getElementById('root')).render(
    <App />  
)