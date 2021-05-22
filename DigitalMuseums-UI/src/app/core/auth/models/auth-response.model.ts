import { AuthUser } from "./auth-user.model";

export interface IAuthResponse {
  token: string;
  user: AuthUser;
}
