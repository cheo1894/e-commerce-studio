import type { ProductContract } from "../Contracts/ProductContract";
import type { AddProductFormData } from "../../Presentation/Schemas/AddProductSchema";
import type { ProductByIdEntity } from "../Entities/productByIdEntity";
import type { AddproductParams } from "./Params/AddProductParams";

export class UpdateProductUseCase {
  private readonly repo: ProductContract;
  constructor(repo: ProductContract) {
    this.repo = repo;
  }

  async execute(
    id: number,
    payload: AddproductParams,
  ): Promise<ProductByIdEntity> {
    return await this.repo.UpdateProduct(id, payload);
  }
}
