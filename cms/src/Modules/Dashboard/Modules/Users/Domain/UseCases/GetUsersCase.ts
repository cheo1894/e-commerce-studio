import type { UserContract } from "../Contracts/UserContract";
export class GetUsersCase {
  private readonly repo: UserContract;

  constructor(repo: UserContract) {
    this.repo = repo;
  }

  async execute() {
    return await this.repo.GetUser();
  }
}
