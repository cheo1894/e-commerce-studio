import { lazy } from "react";
const UserList = lazy(() => import("./Presentation/Pages/UserList/index"));
const UserRoles = lazy(() => import("./Presentation/Pages/UserRoles/index"));
export const UserRoutes = {
  path: "users",

  children: [
    { index: true, element: <UserList /> },
    { path: "list", element: <UserList /> },
    { path: "roles", element: <UserRoles /> },
  ],
};
