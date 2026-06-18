import React from "react";
import "./index.css";
type Props = {
  Type?: React.ButtonHTMLAttributes<HTMLButtonElement>["type"];
  Title?: string;
  onClick?: () => void;
  disabled?: boolean;
};

function Index({ Type, Title, onClick, disabled }: Props) {
  return (
    <button
      className="Button-body"
      type={Type ?? "button"}
      onClick={onClick}
      disabled={disabled ?? false}
    >
      <h5 className="Button-title">{Title ?? ""}</h5>
    </button>
  );
}

export default Index;
