import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";

export function TodayConsultationsPage() {
  const [consultations, setConsultations] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchTodayConsultations();
  }, []);

  const fetchTodayConsultations = async () => {
    try {
      setLoading(true);
      const today = new Date().toISOString().split('T')[0];
      
      // Obtener consultas del día de hoy con estado Pendent o Confirmed
      const res = await API.GetAll(
        `Consultations?DateFrom=${today}&DateTo=${today}&IncludePatient=true&IncludeReason=true&IncludeDayAvailable=true`
      );
      console.log("Today consultations response:", res);
      const data = res?.data?.items || res?.data || res || [];
      
      // Filtrar solo pendientes y confirmadas
      const filtered = Array.isArray(data) 
        ? data.filter((c: any) => 
            c.state?.toLowerCase() === 'pendent' || 
            c.state?.toLowerCase() === 'confirmed'
          )
        : [];
      
      setConsultations(filtered);
    } catch (error) {
      console.error("Error fetching today consultations:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleAttend = (consultationId: number) => {
    navigate(`/consultations/${consultationId}/attend`);
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
            <i className="bi bi-calendar-day me-2"></i>
            Consultas del Día
          </h2>
          <p className="text-muted">
            Gestiona las consultas programadas para hoy
          </p>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : consultations.length === 0 ? (
          <div className="alert alert-info text-center">
            <i className="bi bi-info-circle me-2"></i>
            No hay consultas programadas para hoy.
          </div>
        ) : (
          <div className="card shadow-sm">
            <div className="card-body">
              <div className="table-responsive">
                <table className="table table-hover">
                  <thead className="table-light">
                    <tr>
                      <th>Turno</th>
                      <th>Paciente</th>
                      <th>Motivo</th>
                      <th>Horario</th>
                      <th>Clínica</th>
                      <th>Estado</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {consultations.map((consultation) => (
                      <tr key={consultation.id}>
                        <td>
                          <span className="badge bg-primary">
                            #{consultation.turn ?? '-'}
                          </span>
                        </td>
                        <td className="fw-semibold">
                          <i className="bi bi-person me-2"></i>
                          {consultation.patient?.user?.nameComplete || 
                           consultation.patient?.fullName || '-'}
                        </td>
                        <td>
                          <i className="bi bi-clipboard2-pulse me-2 text-muted"></i>
                          {consultation.reason?.title || '-'}
                        </td>
                        <td>
                          <i className="bi bi-clock me-2 text-muted"></i>
                          {formatTime(consultation.dayAvailable?.startTime)} - 
                          {formatTime(consultation.dayAvailable?.endTime)}
                        </td>
                        <td>
                          <i className="bi bi-building me-2 text-muted"></i>
                          {consultation.dayAvailable?.clinicName || '-'}
                        </td>
                        <td>
                          <span className={`badge bg-${getStateColor(consultation.state)}`}>
                            {getStateText(consultation.state)}
                          </span>
                        </td>
                        <td className="text-end">
                          <button
                            className="btn btn-sm btn-primary"
                            onClick={() => handleAttend(consultation.id)}
                            title="Atender paciente"
                          >
                            <i className="bi bi-stethoscope me-1"></i>
                            Atender
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}
      </div>
    </>
  );
}