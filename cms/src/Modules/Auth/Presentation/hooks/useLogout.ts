import React from "react";
import { LoginDatasource } from "../../Data/DataSource/LoginDataSoucer";
import { Api } from "@/Shared/Utils/api";
import { LoginImplementation } from "../../Data/implementations/LoginImplementation";
import { LogoutUseCase } from "../../Domain/UseCases/Logout";
import { useNavigate } from "react-router";

function useLogout() {
  const navigate = useNavigate();
  const api = new Api();
  const datasource = new LoginDatasource(api);
  const implementation = new LoginImplementation(datasource);
  const useCase = new LogoutUseCase(implementation);

  const logout = async () => {
    const res = await useCase.execute();
    if (res) {
      navigate("/auth");
    }
  };

  return { logout };
}

export default useLogout;
