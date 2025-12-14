import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";

export function ConsultationsList() {
  const [consultations, setConsultations] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchConsultations();
  }, []);

  const fetchConsultations = async () => {
    try {
      setLoading(true);
      const res = await API.GetAll(`Consultations?IncludePatient=true&IncludeReason=true&IncludeDayAvailable=true`);
      console.log(res);
      setConsultations(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching consultations:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleView = (id: number) => {
    navigate(`/consultations/${id}`);
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Consultas</h2>
          <p className="text-muted">Listado de consultas</p>
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
              <div className="table-responsive">
                <table className="table table-hover">
                  <thead className="table-light">
                    <tr>
                      <th>ID</th>
                      <th>Paciente</th>
                      <th>Razón</th>
                      <th>Día</th>
                      <th>Turno</th>
                      <th>Estado</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {consultations.length === 0 ? (
                      <tr>
                        <td colSpan={7} className="text-center text-muted py-4">No hay consultas</td>
                      </tr>
                    ) : (
                      consultations.map((c: any) => (
                        <tr key={c.id}>
                          <td>{c.id}</td>
                          <td className="fw-semibold">{(c.patient && c.patient.fullName) || '-'}</td>
                          <td>{(c.reason && c.reason.title) || c.reasonTitle || '-'}</td>
                          <td>{(c.dayAvailable && c.dayAvailable.date) || c.day || '-'}</td>
                          <td>{c.turn ?? '-'}</td>
                          <td>{c.state}</td>
                          <td className="text-end">
                            <button className="btn btn-sm btn-outline-primary" onClick={() => handleView(c.id)} title="Ver consulta">
                              <i className="bi bi-eye"></i>
                            </button>
                          </td>
                        </tr>
                      ))
                    )}
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
