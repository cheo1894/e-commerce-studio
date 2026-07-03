import "./index.css";
import { useParams } from "react-router";
import Divider from "@/Shared/Components/Divider";
import Spacer from "@/Shared/Components/Spacer";
import Button from "@/Shared/Components/Button";
import useGetProductById from "../../hooks/useGetProductById";
import useDeleteProduct from "../../hooks/useDeleteProduct";
import AddProductModal from "../../Components/AddProductModal";
import { useMemo, useState } from "react";
import type { AddProductFormData } from "../../Schemas/AddProductSchema";

type Props = {};

function Index({}: Props) {
  const { id } = useParams();
  const { product, loading, GetProductById } = useGetProductById({
    id: Number(id),
  });
  const { DeleteProduct } = useDeleteProduct();
  const [openModal, setOpenModal] = useState(false);

  const formData: AddProductFormData = useMemo(
    () => ({
      name: product?.productName ?? "",
      price: product?.productPrice ?? 0,
      quantity: product?.quantity ?? 0,
      image: product?.imageUrl ?? "",
      category: product?.categoryId ?? 0,
      description: product?.productDescription ?? "",
    }),
    [product],
  );

  return (
    <>
      {loading === false ? (
        <div className="product-container">
          <br />
          <br />
          <div className="product-row" style={{ width: "100%" }}>
            <Spacer type="row" />
            <div
              className="product-row"
              style={{ height: "100%", width: "300px", alignItems: "center" }}
            >
              <Button
                Title="Edit"
                onClick={() => {
                  setOpenModal(true);
                }}
              />
              <div style={{ width: "30px" }} />
              <Button
                Title="Delete"
                variant="danger"
                onClick={async () => {
                  await DeleteProduct(Number(id));
                }}
              />
            </div>
          </div>
          <div className="product-row">
            <div className="product-image">
              <img className="product-img" src={product?.imageUrl} alt="" />
            </div>
            <div className="product-info">
              <h1 className="product-name">{product?.productName}</h1>
              <Divider />
              <p>Cantidad disponible: {product?.quantity}</p>
              <p>Price: ${product?.productPrice}</p>
              <p>Available: {product?.quantity}</p>
              <p>{product?.productDescription}</p>
              <p>Category: {product?.category}</p>
              <p>Created At: {product?.createdAt}</p>
              <p>Updated At: {product?.updatedAt}</p>
              <p>Created By: {product?.createdByName}</p>
            </div>
          </div>
        </div>
      ) : (
        <div>Cargando...</div>
      )}

      <AddProductModal
        onClose={() => setOpenModal(false)}
        onSave={async () => {
          setOpenModal(false);
          await GetProductById(Number(id));
        }}
        visible={openModal}
        formData={formData}
        type="update"
        productId={Number(id)}
      />
    </>
  );
}

export default Index;
