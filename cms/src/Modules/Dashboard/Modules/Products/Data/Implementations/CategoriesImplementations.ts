import type { CategoriesContract } from "../../Domain/Contracts/CategoriesContract";
import type { CategoryModel } from "../Models/CategoryModel";
import { CategoriesSource } from "../Sources/CategoriesSource";
import type { CategoryEntity } from "../../Domain/Entities/CategoryEntity";

export class CategoriesImplementations implements CategoriesContract {
  private readonly categoriesSource: CategoriesSource;

  constructor(categoriesSource: CategoriesSource) {
    this.categoriesSource = categoriesSource;
  }

  async GetCategories(): Promise<CategoryEntity[]> {
    const res = await this.categoriesSource.GetCategories();
    const data = res.map((c: CategoryModel) => {
      const { ...categoryData }: CategoryEntity = c;
      return categoryData;
    });
    return data;
  }
}
