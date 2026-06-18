import type { ProductEntity } from "../Entities/ProductEntity";

export interface ProductContract {
  GetProducts(): Promise<ProductEntity[]>;
}
