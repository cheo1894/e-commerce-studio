import { useState } from "react";

import useProductsList from "./useProductsList";

function useProductsPage() {
  const [openModal, setOpenModal] = useState(false);
  const [search, setSearch] = useState("");
  const { productsList, loading } = useProductsList();

  return {
    openModal,
    setOpenModal,
    search,
    setSearch,
    productsList,
    loading,
  };
}

export default useProductsPage;
