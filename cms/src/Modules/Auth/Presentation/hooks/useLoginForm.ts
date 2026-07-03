import React, { useState } from "react";
import { useNavigate } from "react-router";
import { LoginDatasource } from "../../Data/DataSource/LoginDataSoucer";
import { LoginImplementation } from "../../Data/implementations/LoginImplementation";
import { LoginUseCase } from "../../Domain/UseCases/Login";
import { useForm } from "react-hook-form";
import { AuthSchema, type AuthFormData } from "../Schemas/AuthSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { Api } from "@/Shared/Utils/api";

function useLoginForm() {
  const {
    register,
    handleSubmit,
    reset,
    setError,
    formState: { errors, isSubmitting, isSubmitted },
  } = useForm<AuthFormData>({
    resolver: zodResolver(AuthSchema),
    defaultValues: { user: "", password: "" },
  });
  const api = new Api();
  const dataSource = new LoginDatasource(api);
  const implementation = new LoginImplementation(dataSource);
  const loginUseCase = new LoginUseCase(implementation);
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const onSubmit = async (data: AuthFormData) => {
    //No se usa el preventDefault porque hanleSubmit previene el reload del navegador

    // setLoading(true);
    try {
      const result = await loginUseCase.execute({
        userName: data.user,
        password: data.password,
      });
      if (result !== null) {
        navigate("/dashboard/products/list");
        return;
      }
      setError("root", { type: "manual", message: "Credenciales invalidas" });
    } catch (error) {
      setError("root", { type: "manual", message: "Error al iniciar sesión" });
    } finally {
      // reset();
    }
  };

  return {
    handleSubmit,
    register,
    loading,
    setLoading,
    onSubmit,
    errors,
    isSubmitting,
    isSubmitted,
    reset,
  };
}

export default useLoginForm;
