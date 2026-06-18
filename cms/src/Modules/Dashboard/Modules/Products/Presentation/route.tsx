import { lazy } from "react";

const ProductsList = lazy(() => import("./Pages/ProductsList"));
const CategoryList = lazy(() => import("./Pages/CategoryList"));
export const ProductsRoutes = {
  path: "products",
  children: [
    { index: true, element: <ProductsList /> },
    { path: "list", element: <ProductsList /> },
    { path: "categories", element: <CategoryList /> },
  ],
};
