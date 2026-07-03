import type { ProductContract } from "../../Domain/Contracts/ProductContract";
import type { ProductByIdEntity } from "../../Domain/Entities/productByIdEntity";
import type { ProductEntity } from "../../Domain/Entities/ProductEntity";
import type { AddproductParams } from "../../Domain/UseCases/Params/AddProductParams";
import type { ProductModel } from "../Models/ProductModel";
import type { ProductPayloadModel } from "../Models/ProductPayloadModel";
import { ProductDataSource } from "../Sources/ProductDataSource";

export class ProductImplementations implements ProductContract {
  private readonly productDataSource: ProductDataSource;

  constructor(productDataSource: ProductDataSource) {
    this.productDataSource = productDataSource;
  }

  async GetProducts(): Promise<ProductEntity[]> {
    const res = await this.productDataSource.GetProducts();

    const productsList = res.map((p: ProductModel) => {
      const { productDescription, imageUrl, ...productData } = p;

      const product: ProductEntity = productData;

      return product;
    });

    return productsList;
  }

  async GetProductById(id: number): Promise<ProductByIdEntity> {
    const res = await this.productDataSource.GetProductById(id);
    const { ...data }: ProductByIdEntity = res;
    return data;
  }

  async AddProduct(payload: AddproductParams): Promise<ProductEntity> {
    const { ...dataPayload }: ProductPayloadModel = payload;
    const res = await this.productDataSource.AddProduct(dataPayload);
    const { ...data }: ProductEntity = res;
    return data;
  }

  async UpdateProduct(
    id: number,
    payload: AddproductParams,
  ): Promise<ProductByIdEntity> {
    const { ...dataPayload }: ProductPayloadModel = payload;
    const res = await this.productDataSource.UpdateProduct(id, dataPayload);
    const { ...data }: ProductByIdEntity = res;
    return data;
  }

  async DeleteProduct(id: number): Promise<boolean> {
    return await this.productDataSource.DeteleProduct(id);
  }
}
