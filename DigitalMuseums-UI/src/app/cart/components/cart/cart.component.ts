import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { LocationBase } from 'src/app/core/shared/models/location-base';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { LocationService } from 'src/app/core/shared/services/location.service';
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
export class CartComponent extends LocationBase implements OnInit {
    public cartDetails$: Observable<CartDetails>;
    public totalOrderPrice$: Observable<number>;

    public orderDetails: Array<CartItem>;
    public formGroup: FormGroup;

    constructor(
        private translateService: TranslateService,
        private currentUserService: CurrentUserService,
        private cartService: CartService,
        private cartStateService: CartStateService,
        protected locationService: LocationService,
        private fb: FormBuilder,
    ) {
        super(locationService);
        this.cartStateService.loadCartDetails();
    }

    get cartItemsCount(): number {
        return !!this.orderDetails ? this.orderDetails.length : 0;
    }

    get isFormValid(): boolean {
        return !!this.formGroup ? this.formGroup.valid : false;
    }

    get isDeliveryControlDisabled(): boolean {
        return !this.orderDetails || (!!this.orderDetails && !this.orderDetails.length);
    }

    ngOnInit(): void {
        this.initForm();
        this.initSubscriptions();

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
        return !this.cartStateService.isDecreasingAllowed(souvenirId);
    }

    increaseItemQuantity(souvenirId: number): void {
        if (this.isIncreaseBtnDisabled(souvenirId)) {
            return;
        }

        this.cartStateService.increaseCartItemQuantity(souvenirId);
    }

    decreaseItemQuantity(souvenirId: number): void {
        if (this.isDecreaseBtnDisabled(souvenirId)) {
            return;
        } 

        this.cartStateService.decreaseCartItemQuantity(souvenirId);
    }

    getSouvenirImage(souvenirId: number): string {
        const cartItem = this.orderDetails.find(x => x.souvenir.id === souvenirId);
        return cartItem.souvenir.imagePaths[0];
    }

    deleteItem(souvenirId: number): void {
        this.cartStateService.deleteCartItem(souvenirId);
    }

    private initForm(): void {
        this.formGroup = this.fb.group({
            countryId: [null, [Validators.required]],
            regionId: [null, [Validators.required]],
            cityId: [null, [Validators.required]],
            address: [null, [Validators.required]],
        });
    }

    private initSubscriptions(): void {
        this.countryValueChanges(this.formGroup.controls.countryId);
        this.regionValueChanges(this.formGroup.controls.regionId);
    }
}
