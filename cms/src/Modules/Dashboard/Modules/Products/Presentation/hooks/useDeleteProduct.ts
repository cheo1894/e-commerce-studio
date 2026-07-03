import { Api } from "@/Shared/Utils/api";
import React from "react";
import { useNavigate } from "react-router";
import { ProductDataSource } from "../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../Data/Implementations/ProductImplementations";
import { DeleteProductUseCase } from "../../Domain/UseCases/DeleteProductUseCase";

function useDeleteProduct() {
  const navigate = useNavigate();
  const api = new Api();
  const dataSource = new ProductDataSource(api);
  const implementation = new ProductImplementations(dataSource);
  const useCase = new DeleteProductUseCase(implementation);

  const DeleteProduct = async (id: number) => {
    try {
      const res = await useCase.execute(id);
      if (res === true) {
        navigate("/dashboard/products/list");
      }
    } catch (error) {
      console.log("ERROR AL BORRAR PRODUCTO >>>", error);
    }
  };
  return { DeleteProduct };
}

export default useDeleteProduct;
