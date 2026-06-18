import Modal from "@/Shared/Components/Modal";
import TextInput from "@/Shared/Components/TextInput";
import "./index.css";
import TextArea from "@/Shared/Components/TextArea";
import Select from "@/Shared/Components/Select";
import useCategoriesList from "../../hooks/useCategoriesList";

type Props = {
  visible: boolean;
  onClose: () => void;
};

function Index({ visible, onClose }: Props) {
  const { list } = useCategoriesList();
  return (
    <Modal visible={visible} title="Add Product" onClose={onClose}>
      <div className="modal-row">
        <TextInput Title="Name" value={""} />
        <TextInput Title="Price" value={""} />
      </div>
      <div className="modal-row">
        <TextInput Title="Quantity" value={""} />
        <Select options={list ?? []} title="Category" />
      </div>
      <div className="modal-row">
        <TextInput Title="Image" value={""} />
      </div>
      <div className="modal-row">
        <TextArea Title="Description" />
      </div>
    </Modal>
  );
}

export default Index;
