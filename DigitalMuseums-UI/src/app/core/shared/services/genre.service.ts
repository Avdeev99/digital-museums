import { IOption } from './../../form/form.interface';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";

@Injectable({
    providedIn: 'root',
})
export class GenreService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public getAll(): Observable<Array<IOption>> {
        return this.httpClient.get<Array<IOption>>(api.getGenres);
    }
}