import "./App.css";
import NavMenu from "./components/navmenu";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Customers from "./pages/customers";
import CreateCustomer from "./pages/createcustomer";
import Technicians from "./pages/technicians";
import { AuthProvider } from "./services/AuthProvider";
import { useAuth } from "./hooks/useAuth";
import Login from "./pages/login";

function ProtectedApp() {
  const { auth } = useAuth();
  if (!auth.user) {
    return (
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    );
  }
  return (
    <>
      <NavMenu />
      <div className="flex-grow p-4 ml-56">
        <Routes>
          <Route path="/" element={<div>Home</div>} />
          <Route path="/orders" element={<div>Orders</div>} />
          <Route path="/technicians" element={<Technicians />} />
          <Route path="/customers" element={<Customers />} />
          <Route path="/customer/create" element={<CreateCustomer />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </div>
    </>
  );
}

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <ProtectedApp />
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
