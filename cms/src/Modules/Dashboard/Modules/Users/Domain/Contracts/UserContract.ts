import type { UserEntity } from "../Entities/UserEntity";

export interface UserContract {
  GetUser(): Promise<UserEntity[]>;
}
