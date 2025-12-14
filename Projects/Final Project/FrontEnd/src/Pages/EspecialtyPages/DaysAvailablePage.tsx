import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { DayAvailable, DayAvailableCreate } from "../../Models/ClinicsAndDaysModels";

export default function DaysAvailableCRUD() {
  const [days, setDays] = useState<DayAvailable[]>([]);
  const [clinics, setClinics] = useState<any[]>([]);
  const [doctors, setDoctors] = useState<any[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentDay, setCurrentDay] = useState<DayAvailable | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState<Partial<DayAvailableCreate & { id?: number; doctorId?: number; isAvailable?: boolean }>>({
    clinicId: undefined,
    date: "",
    startTime: "",
    endTime: "",
    limit: 1,
    doctorId: undefined,
    isAvailable: true,
  });

  useEffect(() => {
    fetchDays();
    fetchClinics();
    fetchDoctors();
  }, []);

  const fetchDays = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("DaysAvailable");
      const data = response?.data ?? response;
      setDays(Array.isArray(data) ? data : []);
    } catch (error) {
      console.error("Error fetching days:", error);
    } finally {
      setLoading(false);
    }
  };

  const fetchClinics = async () => {
    try {
      const response = await API.GetAll("Clinics");
      const data = response?.data ?? response;
      setClinics(Array.isArray(data) ? data : []);
    } catch (error) {
      console.error("Error fetching clinics:", error);
    }
  };

  const fetchDoctors = async () => {
    try {
      const response = await API.GetAll("Doctor");
      const data = response?.data ?? response;
      setDoctors(Array.isArray(data) ? data : []);
    } catch (error) {
      console.error("Error fetching doctors:", error);
    }
  };

  const handleOpenModal = (day?: DayAvailable) => {
    if (day) {
      setIsEditing(true);
      setCurrentDay(day);
      const startTimeValue = day.startTime?.substring(0, 5) ?? '';
      const endTimeValue = day.endTime?.substring(0, 5) ?? '';
      setFormData({
        id: day.id,
        clinicId: day.clinicId,
        date: day.date,
        startTime: startTimeValue,
        endTime: endTimeValue,
        limit: day.limit,
        doctorId: (day as any).doctorId,
        isAvailable: (day as any).isAvailable ?? true,
      });
    } else {
      setIsEditing(false);
      setCurrentDay(null);
      setFormData({ clinicId: undefined, date: "", startTime: "", endTime: "", limit: 1, doctorId: undefined });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setIsEditing(false);
    setCurrentDay(null);
    setFormData({ clinicId: undefined, date: "", startTime: "", endTime: "", limit: 1, doctorId: undefined, isAvailable: true });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value, type, checked } = e.target as any;
    setFormData((prev) => ({ 
      ...prev, 
      [name]: type === 'checkbox' ? checked : (type === 'number' ? Number(value) : value)
    }));
  };

  const consultationsCount = (d: any) => d.consultations?.length ?? d.consultationsCount ?? d.Consultations?.length ?? 0;
  const isAvailable = (d: any) => d.availableSlots > 0;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const startTimeFormatted = (formData.startTime ?? '').length === 5 
        ? formData.startTime + ':00'
        : formData.startTime;
      const endTimeFormatted = (formData.endTime ?? '').length === 5
        ? formData.endTime + ':00'
        : formData.endTime;
        
      const payload = {
        clinicId: formData.clinicId,
        date: formData.date,
        startTime: startTimeFormatted,
        endTime: endTimeFormatted,
        limit: formData.limit,
        doctorId: formData.doctorId,
        isAvailable: formData.isAvailable,
      };
      if (isEditing && currentDay) {
        const response = await API.Put(`DaysAvailable/${currentDay.id}`, { id: currentDay.id, ...payload });
        if (response) {
          await fetchDays();
          handleCloseModal();
        }
      } else {
        const response = await API.Post("DaysAvailable", payload);
        if (response) {
          await fetchDays();
          handleCloseModal();
        }
      }
    } catch (error) {
      console.error("Error saving day:", error);
      alert("Error al guardar el día disponible");
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar este día disponible?")) {
      try {
        await API.Delete(`DaysAvailable/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting day:", error);
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">Días Disponibles</h2>
            <p className="text-muted">Administra los días disponibles por clínica</p>
          </div>
          <button className="btn btn-primary" onClick={() => handleOpenModal()}>
            <i className="bi bi-plus-circle me-2"></i>
            Nuevo Día
          </button>
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
                      <th>Clínica</th>
                      <th>Fecha</th>
                      <th>Inicio</th>
                      <th>Fin</th>
                      <th>Límite</th>
                      <th>Consultas</th>
                      <th>Slots</th>
                      <th>Disponible</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {days.length === 0 ? (
                      <tr>
                        <td colSpan={10} className="text-center text-muted py-4">No hay días registrados</td>
                      </tr>
                    ) : (
                      days.map((d) => (
                        <tr key={d.id}>
                          <td>{d.id}</td>
                          <td>{d.clinic?.name ?? d.clinic?.name ?? '-'}</td>
                          <td>{d.date}</td>
                          <td>{d.startTime}</td>
                          <td>{d.endTime}</td>
                          <td>{d.limit}</td>
                          <td><span className="badge bg-info">{consultationsCount(d)}</span></td>
                          <td><span className="badge bg-warning text-dark">{d.availableSlots}</span></td>
                          <td><span className={`badge ${isAvailable(d) ? 'bg-success' : 'bg-danger'}`}>{isAvailable(d) ? 'Sí' : 'No'}</span></td>
                          <td className="text-end">
                            <button className="btn btn-sm btn-outline-primary me-2" onClick={() => handleOpenModal(d)} title="Editar"><i className="bi bi-pencil"></i></button>
                            <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(d.id)} title="Eliminar"><i className="bi bi-trash"></i></button>
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

      {showModal && (
        <>
          <div className="modal fade show d-block" tabIndex={-1} style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
            <div className="modal-dialog modal-dialog-centered">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title">{isEditing ? 'Editar Día' : 'Nuevo Día'}</h5>
                  <button type="button" className="btn-close" onClick={handleCloseModal}></button>
                </div>
                <form onSubmit={handleSubmit}>
                  <div className="modal-body">
                    <div className="mb-3">
                      <label className="form-label">Clínica *</label>
                      <select name="clinicId" className="form-select" value={formData.clinicId as any ?? ''} onChange={handleInputChange} required>
                        <option value="">-- Seleccione --</option>
                        {clinics.map((c: any) => (
                          <option key={c.id} value={c.id}>{c.name}</option>
                        ))}
                      </select>
                    </div>

                    <div className="mb-3">
                      <label className="form-label">Fecha *</label>
                      <input type="date" name="date" className="form-control" value={formData.date ?? ''} onChange={handleInputChange} required />
                    </div>

                    <div className="mb-3">
                      <label className="form-label">Inicio *</label>
                      <input type="time" name="startTime" className="form-control" value={formData.startTime ?? ''} onChange={handleInputChange} required />
                    </div>

                    <div className="mb-3">
                      <label className="form-label">Fin *</label>
                      <input type="time" name="endTime" className="form-control" value={formData.endTime ?? ''} onChange={handleInputChange} required />
                    </div>

                    <div className="mb-3">
                      <label className="form-label">Límite *</label>
                      <input type="number" name="limit" min={1} className="form-control" value={formData.limit ?? 1} onChange={handleInputChange} required />
                    </div>
                  </div>
                  <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" onClick={handleCloseModal}>Cancelar</button>
                    <button type="submit" className="btn btn-primary">{isEditing ? 'Actualizar' : 'Guardar'}</button>
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
