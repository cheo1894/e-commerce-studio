import { Api } from "@/Shared/Utils/api";
import type { UserModel } from "../Models/UserModel";

export class UsersDataSource {
  private readonly api = new Api();
  async GetUsers(): Promise<UserModel[]> {
    const res = this.api.get<UserModel[]>("/api/Users");
    console.log("USUARIOS QUE ME LLEGAN >>>", res);
    return res;
  }
}
