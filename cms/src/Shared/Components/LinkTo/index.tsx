import { Link } from "react-router";
import "./index.css";

type Props = {
  to: string;
  title: string;
  disabled?: boolean;
  onMouseEnter?: () => void;
  onMouseLeave?: () => void;
  className?: string;
  onClick?: () => void;
};

function index({
  to,
  title,
  disabled = false,
  onMouseEnter,
  onMouseLeave,
  className = "",
  onClick,
}: Props) {
  return (
    <Link
      className={`to ${className}`}
      to={disabled ? "#" : to}
      onClick={(e) => {
        e.preventDefault();
        if (disabled || !onClick) return;
        onClick();
      }}
      onMouseEnter={onMouseEnter}
      onMouseLeave={onMouseLeave}
    >
      {title}
    </Link>
  );
}

export default index;
