import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { IValidatorError } from '../../form.interface';

@Component({
  selector: 'app-field-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss'],
})
export class CustomErrorComponent implements OnInit {
  @Input() public control: FormControl;
  @Input() public validatorErrors: IValidatorError[];
  public constructor() {}
  public ngOnInit(): void {}
}
