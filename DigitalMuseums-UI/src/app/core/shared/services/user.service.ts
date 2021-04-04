import { IOption } from './../../form/form.interface';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";

@Injectable({
    providedIn: 'root',
})
export class UserService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public getBaseList(): Observable<Array<IOption>> {
        const requestUrl: string = `${api.user}/base/list`;

        return this.httpClient.get<Array<IOption>>(requestUrl);
    }
}