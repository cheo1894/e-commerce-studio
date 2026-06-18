import React, { useState } from "react";
import useCategoriesList from "./useCategoriesList";

function useCategoriesPage() {
  const [search, setSearch] = useState("");
  const { list, loading } = useCategoriesList();
  return { list, loading, search, setSearch };
}

export default useCategoriesPage;
