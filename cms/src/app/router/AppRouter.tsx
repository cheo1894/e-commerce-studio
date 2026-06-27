import { createBrowserRouter, Navigate } from "react-router";
import { RouterProvider } from "react-router/dom";
import { AuthRoutes } from "../../Modules/Auth/Presentation/route.tsx";
import { DashboardRoutes } from "@/Modules/Dashboard/Presentation/route.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Navigate to="/dashboard/products" />,
  },
  AuthRoutes,
  DashboardRoutes,
]);

export default function AppRouter() {
  return <RouterProvider router={router} />;
}
