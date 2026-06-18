import type { LoginContract } from "../contracts/LoginContract";

export class LogoutUseCase {
  private readonly repo: LoginContract;

  constructor(repo: LoginContract) {
    this.repo = repo;
  }

  async execute(): Promise<boolean> {
    const res = await this.repo.logoutImplementation();
    return res;
  }
}
