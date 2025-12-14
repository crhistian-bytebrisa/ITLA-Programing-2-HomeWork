
import { MenuCard } from "../../Components/Menucard";
import { Navbar } from "../../Components/NavBar";

export function DoctorHome() {
  const menuItems = [
    {
      title: "Clínicas",
      icon: "bi-building",
      description: "Gestiona las clínicas del sistema",
      color: "primary",
      path: "/clinics"
    },
    {
      title: "Días Disponibles",
      icon: "bi-calendar-week",
      description: "Administra los días y horarios disponibles",
      color: "success",
      path: "/daysavailable"
    },
    {
      title: "Consultas del Día",
      icon: "bi-calendar-day",
      description: "Revisa las consultas programadas para hoy",
      color: "info",
      path: "/todayconsultations"
    },
    {
      title: "Consultas",
      icon: "bi-clipboard-check",
      description: "Gestiona todas las consultas médicas",
      color: "warning",
      path: "/consultations"
    },
    {
      title: "Medicinas",
      icon: "bi-prescription2",
      description: "Administra el catálogo de medicamentos",
      color: "danger",
      path: "/medicines"
    },
    {
      title: "Análisis",
      icon: "bi-file-medical",
      description: "Gestiona los tipos de análisis médicos",
      color: "secondary",
      path: "/analyses"
    },
    {
      title: "Pacientes",
      icon: "bi-people",
      description: "Administra la información de pacientes",
      color: "primary",
      path: "/patients"
    },
    {
      title: "Seguros",
      icon: "bi-shield-check",
      description: "Gestiona las aseguradoras médicas",
      color: "success",
      path: "/insurances"
    },
    {
      title: "Permisos",
      icon: "bi-heart-pulse",
      description: "Administra permisos y autorizaciones",
      color: "info",
      path: "/permissions"
    }
  ];

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="text-center mb-5">
          <h2 className="fw-bold text-primary">Panel del Doctor</h2>
          <p className="text-muted">Gestiona tus consultas y pacientes</p>
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
      <br />
      <br />
    </>
  );
}