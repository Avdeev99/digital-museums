import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";
import { StatisticsDetails } from "../models/statistics-details.model";

@Injectable({
    providedIn: 'root',
})
export class StatisticsService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public get(): Observable<StatisticsDetails> {
        const requestUrl: string = `${api.statistics}`

        return this.httpClient.get<StatisticsDetails>(requestUrl);
    }
}