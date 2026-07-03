import type { ProductModel } from "../../Data/Models/ProductModel";
import type { ProductPayloadModel } from "../../Data/Models/ProductPayloadModel";
import type { ProductByIdEntity } from "../Entities/productByIdEntity";
import type { ProductEntity } from "../Entities/ProductEntity";

export interface ProductContract {
  GetProducts(): Promise<ProductEntity[]>;

  GetProductById(id: number): Promise<ProductByIdEntity>;

  AddProduct(payload: ProductPayloadModel): Promise<ProductEntity>;

  UpdateProduct(
    id: number,
    payload: ProductPayloadModel,
  ): Promise<ProductByIdEntity>;

  DeleteProduct(id: number): Promise<boolean>;
}
