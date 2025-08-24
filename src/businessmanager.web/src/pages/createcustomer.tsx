import type { Address } from "../types/types";
import {createCustomer} from "../services/customerService";
import { useState } from "react";

export default function CreateCustomer() {
  const [errors, setErrors] = useState<string[]>([]);

  function validate(data: Record<string, FormDataEntryValue>) {
    const errs: string[] = [];
    if (!data.name || !(data.name as string).trim()) errs.push("El nombre es obligatorio.");
    if (!data.email || !(data.email as string).trim()) {
      errs.push("El correo electrónico es obligatorio.");
    } else if (!/^\S+@\S+\.\S+$/.test(data.email as string)) {
      errs.push("El correo electrónico no es válido.");
    }
    if (!data.phone || !(data.phone as string).trim()) errs.push("El teléfono es obligatorio.");
    if (!data.document || !(data.document as string).trim()) errs.push("El documento de identidad es obligatorio.");
    if (!data.street || !(data.street as string).trim()) errs.push("La calle es obligatoria.");
    if (!data.city || !(data.city as string).trim()) errs.push("La ciudad es obligatoria.");
    if (!data.state || !(data.state as string).trim()) errs.push("El estado/provincia es obligatorio.");
    if (!data.zip || !(data.zip as string).trim()) errs.push("El código postal es obligatorio.");
    // Create a regex for nif that allow X00000000X or 00000000X
    const nifRegex = /^(\w\d{8}\w|\d{8}\w)$/;
    if (!data.document || !(data.document as string).trim()) {
      errs.push("El documento de identidad es obligatorio.");
    } else if (!nifRegex.test(data.document as string)) {
      errs.push("El documento de identidad no es válido.");
    }
    // Create a regex for phone number that allows +34 600000000 or 600000000
    const phoneRegex = /^(?:\+34)?\d{9}$/;
    if (!data.phone || !(data.phone as string).trim()) {
      errs.push("El teléfono es obligatorio.");
    } else if (!phoneRegex.test(data.phone as string)) {
      errs.push("El teléfono no es válido.");
    }
    // Create a regex for zip code that allow 12345
    const zipRegex = /^\d{5}$/;
    if (!data.zip || !(data.zip as string).trim()) {
      errs.push("El código postal es obligatorio.");
    } else if (!zipRegex.test(data.zip as string)) {
      errs.push("El código postal no es válido.");
    }
    return errs;
  }

  async function handleFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const data = Object.fromEntries(formData);
    const validationErrors = validate(data);
    if (validationErrors.length > 0) {
      setErrors(validationErrors);
      return;
    }
    setErrors([]);
    const customer = {
      name: data.name as string,
      surname: data.surname as string,
      email: data.email as string,
      phoneNumber: data.phone as string,
      nif: data.document as string,
      address: {
        street: data.street as string,
        city: data.city as string,
        state: data.state as string,
        zipCode: data.zip as string,
        country: (data.country as string) || "",
      } as Address,
    };
    try {
      await createCustomer(customer);
      window.location.href = "/customers";
    } catch (error: any) {
      setErrors(["Error al crear el cliente: " + error.message]);
    }
  }

  return (
    <div className="flex flex-col items-center size-full overflow-y-scroll">
      <h1 className="text-2xl font-bold mb-4">Crear Cliente</h1>
      <form
        className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full max-w-lg"
        onSubmit={handleFormSubmit}
      >
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="name"
          >
            Nombre
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            name="name"
            type="text"
            placeholder="Nombre del cliente"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="surname"
          >
            Apellidos
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="surname"
            name="surname"
            type="text"
            placeholder="Apellidos"
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="email"
          >
            Correo electrónico
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="email"
            name="email"
            type="email"
            placeholder="Correo electrónico"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="phone"
          >
            Teléfono
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="phone"
            name="phone"
            type="tel"
            placeholder="Teléfono"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="document"
          >
            Documento de identidad
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="document"
            name="document"
            type="text"
            placeholder="Documento de identidad"
            required
          />
        </div>
        <h2 className="text-xl font-semibold mb-2 mt-6 text-gray-800">Dirección</h2>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="street"
          >
            Calle
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="street"
            name="street"
            type="text"
            placeholder="Calle"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="city"
          >
            Ciudad
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="city"
            name="city"
            type="text"
            placeholder="Ciudad"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="state"
          >
            Estado/Provincia
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="state"
            name="state"
            type="text"
            placeholder="Estado/Provincia"
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="country"
          >
            País
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="country"
            name="country"
            type="text"
            placeholder="País"
          />
        </div>
        <div className="mb-6">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="zip"
          >
            Código postal
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="zip"
            name="zip"
            type="text"
            placeholder="Código postal"
            required
          />
        </div>
        <div className="flex items-center justify-between">
          <button
            className="cursor-pointer bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Crear cliente
          </button>
        </div>
      </form>
      {errors.length > 0 && (
        <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4 w-full max-w-lg">
          <ul className="list-disc pl-5">
            {errors.map((err, idx) => (
              <li key={idx}>{err}</li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
