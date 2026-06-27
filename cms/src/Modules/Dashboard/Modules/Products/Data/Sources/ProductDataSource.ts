import { Api } from "@/Shared/Utils/api";
import type { ProductModel } from "../Models/ProductModel";
import type { ProductPayloadModel } from "../Models/ProductPayloadModel";

export class ProductDataSource {
  private readonly api: Api;

  constructor(api: Api) {
    this.api = api;
  }

  async GetProducts(): Promise<ProductModel[]> {
    const res = this.api.get<ProductModel[]>("/api/ProductCatalog");
    return res;
  }

  async AddProduct(payload: ProductPayloadModel): Promise<ProductModel> {
    const res = this.api.post<ProductModel>("/api/ProductCatalog/add", payload);
    return res;
  }
}
