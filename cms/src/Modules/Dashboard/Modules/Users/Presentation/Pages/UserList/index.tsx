import Table from "@/Shared/Components/Table";
import TableData from "@/Shared/Components/Table/TableData";
import TableHead from "@/Shared/Components/Table/TableHead";
import "@/Styles/ListPages.css";
import TextInput from "@/Shared/Components/TextInput";
import type { UserEntity } from "../../../Domain/Entities/UserEntity";
import useGetUsers from "../../hooks/useGetUsers";
import Button from "@/Shared/Components/Button";
import ActionMenu from "@/Shared/Components/ActionMenu";

type Props = {};

function Index({}: Props) {
  const { seach, setSearch, loading, usersList } = useGetUsers();

  return (
    <>
      <h1 className="title">Users</h1>
      <br />

      <div className="row-options-1">
        <div className="div-input-search">
          <TextInput value={seach} PlaceHolder="Search" />
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
              <TableHead>Role</TableHead>
              <TableHead>Actions</TableHead>
            </tr>
          </thead>
          <tbody>
            {usersList &&
              usersList.length > 0 &&
              usersList.map((u: UserEntity, index) => {
                return (
                  <tr key={index}>
                    <TableData>{u.userId}</TableData>
                    <TableData>{u.userName}</TableData>
                    <TableData>{u.roleName}</TableData>
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

      {loading === false && usersList && usersList.length === 0 && (
        <h3 className="tag">No se encontraron usuarios</h3>
      )}
    </>
  );
}

export default Index;
