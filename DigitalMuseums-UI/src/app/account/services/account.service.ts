import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { api } from "src/app/museum/constants/api.constants";
import { ChangePasswordRequest } from "../models/change-password-request.model";

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public changePassword(changePasswordRequest: ChangePasswordRequest): Observable<any> {
        return this.httpClient.post(api.exhibit, changePasswordRequest);
    }
}