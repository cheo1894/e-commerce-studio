import type { LoginContract } from "../../Domain/contracts/LoginContract";
import type { LoginResponseEntitie } from "../../Domain/Entities/LoginEnties";
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
}
