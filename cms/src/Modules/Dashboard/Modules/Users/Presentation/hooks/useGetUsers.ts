import React, { useEffect, useMemo, useRef, useState } from "react";
import { UsersDataSource } from "../../Data/Sources/UsersDataSource";
import type { UserEntity } from "../../Domain/Entities/UserEntity";
import { UsersImplementations } from "../../Data/Implementations/UsersImplementations";
import { GetUsersCase } from "../../Domain/UseCases/GetUsersCase";

function useGetUsers() {
  const [seach, setSearch] = useState("");
  const [loading, setLoading] = useState(false);
  const [usersList, setUsersList] = useState<UserEntity[]>([]);
  const calledRef = useRef(false);
  const usersDataSource = useMemo(() => new UsersDataSource(), []);
  const usersImplementations = useMemo(
    () => new UsersImplementations(usersDataSource),
    [],
  );
  const getUsersCase = useMemo(
    () => new GetUsersCase(usersImplementations),
    [],
  );

  const getUsers = async () => {
    try {
      setLoading(true);
      const res = await getUsersCase.execute();
      if (res !== null) {
        setUsersList(res);
      }
    } catch (error) {
      console.log("Error", `${error}`);
      setUsersList([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (calledRef.current) return;
    calledRef.current = true;
    getUsers();
  }, [getUsersCase]);

  return {
    seach,
    setSearch,
    loading,
    usersList,
  };
}

export default useGetUsers;
