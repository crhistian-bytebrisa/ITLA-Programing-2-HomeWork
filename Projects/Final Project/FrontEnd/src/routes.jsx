import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import Register from './pages/RegisterPage'
import Login from './pages/LoginPage'
import TestPage from './pages/TestPage'

function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        
        <Route path="/register" element={<Register />} />
        
        <Route path="*" element={<Navigate to="/login" />} />

        <Route path='/test' element={<TestPage/>} />
      </Routes>
    </BrowserRouter>
  )
}

export default AppRoutes