import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GenreDetails } from "src/app/genre/models/genre-details.model";
import { api } from "../constants/api.constants";

@Injectable({
    providedIn: 'root',
})
export class GenreService {
    public constructor(
        private httpClient: HttpClient,
    ) { }

    public get(id: number): Observable<GenreDetails> {
        const requestUrl: string = `${api.genre}/${id}`

        return this.httpClient.get<GenreDetails>(requestUrl);
    }

    public getAll(): Observable<Array<any>> {
        return this.httpClient.get<Array<any>>(api.getGenres);
    }

    public create(genre: GenreDetails): Observable<void> {
        return this.httpClient.post<void>(api.genre, genre);
    }

    public update(genre: GenreDetails): Observable<void> {
        const requestUrl: string = `${api.genre}/${genre.id}`;

        return this.httpClient.put<void>(requestUrl, genre);
    }

    public delete(id: number): Observable<void> {
        const requestUrl: string = `${api.genre}/${id}`

        return this.httpClient.delete<void>(requestUrl);
    }
}