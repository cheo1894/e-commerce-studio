import type { loginRequest } from "../../Data/models/LoginModel";
import type { LoginResponseEntitie } from "../Entities/LoginEnties";
import type { UserEntity } from "../Entities/UserEntity";

export interface LoginContract {
  loginImplentation(credentials: loginRequest): Promise<LoginResponseEntitie>;
  logoutImplementation(): Promise<boolean>;
  getUserImplementation(): Promise<UserEntity | null>;
}
