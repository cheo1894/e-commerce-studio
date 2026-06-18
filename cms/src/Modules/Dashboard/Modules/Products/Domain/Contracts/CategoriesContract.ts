import type { CategoryEntity } from "../Entities/CategoryEntity";
export interface CategoriesContract {
  GetCategories(): Promise<CategoryEntity[]>;
}
