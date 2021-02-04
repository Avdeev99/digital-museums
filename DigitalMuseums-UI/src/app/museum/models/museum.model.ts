export interface Museum {
    id: number;
    name: string;
    description: string;
    countryId: number;
    regionId: number;
    cityId: number;
    address: string;
    genreId: number;
    images: FileList;
}