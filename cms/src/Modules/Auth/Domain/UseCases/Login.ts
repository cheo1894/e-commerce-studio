import type { loginRequest } from "../../Data/models/LoginModel";
import type { LoginContract } from "../contracts/LoginContract";

export class LoginUseCase {
  private readonly repo: LoginContract;

  constructor(repo: LoginContract) {
    this.repo = repo;
  }

  async execute(credentials: loginRequest) {
    return this.repo.loginImplentation(credentials);
  }
}
