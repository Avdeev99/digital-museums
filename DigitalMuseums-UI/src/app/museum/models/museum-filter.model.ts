import { SortingMethod } from "../constants/sorting-method.enum";

export interface MuseumFilter {
    name: string;
    countryId?: number;
    regionId?: number;
    cityId?: number;
    genres: Array<number>;
    sortingMethod: SortingMethod;
}