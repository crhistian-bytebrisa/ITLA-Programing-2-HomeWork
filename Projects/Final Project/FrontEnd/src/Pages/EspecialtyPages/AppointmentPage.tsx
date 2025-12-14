import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { useAuth } from "../../Context/AutContext";

export function AppointmentPage() {
  const { user } = useAuth();
  const [appointments, setAppointments] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const patientId = user?.patient?.id;
    if (patientId) {
      fetchAppointments(patientId);
    }
  }, [user]);

  const fetchAppointments = async (patientId: number) => {
    try {
      setLoading(true);
      // Obtener solo consultas pendientes
      const res = await API.GetAll(
        `Patients/${patientId}/Consultations?State=Pendent&IncludePatient=true&IncludeReason=true&IncludeDayAvailable=true`
      );
      console.log("Appointments response:", res);
      const data = res?.data?.items || res?.data || res || [];
      setAppointments(Array.isArray(data) ? data : []);
    } catch (error) {
      console.error("Error fetching appointments:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleCancelAppointment = async (consultationId: number) => {
    if (!window.confirm("¿Está seguro de que desea cancelar esta cita?")) {
      return;
    }

    try {
      // Cambiar el estado a "Cancelled"
      const consultation = appointments.find(a => a.id === consultationId);
      if (!consultation) return;

      await API.Put(`Consultations/${consultationId}`, {
        id: consultationId,
        patientId: consultation.patientId,
        reasonId: consultation.reasonId,
        dayAvailableId: consultation.dayAvailableId,
        state: "Cancelled"
      });

      alert("Cita cancelada exitosamente");
      
      // Recargar las citas
      const patientId = user?.patient?.id;
      if (patientId) {
        await fetchAppointments(patientId);
      }
    } catch (error) {
      console.error("Error cancelling appointment:", error);
      alert("Error al cancelar la cita. Por favor intente nuevamente.");
    }
  };

  const formatDate = (dateStr: string) => {
    if (!dateStr) return "-";
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
    return timeStr.substring(0, 5);
  };

  const getStateColor = (state: string) => {
    switch (state?.toLowerCase()) {
      case 'pendent':
        return 'warning';
      case 'confirmed':
        return 'success';
      case 'cancelled':
        return 'danger';
      default:
        return 'secondary';
    }
  };

  const getStateText = (state: string) => {
    switch (state?.toLowerCase()) {
      case 'pendent':
        return 'Pendiente';
      case 'confirmed':
        return 'Confirmada';
      case 'cancelled':
        return 'Cancelada';
      default:
        return state;
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">
            <i className="bi bi-calendar-check me-2"></i>
            Citas Pendientes
          </h2>
          <p className="text-muted">
            Revisa tus próximas citas médicas programadas
          </p>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : appointments.length === 0 ? (
          <div className="alert alert-info text-center">
            <i className="bi bi-info-circle me-2"></i>
            No tienes citas pendientes en este momento.
          </div>
        ) : (
          <div className="row g-4">
            {appointments.map((appointment) => (
              <div key={appointment.id} className="col-12">
                <div className="card shadow-sm border-0">
                  <div className="card-body">
                    <div className="row align-items-center">
                      {/* Columna de fecha y hora */}
                      <div className="col-md-4">
                        <div className="mb-3 mb-md-0">
                          <div className="d-flex align-items-center mb-2">
                            <i className="bi bi-calendar3 text-primary me-2 fs-5"></i>
                            <div>
                              <div className="fw-bold text-primary">
                                {formatDate(appointment.dayAvailable?.date)}
                              </div>
                              <small className="text-muted">
                                <i className="bi bi-clock me-1"></i>
                                {formatTime(appointment.dayAvailable?.startTime)} - {formatTime(appointment.dayAvailable?.endTime)}
                              </small>
                            </div>
                          </div>
                        </div>
                      </div>

                      {/* Columna de detalles */}
                      <div className="col-md-5">
                        <div className="mb-2">
                          <i className="bi bi-clipboard2-pulse text-muted me-2"></i>
                          <strong>Motivo:</strong> {appointment.reason?.title || '-'}
                        </div>
                        
                        {appointment.dayAvailable?.clinic && (
                          <>
                            <div className="mb-2">
                              <i className="bi bi-building text-muted me-2"></i>
                              <strong>Clínica:</strong> {appointment.dayAvailable.clinic.name}
                            </div>
                            {appointment.dayAvailable.clinic.address && (
                              <div className="mb-2">
                                <i className="bi bi-geo-alt text-muted me-2"></i>
                                <small className="text-muted">{appointment.dayAvailable.clinic.address}</small>
                              </div>
                            )}
                            {appointment.dayAvailable.clinic.phoneNumber && (
                              <div>
                                <i className="bi bi-telephone text-muted me-2"></i>
                                <small className="text-muted">{appointment.dayAvailable.clinic.phoneNumber}</small>
                              </div>
                            )}
                          </>
                        )}

                        <div className="mt-2">
                          <i className="bi bi-hash text-muted me-2"></i>
                          <strong>Turno:</strong> {appointment.turn ?? '-'}
                        </div>
                      </div>

                      {/* Columna de estado y acciones */}
                      <div className="col-md-3 text-md-end">
                        <div className="mb-3">
                          <span className={`badge bg-${getStateColor(appointment.state)} px-3 py-2`}>
                            {getStateText(appointment.state)}
                          </span>
                        </div>
                        <button
                          className="btn btn-outline-danger btn-sm"
                          onClick={() => handleCancelAppointment(appointment.id)}
                          title="Cancelar cita"
                        >
                          <i className="bi bi-x-circle me-1"></i>
                          Cancelar Cita
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
}