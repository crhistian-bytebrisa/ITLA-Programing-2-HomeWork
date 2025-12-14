import { Routes, Route, Navigate } from "react-router-dom";
import { ProtectedRoute } from "./Routes/ProtectedRoute";
import { AdminHome } from "./Pages/Home/AdminHome";
import { DoctorHome } from "./Pages/Home/DoctorHome";
import { HomePage } from "./Pages/Home/HomePage";
import { LoginPage } from "./Pages/LoginPage";
import { PatientHome } from "./Pages/Home/PatientHome";
import { RegisterPage } from "./Pages/RegisterPage";
import { ProfilePage } from "./Pages/ProfilePage";
import { useAuth } from "./Context/AutContext";
import { AnalysesCRUD } from "./Pages/EspecialtyPages/AnalysesPage";
import { ClinicsCRUD } from "./Pages/EspecialtyPages/ClinicsPage";
import { PermissionsCRUD } from "./Pages/EspecialtyPages/PermissionsPage";
import { MedicinesCRUD } from "./Pages/EspecialtyPages/MedicinesPage";
import { InsurancesCRUD } from "./Pages/EspecialtyPages/InsurancesPage";
import { PatientsList } from "./Pages/EspecialtyPages/PatientsPage";
import { PatientDetails } from "./Pages/EspecialtyPages/PatientDetailsPage";
import { ConsultationsList } from "./Pages/EspecialtyPages/ConsultationsPage";
import { ConsultationDetails } from "./Pages/EspecialtyPages/ConsultationDetailsPage";
import { UserConsultationsPage } from "./Pages/Home/UserConsultationsPage";
import { UserMedicinesPage } from "./Pages/Home/UserMedicinesPage";
import { UserDocumentsPage } from "./Pages/Home/UserDocumentsPage";
import DaysAvailableCRUD from "./Pages/EspecialtyPages/DaysAvailablePage";
import { SchedulePage } from "./Pages/EspecialtyPages/SchedulePage";
import { AppointmentPage } from "./Pages/EspecialtyPages/AppointmentPage";
import { TodayConsultationsPage } from "./Pages/EspecialtyPages/TodayConsultationsPage";
import { AttendConsultationPage } from "./Pages/EspecialtyPages/AttendConsultationPage";


function App() {
  const {roles} = useAuth();
  var home = ""
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

  return (
    <Routes>
      <Route path="/admin" element={
        <ProtectedRoute allowedRoles={["Admin"]}>
          <AdminHome />
        </ProtectedRoute>
      } />

      <Route path="/doctor" element={
        <ProtectedRoute allowedRoles={["Doctor"]}>
          <DoctorHome />
        </ProtectedRoute>
      } />

      <Route path="/user" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <PatientHome />
        </ProtectedRoute>
      } />

      <Route path="/profile" element={
        <ProtectedRoute allowedRoles={["User","Doctor","Admin"]}>
          <ProfilePage />
        </ProtectedRoute>
      } />

      <Route path="/analyses" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <AnalysesCRUD />
        </ProtectedRoute>
      } />

      <Route path="/clinics" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <ClinicsCRUD />
        </ProtectedRoute>
      } />

      <Route path="/permissions" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <PermissionsCRUD />
        </ProtectedRoute>
      } />

      <Route path="/medicines" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <MedicinesCRUD />
        </ProtectedRoute>
      } />

      <Route path="/patients" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <PatientsList />
        </ProtectedRoute>
      } />

      <Route path="/patients/:id" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin","User"]}>
          <PatientDetails />
        </ProtectedRoute>
      } />

      <Route path="/insurances" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <InsurancesCRUD />
        </ProtectedRoute>
      } />

      <Route path="/consultations" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <ConsultationsList />
        </ProtectedRoute>
      } />

      <Route path="/consultations/:id" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin","User"]}>
          <ConsultationDetails />
        </ProtectedRoute>
      } />

      <Route path="/daysavailable" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <DaysAvailableCRUD />
        </ProtectedRoute>
      } />

      <Route path="/todayconsultations" element={
        <ProtectedRoute allowedRoles={["Doctor","Admin"]}>
          <TodayConsultationsPage />
        </ProtectedRoute>
      } />

      <Route path="/consultations/:id/attend" element={
        <ProtectedRoute allowedRoles={["Doctor", "Admin"]}>
          <AttendConsultationPage />
        </ProtectedRoute>
      } />

      <Route path="/user/schedules" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <SchedulePage />
        </ProtectedRoute>
      } />

      <Route path="/user/appointments" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <AppointmentPage />
        </ProtectedRoute>
      } />

      

      <Route path="/user/consultations" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <UserConsultationsPage />
        </ProtectedRoute>
      } />

      <Route path="/user/medications" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <UserMedicinesPage />
        </ProtectedRoute>
      } />

      <Route path="/user/documents" element={
        <ProtectedRoute allowedRoles={["User"]}>
          <UserDocumentsPage />
        </ProtectedRoute>
      } />

      <Route path="/" element={<HomePage />} />

      <Route path="/home" element={<HomePage />} />

      <Route path="/login" element={<LoginPage />} />

      <Route path="/register" element={<RegisterPage/>}/>

      <Route path="/*" element={<Navigate to={home} />} />
    </Routes>
  );
}

export default App;
