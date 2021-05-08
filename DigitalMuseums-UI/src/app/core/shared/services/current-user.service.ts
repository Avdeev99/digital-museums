
import { Injectable } from "@angular/core";
import { storage } from "../../auth/constants/api.constants";
import { AuthRole } from "../../auth/models/auth-role.enum";
import { AuthUser } from "../../auth/models/auth-user.model";


@Injectable({
    providedIn: 'root',
})
export class CurrentUserService {
    public constructor() {}

    public getUser(): AuthUser {
        const currentUser: AuthUser = JSON.parse(localStorage.getItem(storage.currentUser));

        return currentUser;
    }

    public getUserToken(): string {
        const token: string = JSON.parse(localStorage.getItem(storage.token));

        return token;
    }

    public getRole(): AuthRole {
        const currentUser: AuthUser = JSON.parse(localStorage.getItem(storage.currentUser));
    
        return currentUser?.role;
    }
}