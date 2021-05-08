import { AuthRole } from "./auth-role.enum";

export interface AuthUser {
    id: number;
    userName: string;
    email: string;
    role: AuthRole;
  }