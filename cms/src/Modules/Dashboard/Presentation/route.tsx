// import ProtectedRoute from "@/Shared/Components/ProtectedRoute";
import { ProductsRoutes } from "../Modules/Products/Presentation/route";
import { UserRoutes } from "../Modules/Users/route";
import DashboardLayout from "./Layouts/DashboardLayout";

export const DashboardRoutes = {
  path: "dashboard",
  element: <DashboardLayout />,
  children: [ProductsRoutes, UserRoutes],
};
