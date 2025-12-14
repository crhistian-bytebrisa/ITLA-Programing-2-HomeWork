import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { useAuth } from "../../Context/AutContext";

export function UserMedicinesPage() {
  const [medicines, setMedicines] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  useEffect(() => {
    const patientId = user?.patient?.id;
    console.log('User data:', user);
    console.log('Patient ID:', patientId);
    if (patientId) {
      fetchMedicines(patientId);
    }
  }, [user]);

  const fetchMedicines = async (patientId: number) => {
    try {
      setLoading(true);
      const res = await API.GetAll(`Patients/${patientId}/Medicines`);
      console.log('Medicines response:', res);
      setMedicines(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching medicines:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Historial de Medicamentos</h2>
          <p className="text-muted">Consulta tus medicamentos recetados</p>
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
              {medicines.length === 0 ? (
                <p className="text-muted text-center py-4">No tienes medicamentos registrados</p>
              ) : (
                <div className="table-responsive">
                  <table className="table table-hover">
                    <thead className="table-light">
                      <tr>
                        <th>Medicamento</th>
                        <th>Formato</th>
                        <th>Dosaje Inicial</th>
                        <th>Dosaje Final</th>
                      </tr>
                    </thead>
                    <tbody>
                      {medicines.map((m: any) => (
                        <tr key={m.id}>
                          <td className="fw-semibold">{m.name || m.medicineName}</td>
                          <td>{m.format || m.medicineFormat}</td>
                          <td>{m.startDosage || '-'}</td>
                          <td>{m.endDosage || '-'}</td>
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
