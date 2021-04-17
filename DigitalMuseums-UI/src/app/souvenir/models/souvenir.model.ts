export interface Souvenir {
    id?: number;
    museumId: number;
    name: string;
    description: string;
    price: number;
    availableUnits: number;
    tags: string[];
    images: FileList;
}