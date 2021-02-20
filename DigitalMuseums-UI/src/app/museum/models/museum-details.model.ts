import { IOption } from "src/app/core/form/form.interface";

export interface MuseumDetails {
    id: number;
    name: string;
    description: string;
    country: IOption;
    region: IOption;
    city: IOption;
    address: string;
    genre: IOption;
    imagePaths: string[];
}