import type { RoleContract } from "../../Domain/Contracts/RoleContract";
import type { RoleEntity } from "../../Domain/Entities/RoleEntity";
import type { RoleModel } from "../Models/RoleModel";
import { RolesDataSource } from "../Sources/RolesDataSource";

export class RoleImplementations implements RoleContract {
  private readonly rolesDataSource: RolesDataSource;

  constructor(rolesDataSource: RolesDataSource) {
    this.rolesDataSource = rolesDataSource;
  }
  async GetRoles(): Promise<RoleEntity[]> {
    const res = await this.rolesDataSource.GetRoles();

    const RolesList = res.map((r: RoleModel) => {
      const { ...role }: RoleEntity = r;

      return role;
    });

    return RolesList;
  }
}
