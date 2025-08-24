import { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { LoginUser } from "../services/userServices";

export default function Login() {
  const { auth, setAuth } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  if(!auth.user) return (
    <div className="flex items-center justify-center h-screen">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          LoginUser(email, password).then((data) => {
            console.log("Login response:", data);
            setAuth({ user: data });
          });
        }}
        className="flex flex-col gap-2"
      >
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="border p-2 rounded"
          required
        />
        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="border p-2 rounded"
          required
        />
        <button type="submit" className="cursor-pointer bg-blue-500 text-white p-2 rounded hover:bg-blue-600 active:bg-blue-700">
          Iniciar sesión
        </button>
      </form>
    </div>
  );
}
