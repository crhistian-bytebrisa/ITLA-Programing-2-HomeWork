import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Analysis, AnalysisCreate, AnalysisUpdate } from "../../Models/AnalysesModels";

export function AnalysesCRUD() {
  const [analyses, setAnalyses] = useState<Analysis[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentAnalysis, setCurrentAnalysis] = useState<AnalysisUpdate | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState<AnalysisCreate>({
    name: "",
    description: ""
  });

  useEffect(() => {
    fetchAnalyses();
  }, []);

  const fetchAnalyses = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Analyses");
      setAnalyses(response.data);
    } catch (error) {
      console.error("Error fetching analyses:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (analysis?: Analysis) => {
    if (analysis) {
      setIsEditing(true);
      setCurrentAnalysis({
        id: analysis.id,
        name: analysis.name,
        description: analysis.description
      });
      setFormData({
        name: analysis.name,
        description: analysis.description
      });
    } else {
      setIsEditing(false);
      setCurrentAnalysis(null);
      setFormData({
        name: "",
        description: ""
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setIsEditing(false);
    setCurrentAnalysis(null);
    setFormData({
      name: "",
      description: ""
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
      if (isEditing && currentAnalysis) {
        await API.Put(`Analyses/${currentAnalysis.id}`, {
          id: currentAnalysis.id,
          name: formData.name,
          description: formData.description
        });
        window.location.reload();
      } else {
        await API.Post("Analyses", formData);
        await fetchAnalyses();
        handleCloseModal();
      }
    } catch (error) {
      console.error("Error saving analysis:", error);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar este análisis?")) {
      try {
        await API.Delete(`Analyses/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting analysis:", error);
      }
    }
  };

  return (
    <>
      <Navbar />

      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">
              <i className="bi bi-flask me-2"></i>
              Gestión de Análisis
            </h2>
            <p className="text-muted">Administra los tipos de análisis médicos del sistema</p>
          </div>
          <button className="btn btn-primary" onClick={() => handleOpenModal()}>
            <i className="bi bi-plus-circle me-2"></i>
            Nuevo Análisis
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
                      <th>Prescripciones</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {analyses.length === 0 ? (
                      <tr>
                        <td colSpan={5} className="text-center text-muted py-4">
                          No hay análisis registrados
                        </td>
                      </tr>
                    ) : (
                      analyses.map((analysis) => (
                        <tr key={analysis.id}>
                          <td>{analysis.id}</td>
                          <td className="fw-semibold">
                            <i className="bi bi-clipboard2-pulse me-2 text-primary"></i>
                            {analysis.name}
                          </td>
                          <td>{analysis.description || "—"}</td>
                          <td>
                            <span className="badge bg-info">
                              {analysis.prescriptionAnalysesCount}
                            </span>
                          </td>
                          <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary me-2"
                              onClick={() => handleOpenModal(analysis)}
                              title="Editar"
                            >
                              <i className="bi bi-pencil"></i>
                            </button>
                            <button
                              className="btn btn-sm btn-outline-danger"
                              onClick={() => handleDelete(analysis.id)}
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
                    <i className="bi bi-clipboard2-pulse me-2"></i>
                    {isEditing ? "Editar Análisis" : "Nuevo Análisis"}
                  </h5>
                  <button type="button" className="btn-close" onClick={handleCloseModal}></button>
                </div>

                <form onSubmit={handleSubmit}>
                  <div className="modal-body">
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-tag me-2"></i>
                        Nombre *
                      </label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        value={formData.name}
                        onChange={handleInputChange}
                        required
                      />
                    </div>

                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-textarea-t me-2"></i>
                        Descripción
                      </label>
                      <textarea
                        name="description"
                        className="form-control"
                        rows={3}
                        value={formData.description}
                        onChange={handleInputChange}
                      />
                    </div>
                  </div>

                  <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" onClick={handleCloseModal}>
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
