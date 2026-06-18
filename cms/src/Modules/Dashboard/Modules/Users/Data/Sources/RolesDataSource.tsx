import type { RoleModel } from "../Models/RoleModel";
import { Api } from "@/Shared/Utils/api";

export class RolesDataSource {
  private readonly api = new Api();

  async GetRoles(): Promise<RoleModel[]> {
    const res = this.api.get<RoleModel[]>("/api/Roles");
    return res;
  }
}
