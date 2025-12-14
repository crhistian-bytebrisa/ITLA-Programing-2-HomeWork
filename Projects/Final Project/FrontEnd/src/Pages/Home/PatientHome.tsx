import { MenuCard } from "../../Components/Menucard";
import { Navbar } from "../../Components/NavBar";

export function PatientHome() {
  const menuItems = [
    {
      title: "Historial de Consultas",
      icon: "bi-clipboard-pulse",
      description: "Revisa tus consultas médicas anteriores",
      color: "primary",
      path: "/user/consultations"
    },
    {
      title: "Historial de Medicamentos",
      icon: "bi-capsule",
      description: "Consulta tus medicamentos recetados",
      color: "success",
      path: "/user/medications"
    },
    {
      title: "Documentos Médicos",
      icon: "bi-file-earmark-medical",
      description: "Accede a tus documentos y resultados",
      color: "info",
      path: "/user/documents"
    },
    {
      title: "Agendar Citas",
      icon: "bi-calendar-plus",
      description: "Agenda una nueva cita médica",
      color: "warning",
      path: "/user/schedules"
    },
    {
      title: "Citas Pendientes",
      icon: "bi-calendar-check",
      description: "Revisa tus próximas citas",
      color: "danger",
      path: "/user/appointments"
    }
  ];

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="text-center mb-5">
          <h2 className="fw-bold text-primary">Portal del Paciente</h2>
          <p className="text-muted">Gestiona tu información médica de forma rápida y segura</p>
        </div>

        <div className="row g-4">
          {menuItems.map((item, index) => (
            <div key={index} className="col-md-6 col-lg-4">
              <MenuCard
                title={item.title}
                icon={item.icon}
                description={item.description}
                color={item.color}
                path={item.path}
              />
            </div>
          ))}
        </div>
      </div>
    </>
  );
}