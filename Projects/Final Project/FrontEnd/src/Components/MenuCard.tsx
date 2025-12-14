import { useNavigate } from "react-router-dom";

interface MenuCardProps {
  title: string;
  icon: string;
  description: string;
  color: string;
  path: string;
}

export function MenuCard({ title, icon, description, color, path }: MenuCardProps) {
  const navigate = useNavigate();

  return (
    <div
      className="card h-100 shadow-sm border-0 hover-card"
      style={{ cursor: "pointer", transition: "transform 0.2s" }}
      onClick={() => navigate(path)}
      onMouseEnter={(e) => e.currentTarget.style.transform = "translateY(-5px)"}
      onMouseLeave={(e) => e.currentTarget.style.transform = "translateY(0)"}
    >
      <div className="card-body text-center p-4">
        <div className="mb-3">
          <i className={`${icon} text-${color}`} style={{ fontSize: "3rem" }}></i>
        </div>
        <h5 className="card-title fw-bold mb-2">{title}</h5>
        <p className="card-text text-muted small">{description}</p>
      </div>
      <div className={`card-footer bg-${color} bg-opacity-10 border-0 text-center py-2`}>
        <small className={`text-${color} fw-semibold`}>
          Ir <i className="bi bi-arrow-right"></i>
        </small>
      </div>
    </div>
  );
}