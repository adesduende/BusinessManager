import { LineChart } from "@mui/x-charts/LineChart";
import TechnicianButton from "../components/technicianButton";
import type {} from "@mui/x-charts/themeAugmentation";

// Create a dataset of max days of current month
const daysOfMonth = Array.from(
    { length: new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).getDate() },
    (_, i) => i + 1
);

export default function Technicians() {
  return (
    <div className="flex flex-col items-center size-full">
      <h1 className="text-2xl font-bold">Técnicos</h1>
      <TechnicianButton
        title="Lista de Técnicos"
        iconSrc="./src/assets/icons/technician.svg"
        iconAlt="Lista de Técnicos"
        url="/technicians"
      />
      <TechnicianButton
        title="Añadir nuevo técnico"
        iconSrc="./src/assets/icons/addTechnician.svg"
        iconAlt="Añadir nuevo técnico"
        url="/technicians/new"
      />
      {/* Charts */}
      <div className="flex flex-row items-center justify-center mt-8 mb-8 gap-8 w-full flex-wrap">
        <div className="w-full max-w-md bg-gray-50 rounded-2xl justify-center items-center flex flex-col shadow-2xl bg-linear-to-r from-amber-200 to-amber-50">
          <h2 className="text-lg font-bold text-gray-800 ">
            Ordenes Terminadas por Día
          </h2>
          <LineChart
            sx={{
              "& .MuiLineElement-root": {
                strokeWidth: 2,
              },
              "& .MuiChartsAxis-line, .MuiChartsAxis-tick": {
                strokeWidth: 2,
                stroke: "gray",
              },
              width: "100%",
            }}
            experimentalFeatures={{ preferStrictDomainInLineCharts: true }}
            xAxis={[
              {
                dataKey: "day",
                data: daysOfMonth,
              },
            ]}
            series={[
              {
                data: [100, 5.5, 2, 8.5, 1.5, 5],
                label: "Ordenes",
                color: "#eba134",
                area: false,
              },
            ]}
            height={200}
          />
        </div>
        <div className="w-full max-w-md bg-gray-50 rounded-2xl justify-center items-center flex flex-col shadow-2xl bg-linear-to-r from-amber-200 to-amber-50">
          <h2 className="text-lg font-bold text-gray-800 ">Bajas por día</h2>
          <LineChart
            sx={{
              "& .MuiLineElement-root": {
                strokeWidth: 2,
              },
              "& .MuiChartsAxis-line, .MuiChartsAxis-tick": {
                strokeWidth: 2,
                stroke: "gray",
              },
              width: "100%",
            }}
            experimentalFeatures={{ preferStrictDomainInLineCharts: true }}
            xAxis={[
              {
                dataKey: "day",
                data: daysOfMonth,
              },
            ]}
            series={[
              {
                data: [100, 5.5, 2, 8.5, 1.5, 5],
                label: "Bajas",
                color: "#eba134",
                area: false,
              },
            ]}
            height={200}
          />
        </div>
      </div>
    </div>
  );
}
