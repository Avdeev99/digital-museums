import { Component, OnInit, Input, Optional, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IValidatorError } from '../../form.interface';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
})
export class CustomInputComponent implements OnInit, OnDestroy {
  @Input() @Optional() public label: string;
  @Input() @Optional() public control: FormControl;
  @Input() @Optional() public validatorErrors: IValidatorError[] = [];
  @Input() @Optional() public inputType: 'text' | 'number' | 'date' | 'password' | 'email' = 'text';
  @Input() @Optional() public readonly: boolean;
  @Output() public valueChange: EventEmitter<FormControl> = new EventEmitter();
  @Input() @Optional() private value: string;
  private unsubscribe$: Subject<void> = new Subject();
  public constructor() {}

  public ngOnInit(): void {
    this.control = !!this.control ? this.control : new FormControl(this.value);
    this.control.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
      this.valueChange.emit(this.control);
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
