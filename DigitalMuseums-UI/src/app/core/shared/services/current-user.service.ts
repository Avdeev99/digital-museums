
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { storage } from "../../auth/constants/api.constants";
import { AuthRole } from "../../auth/models/auth-role.enum";
import { AuthUser } from "../../auth/models/auth-user.model";


@Injectable({
    providedIn: 'root',
})
export class CurrentUserService {
    public user$$: BehaviorSubject<AuthUser>;

    public constructor() {
        this.initUser();
    }

    public setUser(user: AuthUser): void {
        localStorage.setItem(storage.currentUser, JSON.stringify(user));
        this.user$$.next(user);
    }

    public getUser(): Observable<AuthUser> {
        return this.user$$.asObservable();
    }

    public getUserData(): AuthUser {
        return this.user$$.getValue();
    }

    // public getUser(): AuthUser {
    //     const currentUser: AuthUser = JSON.parse(localStorage.getItem(storage.currentUser));

    //     return currentUser;
    // }

    public getUserToken(): string {
        const token: string = JSON.parse(localStorage.getItem(storage.token));

        return token;
    }

    public getRole(): AuthRole {
        const currentUser: AuthUser = JSON.parse(localStorage.getItem(storage.currentUser));
    
        return currentUser?.role;
    }

    private initUser(): void {
        this.user$$ = new BehaviorSubject(null);
        const user: AuthUser = JSON.parse(localStorage.getItem(storage.currentUser));
        this.setUser(user);
    }
}