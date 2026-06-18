import { Api } from "@/Shared/Utils/api";
import type { ProductModel } from "../Models/ProductModel";

export class ProductDataSource {
  private readonly api = new Api();
  async GetProducts(): Promise<ProductModel[]> {
    const res = this.api.get<ProductModel[]>("/api/ProductCatalog");
    return res;
  }
}
