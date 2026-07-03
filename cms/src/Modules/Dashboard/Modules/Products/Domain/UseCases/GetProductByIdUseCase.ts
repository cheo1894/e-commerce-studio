import type { ProductContract } from "../Contracts/ProductContract";

export class GetProductByIdUseCase {
  private readonly repo: ProductContract;

  constructor(repo: ProductContract) {
    this.repo = repo;
  }

  async excute(id: number) {
    console.log("ESTAMOS EJECUTANDO CON >", id);
    return await this.repo.GetProductById(id);
  }
}
