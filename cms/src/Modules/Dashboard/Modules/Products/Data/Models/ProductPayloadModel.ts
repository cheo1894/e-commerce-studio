import type Decimal from "decimal.js";

export type ProductPayloadModel = {
  productName: string;
  productDescription: string;
  productPrice: Decimal;
  quantity: number;
  imageUrl: string;
  categoryId: number;
};
