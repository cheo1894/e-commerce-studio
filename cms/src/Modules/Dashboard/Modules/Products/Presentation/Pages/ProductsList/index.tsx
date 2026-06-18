import Table from "@/Shared/Components/Table";
import TableHead from "@/Shared/Components/Table/TableHead";
import TextInput from "@/Shared/Components/TextInput";
import Button from "@/Shared/Components/Button";
import "@/Styles/ListPages.css";
import { useEffect, useMemo, useRef, useState } from "react";
import { ProductDataSource } from "../../../Data/Sources/ProductDataSource";
import { ProductImplementations } from "../../../Data/Implementations/ProductImplementations";
import { ProductsUseCases } from "../../../Domain/UseCases/GetProductsUseCase";
import type { ProductEntity } from "../../../Domain/Entities/ProductEntity";
import index from "@/Shared/Components/ButtonLinks";
import TableData from "@/Shared/Components/Table/TableData";
import Decimal from "decimal.js";
import { format } from "date-fns";
import { CgEye, CgPen, CgTrash } from "react-icons/cg";
import ActionMenu from "@/Shared/Components/ActionMenu";
import Modal from "@/Shared/Components/Modal";
import AddProductModal from "../../Components/AddProductModal";
import useProductsPage from "../../hooks/useProductsPage";

type Props = {};

function Index({}: Props) {
  const { openModal, setOpenModal, search, setSearch, productsList, loading } =
    useProductsPage();

  return (
    <>
      <h1 className="title">Products</h1>
      <br />

      <div className="row-options-1">
        <div className="div-input-search">
          <TextInput value={search} PlaceHolder="Search" />
        </div>
        <div className="div-buttons">
          <Button
            Title="Add Product"
            onClick={() => {
              setOpenModal(true);
            }}
          />
        </div>
      </div>

      <Table>
        <thead>
          <tr>
            <TableHead>Id</TableHead>
            <TableHead>Name</TableHead>
            <TableHead>Price</TableHead>
            <TableHead>Quantity</TableHead>
            <TableHead>Category</TableHead>
            <TableHead>Created by</TableHead>
            <TableHead>Created At</TableHead>
            <TableHead>Updated At</TableHead>
            <TableHead>Actions</TableHead>
          </tr>
        </thead>
        <tbody>
          {productsList.map((p: ProductEntity, index) => {
            const price = new Decimal(String(p.productPrice));
            const createdAt = format(new Date(p.createdAt), "yyyy/MM/dd");
            const updatedAt = format(new Date(p.updatedAt), "yyyy/MM/dd");
            return (
              <tr key={index}>
                <TableData>{p.productId}</TableData>
                <TableData>{p.productName}</TableData>
                <TableData>{`$${price}`}</TableData>
                <TableData>{p.quantity}</TableData>
                <TableData>{p.category}</TableData>
                <TableData>{p.createdByName}</TableData>
                <TableData>{createdAt}</TableData>
                <TableData>{updatedAt}</TableData>
                <TableData>
                  <ActionMenu />
                </TableData>
              </tr>
            );
          })}
        </tbody>
      </Table>
      <AddProductModal
        onClose={() => setOpenModal(false)}
        visible={openModal}
      />
    </>
  );
}

export default Index;
