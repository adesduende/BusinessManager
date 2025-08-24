const API_URL = "https://localhost:7157";

export async function ValidateUser() {
  const response = await fetch(
    `${API_URL}/user/me`,
    { 
        method: "POST",
        credentials: "include"
    }
  );

  return response.ok ? response.json() : null;
}

export async function LoginUser(email: string, password: string) {
    const response = await fetch(`${API_URL}/user/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "include",
      body: JSON.stringify({ email, password })
    });
    console.log("Login response:", response);
  return response.ok ? response.json() : null;
}

export async function LogOutUser() {
  const response = await fetch(`${API_URL}/user/logout`, {
    method: "POST",
    credentials: "include"
  });
  return response.ok;
}
