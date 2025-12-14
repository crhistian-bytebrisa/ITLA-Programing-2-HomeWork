import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { useAuth } from "../../Context/AutContext";

export function SchedulePage() {
  const { user } = useAuth();
  const [daysAvailable, setDaysAvailable] = useState<any[]>([]);
  const [reasons, setReasons] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [selectedDay, setSelectedDay] = useState<any>(null);
  const [selectedReasonId, setSelectedReasonId] = useState<number>(0);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    fetchDaysAvailable();
    fetchReasons();
  }, []);

  const fetchDaysAvailable = async () => {
    try {
      setLoading(true);      
      const res = await API.GetAll(`DaysAvailable?OnlyAvailable=true`);
      console.log("DaysAvailable response:", res);
      const data = res?.data || res || [];
      setDaysAvailable(Array.isArray(data) ? data : []);
    } catch (error) {
      console.error("Error fetching days available:", error);
    } finally {
      setLoading(false);
    }
  };

  const fetchReasons = async () => {
    try {
      const res = await API.GetAll("Reasons");
      console.log("Reasons response:", res);
      const data = res?.data || res || [];
      // Filtrar solo razones disponibles
      const availableReasons = Array.isArray(data) 
        ? data.filter((r: any) => r.available !== false)
        : [];
      setReasons(availableReasons);
    } catch (error) {
      console.error("Error fetching reasons:", error);
    }
  };

  const handleOpenModal = (day: any) => {
    setSelectedDay(day);
    setSelectedReasonId(0);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedDay(null);
    setSelectedReasonId(0);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!selectedReasonId || !selectedDay) {
      alert("Por favor seleccione una razón para la consulta");
      return;
    }

    const patientId = user?.patient?.id;
    if (!patientId) {
      alert("Error: No se encontró información del paciente");
      return;
    }

    try {
      setSubmitting(true);
      const consultationData = {
        patientId: patientId,
        reasonId: selectedReasonId,
        dayAvailableId: selectedDay.id,
        state: "Pendent" // Estado pendiente por defecto
      };

      console.log("Creating consultation:", consultationData);
      const response = await API.Post("Consultations", consultationData);
      console.log("Consultation created:", response);

      alert("¡Consulta agendada exitosamente!");
      handleCloseModal();
      // Recargar los días disponibles para actualizar los slots
      await fetchDaysAvailable();
    } catch (error) {
      console.error("Error creating consultation:", error);
      alert("Error al agendar la consulta. Por favor intente nuevamente.");
    } finally {
      setSubmitting(false);
    }
  };

  const formatDate = (dateStr: string) => {
    const date = new Date(dateStr);
    return date.toLocaleDateString('es-ES', { 
      weekday: 'long', 
      year: 'numeric', 
      month: 'long', 
      day: 'numeric' 
    });
  };

  const formatTime = (timeStr: string) => {
    if (!timeStr) return '';
    // Extraer solo HH:mm de "HH:mm:ss"
    return timeStr.substring(0, 5);
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">
            <i className="bi bi-calendar-plus me-2"></i>
            Agendar Cita Médica
          </h2>
          <p className="text-muted">
            Selecciona el día y horario que mejor se ajuste a tu disponibilidad
          </p>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : daysAvailable.length === 0 ? (
          <div className="alert alert-info text-center">
            <i className="bi bi-info-circle me-2"></i>
            No hay días disponibles en este momento. Por favor, intente más tarde.
          </div>
        ) : (
          <div className="row g-4">
            {daysAvailable.map((day) => {
              const availableSlots = day.availableSlots;
              return (
                <div key={day.id} className="col-md-6 col-lg-4">
                  <div className="card h-100 shadow-sm border-0">
                    <div className="card-body">
                      <div className="d-flex justify-content-between align-items-start mb-3">
                        <h5 className="card-title text-primary mb-0">
                          <i className="bi bi-calendar-check me-2"></i>
                          {formatDate(day.date)}
                        </h5>
                        <span className="badge bg-success">
                          {availableSlots} {availableSlots === 1 ? 'slot' : 'slots'}
                        </span>
                      </div>

                      <div className="mb-3">
                        <div className="d-flex align-items-center mb-2">
                          <i className="bi bi-clock text-muted me-2"></i>
                          <span className="text-muted">Horario:</span>
                        </div>
                        <div className="ms-4">
                          <strong>{formatTime(day.startTime)}</strong>
                          <span className="mx-2">-</span>
                          <strong>{formatTime(day.endTime)}</strong>
                        </div>
                      </div>

                      <div className="mb-3">
                        <div className="d-flex align-items-center mb-2">
                          <i className="bi bi-building text-muted me-2"></i>
                          <span className="text-muted">Clínica:</span>
                        </div>
                        <div className="ms-4">
                          <strong>{day.clinic?.name || 'No especificada'}</strong>
                        </div>
                      </div>

                      {day.clinic?.address && (
                        <div className="mb-3">
                          <div className="d-flex align-items-center mb-2">
                            <i className="bi bi-geo-alt text-muted me-2"></i>
                            <span className="text-muted">Dirección:</span>
                          </div>
                          <div className="ms-4">
                            <small>{day.clinic.address}</small>
                          </div>
                        </div>
                      )}
                    </div>

                    <div className="card-footer bg-white border-0 pt-0">
                      <button
                        className="btn btn-primary w-100"
                        onClick={() => handleOpenModal(day)}
                      >
                        <i className="bi bi-calendar-plus me-2"></i>
                        Agendar Cita
                      </button>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>

      {/* Modal para seleccionar razón */}
      {showModal && (
        <>
          <div
            className="modal fade show d-block"
            tabIndex={-1}
            style={{ backgroundColor: "rgba(0,0,0,0.5)" }}
          >
            <div className="modal-dialog modal-dialog-centered">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title">
                    <i className="bi bi-clipboard2-pulse me-2"></i>
                    Confirmar Cita
                  </h5>
                  <button
                    type="button"
                    className="btn-close"
                    onClick={handleCloseModal}
                    disabled={submitting}
                  ></button>
                </div>

                <form onSubmit={handleSubmit}>
                  <div className="modal-body">
                    <div className="alert alert-info mb-3">
                      <strong>Fecha seleccionada:</strong>
                      <br />
                      {selectedDay && formatDate(selectedDay.date)}
                      <br />
                      <strong>Horario:</strong> {selectedDay && formatTime(selectedDay.startTime)} - {selectedDay && formatTime(selectedDay.endTime)}
                    </div>

                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-chat-left-text me-2"></i>
                        Motivo de la consulta *
                      </label>
                      <select
                        className="form-select"
                        value={selectedReasonId}
                        onChange={(e) => setSelectedReasonId(Number(e.target.value))}
                        required
                        disabled={submitting}
                      >
                        <option value={0}>-- Seleccione un motivo --</option>
                        {reasons.map((reason) => (
                          <option key={reason.id} value={reason.id}>
                            {reason.title}
                          </option>
                        ))}
                      </select>
                      {reasons.length === 0 && (
                        <small className="text-muted">
                          No hay motivos disponibles en este momento
                        </small>
                      )}
                    </div>
                  </div>

                  <div className="modal-footer">
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={handleCloseModal}
                      disabled={submitting}
                    >
                      Cancelar
                    </button>
                    <button
                      type="submit"
                      className="btn btn-primary"
                      disabled={submitting || !selectedReasonId}
                    >
                      {submitting ? (
                        <>
                          <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                          Agendando...
                        </>
                      ) : (
                        <>
                          <i className="bi bi-check-circle me-2"></i>
                          Confirmar Cita
                        </>
                      )}
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  );
}