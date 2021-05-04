export interface SouvenirDetails {
    id?: number;
    museumId: number;
    name: string;
    description: string;
    price: number;
    availableUnits: number;
    tags: string[];
    imagePaths: string[];
    imagePath: string;
}