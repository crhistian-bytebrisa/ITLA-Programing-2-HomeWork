import { Navbar } from "../Components/NavBar";
import { useAuth } from "../Context/AutContext";

export function ProfilePage() {
  const { user } = useAuth();

  if (!user) {
    return (
      <div className="container mt-5">
        <h3 className="text-center text-muted">
          <i className="bi bi-person-x fs-3 me-2"></i>
          No hay usuario autenticado.
        </h3>
      </div>
    );
  }

  return (
    <>
      <Navbar />

      <div className="container mt-5" style={{ maxWidth: "650px" }}>
        <div className="card shadow-lg border-0">
          <div className="card-body p-4">

            {/* Encabezado */}
            <div className="d-flex align-items-center mb-4">
              <i className="bi bi-person-circle text-primary fs-1 me-3"></i>
              <div>
                <h3 className="fw-bold m-0">Perfil del Usuario</h3>
                <p className="text-muted m-0">{user.email}</p>
              </div>
            </div>

            {/* Datos generales */}
            <h5 className="fw-bold text-primary">
              <i className="bi bi-info-circle me-2"></i>
              Información General
            </h5>

            <div className="ms-3">
              <p><strong>Nombre completo:</strong> {user.nameComplete}</p>
              <p><strong>Teléfono:</strong> {user.phoneNumber}</p>
            </div>

            <hr />

            {/* Datos de paciente */}
            {user.patient && (
              <>
                <h5 className="fw-bold text-success">
                  <i className="bi bi-heart-pulse me-2"></i>
                  Datos de Paciente
                </h5>

                <div className="ms-3">
                  <p><strong>Seguro:</strong> {user.patient.insurance?.name || user.patient.insuranceId}</p>
                  <p><strong>Identificación:</strong> {user.patient.identification}</p>
                  <p><strong>Fecha de nacimiento:</strong> {user.patient.dateOfBirth}</p>
                  <p><strong>Edad:</strong> {user.patient.age}</p>
                  <p><strong>Tipo de sangre:</strong> {user.patient.bloodType}</p>
                  <p><strong>Género:</strong> {user.patient.gender}</p>
                </div>

                <hr />
              </>
            )}

            {/* Datos de doctor */}
            {user.doctor && (
              <>
                <h5 className="fw-bold text-warning">
                  <i className="bi bi-stethoscope me-2"></i>
                  Datos de Doctor
                </h5>

                <div className="ms-3">
                  <p><strong>Especialidad:</strong> {user.doctor.specialty}</p>
                  <p><strong>Sobre mí:</strong> {user.doctor.aboutMe}</p>
                </div>

                <hr />
              </>
            )}

            {/* Admin */}
            {!user.patient && !user.doctor && (
              <>
                <h5 className="fw-bold text-danger">
                  <i className="bi bi-shield-lock me-2"></i>
                  Administrador
                </h5>
                <div className="ms-3">
                  <p>Acceso completo al sistema.</p>
                </div>

                <hr />
              </>
            )}

          </div>
        </div>
      </div>
    </>
  );
}
