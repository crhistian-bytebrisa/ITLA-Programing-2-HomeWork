import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Clinic, ClinicCreate, ClinicUpdate } from "../../Models/ClinicsAndDaysModels";

export function ClinicsCRUD() {
  const [clinics, setClinics] = useState<Clinic[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentClinic, setCurrentClinic] = useState<ClinicUpdate | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState<ClinicCreate>({
    name: "",
    address: "",
    phoneNumber: ""
  });

  useEffect(() => {
    fetchClinics();
  }, []);

  const fetchClinics = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Clinics");
      setClinics(response.data);
    } catch (error) {
      console.error("Error fetching clinics:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (clinic?: Clinic) => {
    if (clinic) {
      setIsEditing(true);
      setCurrentClinic({
        id: clinic.id,
        name: clinic.name,
        address: clinic.address,
        phoneNumber: clinic.phoneNumber
      });
      setFormData({
        name: clinic.name,
        address: clinic.address,
        phoneNumber: clinic.phoneNumber
      });
    } else {
      setIsEditing(false);
      setCurrentClinic(null);
      setFormData({
        name: "",
        address: "",
        phoneNumber: ""
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setIsEditing(false);
    setCurrentClinic(null);
    setFormData({
      name: "",
      address: "",
      phoneNumber: ""
    });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (isEditing && currentClinic) {
        await API.Put(`Clinics/${currentClinic.id}`, {
          id: currentClinic.id,
          name: formData.name,
          address: formData.address,
          phoneNumber: formData.phoneNumber
        });
        window.location.reload();
        
      } else {
        await API.Post("Clinics", formData);
        await fetchClinics();
        handleCloseModal();
      }
    } catch (error) {
      console.error("Error saving clinic:", error);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar esta clínica?")) {
      try {
        await API.Delete(`Clinics/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting clinic:", error);
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">Gestión de Clínicas</h2>
            <p className="text-muted">Administra las clínicas del sistema</p>
          </div>
          <button
            className="btn btn-primary"
            onClick={() => handleOpenModal()}
          >
            <i className="bi bi-plus-circle me-2"></i>
            Nueva Clínica
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
                      <th>Nombre</th>
                      <th>Dirección</th>
                      <th>Teléfono</th>
                      <th>Días Disponibles</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {clinics.length === 0 ? (
                      <tr>
                        <td colSpan={6} className="text-center text-muted py-4">
                          No hay clínicas registradas
                        </td>
                      </tr>
                    ) : (
                      clinics.map((clinic) => (
                        <tr key={clinic.id}>
                          <td>{clinic.id}</td>
                          <td className="fw-semibold">{clinic.name}</td>
                          <td>{clinic.address}</td>
                          <td>
                            <i className="bi bi-telephone me-2"></i>
                            {clinic.phoneNumber}
                          </td>
                          <td>
                            <span className="badge bg-success">
                              {clinic.daysAvailableCount}
                            </span>
                          </td>
                          <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary me-2"
                              onClick={() => handleOpenModal(clinic)}
                              title="Editar"
                            >
                              <i className="bi bi-pencil"></i>
                            </button>
                            <button
                              className="btn btn-sm btn-outline-danger"
                              onClick={() => handleDelete(clinic.id)}
                              title="Eliminar"
                            >
                              <i className="bi bi-trash"></i>
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

      {/* Modal */}
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
                    {isEditing ? "Editar Clínica" : "Nueva Clínica"}
                  </h5>
                  <button
                    type="button"
                    className="btn-close"
                    onClick={handleCloseModal}
                  ></button>
                </div>
                <form onSubmit={handleSubmit}>
                  <div className="modal-body">
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-hospital me-2"></i>
                        Nombre *
                      </label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Ej: Clínica San Rafael"
                        required
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-geo-alt me-2"></i>
                        Dirección *
                      </label>
                      <textarea
                        name="address"
                        className="form-control"
                        rows={2}
                        value={formData.address}
                        onChange={handleInputChange}
                        placeholder="Ej: Av. Principal #123, Ciudad"
                        required
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-telephone me-2"></i>
                        Teléfono *
                      </label>
                      <input
                        type="tel"
                        name="phoneNumber"
                        className="form-control"
                        value={formData.phoneNumber}
                        onChange={handleInputChange}
                        placeholder="Ej: +1 (809) 555-1234"
                        required
                      />
                    </div>
                  </div>
                  <div className="modal-footer">
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={handleCloseModal}
                    >
                      Cancelar
                    </button>
                    <button type="submit" className="btn btn-primary">
                      <i className="bi bi-save me-2"></i>
                      {isEditing ? "Actualizar" : "Guardar"}
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