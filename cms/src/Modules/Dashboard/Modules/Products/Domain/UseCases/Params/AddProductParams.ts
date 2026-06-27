import type Decimal from "decimal.js";

export type AddproductParams = {
  productName: string;
  productDescription: string;
  productPrice: Decimal;
  quantity: number;
  imageUrl: string;
  categoryId: number;
};
