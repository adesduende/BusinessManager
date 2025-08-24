import type { Customer } from "../types/types";

const API_URL = "https://localhost:7157";

export async function createCustomer(customer: Customer) {
  const response = await fetch(`${API_URL}/customer`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(customer),
  });
  console.log("Response:", response);
  if (!response.ok) {
    throw new Error("Error al crear cliente");
  }
  return response.json();
}

export async function getAllCustomers() {
  const response = await fetch(`${API_URL}/customers`);
  if (!response.ok) {
    throw new Error("Error al obtener clientes");
  }
  return response.json();
}

export async function getCustomerById(id: string) {
  const response = await fetch(`${API_URL}/customer/${id}`);
  if (!response.ok) {
    throw new Error("Error al obtener cliente");
  }
  return response.json();
}

export async function getCustomersByName(name: string) {
  const response = await fetch(`${API_URL}/customer?name=${name}`);
  if (!response.ok) {
    throw new Error("Error al buscar clientes por nombre");
  }
  return response.json();
}

