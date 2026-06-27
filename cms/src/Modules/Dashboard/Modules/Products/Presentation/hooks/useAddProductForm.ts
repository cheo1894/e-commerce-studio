import { useForm } from "react-hook-form";
import {
  AddProductSchema,
  type AddProductFormData,
} from "../Schemas/AddProductSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { Api } from "@/Shared/Utils/api";
import { ProductDataSource } from "../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../Data/Implementations/ProductImplementations";
import { AddProductUseCase } from "../../Domain/UseCases/Params/AddProductUseCase";
import type { AddproductParams } from "../../Domain/UseCases/Params/AddProductParams";
import Decimal from "decimal.js";

type Props = {
  onSave?: () => void;
};

function useAddProductForm({ onSave }: Props) {
  const defaultValues: AddProductFormData = {
    name: "",
    price: 0, // inicial 0, pero la validación min(0.1) fallará hasta que se corrija
    quantity: 0, // inicial 0, min(1) fallará hasta que se corrija
    image: "",
    category: 0,
    description: "",
  };
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
  const api = new Api();
  const dataSoucer = new ProductDataSource(api);
  const implementation = new ProductImplementations(dataSoucer);
  const useCase = new AddProductUseCase(implementation);
  const onSubmit = async (data: AddProductFormData) => {
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

      const request = await useCase.execute(payload);
      if (request === null) {
        setError("root", { message: "Error en la carga del producto" });
        return;
      }

      if (onSave) {
        await onSave();
        reset();
      }
    } catch (error) {
      setError("root", { message: `Error en la carga del producto: ${error}` });
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

export default useAddProductForm;
