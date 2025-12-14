import { useState } from "react";
import { useAuth } from "../Context/AutContext";
import { useNavigate } from "react-router-dom";
import logo from "../Assets/MediAgenda.png";

export function Navbar() {
  const { user, roles, logout } = useAuth();
  const navigate = useNavigate();
  const [isOpen, setIsOpen] = useState(false);

  const handleLogout = async () => {
    await logout();
    navigate("/");
  };

  var home = "";
  if (roles.includes("Admin")) {
    home = "/admin";
  } else if (roles.includes("Doctor")) {
    home = "/doctor";
  } else if (roles.includes("User")) {
    home = "/user";
  } else {
    home = "/home";
  }

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-white shadow-sm px-3">
      <a
        className="navbar-brand d-flex align-items-center fw-bold text-primary"
        href={home}
      >
        <img
          src={logo}
          alt="MediAgenda icon"
          width="32"
          height="32"
          className="me-2"
        />
        MediAgenda
      </a>

      <div className="dropdown ms-auto me-3">
        <button
          className="btn btn-light border rounded-circle"
          type="button"
          onClick={() => setIsOpen(!isOpen)}
        >
          <i className="bi bi-person-circle fs-4"></i>
        </button>

        {isOpen && (
          <>
            <div
              className="dropdown ms-auto"
              style={{ zIndex: 10040 }}
              onClick={() => setIsOpen(false)}
            />
            <ul
            className="dropdown-menu shadow show position-absolute"
            style={{ zIndex: 1050, right: '0', left: 'auto', transform: 'translateX(-20px)' }}
            >
              <li>
                <button
                  className="dropdown-item d-flex align-items-center"
                  onClick={() => {
                    navigate("/profile");
                    setIsOpen(false);
                  }}
                >
                  <i className="bi bi-person me-2"></i>
                  Perfil
                </button>
              </li>

              <li>
                <hr className="dropdown-divider" />
              </li>

              <li>
                <button
                  className="dropdown-item d-flex align-items-center text-danger"
                  onClick={() => {
                    handleLogout();
                    setIsOpen(false);
                  }}
                >
                  <i className="bi bi-box-arrow-right me-2"></i>
                  Logout
                </button>
              </li>
            </ul>
          </>
        )}
      </div>
    </nav>
  );
}