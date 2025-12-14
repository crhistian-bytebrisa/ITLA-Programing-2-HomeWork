import { useEffect, useState } from "react";
import { RegisterDTO } from "../Models/AuthModels";
import {BloodType, Gender} from "../Models/Enums"
import { Insurance } from "../Models/InsurancesModels"; // Ajusta la ruta si es distinta
import { API } from "../Services/APIService";
import { useNavigate } from "react-router-dom";

export function RegisterPage() {
  const navigate = useNavigate();

  const [insurances, setInsurances] = useState<Insurance[]>([]);

  const [registerData, setRegisterData] = useState<RegisterDTO>({
    email: "",
    password: "",
    nameComplete: "",
    phoneNumber: "",
    insuranceId: 0,
    identification: "",
    dateOfBirth: "",
    bloodTypeDTO: BloodType.O_Positive,
    genderDTO: Gender.Male,
  });

  // Obtener seguros al cargar
  useEffect(() => {
    const fetchInsurances = async () => {
      try {
        const response = await API.GetAll("Insurances");
        console.log(response);
        setInsurances(response.data);
      } catch (error) {
        console.error("Error obteniendo insurances:", error);
      }
    };

    fetchInsurances();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    setRegisterData((prev) => ({
      ...prev,
      [name]: name === "insuranceId" ? Number(value) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      console.log("Registering:", registerData);

      const response = await API.Post("Auth/Register", registerData);

      console.log("Registration response:", response);

       if (response.roles.includes("Admin")) {
        navigate("/admin");
      } else if (response.roles.includes("Doctor")) {
        navigate("/doctor");
      } else if (response.roles.includes("User")) {
        navigate("/patient");
      } else {
        navigate("/home");
      }
    } catch (error) {
      console.error("Error registering:", error);
    }
  };

  return (
  <div className="d-flex justify-content-center align-items-start py-5 bg-light" style={{ minHeight: "100vh" }}>
    <div className="card shadow p-4" style={{ width: "80%" }}>
      
      <h3 className="text-center mb-4 fw-bold">Crear cuenta</h3>

      <form onSubmit={handleSubmit}>
        
        <div className="row g-4">
          
          {/* Columna Izquierda */}
          <div className="col-md-6">

            {/* Email */}
            <div className="mb-3">
              <label className="form-label">Email</label>
              <input
                type="email"
                name="email"
                className="form-control"
                value={registerData.email}
                onChange={handleChange}
                required
              />
            </div>

            {/* Contraseña */}
            <div className="mb-3">
              <label className="form-label">Contraseña</label>
              <input
                type="password"
                name="password"
                className="form-control"
                value={registerData.password}
                onChange={handleChange}
                required
              />
            </div>

            {/* Nombre */}
            <div className="mb-3">
              <label className="form-label">Nombre completo</label>
              <input
                type="text"
                name="nameComplete"
                className="form-control"
                value={registerData.nameComplete}
                onChange={handleChange}
                required
              />
            </div>

            {/* Teléfono */}
            <div className="mb-3">
              <label className="form-label">Número telefónico</label>
              <input
                type="text"
                name="phoneNumber"
                className="form-control"
                value={registerData.phoneNumber}
                onChange={handleChange}
                required
              />
            </div>

            <div className="mb-3">
              <br />
                <p><a href="/login">Tengo cuenta</a></p>
            </div>
          </div>

          {/* Columna Derecha */}
          <div className="col-md-6">

            {/* Identificación */}
            <div className="mb-3">
              <label className="form-label">Identificación</label>
              <input
                type="text"
                name="identification"
                className="form-control"
                value={registerData.identification}
                onChange={handleChange}
                required
              />
            </div>

            {/* Seguro */}
            <div className="mb-3">
              <label className="form-label">Seguro médico</label>
              <select
                name="insuranceId"
                className="form-select"
                value={registerData.insuranceId}
                onChange={handleChange}
                required
              >
                <option value={0} disabled>Seleccione un seguro</option>
                {Array.isArray(insurances) &&
                  insurances.map(ins => (
                    <option key={ins.id} value={ins.id}>{ins.name}</option>
                  ))
                }
              </select>
            </div>

            {/* Fecha */}
            <div className="mb-3">
              <label className="form-label">Fecha de nacimiento</label>
              <input
                type="date"
                name="dateOfBirth"
                className="form-control"
                value={registerData.dateOfBirth}
                onChange={handleChange}
                required
              />
            </div>

            {/* Tipo de Sangre */}
            <div className="mb-3">
              <label className="form-label">Tipo de sangre</label>
              <select
                name="bloodTypeDTO"
                className="form-select"
                value={registerData.bloodTypeDTO}
                onChange={handleChange}
                required
              >
                {(Object.values(BloodType) as string[]).map(type => (
                  <option key={type} value={type}>
                    {type
                      .replace("_Positive", " +")
                      .replace("_Negative", " −")
                      .replace("_", " ")}
                  </option>
                ))}
              </select>
            </div>

            {/* genero */}
            <div className="mb-3">
              <label className="form-label">Género</label>
              <select
                name="genderDTO"
                className="form-select"
                value={registerData.genderDTO}
                onChange={handleChange}
                required
              >
                {(Object.values(Gender) as string[]).map(g => (
                  <option key={g} value={g}>
                    {g === "Male" ? "Masculino" : "Femenino"}
                  </option>
                ))}
              </select>
            </div>
          </div>
        </div>

        {/* Boton final y amplio*/}
        <div className="mt-4">
          <button type="submit" className="btn btn-primary w-100 btn-lg">
            Registrarme
          </button>
        </div>

      </form>
    </div>
  </div>
);
}
