import { createContext, useState } from "react";
import { useEffect } from "react";
import type { User } from "../types/types";
import { ValidateUser } from "./userServices";

type AuthContextType = {
  auth: { user?: User | null };
  setAuth: React.Dispatch<React.SetStateAction<{ user?: User | null }>>;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [auth, setAuth] = useState<{ user?: User | null }>({ user: null });

  useEffect(() => {
    // Fetch user info from backend using httpOnly cookie

    ValidateUser().then(data => {
      if (data) setAuth({ user: data });
      else setAuth({ user: null });
    })
    .catch(() => setAuth({ user: null }));
  }, []);

  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;

