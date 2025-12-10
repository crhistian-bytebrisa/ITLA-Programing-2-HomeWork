import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import FloatingCard from '../components/FloatingCard'

function Login() {
  // ⭐ Estados para guardar lo que el usuario escribe
  const [nombre, setNombre] = useState('')
  const [contra, setContra] = useState('')
  const navigate = useNavigate()

  // ⭐ Función cuando hace submit
  const handleSubmit = (e) => {
    e.preventDefault() // Evita recargar la página
    
    console.log('Nombre:', nombre)
    console.log('Contraseña:', contra)
    
    // Aquí harías la petición a tu API
    // Por ahora solo mostramos alerta
    alert(`Bienvenido ${nombre}!`)
    
    // Redirigir a dashboard después del login
    navigate('/paciente/dashboard')
  }

  const [isCardOpen, setIsCardOpen] = useState(false)
  return (
    
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow">
            <div className="card-body">
              <h3 className="text-center mb-4">Iniciar Sesión</h3>
              
              <form onSubmit={handleSubmit}>
                
                {/* Campo Nombre */}
                <div className="mb-3">
                  <label htmlFor="name" className="form-label">
                    Nombre de usuario:
                  </label>
                  <input 
                    type="text" 
                    id="name"
                    className="form-control"
                    value={nombre}
                    onChange={(e) => setNombre(e.target.value)}
                    required
                    maxLength={5}
                  />
                </div>

                {/* Campo Contraseña */}
                <div className="mb-3">
                  <label htmlFor="contra" className="form-label">
                    Contraseña:
                  </label>
                  <input 
                    type="password"
                    id="contra"
                    className="form-control"
                    value={contra}
                    onChange={(e) => setContra(e.target.value)}
                    required
                    
                  />
                </div>

                {/* Botón Submit */}
                <button type="submit" className="btn btn-primary w-100">
                  Iniciar sesion
                </button>

                {/* Link a registro */}
                <p className="text-center mt-3">
                  ¿No tienes cuenta?{' '}
                  <a href="/register">Regístrate aquí</a>
                </p>
                <button 
                className="btn btn-primary btn-lg mt-4"
                onClick={() => setIsCardOpen(true)}
                >

                </button>
              </form>
            <FloatingCard 
              isOpen={isCardOpen}
              onClose={() => setIsCardOpen(false)}
            />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Login