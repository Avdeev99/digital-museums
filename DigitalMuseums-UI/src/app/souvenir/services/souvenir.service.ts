import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { serialize } from "object-to-formdata";
import { Observable } from "rxjs";
import { api } from "src/app/museum/constants/api.constants";
import { SouvenirDetails } from "../models/souvenir-details.model";
import { SouvenirFilter } from "../models/souvenir-filter.model";
import { Souvenir } from "../models/souvenir.model";

@Injectable({
    providedIn: 'root',
})
export class SouvenirService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public create(souvenir: Souvenir): Observable<any> {
        const formData: FormData = this.getFormData(souvenir);

        return this.httpClient.post(api.souvenir, formData);
    }

    public update(souvenir: Souvenir): Observable<any> {
        const formData: FormData = this.getFormData(souvenir);

        return this.httpClient.put(`${api.souvenir}/${souvenir.id}`, formData);
    }

    public get(id: number): Observable<SouvenirDetails> {
        const requestUrl: string = `${api.souvenir}/${id}`

        return this.httpClient.get<SouvenirDetails>(requestUrl);
    }

    public getAll(): Observable<Array<SouvenirDetails>> {
        return this.httpClient.get<Array<SouvenirDetails>>(api.souvenir);
    }

    public getFiltered(filter: SouvenirFilter): Observable<Array<SouvenirDetails>> {
        let httpParams = new HttpParams();
        Object.keys(filter).forEach((key) => {
            if (!!filter[key]) {
                httpParams = httpParams.append(key, filter[key]);
            }
        });

        return this.httpClient.get<Array<SouvenirDetails>>(api.souvenir, { params: httpParams });
    }

    private getFormData(souvenir: Souvenir): FormData {
        const formData: FormData = serialize(souvenir);
        Array.from(souvenir.images).forEach(image => {
            formData.append('images', image, image.name);
        });

        return formData;
    }
}