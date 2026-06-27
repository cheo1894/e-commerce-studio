import { Api } from "@/Shared/Utils/api";
import { LoginDatasource } from "../../Data/DataSource/LoginDataSoucer";
import { LoginImplementation } from "../../Data/implementations/LoginImplementation";
import { GetUserCase } from "../../Domain/UseCases/GetUser";
import { useEffect, useRef, useState } from "react";
import type { UserEntity } from "../../Domain/Entities/UserEntity";

function useGetUser() {
  const api = new Api();
  const dataSource = new LoginDatasource(api);
  const implementation = new LoginImplementation(dataSource);
  const useCase = new GetUserCase(implementation);
  const calledRef = useRef(false);
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState<UserEntity | Record<string, never>>({});

  const getUser = async () => {
    try {
      const res = await useCase.execute();
      if (res === null) {
        setUser({});
        return;
      }
      setUser(res);
    } catch (error) {
      setUser({});
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    if (calledRef.current === true) return;

    calledRef.current = true;
    getUser();
  }, []);

  return { user, loading, getUser };
}

export default useGetUser;
