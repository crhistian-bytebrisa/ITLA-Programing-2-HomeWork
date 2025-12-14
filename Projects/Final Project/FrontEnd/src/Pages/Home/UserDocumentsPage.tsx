import { useState, useEffect } from "react";
import { Navbar } from "../../Components/NavBar";
import { API } from "../../Services/APIService";
import { useAuth } from "../../Context/AutContext";
import { API_BASE_URL, AdaptResponse } from "../../Services/base";

export function UserDocumentsPage() {
  const [documents, setDocuments] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  useEffect(() => {
    const patientId = user?.patient?.id;
    console.log('User data:', user);
    console.log('Patient ID:', patientId);
    if (patientId) {
      fetchDocuments(patientId);
    }
  }, [user]);

  const fetchDocuments = async (patientId: number) => {
    try {
      setLoading(true);
      const res = await API.GetAll(`Patients/${patientId}/Documents`);
      console.log('Documents response:', res);
      setDocuments(res?.data?.items || res?.data || res || []);
    } catch (error) {
      console.error("Error fetching documents:", error);
    } finally {
      setLoading(false);
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
      const cd = response.headers.get('content-disposition');
      let filename = fallbackName || 'document';
      if (cd) {
        const match = cd.match(/filename\*=UTF-8''(.+)|filename="?([^";]+)"?/);
        if (match) {
          filename = decodeURIComponent(match[1] || match[2]);
        }
      }

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

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="mb-4">
          <h2 className="fw-bold text-primary">Documentos Médicos</h2>
          <p className="text-muted">Accede a tus documentos y resultados</p>
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
              {documents.length === 0 ? (
                <p className="text-muted text-center py-4">No tienes documentos registrados</p>
              ) : (
                <ul className="list-group list-group-flush">
                  {documents.map((d: any) => (
                    <li key={d.id} className="list-group-item d-flex justify-content-between align-items-center">
                      <div>
                        <div className="fw-semibold">{d.fileName}</div>
                        <small className="text-muted">{d.documentType}</small>
                      </div>
                      <div>
                        <button className="btn btn-sm btn-outline-primary" onClick={() => downloadDocument(d.id, d.fileName)} title="Descargar">
                          <i className="bi bi-download me-1"></i>
                          Descargar
                        </button>
                      </div>
                    </li>
                  ))}
                </ul>
              )}
            </div>
          </div>
        )}
      </div>
    </>
  );
}
