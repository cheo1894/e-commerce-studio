import React from "react";
import "./index.css";

type Variant = "normal" | "danger";

type Props = {
  variant?: Variant;
  Type?: React.ButtonHTMLAttributes<HTMLButtonElement>["type"];
  Title?: string;
  onClick?: () => void;
  disabled?: boolean;
};

function Index({ Type, Title, onClick, disabled, variant = "normal" }: Props) {
  return (
    <button
      className={`Button-body ${variant}`}
      type={Type ?? "button"}
      onClick={onClick}
      disabled={disabled ?? false}
    >
      <h5 className="Button-title">{Title ?? ""}</h5>
    </button>
  );
}

export default Index;
