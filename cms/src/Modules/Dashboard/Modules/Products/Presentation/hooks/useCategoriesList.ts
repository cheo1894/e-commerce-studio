import React, { useEffect, useMemo, useRef, useState } from "react";
import type { CategoryEntity } from "../../Domain/Entities/CategoryEntity";
import { CategoriesSource } from "../../Data/Sources/CategoriesSource";
import { CategoriesImplementations } from "../../Data/Implementations/CategoriesImplementations";
import { GetCategoriesUseCases } from "../../Domain/UseCases/GetCategoriesUseCases";

function useCategoriesList() {
  const [list, setList] = useState<CategoryEntity[]>();
  const [loading, setLoading] = useState(false);
  const calledRef = useRef(false);
  const categoriesDataSource = useMemo(() => new CategoriesSource(), []);
  const categoriesImplementations = useMemo(
    () => new CategoriesImplementations(categoriesDataSource),
    [],
  );
  const getCategoriesUseCases = useMemo(
    () => new GetCategoriesUseCases(categoriesImplementations),
    [],
  );
  const getCategories = async () => {
    try {
      setLoading(true);
      const res = await getCategoriesUseCases.execute();
      console.log(res);
      if (res === null) {
        setList([]);
        return;
      }

      setList(res);
    } catch (error) {
      setList([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (calledRef.current === true) return;
    calledRef.current = true;
    getCategories();
  }, []);

  return {
    list,
    loading,
  };
}

export default useCategoriesList;
