import type { CategoriesContract } from "../Contracts/CategoriesContract";

export class GetCategoriesUseCases {
  private readonly categoriesContract: CategoriesContract;

  constructor(categoriesContract: CategoriesContract) {
    this.categoriesContract = categoriesContract;
  }

  async execute() {
    return await this.categoriesContract.GetCategories();
  }
}
