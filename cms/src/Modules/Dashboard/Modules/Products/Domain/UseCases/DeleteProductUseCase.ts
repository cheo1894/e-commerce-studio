import type { ProductContract } from "../Contracts/ProductContract";

export class DeleteProductUseCase {
  private readonly repo: ProductContract;
  constructor(repo: ProductContract) {
    this.repo = repo;
  }

  async execute(id: number) {
    return this.repo.DeleteProduct(id);
  }
}
