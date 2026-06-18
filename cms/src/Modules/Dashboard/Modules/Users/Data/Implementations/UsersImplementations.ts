import type { UserContract } from "../../Domain/Contracts/UserContract";
import type { UserEntity } from "../../Domain/Entities/UserEntity";
import type { UserModel } from "../Models/UserModel";
import { UsersDataSource } from "../Sources/UsersDataSource";

export class UsersImplementations implements UserContract {
  private readonly userDataSource: UsersDataSource;

  constructor(userDataSource: UsersDataSource) {
    this.userDataSource = userDataSource;
  }

  async GetUser(): Promise<UserEntity[]> {
    const res = await this.userDataSource.GetUsers();
    const userData = res.map((u: UserModel) => {
      const { ...userData }: UserEntity = u;
      let data = userData;
      return data;
    });

    return userData;
  }
}
