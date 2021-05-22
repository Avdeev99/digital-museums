import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthUser } from "src/app/core/auth/models/auth-user.model";
import { api } from "src/app/core/shared/constants/api.constants";
import { ChangePasswordRequest } from "../models/change-password-request.model";

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public changePassword(changePasswordRequest: ChangePasswordRequest): Observable<any> {
        const requestUrl = `${api.account}/change-password`;

        return this.httpClient.post(requestUrl, changePasswordRequest);
    }

    public editPersonalInfo(name: string): Observable<any> {
        const requestUrl = `${api.account}/user-info`

        return this.httpClient.post(requestUrl, { name });
    }

    public getCurrentUser(): Observable<AuthUser> {
        const requestUrl = `${api.account}/current-user`

        return this.httpClient.get<AuthUser>(requestUrl);
    }
}