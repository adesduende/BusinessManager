import { Link } from "react-router";
import type { Customer } from "../types/types";
import { useEffect, useState } from "react";
import { getAllCustomers, getCustomersByName } from "../services/customerService";

export default function Customers() {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  
  const fetchCustomers = async () => {
    const data = await getAllCustomers();
    setCustomers(data);
  };
  
  useEffect(() => {
    fetchCustomers();
  }, []);

  const handleSearch = (name: string) => {
    const searchCustomers = async (name: string) => {
      const data = await getCustomersByName(name);
      setCustomers(data);
    };

    searchCustomers(name);
  };

  const handleReset = () => {
    fetchCustomers();
  };

  return (
    <div className="flex flex-col items-center size-full">
      {/* Search bar for customers */}
      <div className="flex flex-row items-center justify-between gap-3 mb-4 w-full max-w-4xl">
        <div className="justify-center align-middle flex">
          <input
            type="text"
            placeholder="Buscar clientes..."
            className="border border-gray-300 rounded-lg py-2 px-4 w-full"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <button
            className="cursor-pointer text-white rounded-lg py-2 px-4"
            onClick={() => handleSearch(searchTerm)}
          >
            <img
              src="./src/assets/icons/search.svg"
              alt="Buscar"
              className="size-8"
            />
          </button>
          <button className="flex items-center" onClick={handleReset}>
            <img
              src="./src/assets/icons/reset.svg"
              alt="Resetear"
              className="size-8 cursor-pointer"
            />
          </button>
        </div>
        <Link
          to="/customer/create"
          className="bg-green-500 text-white rounded-lg py-2 px-4 hover:bg-green-600/50 transition-colors"
        >
          <img
            src="./src/assets/icons/add.svg"
            alt="Agregar Cliente"
            className="size-4"
          />
        </Link>
      </div>
      {/* Table for displaying customer data */}
      <table className="table-auto w-full max-w-4xl rounded-lg shadow-lg overflow-hidden">
        <thead className="bg-gray-100">
          <tr>
            <th className="px-4 py-2 text-left font-semibold text-gray-700">
              NIF
            </th>
            <th className="px-4 py-2 text-left font-semibold text-gray-700">
              Nombre
            </th>
            <th className="px-4 py-2 text-left font-semibold text-gray-700">
              Email
            </th>
            <th className="px-4 py-2 text-left font-semibold text-gray-700">
              Teléfono
            </th>
            <th className="px-4 py-2 text-center font-semibold text-gray-700">
              Actions
            </th>
          </tr>
        </thead>
        <tbody>
          {customers.map((customer) => (
            <tr
              key={customer.id}
              className="hover:bg-gray-50/10 transition-colors text-sm"
            >
              <td className="px-4 py-2 border-t border-gray-200">
                {customer.nif}
              </td>
              <td className="px-4 py-2 border-t border-gray-200">
                {customer.name} {customer.surname}
              </td>
              <td className="px-4 py-2 border-t border-gray-200">
                {customer.email}
              </td>
              <td className="px-4 py-2 border-t border-gray-200">
                {customer.phoneNumber}
              </td>
              <td className="px-4 py-2 border-t border-gray-200">
                <div className="flex space-between gap-2 size-full justify-center">
                  <Link
                    to={`/Customers/${customer.id}`}
                    className="size-4 cursor-pointer flex items-center"
                  >
                    <img
                      src="./src/assets/icons/see.svg"
                      alt="Ver Detalles"
                      className="size-4"
                    />
                  </Link>
                  <button className="size-4 cursor-pointer flex items-center">
                    <img
                      src="./src/assets/icons/edit.svg"
                      alt="Editar"
                      className="size-4"
                    />
                  </button>
                  <button className="size-4 cursor-pointer flex items-center">
                    <img
                      src="./src/assets/icons/delete.svg"
                      alt="Eliminar"
                      className="size-4"
                    />
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
