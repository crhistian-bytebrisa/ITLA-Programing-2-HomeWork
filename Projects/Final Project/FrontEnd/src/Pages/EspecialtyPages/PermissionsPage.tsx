import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { Permission, PermissionCreate, PermissionUpdate } from "../../Models/PermissionsModels";

export function PermissionsCRUD() {
  const [permissions, setPermissions] = useState<Permission[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentPermission, setCurrentPermission] = useState<PermissionUpdate | null>(null);
  const [loading, setLoading] = useState(false);

  const [formData, setFormData] = useState<PermissionCreate>({
    name: "",
    description: ""
  });

  useEffect(() => {
    fetchPermissions();
  }, []);

  const fetchPermissions = async () => {
    try {
      setLoading(true);
      const response = await API.GetAll("Permissions");
      setPermissions(response.data);
    } catch (error) {
      console.error("Error fetching permissions:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = (permission?: Permission) => {
    if (permission) {
      setIsEditing(true);
      setCurrentPermission({
        id: permission.id,
        name: permission.name,
        description: permission.description
      });
      setFormData({
        name: permission.name,
        description: permission.description
      });
    } else {
      setIsEditing(false);
      setCurrentPermission(null);
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
    setCurrentPermission(null);
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
      if (isEditing && currentPermission) {
        await API.Put(`Permissions/${currentPermission.id}`, {
          id: currentPermission.id,
          name: formData.name,
          description: formData.description
        });
        window.location.reload();
        
      } else {
        await API.Post("Permissions", formData);
        await fetchPermissions();
        handleCloseModal();
      }
    } catch (error) {
      console.error("Error saving permission:", error);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm("¿Estás seguro de eliminar este permiso?")) {
      try {
        await API.Delete(`Permissions/${id}`);
        window.location.reload();
      } catch (error) {
        console.error("Error deleting permission:", error);
      }
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <div>
            <h2 className="fw-bold text-primary">Gestión de Permisos Médicos</h2>
            <p className="text-muted">Administra los motivos clínicos para acceder a información médica</p>
          </div>
          <button
            className="btn btn-primary"
            onClick={() => handleOpenModal()}
          >
            <i className="bi bi-plus-circle me-2"></i>
            Nuevo Motivo Clínico
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
                      <th>Prescripciones Asociadas</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {permissions.length === 0 ? (
                      <tr>
                        <td colSpan={5} className="text-center text-muted py-4">
                          No hay permisos registrados
                        </td>
                      </tr>
                    ) : (
                      permissions.map((permission) => (
                        <tr key={permission.id}>
                          <td>{permission.id}</td>
                          <td className="fw-semibold">{permission.name}</td>
                          <td>{permission.description}</td>
                          <td>
                            <span className="badge bg-info">
                              {permission.prescriptionsCount}
                            </span>
                          </td>
                          <td className="text-end">
                            <button
                              className="btn btn-sm btn-outline-primary me-2"
                              onClick={() => handleOpenModal(permission)}
                              title="Editar"
                            >
                              <i className="bi bi-pencil"></i>
                            </button>
                            <button
                              className="btn btn-sm btn-outline-danger"
                              onClick={() => handleDelete(permission.id)}
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
                    {isEditing ? "Editar Motivo Clínico" : "Nuevo Motivo Clínico"}
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
                        <i className="bi bi-heart-pulse me-2"></i>
                        Motivo Clínico *
                      </label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Ej: Motivo Quirúrgico"
                        required
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">
                        <i className="bi bi-file-medical me-2"></i>
                        Descripción
                      </label>
                      <textarea
                        name="description"
                        className="form-control"
                        rows={3}
                        value={formData.description}
                        onChange={handleInputChange}
                        placeholder="Ej: Justificación clínica para permiso de ausencia"
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
