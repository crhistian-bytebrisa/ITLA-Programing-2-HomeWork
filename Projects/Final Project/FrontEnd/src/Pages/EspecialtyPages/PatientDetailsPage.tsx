import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { API_BASE_URL, AdaptResponse } from "../../Services/base";
import { Patient } from "../../Models/PatientsModels";

export function PatientDetails() {
  const { id } = useParams();
  const patientId = Number(id || 0);

  const [patient, setPatient] = useState<Patient | null>(null);
  const [loading, setLoading] = useState(false);

  const [medicines, setMedicines] = useState<any[]>([]);
  const [consultations, setConsultations] = useState<any[]>([]);
  const [documents, setDocuments] = useState<any[]>([]);
  const [showDocumentModal, setShowDocumentModal] = useState(false);
  const [documentForm, setDocumentForm] = useState<{ file: File | null; fileName: string }>({ file: null, fileName: "" });

  // Notes (CRUD)
  const [notes, setNotes] = useState<any[]>([]);
  const [showNoteModal, setShowNoteModal] = useState(false);
  const [isEditingNote, setIsEditingNote] = useState(false);
  const [currentNote, setCurrentNote] = useState<any | null>(null);
  const [noteForm, setNoteForm] = useState({ title: "", content: "" });

  useEffect(() => {
    if (!patientId) return;
    fetchPatient();
    fetchMedicines();
    fetchConsultations();
    fetchDocuments();
    fetchNotes();
  }, [patientId]);

  const fetchPatient = async () => {
    try {
      setLoading(true);
      const res = await API.GetById("Patients", patientId);
      console.log("fetchPatient response:", res);
      // soporta tanto { data: patient } como patient directo
      setPatient(res?.data || res || null);
    } catch (error) {
      console.error("Error fetching patient:", error);
    } finally {
      setLoading(false);
    }
  };

  const fetchMedicines = async () => {
    try {
      const res = await API.GetAll(`Patients/${patientId}/Medicines`);
      console.log("fetchMedicines response:", res);
      setMedicines(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching patient medicines:", error);
    }
  };

  const fetchConsultations = async () => {
    try {
      const res = await API.GetAll(`Patients/${patientId}/Consultations`);
      console.log("fetchConsultations response:", res);
      setConsultations(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching patient consultations:", error);
    }
  };

  const fetchDocuments = async () => {
    try {
      const res = await API.GetAll(`Patients/${patientId}/Documents`);
      console.log("fetchDocuments response:", res);
      setDocuments(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching patient documents:", error);
    }
  };

  // Document handlers (upload/delete)
  const handleDocumentFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files && e.target.files[0] ? e.target.files[0] : null;
    setDocumentForm(prev => ({ ...prev, file }));
  };

  const handleDocumentNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDocumentForm(prev => ({ ...prev, fileName: e.target.value }));
  };

  const submitDocument = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!documentForm.file || !documentForm.fileName) {
      alert('Seleccione un archivo y nombre.');
      return;
    }

    try {
      const fd = new FormData();
      fd.append('File', documentForm.file);
      fd.append('PatientId', String(patientId));
      fd.append('FileName', documentForm.fileName);

      const response = await fetch(`${API_BASE_URL}/MedicalDocuments/Upload`, {
        method: 'POST',
        credentials: 'include',
        body: fd
      });
      const parsed = await AdaptResponse(response);
      if (response.ok) {
        await fetchDocuments();
        setShowDocumentModal(false);
        setDocumentForm({ file: null, fileName: '' });
      } else {
        alert('Error al subir documento: ' + (parsed?.message || response.status));
      }
    } catch (error) {
      console.error('Error uploading document', error);
      alert('Error al subir documento');
    }
  };

  const deleteDocument = async (docId: number) => {
    if (!window.confirm('¿Eliminar documento?')) return;
    try {
      const res = await API.Delete(`MedicalDocuments/${docId}`);
      fetchDocuments();
    } catch (error) {
      console.error('Error deleting document', error);
      alert('Error al eliminar documento');
    }
  };

  const downloadDocument = async (docId: number, fallbackName?: string) => {
    try {
      const response = await fetch(`${API_BASE_URL}/MedicalDocuments/${docId}/Download`, {
        method: 'GET',
        credentials: 'include'
      });
      if (!response.ok) {
        const parsed = await AdaptResponse(response);
        alert('Error al descargar documento: ' + (parsed?.message || response.status));
        return;
      }

      const blob = await response.blob();
      // Try to get filename from content-disposition header
      const cd = response.headers.get('content-disposition');
      let filename = fallbackName || 'document';
      if (cd) {
        const match = cd.match(/filename\*=UTF-8''(.+)|filename="?([^";]+)"?/);
        if (match) {
          filename = decodeURIComponent(match[1] || match[2]);
        }
      }

      // fallback: use provided fallbackName
      if (!filename && fallbackName) filename = fallbackName;

      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename || 'download';
      document.body.appendChild(a);
      a.click();
      a.remove();
      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error('Error downloading document', error);
      alert('Error al descargar documento');
    }
  };

  const fetchNotes = async () => {
    try {
      const res = await API.GetAll(`Patients/${patientId}/Notes`);
      console.log("fetchNotes response:", res);
      setNotes(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching patient notes:", error);
    }
  };

  // Notes CRUD handlers
  const openNoteModal = (note?: any) => {
    if (note) {
      setIsEditingNote(true);
      setCurrentNote(note);
      setNoteForm({ title: note.title || "", content: note.content || "" });
    } else {
      setIsEditingNote(false);
      setCurrentNote(null);
      setNoteForm({ title: "", content: "" });
    }
    setShowNoteModal(true);
  };

  const closeNoteModal = () => {
    setShowNoteModal(false);
    setIsEditingNote(false);
    setCurrentNote(null);
    setNoteForm({ title: "", content: "" });
  };

  const handleNoteChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setNoteForm(prev => ({ ...prev, [name]: value }));
  };

  const submitNote = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (isEditingNote && currentNote) {
        // Update note via /NotesPatients/{id}
        const res = await API.Put(`NotesPatients/${currentNote.id}`, { id: currentNote.id, title: noteForm.title, content: noteForm.content });
        fetchNotes();
      } else {
        // Create note via /NotesPatients with patientId in body
        const res = await API.Post(`NotesPatients`, { patientId: patientId, title: noteForm.title, content: noteForm.content });
        fetchNotes();
      }
    } catch (error) {
      console.error('Error note submit', error);
      alert('Error al guardar nota');
    } finally {
      closeNoteModal();
    }
  };

  const deleteNote = async (noteId: number) => {
  if (!window.confirm('¿Eliminar nota?')) return;
    try {
        await API.Delete(`NotesPatients/${noteId}`);
        fetchNotes();
    } catch (error) {
        console.error('Error deleting note', error);
        alert('Error al eliminar nota');
    }
    };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4 d-flex justify-content-between align-items-center">
          <div>
            <h2 className="fw-bold text-primary">Detalle de Paciente</h2>
            <p className="text-muted">Información y registros relacionados</p>
          </div>
        </div>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Cargando...</span>
            </div>
          </div>
        ) : (
          patient && (
            <>
              <div className="card mb-4">
                <div className="card-body">
                  <div className="row">
                    <div className="col-md-4">
                      <p><strong>ID:</strong> {patient.id}</p>
                      <p><strong>Nombre:</strong> {patient.user?.nameComplete || '-'}</p>
                    </div>
                    <div className="col-md-4">
                      <p><strong>Edad:</strong> {patient.age}</p>
                      <p><strong>Género:</strong> {patient.gender}</p>
                    </div>
                    <div className="col-md-4">
                      <p><strong>Tipo de Sangre:</strong> {patient.bloodType}</p>
                      <p><strong>Identificación:</strong> {patient.identification}</p>
                    </div>
                  </div>
                </div>
              </div>

              <div className="mb-4">
                <div className="card mb-4">
                  <div className="card-header d-flex justify-content-between align-items-center">
                    <strong>Medicamentos (historial)</strong>
                  </div>
                  <div className="card-body">
                    {medicines.length === 0 ? (
                      <p className="text-muted">No hay medicamentos registrados</p>
                    ) : (
                      <ul className="list-group list-group-flush">
                        {medicines.map((m: any) => (
                          <li key={m.id} className="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                              <div className="fw-semibold">{m.name || m.medicineName}</div>
                              <small className="text-muted">{m.format || m.medicineFormat}</small>
                            </div>
                            <div>
                              <small className="text-muted">{m.startDosage ? `${m.startDosage} → ${m.endDosage || '-'} ` : ''}</small>
                            </div>
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>

                <div className="card mb-4">
                  <div className="card-header d-flex justify-content-between align-items-center">
                    <strong>Consultas</strong>
                  </div>
                  <div className="card-body">
                    {consultations.length === 0 ? (
                      <p className="text-muted">No hay consultas registradas</p>
                    ) : (
                      <ul className="list-group list-group-flush">
                        {consultations.map((c: any) => (
                          <li key={c.id} className="list-group-item">
                            <div className="fw-semibold">{c.reason?.title || c.reasonId}</div>
                            <div className="small text-muted">{c.dayAvailable?.date || c.state}</div>
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>

                <div className="card mb-4">
                  <div className="card-header d-flex justify-content-between align-items-center">
                    <strong>Documentos Médicos</strong>
                    <div>
                        
                        <button className="btn btn-sm btn-primary me-2" onClick={() => { setShowDocumentModal(true); }}>
                            <i className="bi bi-plus-circle me-2"></i>
                            Nuevo Documento
                        </button>
                    </div>
                  </div>
                  <div className="card-body">
                    {documents.length === 0 ? (
                      <p className="text-muted">No hay documentos</p>
                    ) : (
                      <ul className="list-group list-group-flush">
                        {documents.map((d: any) => (
                          <li key={d.id} className="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                              <div className="fw-semibold">{d.fileName}</div>
                              <small className="text-muted">{d.documentType}</small>
                            </div>
                            <div className="btn-group">
                              <button className="btn btn-sm btn-outline-primary" onClick={() => downloadDocument(d.id, d.fileName)} title="Descargar">
                                <i className="bi bi-download me-1"></i>
                                Descargar
                              </button>
                              <button className="btn btn-sm btn-outline-danger" onClick={() => deleteDocument(d.id)} title="Eliminar">
                                <i className="bi bi-trash me-1"></i>
                                Eliminar
                              </button>
                            </div>
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>
              </div>

              <div className="card mb-4">
                <div className="card-header d-flex justify-content-between align-items-center">
                  <strong>Notas del paciente</strong>
                    <button className="btn btn-sm btn-primary" onClick={() => openNoteModal()}>
                        <i className="bi bi-plus-circle me-2"></i>
                        Nueva Nota
                    </button>
                </div>
                <div className="card-body">
                  {notes.length === 0 ? (
                    <p className="text-muted">No hay notas</p>
                  ) : (
                    <ul className="list-group list-group-flush">
                      {notes.map((n: any) => (
                        <li key={n.id} className="list-group-item d-flex justify-content-between align-items-start">
                          <div className="ms-2 me-auto">
                            <div className="fw-semibold">{n.title}</div>
                            <div className="small text-muted">{n.content}</div>
                          </div>
                          <div className="btn-group">
                            <button className="btn btn-sm btn-outline-primary" onClick={() => openNoteModal(n)} title="Editar"><i className="bi bi-pencil"></i></button>
                            <button className="btn btn-sm btn-outline-danger" onClick={() => deleteNote(n.id)} title="Eliminar"><i className="bi bi-trash"></i></button>
                          </div>
                        </li>
                      ))}
                    </ul>
                  )}
                </div>
              </div>

              {/* Note Modal */}
              {showNoteModal && (
                <div className="modal fade show d-block" tabIndex={-1} style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                  <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                      <div className="modal-header">
                        <h5 className="modal-title">
                          <i className="bi bi-journal-text me-2" />
                          {isEditingNote ? 'Editar Nota' : 'Nueva Nota'}
                        </h5>
                        <button type="button" className="btn-close" onClick={closeNoteModal}></button>
                      </div>
                      <form onSubmit={submitNote}>
                        <div className="modal-body">
                          <div className="mb-3">
                            <label className="form-label">
                              <i className="bi bi-card-text me-2" />
                              Título
                            </label>
                            <input name="title" value={noteForm.title} onChange={handleNoteChange} className="form-control" required />
                          </div>
                          <div className="mb-3">
                            <label className="form-label">
                              <i className="bi bi-file-text me-2" />
                              Contenido
                            </label>
                            <textarea name="content" value={noteForm.content} onChange={handleNoteChange} className="form-control" rows={4} required />
                          </div>
                        </div>
                        <div className="modal-footer">
                          <button type="button" className="btn btn-secondary" onClick={closeNoteModal}>Cancelar</button>
                          <button type="submit" className="btn btn-primary">
                            <i className="bi bi-save me-2" />
                            {isEditingNote ? 'Actualizar' : 'Guardar'}
                          </button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              )}
              {/* Document Modal */}
              {showDocumentModal && (
                <div className="modal fade show d-block" tabIndex={-1} style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                  <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                      <div className="modal-header">
                        <h5 className="modal-title">
                          <i className="bi bi-file-earmark-arrow-up me-2" />
                          Nuevo Documento
                        </h5>
                        <button type="button" className="btn-close" onClick={() => setShowDocumentModal(false)}></button>
                      </div>
                      <form onSubmit={submitDocument}>
                        <div className="modal-body">
                          <div className="mb-3">
                            <label className="form-label">
                              <i className="bi bi-file-earmark me-2" />
                              Archivo
                            </label>
                            <input type="file" className="form-control" onChange={handleDocumentFileChange} required />
                          </div>
                          <div className="mb-3">
                            <label className="form-label">
                              <i className="bi bi-pencil-square me-2" />
                              Nombre del archivo (sin extensión)
                            </label>
                            <input type="text" className="form-control" value={documentForm.fileName} onChange={handleDocumentNameChange} required />
                          </div>
                        </div>
                        <div className="modal-footer">
                          <button type="button" className="btn btn-secondary" onClick={() => setShowDocumentModal(false)}>Cancelar</button>
                          <button type="submit" className="btn btn-primary">
                            <i className="bi bi-upload me-2" />
                            Subir
                          </button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              )}
            </>
          )
        )}
      </div>
    </>
  );
}
