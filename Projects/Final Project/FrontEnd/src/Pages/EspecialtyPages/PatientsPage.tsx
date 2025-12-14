import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Patient } from "../../Models/PatientsModels";

export function PatientsList() {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetchPatients();
  }, []);

  const fetchPatients = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Patients");
      setPatients(response.data || []);
    } catch (error) {
      console.error("Error fetching patients:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleView = (id: number) => {
    navigate(`/patients/${id}`);
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Pacientes</h2>
          <p className="text-muted">Listado de pacientes (ID, nombre, edad, género y tipo de sangre)</p>
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
                      <th>Nombre</th>
                      <th>Edad</th>
                      <th>Género</th>
                      <th>Tipo de Sangre</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {patients.length === 0 ? (
                      <tr>
                        <td colSpan={6} className="text-center text-muted py-4">
                          No hay pacientes registrados
                        </td>
                      </tr>
                    ) : (
                      patients.map((p) => (
                        <tr key={p.id}>
                          <td>{p.id}</td>
                          <td className="fw-semibold">{p.user?.nameComplete || "-"}</td>
                            <td>{p.age ?? (p.dateOfBirth ? Math.floor((new Date().getTime() - new Date(p.dateOfBirth).getTime()) / (1000 * 60 * 60 * 24 * 365)) : "-")}</td>
                            <td>{p.gender || "-"}</td>
                            <td>{p.bloodType || "-"}</td>
                            <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary"
                              onClick={() => handleView(p.id)}
                              title="Ver detalles"
                            >
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
