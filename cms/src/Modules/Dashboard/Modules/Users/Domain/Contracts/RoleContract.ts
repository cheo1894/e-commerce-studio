import type { RoleEntity } from "../Entities/RoleEntity";

export interface RoleContract {
  GetRoles(): Promise<RoleEntity[]>;
}
