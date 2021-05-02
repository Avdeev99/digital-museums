export interface IValidatorError {
  errorName: string;
  errorText: string;
}

export interface IOption {
  id?: string | number;
  name: string;
}

export interface IOptionChecked {
  id?: string | number;
  name: string;
  selected: boolean;
}
