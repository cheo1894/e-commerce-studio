import { useForm } from "react-hook-form";
import {
  AddProductSchema,
  type AddProductFormData,
} from "../Schemas/AddProductSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { Api } from "@/Shared/Utils/api";
import { ProductDataSource } from "../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../Data/Implementations/ProductImplementations";
import { AddProductUseCase } from "../../Domain/UseCases/AddProductUseCase";
import type { AddproductParams } from "../../Domain/UseCases/Params/AddProductParams";
import Decimal from "decimal.js";
import { useEffect, useMemo } from "react";
import { UpdateProductUseCase } from "../../Domain/UseCases/updateProductUseCase";

type Props = {
  onSave?: () => void;
  formData?: AddProductFormData;
  visible?: boolean;
  productId: number | undefined;
};

function useUpdateProductForm({
  onSave,
  formData = {
    name: "arepa",
    price: 0, // inicial 0, pero la validación min(0.1) fallará hasta que se corrija
    quantity: 0, // inicial 0, min(1) fallará hasta que se corrija
    image: "",
    category: 0,
    description: "",
  },
  visible,
  productId,
}: Props) {
  const defaultValues: AddProductFormData = formData;

  const {
    register,
    handleSubmit,
    setError,
    reset,
    formState: { errors, isSubmitting, isSubmitted },
  } = useForm<AddProductFormData>({
    resolver: zodResolver(AddProductSchema),
    defaultValues,
  });

  useEffect(() => {
    if (visible === true && formData) {
      console.log("Datos para el formulario>>>", formData);
      reset(defaultValues);
    }
  }, [visible]);
  const api = new Api();
  const dataSoucer = new ProductDataSource(api);
  const implementation = new ProductImplementations(dataSoucer);
  const useCase = new UpdateProductUseCase(implementation);

  const onSubmit = async (data: AddProductFormData, id: number | undefined) => {
    const decimalPrice = new Decimal(data.price);
    try {
      const payload: AddproductParams = {
        productName: data.name,
        productPrice: decimalPrice,
        productDescription: data.description,
        quantity: data.quantity,
        categoryId: data.category,
        imageUrl: data.image,
      };

      if (!id) throw new Error("No se encontró el Id");

      const request = await useCase.execute(id, payload);
      if (request === null) {
        setError("root", { message: "Error al actualizar el producto" });
        return;
      }

      if (onSave) {
        await onSave();
        reset();
      }
    } catch (error) {
      setError("root", {
        message: `Error al actualizar el producto: ${error}`,
      });
    }
  };

  return {
    register,
    handleSubmit,
    setError,
    errors,
    isSubmitting,
    isSubmitted,
    onSubmit,
    reset,
  };
}

export default useUpdateProductForm;
