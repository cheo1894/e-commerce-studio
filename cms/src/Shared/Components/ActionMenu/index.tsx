import React from "react";
import { CgEye, CgPen, CgTrash } from "react-icons/cg";
import "./index.css";

type Props = {
  onView?: () => void;
  onEdit?: () => void;
  onDelete?: () => void;
};

function Index({ onView, onEdit, onDelete }: Props) {
  return (
    <div className="icon-container">
      <CgEye onClick={onView} className="icon-style" />
      <CgPen onClick={onEdit} className="icon-style" />
      <CgTrash onClick={onDelete} className="icon-style" />
    </div>
  );
}

export default Index;
