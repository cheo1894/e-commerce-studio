import useGetUser from "@/Modules/Auth/Presentation/hooks/useGetUser";
import { type JSX } from "react";
import { Navigate, useLocation } from "react-router";

type Props = { children: JSX.Element };

function index({ children }: Props) {
  const { user, loading } = useGetUser();
  const location = useLocation();

  if (loading) return <div>Cargando...</div>;

  if (Object.keys(user).length === 0)
    return <Navigate to="/auth" replace state={{ from: location }} />;
  return children;
}

export default index;
