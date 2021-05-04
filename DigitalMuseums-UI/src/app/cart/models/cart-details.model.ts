import { CartItem } from "./cart-item.model";

export interface CartDetails {
    created: string;
    orderDetails: Array<CartItem>
}