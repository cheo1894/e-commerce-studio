import React from "react";
import "./index.css";

type Props = {
  text: string;
  onClick: () => void;
};

function index({ text, onClick }: Props) {
  return (
    <p className="Button-link" onClick={onClick}>
      {text}
    </p>
  );
}

export default index;
