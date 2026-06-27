import type { LoginContract } from "../../Domain/contracts/LoginContract";
import type { LoginResponseEntitie } from "../../Domain/Entities/LoginEnties";
import type { UserEntity } from "../../Domain/Entities/UserEntity";
import { LoginDatasource } from "../DataSource/LoginDataSoucer";
import type { loginRequest } from "../models/LoginModel";

export class LoginImplementation implements LoginContract {
  private readonly loginDatasource: LoginDatasource;

  public constructor(loginDatasource: LoginDatasource) {
    this.loginDatasource = loginDatasource;
  }

  async loginImplentation(
    credentials: loginRequest,
  ): Promise<LoginResponseEntitie> {
    const response = await this.loginDatasource.login(credentials);
    const { ...userData }: LoginResponseEntitie = response;
    return userData;
  }
  async logoutImplementation(): Promise<boolean> {
    const res = await this.loginDatasource.logout();
    return res;
  }

  async getUserImplementation(): Promise<UserEntity | null> {
    const res = await this.loginDatasource.getCurrentUser();
    if (res === null) return null;
    const { ...data }: UserEntity = res;
    return data;
  }
}
