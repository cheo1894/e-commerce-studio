import type { LoginContract } from "../contracts/LoginContract";

export class GetUserCase {
  private readonly repo: LoginContract;

  constructor(repo: LoginContract) {
    this.repo = repo;
  }

  async execute() {
    return await this.repo.getUserImplementation();
  }
}
