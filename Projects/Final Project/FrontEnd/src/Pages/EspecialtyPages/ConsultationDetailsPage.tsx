import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";

export function ConsultationDetails() {
  const { id } = useParams();
  const consultationId = Number(id || 0);

  const [consultation, setConsultation] = useState<any | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (!consultationId) return;
    fetchConsultation();
  }, [consultationId]);

  const fetchConsultation = async () => {
    try {
      setLoading(true);
      const res = await API.GetById(`Consultations/${consultationId}?IncludePatient=true&IncludeReason=true&IncludeDayAvailable=true`);
      setConsultation(res?.data || res || null);
    } catch (error) {
      console.error('Error fetching consultation', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Detalle de Consulta</h2>
          <p className="text-muted">Información básica de la consulta</p>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : (
          consultation && (
            <div className="card">
              <div className="card-body">
                <p><strong>ID:</strong> {consultation.id}</p>
                <p><strong>Paciente:</strong> {consultation.patient?.fullName || '-'}</p>
                <p><strong>Razón:</strong> {consultation.reason?.title || '-'}</p>
                <p><strong>Día:</strong> {consultation.dayAvailable?.date || '-'}</p>
                <p><strong>Turno:</strong> {consultation.turn ?? '-'}</p>
                <p><strong>Estado:</strong> {consultation.state}</p>
              </div>
            </div>
          )
        )}
      </div>
    </>
  );
}
