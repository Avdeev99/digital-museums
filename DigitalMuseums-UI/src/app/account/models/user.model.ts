import { Role } from "./role.model";

export interface User {
    id: number;
    userName: string;
    email: string;
    birthDate: string;
    role: Role;
    museumId?: number; 
}