// src/pages/TestPage.jsx
import { useState } from 'react'
import FloatingCard from '../components/FloatingCard'

function TestPage() {
  // ⭐ Estado para controlar si la carta está abierta o cerrada
  const [isCardOpen, setIsCardOpen] = useState(false)

  return (
    <div className="container mt-5">
      <div className="text-center">
        <h1>🎉 Prueba de Carta Flotante</h1>
        <p className="text-muted">Haz clic en el botón para abrir la carta</p>

        {/* ⭐ Botón para abrir la carta */}
        <button 
          className="btn btn-primary btn-lg mt-4"
          onClick={() => setIsCardOpen(true)}
        >
          ✉️ Abrir Carta
        </button>

        {/* ⭐ Contenido de ejemplo */}
        <div className="mt-5">
          <div className="card">
            <div className="card-body">
              <h5>Contenido de la página</h5>
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            </div>
          </div>
        </div>
      </div>

      {/* ⭐ Componente de la carta flotante */}
      <FloatingCard 
        isOpen={isCardOpen}
        onClose={() => setIsCardOpen(false)}
      />
    </div>
  )
}

export default TestPage