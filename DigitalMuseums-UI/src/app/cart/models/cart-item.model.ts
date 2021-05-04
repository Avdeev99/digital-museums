import { SouvenirDetails } from "src/app/souvenir/models/souvenir-details.model";

export interface CartItem {
    souvenir: SouvenirDetails;
    quantity: number;
    price: number;
}