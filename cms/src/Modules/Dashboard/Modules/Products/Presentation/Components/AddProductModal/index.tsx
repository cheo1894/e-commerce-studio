import Modal from "@/Shared/Components/Modal";
import TextInput from "@/Shared/Components/TextInput";
import "./index.css";
import TextArea from "@/Shared/Components/TextArea";
import Select from "@/Shared/Components/Select";
import useCategoriesList from "../../hooks/useCategoriesList";
import useAddProductForm from "../../hooks/useAddProductForm";
import Button from "@/Shared/Components/Button";
import Spacer from "@/Shared/Components/Spacer";
import useUpdateProductForm from "../../hooks/useUpdateProductForm";
import type { AddProductFormData } from "../../Schemas/AddProductSchema";

type formType = "submit" | "update";

type Props = {
  type?: formType;
  visible: boolean;
  onClose: () => void;
  onSave?: () => void;
  formData?: AddProductFormData;
  productId?: number;
};

function Index({
  visible,
  onClose,
  onSave,
  type = "submit",
  formData,
  productId,
}: Props) {
  const { list } = useCategoriesList();
  const {
    register,
    handleSubmit,
    setError,
    errors,
    isSubmitting,
    isSubmitted,
    onSubmit,
    reset,
  } =
    type === "submit"
      ? useAddProductForm({ onSave })
      : useUpdateProductForm({ onSave, formData, visible, productId });

  return (
    <Modal
      visible={visible}
      title="Add Product"
      onClose={() => {
        reset();
        onClose();
      }}
    >
      <form onSubmit={handleSubmit((data) => onSubmit(data, productId))}>
        <div className="modal-row">
          <div style={{ width: "100%" }}>
            <TextInput
              Title="Name"
              placeholder="Ej:Product..."
              {...register("name")}
            />
            {errors.name && (
              <>
                <br />
                <span style={{ color: "red" }}>{errors.name.message}</span>
              </>
            )}
          </div>

          <div style={{ width: "100%" }}>
            <TextInput
              Title="Price"
              placeholder="Ej:9.99"
              type="number"
              step="0.01"
              {...register("price", { valueAsNumber: true })}
            />

            {errors.price && (
              <>
                <br />
                <span style={{ color: "red" }}>{errors.price.message}</span>
              </>
            )}
          </div>
        </div>
        <div className="modal-row">
          <div style={{ width: "100%" }}>
            <TextInput
              Title="Quantity"
              placeholder="Ej:10"
              type="number"
              step="1"
              {...register("quantity", { valueAsNumber: true })}
            />
            {errors.quantity && (
              <>
                <br />
                <span style={{ color: "red" }}>{errors.quantity.message}</span>
              </>
            )}
          </div>

          <div style={{ width: "100%" }}>
            <Select
              options={list ?? []}
              title="Category"
              {...register("category", { valueAsNumber: true })}
            />
            {errors.category && (
              <>
                <br />
                <span style={{ color: "red" }}>{errors.category.message}</span>
              </>
            )}
          </div>
        </div>
        <div style={{ width: "100%" }}>
          <div className="modal-row">
            <TextInput
              Title="Image"
              placeholder="Ej:https://..."
              {...register("image")}
            />
          </div>
          {errors.image && (
            <>
              <br />
              <span style={{ color: "red" }}>{errors.image.message}</span>
            </>
          )}
        </div>

        <div className="modal-row">
          <div style={{ width: "100%" }}>
            <TextArea Title="Description" {...register("description")} />
            {errors.description && (
              <>
                <br />
                <span style={{ color: "red" }}>
                  {errors.description.message}
                </span>
              </>
            )}
          </div>
        </div>

        <div className="modal-row">
          <Spacer type={"row"} />
          <div style={{ width: "200px" }}>
            <Button
              Type="submit"
              Title={
                isSubmitting
                  ? "Saving..."
                  : type === "submit"
                    ? "Save"
                    : "Update"
              }
              disabled={isSubmitting}
            />
          </div>
        </div>
      </form>
    </Modal>
  );
}

export default Index;
