import { createContext, useContext, useState, useEffect, ReactNode } from "react";

// Interfaz para simular una clase con los componentes
interface AuthContextType {
  user: any;
  roles: string[];
  isLoading: boolean;
  login: (userData: any, roleList: string[]) => Promise<void>;
  logout: () => void;
}

// Se crea un contexto con la interfaz
const AuthContext = createContext<AuthContextType | null>(null);

// Creamos una interfaz diciendo que es un nodo de react para que funcione 
interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [user, setUser] = useState<any>(null);
  const [roles, setRoles] = useState<string[]>([]);
  const [isLoading, setIsLoading] = useState(true); // ← NUEVO: inicia en true

  // Al iniciar la app revisa el local storage a ver si los tiene
  useEffect(() => {
    try {
      const savedUser = localStorage.getItem("user");
      const savedRoles = localStorage.getItem("roles");

      // Si tiene datos los setea
      if (savedUser) setUser(JSON.parse(savedUser));
      if (savedRoles) setRoles(JSON.parse(savedRoles));
    } catch (error) {
      console.error("Error al cargar datos del localStorage:", error);
    } finally {
      setIsLoading(false); // ← NUEVO: termina la carga
    }
  }, []);

  // Funcion de login que se encarga de setear los datos en el context y ademas guardarlos en el local storage
  const login = (userData: any, roleList: string[]) => {
    return new Promise<void>((resolve) => {
      setUser(userData);
      setRoles(roleList);

      localStorage.setItem("user", JSON.stringify(userData));
      localStorage.setItem("roles", JSON.stringify(roleList));
      
      // Usa setTimeout para esperar al próximo tick y asegurar que el estado se actualizó
      setTimeout(() => {
        resolve();
      }, 0);
    });
  };

  // Funcion de logout que borra los datos tanto del context como del local storage
  const logout = () => {
    setUser(null);
    setRoles([]);
    localStorage.removeItem("user");
    localStorage.removeItem("roles");
  };

  // Retorna el componente de react con su provider
  return (
    <AuthContext.Provider value={{ user, roles, isLoading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

// Hook para que se pueda utilizar el context
export function useAuth(): AuthContextType {
  const context = useContext(AuthContext);
  
  if (!context) {
    throw new Error("useAuth debe usarse dentro de un AuthProvider");
  }
  
  return context;
}