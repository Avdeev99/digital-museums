import { IOption } from "src/app/core/form/form.interface";

export interface ExhibitDetails {
    id?: number;
    museumId: number;
    name: string;
    description: string;
    author: string;
    date: string;
    tags: string[];
    imagePaths: string[];
    imagePath: string;
    exhibitions: IOption[];
}