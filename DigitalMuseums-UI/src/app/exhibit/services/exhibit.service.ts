import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { serialize } from "object-to-formdata";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";
import { ExhibitDetails } from "../models/exhibit-details.model";
import { ExhibitFilter } from "../models/exhibit-filter.model";
import { Exhibit } from "../models/exhibit.model";

@Injectable({
    providedIn: 'root',
})
export class ExhibitService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public create(exhibit: Exhibit): Observable<any> {
        const formData: FormData = this.getFormData(exhibit);

        return this.httpClient.post(api.exhibit, formData);
    }

    public update(exhibit: Exhibit): Observable<any> {
        const formData: FormData = this.getFormData(exhibit);

        return this.httpClient.put(`${api.exhibit}/${exhibit.id}`, formData);
    }

    public get(id: number): Observable<ExhibitDetails> {
        const requestUrl: string = `${api.exhibit}/${id}`

        return this.httpClient.get<ExhibitDetails>(requestUrl);
    }

    public getAll(): Observable<Array<ExhibitDetails>> {
        return this.httpClient.get<Array<ExhibitDetails>>(api.exhibit);
    }

    public getFiltered(filter: ExhibitFilter): Observable<Array<ExhibitDetails>> {
        let httpParams = new HttpParams();
        Object.keys(filter).forEach((key) => {
            if (!!filter[key]) {
                httpParams = httpParams.append(key, filter[key]);
            }
        });

        return this.httpClient.get<Array<ExhibitDetails>>(api.exhibit, { params: httpParams });
    }

    private getFormData(exhibit: Exhibit): FormData {
        const formData: FormData = serialize(exhibit);
        Array.from(exhibit.images).forEach(image => {
            formData.append('images', image, image.name);
        });

        return formData;
    }
}