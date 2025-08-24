import { useEffect, useState } from "react";
import { useAuth } from "../hooks/useAuth";
import NavButton from "./navbutton";
import { LogOutUser } from "../services/userServices";

function NavMenu () {
    const [active, setActive] = useState<string>(
      () => localStorage.getItem("navActive") || "home"
    );

    const { auth, setAuth } = useAuth();

    const handleLogout = ()=>{
      LogOutUser().then(() => {
        setAuth({ user: null });
      });
    }
    
    useEffect(() => {
      localStorage.setItem("navActive", active);
    }, [active]);
    
    return (
      <nav className="w-56 h-full gap-1 flex flex-col from-amber-500 to-amber-700 bg-gradient-to-b shadow-lg drop-shadow-blue-950 drop-shadow-md fixed">
        <div className="text-white text-lg align-middle justify-center flex flex-col items-center">
          <img
            src="./src/assets/business.svg"
            alt="Business Logo"
            className="size-20"
          />
          <span className="ml-2 text-xl">Business Manager</span>
        </div>
        <ul className="flex flex-col gap-1 mt-4 w-full">
          <li className="w-full flex">
            <NavButton
              isActive={active === "home"}
              label="Home"
              onClick={() => setActive("home")}
              url="/"
            />
          </li>
          <li className="w-full flex">
            <NavButton
              isActive={active === "orders"}
              label="Orders"
              onClick={() => setActive("orders")}
              url="/orders"
            />
          </li>
          <li className="w-full flex">
            <NavButton
              isActive={active === "technicians"}
              label="Technicians"
              onClick={() => setActive("technicians")}
              url="/technicians"
            />
          </li>
          <li className="w-full flex">
            <NavButton
              isActive={active === "customers"}
              label="Customers"
              onClick={() => setActive("customers")}
              url="/customers"
            />
          </li>
        </ul>
        <div className="flex absolute bottom-5 justify-center w-full">
          <button onClick={handleLogout}>
            <span className="cursor-pointer font-black">Log Out</span>{" "}
            {auth.user?.name}
          </button>
        </div>
      </nav>
    );
}

export default NavMenu;