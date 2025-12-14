import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Medicine, MedicineCreate } from "../../Models/MedicinesModels";

export function MedicinesCRUD() {
  const [medicines, setMedicines] = useState<Medicine[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentMedicine, setCurrentMedicine] = useState<Medicine | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState({
    name: "",
    description: "",
    format: "",
    isActive: true
  });

  useEffect(() => {
    fetchMedicines();
  }, []);

  const fetchMedicines = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Medicines");
      setMedicines(response.data);
    } catch (error) {
      console.error("Error fetching medicines:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (medicine?: Medicine) => {
    if (medicine) {
      setIsEditing(true);
      setCurrentMedicine(medicine);
      setFormData({
        name: medicine.name,
        description: medicine.description,
        format: medicine.format,
        isActive: medicine.isActive
      });
    } else {
      setIsEditing(false);
      setCurrentMedicine(null);
      setFormData({
        name: "",
        description: "",
        format: "",
        isActive: true
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setIsEditing(false);
    setCurrentMedicine(null);
    setFormData({
      name: "",
      description: "",
      format: "",
      isActive: true
    });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value, type } = e.target as any;

    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? (e.target as HTMLInputElement).checked : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (isEditing && currentMedicine) {
        await API.Put(`Medicines/${currentMedicine.id}`, {
          id: currentMedicine.id,
          name: formData.name,
          description: formData.description,
          format: formData.format,
          isActive: formData.isActive
        });
        window.location.reload();
      } else {
        await API.Post("Medicines", {
          name: formData.name,
          description: formData.description,
          format: formData.format
        });
        await fetchMedicines();
        handleCloseModal();
      }
    } catch (error) {
      console.error("Error saving medicine:", error);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar este medicamento?")) {
      try {
        await API.Delete(`Medicines/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting medicine:", error);
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">Gestión de Medicamentos</h2>
            <p className="text-muted">Administra el catálogo de medicamentos disponibles</p>
          </div>
          <button
            className="btn btn-primary"
            onClick={() => handleOpenModal()}
          >
            <i className="bi bi-plus-circle me-2"></i>
            Nuevo Medicamento
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
                      <th>Descripción</th>
                      <th>Formato</th>
                      <th>Estado</th>
                      <th>Prescripciones</th>
                      <th>Recetados</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {medicines.length === 0 ? (
                      <tr>
                        <td colSpan={8} className="text-center text-muted py-4">
                          No hay medicamentos registrados
                        </td>
                      </tr>
                    ) : (
                      medicines.map((medicine) => (
                        <tr key={medicine.id}>
                          <td>{medicine.id}</td>
                          <td className="fw-semibold">{medicine.name}</td>
                          <td>{medicine.description}</td>
                          <td>
                            <span className="badge bg-secondary">
                              {medicine.format}
                            </span>
                          </td>
                          <td>
                            <span className={`badge ${medicine.isActive ? 'bg-success' : 'bg-danger'}`}>
                              {medicine.isActive ? 'Activo' : 'Inactivo'}
                            </span>
                          </td>
                          <td>
                            <span className="badge bg-info">
                              {medicine.prescriptionMedicinesCount}
                            </span>
                          </td>
                          <td>
                            <span className="badge bg-warning text-dark">
                              {medicine.currentMedicamentsCount}
                            </span>
                          </td>
                          <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary me-2"
                              onClick={() => handleOpenModal(medicine)}
                              title="Editar"
                            >
                              <i className="bi bi-pencil"></i>
                            </button>
                            <button
                              className="btn btn-sm btn-outline-danger"
                              onClick={() => handleDelete(medicine.id)}
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
                    {isEditing ? "Editar Medicamento" : "Nuevo Medicamento"}
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
                        <i className="bi bi-capsule me-2"></i>
                        Nombre del Medicamento *
                      </label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Ej: Ibuprofen"
                        required
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-file-text me-2"></i>
                        Descripción
                      </label>
                      <textarea
                        name="description"
                        className="form-control"
                        rows={3}
                        value={formData.description}
                        onChange={handleInputChange}
                        placeholder="Ej: Antiinflamatorio no esteroideo para el dolor"
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-box2 me-2"></i>
                        Formato *
                      </label>
                      <input
                        type="text"
                        name="format"
                        className="form-control"
                        value={formData.format}
                        onChange={handleInputChange}
                        placeholder="Ej: Tableta 400mg, Inyección 1ml"
                        required
                      />
                    </div>
                    <div className="mb-3 form-check">
                      <input
                        type="checkbox"
                        name="isActive"
                        className="form-check-input"
                        id="isActiveCheck"
                        checked={formData.isActive}
                        onChange={handleInputChange}
                      />
                      <label className="form-check-label" htmlFor="isActiveCheck">
                        <i className="bi bi-check-circle me-2"></i>
                        Medicamento Activo
                      </label>
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
