import type { ProductPayloadModel } from "../../Data/Models/ProductPayloadModel";
import type { ProductEntity } from "../Entities/ProductEntity";

export interface ProductContract {
  GetProducts(): Promise<ProductEntity[]>;

  AddProduct(payload: ProductPayloadModel): Promise<ProductEntity>;
}
