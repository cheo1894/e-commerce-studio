import { Children, lazy } from "react";
import AuthLayout from "./Layouts/AuthLayout";
const LoginPage = lazy(() => import("./Pages/LoginPage"));
const RegisterPage = lazy(() => import("./Pages/RegisterPage"));

export const AuthRoutes = {
  path: "auth",
  element: <AuthLayout />,
  children: [
    { index: true, element: <LoginPage /> },
    { path: "login", element: <LoginPage /> },
    { path: "register", element: <RegisterPage /> },
  ],
};
