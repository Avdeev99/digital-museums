import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { serialize } from "object-to-formdata";
import { Observable, of } from "rxjs";
import { api } from "../constants/api.constants";
import { Museum } from "../models/museum.model";

@Injectable({
    providedIn: 'root',
})
export class MuseumService {
    public constructor(
        private httpClient: HttpClient,
      ) {}

    public create(museum: Museum): Observable<any> {
        const formData: FormData = serialize(museum);
        Array.from(museum.images).forEach(image => {
            formData.append('images', image, image.name);
        });

        return this.httpClient.post(api.createMuseum, formData);
    }
}