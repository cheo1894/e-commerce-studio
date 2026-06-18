import type { ProductContract } from "../../Domain/Contracts/ProductContract";
import type { ProductEntity } from "../../Domain/Entities/ProductEntity";
import type { ProductModel } from "../Models/ProductModel";
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
}
