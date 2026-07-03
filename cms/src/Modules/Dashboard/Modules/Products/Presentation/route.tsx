import { lazy } from "react";

const ProductsList = lazy(() => import("./Pages/ProductsList"));
const CategoryList = lazy(() => import("./Pages/CategoryList"));
const Product = lazy(() => import("./Pages/Product"));
export const ProductsRoutes = {
  path: "products",
  children: [
    { index: true, element: <ProductsList /> },
    { path: "list", element: <ProductsList /> },
    { path: "categories", element: <CategoryList /> },
    { path: "product/:id", element: <Product /> },
  ],
};
