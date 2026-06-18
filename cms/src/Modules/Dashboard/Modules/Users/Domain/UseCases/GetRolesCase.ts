import type { RoleContract } from "../Contracts/RoleContract";
export class GetRolesCase {
  private readonly roleContract: RoleContract;
  constructor(roleContract: RoleContract) {
    this.roleContract = roleContract;
  }

  async execute() {
    return await this.roleContract.GetRoles();
  }
}
