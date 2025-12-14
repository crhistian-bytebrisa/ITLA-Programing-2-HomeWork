import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { useAuth } from "../../Context/AutContext";

export function UserConsultationsPage() {
  const [consultations, setConsultations] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  useEffect(() => {
    const patientId = user?.patient?.id;
    console.log('User data:', user);
    console.log('Patient ID:', patientId);
    if (patientId) {
      fetchConsultations(patientId);
    }
  }, [user]);

  const fetchConsultations = async (patientId: number) => {
    try {
      setLoading(true);
      const res = await API.GetAll(`Patients/${patientId}/Consultations?IncludePatient=true&IncludeReason=true&IncludeDayAvailable=true`);
      console.log('Consultations response:', res);
      setConsultations(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching consultations:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Historial de Consultas</h2>
          <p className="text-muted">Revisa tus consultas médicas anteriores</p>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : (
          <div className="card shadow-sm">
            <div className="card-body">
              {consultations.length === 0 ? (
                <p className="text-muted text-center py-4">No tienes consultas registradas</p>
              ) : (
                <div className="table-responsive">
                  <table className="table table-hover">
                    <thead className="table-light">
                      <tr>
                        <th>ID</th>
                        <th>Razón</th>
                        <th>Día</th>
                        <th>Turno</th>
                        <th>Estado</th>
                      </tr>
                    </thead>
                    <tbody>
                      {consultations.map((c: any) => (
                        <tr key={c.id}>
                          <td>{c.id}</td>
                          <td>{(c.reason && c.reason.title) || '-'}</td>
                          <td>{(c.dayAvailable && c.dayAvailable.date) || '-'}</td>
                          <td>{c.turn ?? '-'}</td>
                          <td>{c.state}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </div>
          </div>
        )}
      </div>
    </>
  );
}
