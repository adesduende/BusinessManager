import { Link } from "react-router";

export default function NavButton({ label, onClick , url, isActive = false }:{label:string, onClick:() => void, url:string, isActive:boolean}) {
        
    return (
      <div
        className={`${
          isActive ? "bg-amber-400/50" : "bg-amber-400/20"
        } flex flex-row w-full h-10 text-left  hover:bg-amber-500 active:bg-amber-700 `}
      >
        <span
          className={`${isActive ? "bg-amber-300" : "bg-amber-950"} w-1 h-full`}
        ></span>
        <Link className="p-2 size-full" onClick={onClick} to={url}>
          {label}
        </Link>
      </div>
    );
}
