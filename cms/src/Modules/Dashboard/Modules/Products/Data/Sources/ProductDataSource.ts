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

  async GetProductById(id: number): Promise<ProductModel> {
    const res = this.api.get<ProductModel>(`/api/ProductCatalog/${id}`);
    return res;
  }

  async AddProduct(payload: ProductPayloadModel): Promise<ProductModel> {
    const res = this.api.post<ProductModel>("/api/ProductCatalog/add", payload);
    return res;
  }

  async UpdateProduct(
    id: number,
    payload: ProductPayloadModel,
  ): Promise<ProductModel> {
    const res = this.api.put<ProductModel>(
      `/api/ProductCatalog/${id}`,
      payload,
    );
    return res;
  }

  async DeteleProduct(id: number): Promise<boolean> {
    const res = this.api.delete(`/api/ProductCatalog/${id}`);

    if (res != null) {
      return true;
    }

    return false;
  }
}
