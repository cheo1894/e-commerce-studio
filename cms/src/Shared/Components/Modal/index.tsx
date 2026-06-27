import React from "react";
import "./index.css";
import { RxCross2 } from "react-icons/rx";
import Spacer from "@/Shared/Components/Spacer";

type Props = {
  visible: boolean;
  onClose?: () => void;
  children?: React.ReactNode;
  style?: React.CSSProperties;
  title?: string;
};

function index({ visible, children, style, title, onClose }: Props) {
  if (!visible) return null;
  return (
    <div className="modal-overlay">
      <div className="modal-content" style={style}>
        <div className="modal-header">
          <h1>{title ?? ""}</h1>
          <Spacer type={"column"} />
          <RxCross2 size={30} onClick={onClose} />
        </div>
        <div className="modal-body">{children}</div>
      </div>
    </div>
  );
}

export default index;
