import type { loginRequest } from "../../Data/models/LoginModel";
import type { LoginResponseEntitie } from "../Entities/LoginEnties";

export interface LoginContract {
  loginImplentation(credentials: loginRequest): Promise<LoginResponseEntitie>;
  logoutImplementation(): Promise<boolean>;
}
