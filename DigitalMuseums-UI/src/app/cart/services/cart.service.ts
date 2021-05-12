import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { api } from "src/app/core/shared/constants/api.constants";
import { CartDetails } from "../models/cart-details.model";

@Injectable({
    providedIn: 'root',
})
export class CartService {
    cartDetails: BehaviorSubject<CartDetails>;

    public constructor(
        private httpClient: HttpClient,
    ) {}

    public pay(token: string): Observable<any> {
        const requestUrl: string = `${api.cart}/payment`

        return this.httpClient.post<any>(requestUrl, { token });
    }

    public updateCartItem(souvenirId: number, quantity: number): Observable<void> {
        return this.httpClient.put<any>(api.cart, { souvenirId, quantity });
    }

    public getCurrentCart(): Observable<CartDetails> {
        return this.httpClient.get<CartDetails>(api.cart);
    }

    public deleteCartItem(souvenirId: number): Observable<void> {
        const requestUrl: string = `${api.cart}/souvenir/${souvenirId}`

        return this.httpClient.delete<any>(requestUrl);
    }

    public addCartItem(souvenirId: number): Observable<void> {
        return this.httpClient.post<any>(api.cart, { souvenirId });
    }
}