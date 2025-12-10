import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Login from './LoginPage'

function Register() {
  const [nombre, setNombre] = useState('')
  const [contra, setContra] = useState('')
  const [descripcion, setDescripcion] = useState('')  // ⭐ Nueva
  const [tiempo, setTiempo] = useState('')
  const navigate = useNavigate()

  const MAX_NOMBRE = 100
  const MAX_CONTRA = 50
  const MAX_DESCRIPCION = 500  // ⭐ Límite para descripción

  // Función para color del contador
  const getCounterColor = (length, max) => {
    const percentage = (length / max) * 100
    
    if (percentage < 50) return 'text-muted'
    if (percentage < 80) return 'text-warning'
    return 'text-danger'
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    
    console.log('Nombre:', nombre)
    console.log('Contraseña:', contra)
    console.log('Descripción:', descripcion)
    console.log('Tiempo:', tiempo)
    
    alert('Usuario registrado!')
    navigate('/login')
  }

  return (
    <>
        <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card shadow">
            <div className="card-body">
              <h3 className="text-center mb-4">Registro</h3>
              
              <form onSubmit={handleSubmit}>
                
                {/* Campo Nombre */}
                <div className="mb-3">
                  <label htmlFor="name" className="form-label">
                    Nombre completo:
                  </label>
                  <input 
                    type="text" 
                    id="name"
                    className="form-control"
                    value={nombre}
                    onChange={(e) => setNombre(e.target.value)}
                    maxLength={MAX_NOMBRE}
                  />
                  <small className={`form-text ${getCounterColor(nombre.length, MAX_NOMBRE)}`}>
                    {nombre.length}/{MAX_NOMBRE} caracteres
                  </small>
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
                    maxLength={MAX_CONTRA}
                  />
                  <small className={`form-text ${getCounterColor(contra.length, MAX_CONTRA)}`}>
                    {contra.length}/{MAX_CONTRA} caracteres
                  </small>
                </div>

                {/* ⭐ Campo Descripción - TEXTAREA */}
                <div className="mb-3">
                  <label htmlFor="descripcion" className="form-label">
                    Descripción:
                  </label>
                  <textarea 
                    id="descripcion"
                    className="form-control"
                    value={descripcion}
                    onChange={(e) => setDescripcion(e.target.value)}
                    maxLength={MAX_DESCRIPCION}
                    rows={4}  // ⭐ Altura del textarea (4 líneas)
                    placeholder="Cuéntanos sobre ti..."
                  />
                  <small className={`form-text ${getCounterColor(descripcion.length, MAX_DESCRIPCION)}`}>
                    {descripcion.length}/{MAX_DESCRIPCION} caracteres
                  </small>
                </div>

                {/* Campo Tiempo */}
                <div className="mb-3">
                  <label htmlFor="time" className="form-label">
                    Hora de preferencia:
                  </label>
                  <input 
                    type="time"
                    id="time"
                    className="form-control"
                    value={tiempo}
                    onChange={(e) => setTiempo(e.target.value)}
                  />
                </div>

                <button type="submit" className="btn btn-success w-100">
                  Registrate
                </button>

                <p className="text-center mt-3">
                  ¿Ya tienes cuenta?{' '}
                  <a href="/login">Inicia sesión aquí</a>
                </p>
              </form>

            </div>
          </div>
        </div>
      </div>
    </div>
    <Login/>
    </>

  )
}

export default Register