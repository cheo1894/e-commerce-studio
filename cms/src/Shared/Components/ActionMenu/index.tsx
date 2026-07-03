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
      <CgEye onClick={onView} className="action-icon" />
      <CgPen onClick={onEdit} className="action-icon" />
      <CgTrash onClick={onDelete} className="action-icon" />
    </div>
  );
}

export default Index;
