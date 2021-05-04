import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { CartDetails } from "../models/cart-details.model";
import { CartService } from "./cart.service";

@Injectable({
    providedIn: 'root',
})
export class CartStateService {
    cartDetails$$: BehaviorSubject<CartDetails>;
    totalOrderPrice$$: BehaviorSubject<number>;

    public constructor(
        private cartService: CartService,
    ) { 
        this.cartDetails$$ = new BehaviorSubject<CartDetails>(null);
        this.totalOrderPrice$$ = new BehaviorSubject<number>(null);
    }

    public loadCartDetails(): void {
        this.cartService.getCurrentCart().subscribe((data: CartDetails) => {
            this.setCartDetails(data);
            this.setTotalOrderPrice();
        });
    }

    public setCartDetails(data: CartDetails): void {
        this.cartDetails$$.next(data);
    }

    public getCartDetails(): Observable<CartDetails> {
        return this.cartDetails$$.asObservable();
    }

    public setTotalOrderPrice(): void {
        const cardDetails: CartDetails = this.cartDetails$$.getValue();
        const totalOrderPrice: number = cardDetails.orderDetails.reduce((n, {price}) => n + price, 0);
        this.totalOrderPrice$$.next(totalOrderPrice);
    }

    public getTotalOrderPrice(): Observable<number> {
        return this.totalOrderPrice$$.asObservable();
    }

    public increaseCartItemQuantity(souvenirId: number): void {
        const cartDetails = this.cartDetails$$.getValue();
        const cartItem = cartDetails.orderDetails.find(x => !!x.souvenir && x.souvenir.id === souvenirId);

        if (cartItem.quantity >= cartItem.souvenir.availableUnits) {
            return;
        }

        cartItem.quantity++;

        // cartDetails.orderDetails[cartItemIndex] = {
        //     ...cartItem,
        //     quantity: cartItem.quantity++,
        // };

        this.cartService.updateCartItem(cartItem.souvenir.id, cartItem.quantity).subscribe(() => {
            this.loadCartDetails();
        });
    }

    public decreaseCartItemQuantity(souvenirId: number): void {
        const cartDetails = this.cartDetails$$.getValue();
        const cartItem = cartDetails.orderDetails.find(x => !!x.souvenir && x.souvenir.id === souvenirId);

        if (cartItem.quantity <= 1) {
            return;
        }

        cartItem.quantity--;

        // cartDetails.orderDetails[cartItemIndex] = {
        //     ...cartItem,
        //     quantity: cartItem.quantity++,
        // };

        this.cartService.updateCartItem(cartItem.souvenir.id, cartItem.quantity).subscribe(() => {
            this.loadCartDetails();
        });
    }

    public isIncreasingAllowed(souvenirId: number): boolean {
        const cartDetails = this.cartDetails$$.getValue();
        const cartItem = cartDetails.orderDetails.find(x => !!x.souvenir && x.souvenir.id === souvenirId);

        return cartItem.quantity < cartItem.souvenir.availableUnits;
    }

    public isDecreasingAllowed(souvenirId: number): boolean {
        const cartDetails = this.cartDetails$$.getValue();
        const cartItem = cartDetails.orderDetails.find(x => !!x.souvenir && x.souvenir.id === souvenirId);

        return cartItem.quantity > 1;
    }
}