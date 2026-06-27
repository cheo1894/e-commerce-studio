import { Api } from "@/Shared/Utils/api";
import type { loginRequest, LoginResponse } from "../models/LoginModel";
import type { UserModel } from "../models/UserModel";

export class LoginDatasource {
  private readonly api: Api;

  constructor(api: Api) {
    this.api = api;
  }

  async login(payload: loginRequest): Promise<LoginResponse> {
    try {
      const res = this.api.post<LoginResponse>("/api/bff/auth/login", payload);
      return res;
    } catch (error) {
      throw new Error(`Error en el login: ${error}`);
    }
  }

  async logout(): Promise<boolean> {
    try {
      const res = this.api.post("/api/bff/auth/logout", {});
      return true;
    } catch (error) {
      throw new Error(`Error al cerrar sesión: ${error}`);
    }
  }

  async getCurrentUser(): Promise<UserModel | null> {
    try {
      const res = this.api.get<UserModel>("/api/bff/auth/user");
      return res as Promise<UserModel>;
    } catch (error) {
      return null;
    }
  }
}
