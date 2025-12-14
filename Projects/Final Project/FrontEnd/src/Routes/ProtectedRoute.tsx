import { Navigate } from "react-router-dom";
import { useAuth } from "../Context/AutContext";
import { ReactNode } from "react";

// Se declara la interfaz para los datos a recibir
interface ProtectedRouteProps {
  // el nodo de react
  children: ReactNode;
  // los roles del usuario
  allowedRoles: string[];
}

export function ProtectedRoute({ children, allowedRoles }: ProtectedRouteProps) {
  // los roles y el estado de carga
  const { roles, isLoading } = useAuth();

  console.log("Roles del usuario:", roles);
  console.log("Roles permitidos:", allowedRoles);
  console.log("Cargando:", isLoading);

  //Espera a que termine de cargar
  if (isLoading) {
    return (
      <div style={{ 
        display: 'flex', 
        justifyContent: 'center', 
        alignItems: 'center', 
        height: '100vh' 
      }}>
        <div>Cargando...</div>
      </div>
    );
  }

  // Vemos si el usuario tiene los roles que se piden para esa pagina
  const hasAccess = roles.some(role => allowedRoles.includes(role));

  // declaramos el home al que mandaremos por defecto
  var home = ""

// Verificamos los roles DEL USUARIO, no los permitidos
  if (roles.includes("Admin")) {
    home = "/admin";
  } else if (roles.includes("Doctor")) {
    home = "/doctor";
  } else if (roles.includes("User")) {
    home = "/user";
  } else {
    // Si no tiene roles (no autenticado), lo mandamos a login/home
    home = "/";
  }

      // lo mandamos al home por defecto
  return hasAccess ? children : <Navigate to={home} />;
}