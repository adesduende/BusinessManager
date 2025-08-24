interface TechnicianButtonProps {
  title: string;
  iconSrc: string;
  iconAlt: string;
  url: string;
}

export default function TechnicianButton({ title, iconSrc, iconAlt, url }: TechnicianButtonProps) {
  return (
    <a href={url} className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full max-w-lg mt-4 flex flex-row justify-between items-center hover:bg-gray-500/50">
      <h2 className="text-xl font-semibold mb-2 mt-6 text-gray-800">
        {title}
      </h2>
      <img
        src={iconSrc}
        alt={iconAlt}
        className="h-16"
      />
    </a>
  );
}
