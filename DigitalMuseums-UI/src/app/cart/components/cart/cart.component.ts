import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { environment } from 'src/environments/environment.prod';
import { CartDetails } from '../../models/cart-details.model';
import { CartItem } from '../../models/cart-item.model';
import { CartStateService } from '../../services/cart-state.service';
import { CartService } from '../../services/cart.service';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
    public cartDetails$: Observable<CartDetails>;
    public totalOrderPrice$: Observable<number>;

    public orderDetails: Array<CartItem>;

    constructor(
        private translateService: TranslateService,
        private currentUserService: CurrentUserService,
        private cartService: CartService,
        private cartStateService: CartStateService,
    ) { 
        this.cartStateService.loadCartDetails();
    }

    ngOnInit(): void {
        this.cartDetails$ = this.cartStateService.getCartDetails();
        this.totalOrderPrice$ = this.cartStateService.getTotalOrderPrice();

        this.cartDetails$.subscribe(data => {
            if (!!data) {
                this.orderDetails = data.orderDetails;
            }
        });
    }

    openCheckout(amount: number, tokenCallback) {
        let handler = (<any>window).StripeCheckout.configure({
            key: environment.stripePk,
            locale: "auto",
            token: tokenCallback
        });

        const panelLabel: string = this.translateService.instant('cart.pay');
        const user: AuthUser = this.currentUserService.getUser();

        handler.open({
            name: "Digital Museums",
            zipCode: false,
            currency: "uah",
            amount: amount,
            panelLabel: panelLabel,
            allowRememberMe: false,
            email: user.email,
        });
    }

    checkout(): void {
        this.openCheckout(10000, (token: any) => {
            this.cartService.pay(token.id).subscribe();
        });
    }

    isIncreaseBtnDisabled(souvenirId: number): boolean {
        return !this.cartStateService.isIncreasingAllowed(souvenirId);
    }

    isDecreaseBtnDisabled(souvenirId: number): boolean {
        const t = !this.cartStateService.isDecreasingAllowed(souvenirId);
        return t;
    }

    increaseItemQuantity(souvenirId: number): void {
        this.cartStateService.increaseCartItemQuantity(souvenirId);
    }

    decreaseItemQuantity(souvenirId: number): void {
        this.cartStateService.decreaseCartItemQuantity(souvenirId);
    }
}
