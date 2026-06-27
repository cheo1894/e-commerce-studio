import { useEffect, useMemo, useRef, useState } from "react";
import type { ProductEntity } from "../../Domain/Entities/ProductEntity";
import { ProductDataSource } from "../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../Data/Implementations/ProductImplementations";
import { ProductsUseCases } from "../../Domain/UseCases/GetProductsUseCase";
import { Api } from "@/Shared/Utils/api";

function useProductsList() {
  const calledRef = useRef(false);
  const [productsList, setProductsList] = useState<ProductEntity[]>([]);
  const [loading, setLoading] = useState(false);
  const api = new Api();
  const productsDatasource = useMemo(() => new ProductDataSource(api), []);
  const productImplementations = useMemo(
    () => new ProductImplementations(productsDatasource),
    [],
  );
  const productsUseCases = useMemo(
    () => new ProductsUseCases(productImplementations),
    [],
  );
  const GetProducts = async () => {
    try {
      setLoading(true);
      const res = await productsUseCases.execute();
      if (res === null) {
        setProductsList([]);
        return;
      }
      console.log("LISTA DE PRODUCTOS", res);
      setProductsList(res);
    } catch (error) {
      console.log("Error al cargar la lista de productos");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (calledRef.current === true) return;

    calledRef.current = true;

    GetProducts();
  }, []);
  return {
    productsList,
    loading,
    GetProducts,
  };
}

export default useProductsList;
