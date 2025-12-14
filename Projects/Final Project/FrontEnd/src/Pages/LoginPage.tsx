import { useState } from "react";
import { LoginDTO } from "../Models/AuthModels";
import { API } from "../Services/APIService";
import { useAuth } from "../Context/AutContext";
import { useNavigate } from "react-router-dom";

export function LoginPage() {
  const [loginData, setLoginData] = useState<LoginDTO>({
    email: "",
    password: ""
  });

  const { login } = useAuth();
  const navigate = useNavigate();

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setLoginData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const response = await API.Post("Auth/Login", loginData);
      await login(response.user, response.roles);

      console.log(response);
      await new Promise(resolve => setTimeout(resolve, 100));

      if (response.roles.includes("Admin")) {
        navigate("/admin");
      } else if (response.roles.includes("Doctor")) {
        navigate("/doctor");
      } else if (response.roles.includes("User")) {
        navigate("/user");
      } else {
        navigate("/home");
      }

    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card shadow p-4" style={{ width: "350px" }}>
        <h4 className="text-center mb-4">Inicio de sesion</h4>

        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label">Email:</label>
            <input
              type="email"
              name="email"
              className="form-control"
              placeholder="Email"
              value={loginData.email}
              onChange={handleInputChange}
              required
            />
          </div>

          <div className="mb-3">
            <label className="form-label">Contraseña:</label>
            <input
              type="password"
              name="password"
              className="form-control"
              placeholder="Contraseña"
              value={loginData.password}
              onChange={handleInputChange}
              required
            />
          </div>
          
          <p><a href="/register">Registrarse</a></p>

          <button type="submit" className="btn btn-primary w-100">
            Login
          </button>
        </form>
      </div>
    </div>
  );
}
