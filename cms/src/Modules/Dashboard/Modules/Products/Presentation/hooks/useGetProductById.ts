import { Api } from "@/Shared/Utils/api";
import React, { useEffect, useRef, useState } from "react";
import { ProductDataSource } from "../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../Data/Implementations/ProductImplementations";
import { GetProductByIdUseCase } from "../../Domain/UseCases/GetProductByIdUseCase";
import type { ProductByIdEntity } from "../../Domain/Entities/productByIdEntity";
import { format } from "date-fns";
import type { ProductEntity } from "../../Domain/Entities/ProductEntity";

type Props = {
  id: number;
};

function useGetProductById({ id }: Props) {
  const calledRef = useRef(false);
  const [product, setProduct] = useState<ProductByIdEntity | null>(null);
  const [loading, setLoading] = useState(false);

  const api = new Api();
  const source = new ProductDataSource(api);
  const implementations = new ProductImplementations(source);
  const useCase = new GetProductByIdUseCase(implementations);

  const GetProductById = async (id: number) => {
    try {
      setLoading(true);
      const res = await useCase.excute(id);
      console.log("PRODUCTO >>>>>>>>>>>>", res);
      const createdAtString = format(res.createdAt, "yyyy/MM/dd");
      const updatedAtString = format(res.updatedAt, "yyyy/MM/dd");
      const newRes: ProductByIdEntity = {
        ...res,
        createdAt: createdAtString,
        updatedAt: updatedAtString,
      };
      setProduct(newRes);
    } catch (error) {
      setProduct(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (calledRef.current === true) return;
    calledRef.current = true;

    GetProductById(Number(String(id)));
  }, []);

  return { product, loading, GetProductById };
}

export default useGetProductById;
