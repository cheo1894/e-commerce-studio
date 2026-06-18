import { Api } from "@/Shared/Utils/api";
import type { CategoryModel } from "../Models/CategoryModel";

export class CategoriesSource {
  private readonly api = new Api();
  async GetCategories(): Promise<CategoryModel[]> {
    const res = this.api.get<CategoryModel[]>("/api/Category");
    return res;
  }
}
