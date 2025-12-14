import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Insurance, InsuranceCreate, InsuranceUpdate } from "../../Models/InsurancesModels";

export function InsurancesCRUD() {
  const [insurances, setInsurances] = useState<Insurance[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentInsurance, setCurrentInsurance] = useState<InsuranceUpdate | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState<InsuranceCreate>({
    name: ""
  });

  useEffect(() => {
    fetchInsurances();
  }, []);

  const fetchInsurances = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Insurances");
      setInsurances(response.data);
    } catch (error) {
      console.error("Error fetching insurances:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (insurance?: Insurance) => {
    if (insurance) {
      setIsEditing(true);
      setCurrentInsurance({
        id: insurance.id,
        name: insurance.name
      });
      setFormData({
        name: insurance.name
      });
    } else {
      setIsEditing(false);
      setCurrentInsurance(null);
      setFormData({
        name: ""
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setIsEditing(false);
    setCurrentInsurance(null);
    setFormData({
      name: ""
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
      if (isEditing && currentInsurance) {
        await API.Put(`Insurances/${currentInsurance.id}`, {
          id: currentInsurance.id,
          name: formData.name
        });
        window.location.reload();
      } else {
        await API.Post("Insurances", formData);
        await fetchInsurances();
        handleCloseModal();
      }
    } catch (error) {
      console.error("Error saving insurance:", error);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar este seguro?")) {
      try {
        await API.Delete(`Insurances/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting insurance:", error);
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">Gestión de Seguros</h2>
            <p className="text-muted">Administra las compañías de seguros médicos</p>
          </div>
          <button
            className="btn btn-primary"
            onClick={() => handleOpenModal()}
          >
            <i className="bi bi-plus-circle me-2"></i>
            Nuevo Seguro
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
                      <th>Pacientes Afiliados</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {insurances.length === 0 ? (
                      <tr>
                        <td colSpan={4} className="text-center text-muted py-4">
                          No hay seguros registrados
                        </td>
                      </tr>
                    ) : (
                      insurances.map((insurance) => (
                        <tr key={insurance.id}>
                          <td>{insurance.id}</td>
                          <td className="fw-semibold">{insurance.name}</td>
                          <td>
                            <span className="badge bg-primary">
                              {insurance.patientsCount}
                            </span>
                          </td>
                          <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary me-2"
                              onClick={() => handleOpenModal(insurance)}
                              title="Editar"
                            >
                              <i className="bi bi-pencil"></i>
                            </button>
                            <button
                              className="btn btn-sm btn-outline-danger"
                              onClick={() => handleDelete(insurance.id)}
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
                    {isEditing ? "Editar Seguro" : "Nuevo Seguro"}
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
                        <i className="bi bi-shield-check me-2"></i>
                        Nombre de la Aseguradora *
                      </label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Ej: Seguros Médicos Universal"
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
