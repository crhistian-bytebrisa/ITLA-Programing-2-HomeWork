// src/components/FloatingCard.jsx
import { useState } from 'react'

function FloatingCard({ isOpen, onClose }) {
  const [mensaje, setMensaje] = useState('')

  const handleEnviar = () => {
    if (!mensaje.trim()) {
      alert('⚠️ Por favor escribe un mensaje')
      return
    }

    // ⭐ Mostrar el mensaje en una alerta
    alert(`📨 Mensaje enviado:\n\n"${mensaje}"`)
    
    // Limpiar el mensaje y cerrar la carta
    setMensaje('')
    onClose()
  }

  // Si isOpen es false, no mostrar nada
  if (!isOpen) return null

  return (
    <>
      {/* ⭐ Overlay oscuro detrás de la carta */}
      <div 
        className="modal-backdrop show"
        onClick={onClose}
        style={{
          position: 'fixed',
          top: 0,
          left: 0,
          width: '100%',
          height: '100%',
          backgroundColor: 'rgba(0, 0, 0, 0.5)',
          zIndex: 1040
        }}
      />

      {/* ⭐ La carta flotante */}
      <div 
        className="card shadow-lg"
        style={{
          position: 'fixed',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: '90%',
          maxWidth: '500px',
          zIndex: 1050,
          animation: 'slideDown 0.5s ease-out'
        }}
      >
        <div className="card-header bg-primary text-white d-flex justify-content-between align-items-center">
          <h5 className="mb-0">✉️ Escribe tu mensaje</h5>
          {/* ⭐ Botón X para cerrar */}
          <button 
            type="button" 
            className="btn-close btn-close-white"
            onClick={onClose}
          />
        </div>

        <div className="card-body">
          {/* ⭐ Textarea para el mensaje */}
          <div className="mb-3">
            <label htmlFor="mensaje" className="form-label">
              Mensaje:
            </label>
            <textarea 
              id="mensaje"
              className="form-control"
              value={mensaje}
              onChange={(e) => setMensaje(e.target.value)}
              rows={5}
              placeholder="Escribe tu mensaje aquí..."
              maxLength={500}
              autoFocus  // ⭐ Se enfoca automáticamente
            />
            <small className="form-text text-muted">
              {mensaje.length}/500 caracteres
            </small>
          </div>

          {/* ⭐ Botones */}
          <div className="d-flex gap-2">
            <button 
              type="button"
              className="btn btn-success flex-grow-1"
              onClick={handleEnviar}
            >
              📤 Enviar
            </button>
            <button 
              type="button"
              className="btn btn-secondary"
              onClick={onClose}
            >
              Cancelar
            </button>
          </div>
        </div>
      </div>

      {/* ⭐ Estilos para la animación */}
      <style>{`
        @keyframes slideDown {
          from {
            transform: translate(-50%, -150%);
            opacity: 0;
          }
          to {
            transform: translate(-50%, -50%);
            opacity: 1;
          }
        }
      `}</style>
    </>
  )
}

export default FloatingCard