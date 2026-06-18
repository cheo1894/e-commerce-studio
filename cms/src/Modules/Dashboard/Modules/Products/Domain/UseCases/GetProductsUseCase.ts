import type { ProductContract } from "../Contracts/ProductContract";
export class ProductsUseCases {
  private readonly productContract: ProductContract;

  constructor(productContract: ProductContract) {
    this.productContract = productContract;
  }

  async execute() {
    return this.productContract.GetProducts();
  }
}
