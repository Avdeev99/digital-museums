import { Component, OnInit, OnDestroy, Input, Optional, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IValidatorError } from '../../form.interface';

@Component({
  selector: 'app-textarea',
  templateUrl: './textarea.component.html',
  styleUrls: ['./textarea.component.scss'],
})
export class CustomTextareaComponent implements OnInit, OnDestroy {
  @Input() @Optional() public label: string;
  @Input() @Optional() public control: FormControl;
  @Input() @Optional() public validatorErrors: IValidatorError[];
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
