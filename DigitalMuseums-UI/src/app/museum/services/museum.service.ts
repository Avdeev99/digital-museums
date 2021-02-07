import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { serialize } from "object-to-formdata";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";
import { MuseumDetails } from "../models/museum-details.model";
import { MuseumFilter } from "../models/museum-filter.model";
import { Museum } from "../models/museum.model";

@Injectable({
    providedIn: 'root',
})
export class MuseumService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public create(museum: Museum): Observable<any> {
        const formData: FormData = this.getFormData(museum);

        return this.httpClient.post(api.museum, formData);
    }

    public update(museum: Museum): Observable<any> {
        const formData: FormData = this.getFormData(museum);

        return this.httpClient.put(api.museum, formData);
    }

    public get(id: number): Observable<MuseumDetails> {
        const requestUrl: string = `${api.museum}/${id}`

        return this.httpClient.get<MuseumDetails>(requestUrl);
    }

    public getAll(): Observable<Array<MuseumDetails>> {
        return this.httpClient.get<Array<MuseumDetails>>(api.museum);
    }

    public getFiltered(filter: MuseumFilter): Observable<Array<MuseumDetails>> {
        let httpParams = new HttpParams();
        Object.keys(filter).forEach((key) => {
            httpParams = httpParams.append(key, filter[key]);
        });

        return this.httpClient.get<Array<MuseumDetails>>(api.museum, { params: httpParams });
    }

    private getFormData(museum: Museum): FormData {
        const formData: FormData = serialize(museum);
        Array.from(museum.images).forEach(image => {
            formData.append('images', image, image.name);
        });

        return formData;
    }
}