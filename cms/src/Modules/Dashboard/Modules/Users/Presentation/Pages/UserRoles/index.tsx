import { useEffect, useMemo, useRef, useState } from "react";
import { RolesDataSource } from "../../../Data/Sources/RolesDataSource";
import { RoleImplementations } from "../../../Data/Implementations/RoleImplementations";
import { GetRolesCase } from "../../../Domain/UseCases/GetRolesCase";
import TextInput from "@/Shared/Components/TextInput";
import Button from "@/Shared/Components/Button";
import Table from "@/Shared/Components/Table";
import TableHead from "@/Shared/Components/Table/TableHead";
import TableData from "@/Shared/Components/Table/TableData";
import type { RoleEntity } from "../../../Domain/Entities/RoleEntity";
import "@/Styles/ListPages.css";
import ActionMenu from "@/Shared/Components/ActionMenu";

function Index() {
  const calledRef = useRef(false);
  const [search, setSearch] = useState("");
  const [roleList, setRoleList] = useState<RoleEntity[]>([]);
  const [loading, setLoading] = useState(false);
  const rolesDataSource = useMemo(() => new RolesDataSource(), []);
  const rolesImplementation = useMemo(
    () => new RoleImplementations(rolesDataSource),
    [],
  );
  const getRolesCase = useMemo(() => new GetRolesCase(rolesImplementation), []);

  const getRoles = async () => {
    try {
      setLoading(true);
      const res = await getRolesCase.execute();
      if (res !== null && res.length > 0) {
        console.log(res);
        setRoleList(res);
      }
    } catch (error) {
      console.log("Error > ", `${error}`);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (calledRef.current === true) return;

    calledRef.current = true;

    getRoles();
  });

  return (
    <>
      <h1 className="title">Users</h1>
      <br />

      <div className="row-options-1">
        <div className="div-input-search">
          <TextInput value={search} PlaceHolder="Search" />
        </div>
        <div className="div-buttons">
          <Button Title="Add User" />
        </div>
      </div>

      {loading === false && (
        <Table>
          <thead>
            <tr>
              <TableHead>Id</TableHead>
              <TableHead>Name</TableHead>
              <TableHead>Actions</TableHead>
            </tr>
          </thead>
          <tbody>
            {roleList &&
              roleList.length > 0 &&
              roleList.map((r: RoleEntity, index) => {
                return (
                  <tr key={index}>
                    <TableData>{r.roleId}</TableData>
                    <TableData>{r.roleName}</TableData>
                    <TableData>
                      <ActionMenu />
                    </TableData>
                  </tr>
                );
              })}
          </tbody>
        </Table>
      )}

      {loading === true && <h3 className="tag">Cargando...</h3>}

      {loading === false && roleList && roleList.length === 0 && (
        <h3 className="tag">No se encontraron roles</h3>
      )}
    </>
  );
}

export default Index;
