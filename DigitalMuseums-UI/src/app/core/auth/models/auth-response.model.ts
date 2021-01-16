import { User } from "src/app/account/models/user.model";

export interface IAuthResponse {
  token: string;
  user: User;
}
