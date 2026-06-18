import type { CategoryEntity } from "../../../Domain/Entities/CategoryEntity";
import TextInput from "@/Shared/Components/TextInput";
import Button from "@/Shared/Components/Button";
import Table from "@/Shared/Components/Table";
import TableHead from "@/Shared/Components/Table/TableHead";
import TableData from "@/Shared/Components/Table/TableData";
import "@/Styles/ListPages.css";
import ActionMenu from "@/Shared/Components/ActionMenu";
import useCategoriesPage from "../../hooks/useCategoriesPage";

type Props = {};

function Index({}: Props) {
  const { list, loading, search, setSearch } = useCategoriesPage();

  return (
    <>
      <h1 className="title">Categories</h1>
      <br />

      <div className="row-options-1">
        <div className="div-input-search">
          <TextInput value={search} PlaceHolder="Search" />
        </div>
        <div className="div-buttons">
          <Button Title="Add Category" />
        </div>
      </div>

      <Table>
        <thead>
          <tr>
            <TableHead>Id</TableHead>
            <TableHead>Category</TableHead>
            <TableHead>Actions</TableHead>
          </tr>
        </thead>
        <tbody>
          {list &&
            list.map((c: CategoryEntity, index) => {
              return (
                <tr key={index}>
                  <TableData>{c.categoryId}</TableData>
                  <TableData>{c.categoryName}</TableData>
                  <TableData>
                    <ActionMenu />
                  </TableData>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </>
  );
}

export default Index;
