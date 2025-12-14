import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";

export function AttendConsultationPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const consultationId = Number(id || 0);

  const [consultation, setConsultation] = useState<any>(null);
  const [prescription, setPrescription] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  const [medicines, setMedicines] = useState<any[]>([]);
  const [analyses, setAnalyses] = useState<any[]>([]);
  const [permissions, setPermissions] = useState<any[]>([]);

  const [prescriptionMedicines, setPrescriptionMedicines] = useState<any[]>([]);
  const [prescriptionAnalyses, setPrescriptionAnalyses] = useState<any[]>([]);
  const [prescriptionPermissions, setPrescriptionPermissions] = useState<any[]>([]);

  const [showMedicineModal, setShowMedicineModal] = useState(false);
  const [showAnalysisModal, setShowAnalysisModal] = useState(false);
  const [showPermissionModal, setShowPermissionModal] = useState(false);

  const [medicineForm, setMedicineForm] = useState({
    medicineId: 0,
    instructions: "",
    startDosage: "",
    endDosage: ""
  });

  const [analysisForm, setAnalysisForm] = useState({
    analysisId: 0,
    recomendations: ""
  });

  const [permissionForm, setPermissionForm] = useState({
    permissionId: 0
  });

  useEffect(() => {
    if (!consultationId) return;
    fetchConsultation();
    fetchCatalogs();
  }, [consultationId]);

  const fetchConsultation = async () => {
    try {
      setLoading(true);
      const res = await API.GetById("Consultations", consultationId);
      const consultData = res?.data || res;
      
      if (!consultData) {
        alert("No se pudo cargar la consulta");
        navigate("/doctor");
        return;
      }
      
      setConsultation(consultData);

      if (consultData.prescription && consultData.prescription.id) {
        setPrescription(consultData.prescription);
        await fetchPrescriptionItems(consultData.prescription.id);
      } else {
        await createPrescription();
      }
    } catch (error) {
      alert("Error al cargar la consulta");
    } finally {
      setLoading(false);
    }
  };

  const createPrescription = async () => {
    try {
      const payload = {
        consultationId: consultationId,
        generalRecomendations: "Prescripción médica"
      };
      
      const res = await API.Post("Prescriptions", payload);
      const prescData = res?.data || res;
      
      if (!prescData || !prescData.id) {
        alert("Error al crear la prescripción. Por favor recargue la página.");
        return;
      }
      
      setPrescription(prescData);
      await fetchPrescriptionItems(prescData.id);
    } catch (error) {
      alert("Error al crear la prescripción. Verifique que la consulta no tenga ya una prescripción asociada.");
    }
  };

  const fetchPrescription = async (prescriptionId: number) => {
    try {
      const res = await API.GetById("Prescriptions", prescriptionId);
      const prescData = res?.data || res;
      
      setPrescription(prescData);
      await fetchPrescriptionItems(prescriptionId);
    } catch (error) {
      alert("Error al cargar la prescripción");
    }
  };

  const fetchPrescriptionItems = async (prescriptionId: number) => {
    try {
      const [meds, anals, perms] = await Promise.all([
        API.GetAll(`Prescriptions/${prescriptionId}/Medicines`),
        API.GetAll(`Prescriptions/${prescriptionId}/Analysis`),
        API.GetAll(`Prescriptions/${prescriptionId}/Permissions`)
      ]);
      console.log(meds)
      setPrescriptionMedicines(meds?.data || meds || []);
      setPrescriptionAnalyses(anals?.data || anals || []);
      setPrescriptionPermissions(perms?.data || perms || []);
    } catch (error) {
      alert("Error al cargar items de la prescripción");
    }
  };

  const fetchCatalogs = async () => {
    try {
      const [medsRes, analsRes, permsRes] = await Promise.all([
        API.GetAll("Medicines"),
        API.GetAll("Analyses"),
        API.GetAll("Permissions")
      ]);
      
      setMedicines(medsRes?.data || medsRes || []);
      setAnalyses(analsRes?.data || analsRes || []);
      setPermissions(permsRes?.data || permsRes || []);
    } catch (error) {
      alert("Error al cargar catálogos");
    }
  };

  const handleAddMedicine = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!prescription?.id) {
      alert("No hay prescripción disponible");
      return;
    }

    if (!medicineForm.medicineId || medicineForm.medicineId === 0) {
      alert("Por favor seleccione un medicamento");
      return;
    }

    const medicineExists = prescriptionMedicines.some(
      (pm: any) => pm.medicineId === medicineForm.medicineId
    );

    if (medicineExists) {
      alert("Este medicamento ya está agregado en la prescripción. Por favor seleccione otro o elimine el existente primero.");
      return;
    }

    try {
      const payload = {
        prescriptionId: prescription.id,
        medicineId: medicineForm.medicineId,
        instructions: medicineForm.instructions,
        startDosage: medicineForm.startDosage,
        endDosage: medicineForm.endDosage
      };
      
      await API.Post(`Prescriptions/${prescription.id}/Medicines`, payload);
      await fetchPrescriptionItems(prescription.id);
      setShowMedicineModal(false);
      setMedicineForm({ medicineId: 0, instructions: "", startDosage: "", endDosage: "" });
      alert("Medicamento agregado exitosamente");
    } catch (error) {
      alert("Error al agregar medicamento. Verifique los datos e intente nuevamente.");
    }
  };

  const handleDeleteMedicine = async (medicineId: number) => {
    if (!prescription?.id || !window.confirm("¿Eliminar medicamento?")) return;
    try {
      await API.Delete(`Prescriptions/${prescription.id}/Medicines/${medicineId}`);
      await fetchPrescriptionItems(prescription.id);
    } catch (error) {
      alert("Error al eliminar medicamento");
    }
  };

  const handleAddAnalysis = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!prescription?.id) {
      alert("No hay prescripción disponible");
      return;
    }

    if (!analysisForm.analysisId || analysisForm.analysisId === 0) {
      alert("Por favor seleccione un análisis");
      return;
    }

    const analysisExists = prescriptionAnalyses.some(
      (pa: any) => pa.analysisId === analysisForm.analysisId
    );

    if (analysisExists) {
      alert("Este análisis ya está agregado en la prescripción. Por favor seleccione otro o elimine el existente primero.");
      return;
    }

    try {
      const payload = {
        prescriptionId: prescription.id,
        analysisId: analysisForm.analysisId,
        recomendations: analysisForm.recomendations
      };
      
      await API.Post(`Prescriptions/${prescription.id}/Analysis`, payload);
      await fetchPrescriptionItems(prescription.id);
      setShowAnalysisModal(false);
      setAnalysisForm({ analysisId: 0, recomendations: "" });
      alert("Análisis agregado exitosamente");
    } catch (error) {
      alert("Error al agregar análisis. Verifique los datos e intente nuevamente.");
    }
  };

  const handleDeleteAnalysis = async (analysisId: number) => {
    if (!prescription?.id || !window.confirm("¿Eliminar análisis?")) return;
    try {
      await API.Delete(`Prescriptions/${prescription.id}/Analysis/${analysisId}`);
      await fetchPrescriptionItems(prescription.id);
    } catch (error) {
      alert("Error al eliminar análisis");
    }
  };

  const handleAddPermission = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!prescription?.id) {
      alert("No hay prescripción disponible");
      return;
    }

    if (!permissionForm.permissionId || permissionForm.permissionId === 0) {
      alert("Por favor seleccione un permiso");
      return;
    }

    const permissionExists = prescriptionPermissions.some(
      (pp: any) => pp.permissionId === permissionForm.permissionId
    );

    if (permissionExists) {
      alert("Este permiso ya está agregado en la prescripción. Por favor seleccione otro o elimine el existente primero.");
      return;
    }

    try {
      const payload = {
        prescriptionId: prescription.id,
        permissionId: permissionForm.permissionId
      };
      
      await API.Post(`Prescriptions/${prescription.id}/Permissions`, payload);
      await fetchPrescriptionItems(prescription.id);
      setShowPermissionModal(false);
      setPermissionForm({ permissionId: 0 });
      alert("Permiso agregado exitosamente");
    } catch (error) {
      alert("Error al agregar permiso. Verifique los datos e intente nuevamente.");
    }
  };

  const handleDeletePermission = async (permissionId: number) => {
    if (!prescription?.id || !window.confirm("¿Eliminar permiso?")) return;
    try {
      await API.Delete(`Prescriptions/${prescription.id}/Permissions/${permissionId}`);
      await fetchPrescriptionItems(prescription.id);
    } catch (error) {
      alert("Error al eliminar permiso");
    }
  };

  const handleCompleteConsultation = async () => {
    if (!window.confirm("¿Finalizar consulta? Esto cambiará el estado a Completada.")) return;

    try {
      await API.Put(`Consultations/${consultationId}`, {
        id: consultationId,
        patientId: consultation.patientId,
        reasonId: consultation.reasonId,
        dayAvailableId: consultation.dayAvailableId,
        state: "Completed"
      });
      alert("Consulta completada exitosamente");
      navigate("/doctor");
    } catch (error) {
      alert("Error al completar la consulta");
    }
  };

  if (loading) {
    return (
      <>
        <Navbar />
        <div className="container mt-5 text-center">
          <div className="spinner-border text-primary" role="status">
            <span className="visually-hidden">Cargando...</span>
          </div>
        </div>
      </>
    );
  }

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4 d-flex justify-content-between align-items-center">
          <div>
            <h2 className="fw-bold text-primary">
              <i className="bi bi-stethoscope me-2"></i>
              Atención de Consulta
            </h2>
            {consultation && (
              <p className="text-muted mb-0">
                <strong>Paciente:</strong> {consultation.patient?.user?.nameComplete || consultation.patient?.fullName || '-'}
              </p>
            )}
          </div>
          <button
            className="btn btn-success"
            onClick={handleCompleteConsultation}
          >
            <i className="bi bi-check-circle me-2"></i>
            Finalizar Consulta
          </button>
        </div>

        <div className="card mb-4">
          <div className="card-header d-flex justify-content-between align-items-center bg-primary text-white">
            <h5 className="mb-0">
              <i className="bi bi-capsule me-2"></i>
              Medicamentos Recetados
            </h5>
            <button
              className="btn btn-light btn-sm"
              onClick={() => setShowMedicineModal(true)}
            >
              <i className="bi bi-plus-circle me-1"></i>
              Agregar
            </button>
          </div>
          <div className="card-body">
            {prescriptionMedicines.length === 0 ? (
              <p className="text-muted">No se han agregado medicamentos</p>
            ) : (
              <div className="list-group list-group-flush">
                {prescriptionMedicines.map((pm: any) => (
                  <div key={pm.medicineId} className="list-group-item">
                    <div className="d-flex justify-content-between align-items-start">
                      <div className="flex-grow-1">
                        <h6 className="mb-1 fw-bold">
                          {pm.name || pm.medicineName}
                          <span className="badge bg-secondary ms-2">{pm.format || pm.medicineFormat}</span>
                        </h6>
                      </div>
                      <button
                        className="btn btn-sm btn-outline-danger ms-3"
                        onClick={() => handleDeleteMedicine(pm.medicineId)}
                        title="Eliminar"
                      >
                        <i className="bi bi-trash"></i>
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>

        <div className="card mb-4">
          <div className="card-header d-flex justify-content-between align-items-center bg-success text-white">
            <h5 className="mb-0">
              <i className="bi bi-clipboard2-pulse me-2"></i>
              Análisis Médicos
            </h5>
            <button
              className="btn btn-light btn-sm"
              onClick={() => setShowAnalysisModal(true)}
            >
              <i className="bi bi-plus-circle me-1"></i>
              Agregar
            </button>
          </div>
          <div className="card-body">
            {prescriptionAnalyses.length === 0 ? (
              <p className="text-muted">No se han solicitado análisis</p>
            ) : (
              <ul className="list-group list-group-flush">
                {prescriptionAnalyses.map((pa: any) => (
                  <li key={pa.analysisId} className="list-group-item d-flex justify-content-between align-items-start">
                    <div>
                      <div className="fw-bold">{pa.analysis?.name || pa.analysisName}</div>
                      <div className="mt-2 text-muted">
                        <strong>Recomendaciones:</strong> {pa.recomendations}
                      </div>
                    </div>
                    <button
                      className="btn btn-sm btn-outline-danger"
                      onClick={() => handleDeleteAnalysis(pa.analysisId)}
                    >
                      <i className="bi bi-trash"></i>
                    </button>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>

        <div className="card mb-4">
          <div className="card-header d-flex justify-content-between align-items-center bg-warning text-dark">
            <h5 className="mb-0">
              <i className="bi bi-file-earmark-medical me-2"></i>
              Permisos Médicos
            </h5>
            <button
              className="btn btn-dark btn-sm"
              onClick={() => setShowPermissionModal(true)}
            >
              <i className="bi bi-plus-circle me-1"></i>
              Agregar
            </button>
          </div>
          <div className="card-body">
            {prescriptionPermissions.length === 0 ? (
              <p className="text-muted">No se han otorgado permisos</p>
            ) : (
              <ul className="list-group list-group-flush">
                {prescriptionPermissions.map((pp: any) => (
                  <li key={pp.permissionId} className="list-group-item d-flex justify-content-between align-items-center">
                    <div>
                      <div className="fw-bold">{pp.permission?.name || pp.permissionName}</div>
                      <small className="text-muted">{pp.permission?.description}</small>
                    </div>
                    <button
                      className="btn btn-sm btn-outline-danger"
                      onClick={() => handleDeletePermission(pp.permissionId)}
                    >
                      <i className="bi bi-trash"></i>
                    </button>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>
      </div>

      {showMedicineModal && (
        <div 
          className="modal fade show d-block" 
          tabIndex={-1} 
          style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}
          onClick={(e) => {
            if (e.target === e.currentTarget) {
              setShowMedicineModal(false);
            }
          }}
        >
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">
                  <i className="bi bi-capsule me-2"></i>
                  Agregar Medicamento
                </h5>
                <button 
                  type="button" 
                  className="btn-close" 
                  onClick={() => setShowMedicineModal(false)}
                ></button>
              </div>
              <form onSubmit={handleAddMedicine}>
                <div className="modal-body">
                  <div className="mb-3">
                    <label className="form-label">Medicamento *</label>
                    <select
                      className="form-select"
                      value={medicineForm.medicineId}
                      onChange={(e) => setMedicineForm({...medicineForm, medicineId: Number(e.target.value)})}
                      required
                    >
                      <option value={0}>-- Seleccione --</option>
                      {medicines.map((m: any) => (
                        <option key={m.id} value={m.id}>{m.name} - {m.format}</option>
                      ))}
                    </select>
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Fecha Inicio *</label>
                    <input
                      type="date"
                      className="form-control"
                      value={medicineForm.startDosage}
                      onChange={(e) => setMedicineForm({...medicineForm, startDosage: e.target.value})}
                      required
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Fecha Fin *</label>
                    <input
                      type="date"
                      className="form-control"
                      value={medicineForm.endDosage}
                      onChange={(e) => setMedicineForm({...medicineForm, endDosage: e.target.value})}
                      required
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Instrucciones *</label>
                    <textarea
                      className="form-control"
                      rows={3}
                      value={medicineForm.instructions}
                      onChange={(e) => setMedicineForm({...medicineForm, instructions: e.target.value})}
                      required
                    />
                  </div>
                </div>
                <div className="modal-footer">
                  <button 
                    type="button" 
                    className="btn btn-secondary" 
                    onClick={() => setShowMedicineModal(false)}
                  >
                    Cancelar
                  </button>
                  <button type="submit" className="btn btn-primary">Agregar</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {showAnalysisModal && (
        <div 
          className="modal fade show d-block" 
          tabIndex={-1} 
          style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}
          onClick={(e) => {
            if (e.target === e.currentTarget) {
              setShowAnalysisModal(false);
            }
          }}
        >
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">
                  <i className="bi bi-clipboard2-pulse me-2"></i>
                  Solicitar Análisis
                </h5>
                <button 
                  type="button" 
                  className="btn-close" 
                  onClick={() => setShowAnalysisModal(false)}
                ></button>
              </div>
              <form onSubmit={handleAddAnalysis}>
                <div className="modal-body">
                  <div className="mb-3">
                    <label className="form-label">Análisis *</label>
                    <select
                      className="form-select"
                      value={analysisForm.analysisId}
                      onChange={(e) => setAnalysisForm({...analysisForm, analysisId: Number(e.target.value)})}
                      required
                    >
                      <option value={0}>-- Seleccione --</option>
                      {analyses.map((a: any) => (
                        <option key={a.id} value={a.id}>{a.name}</option>
                      ))}
                    </select>
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Recomendaciones *</label>
                    <textarea
                      className="form-control"
                      rows={3}
                      value={analysisForm.recomendations}
                      onChange={(e) => setAnalysisForm({...analysisForm, recomendations: e.target.value})}
                      required
                    />
                  </div>
                </div>
                <div className="modal-footer">
                  <button 
                    type="button" 
                    className="btn btn-secondary" 
                    onClick={() => setShowAnalysisModal(false)}
                  >
                    Cancelar
                  </button>
                  <button type="submit" className="btn btn-success">Agregar</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {showPermissionModal && (
        <div 
          className="modal fade show d-block" 
          tabIndex={-1} 
          style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}
          onClick={(e) => {
            if (e.target === e.currentTarget) {
              setShowPermissionModal(false);
            }
          }}
        >
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">
                  <i className="bi bi-file-earmark-medical me-2"></i>
                  Otorgar Permiso
                </h5>
                <button 
                  type="button" 
                  className="btn-close" 
                  onClick={() => setShowPermissionModal(false)}
                ></button>
              </div>
              <form onSubmit={handleAddPermission}>
                <div className="modal-body">
                  <div className="mb-3">
                    <label className="form-label">Tipo de Permiso *</label>
                    <select
                      className="form-select"
                      value={permissionForm.permissionId}
                      onChange={(e) => setPermissionForm({permissionId: Number(e.target.value)})}
                      required
                    >
                      <option value={0}>-- Seleccione --</option>
                      {permissions.map((p: any) => (
                        <option key={p.id} value={p.id}>{p.name}</option>
                      ))}
                    </select>
                  </div>
                </div>
                <div className="modal-footer">
                  <button 
                    type="button" 
                    className="btn btn-secondary" 
                    onClick={() => setShowPermissionModal(false)}
                  >
                    Cancelar
                  </button>
                  <button type="submit" className="btn btn-warning">Agregar</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  );
}