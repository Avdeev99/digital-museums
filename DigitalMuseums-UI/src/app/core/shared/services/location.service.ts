import { IOption } from './../../form/form.interface';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";

@Injectable({
    providedIn: 'root',
})
export class LocationService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public getCountries(): Observable<Array<IOption>> {
        return this.httpClient.get<Array<IOption>>(api.getCountries);
    }

    public getRegionsByCountry(countryId: number): Observable<Array<IOption>> {
        if (!countryId) {
            return of([]);
        }

        const requestUrl: string = `${api.getRegions}/country/${countryId}`
        return this.httpClient.get<Array<IOption>>(requestUrl);
    }

    public getCitiesByRegion(regionId: number): Observable<Array<IOption>> {
        if (!regionId) {
            return of([]);
        }

        const requestUrl: string = `${api.getCities}/region/${regionId}`
        return this.httpClient.get<Array<IOption>>(requestUrl);
    }
}