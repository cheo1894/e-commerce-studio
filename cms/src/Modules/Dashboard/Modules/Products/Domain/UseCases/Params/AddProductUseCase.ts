import type { ProductContract } from "../../Contracts/ProductContract";
import type { ProductEntity } from "../../Entities/ProductEntity";
import type { AddproductParams } from "./AddProductParams";

export class AddProductUseCase {
  private readonly repo: ProductContract;

  constructor(repo: ProductContract) {
    this.repo = repo;
  }

  async execute(payload: AddproductParams): Promise<ProductEntity> {
    console.log("Estamos en el execute");
    return await this.repo.AddProduct(payload);
  }
}
